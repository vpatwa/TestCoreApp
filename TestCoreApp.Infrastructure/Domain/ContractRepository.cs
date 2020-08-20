using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TestCoreApp.Domain.Contracts;
using TestCoreApp.Domain.Contracts.Request;
using TestCoreApp.Domain.Contracts.Response;
using TestCoreApp.Domain.Entities;
using TestCoreApp.Domain.Persistence;

namespace TestCoreApp.Infrastructure.Domain.CustomerContracts
{
    public class ContractRepository : IContractRepository
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        public ContractRepository(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        internal IDbConnection connection => _sqlConnectionFactory.GetOpenConnection();

        public async Task<DataRequestResult> CreateCustomerContractAsync(CreateContractCommand request)
        {
            try
            {
                using IDbConnection DbCon = connection;

                var age = CalculateAge(request.CustomerDOB);

                // Find convarage Plan based on customer country and sale date
                var covaragePlanIds = DbCon.GetAll<CoveragePlan>()
                    .Where(a => request.SaleDate.Date >= a.EligibilityDateFrom.Date
                        && request.SaleDate.Date <= a.EligibilityDateTo.Date
                        && (string.IsNullOrWhiteSpace(a.EligibilityCountry) || a.EligibilityCountry.ToLower() == request.CustomerCountry.ToLower()))
                    .Select(a => a.Id).ToList();

                if (!covaragePlanIds.Any())
                {
                    return await Task.FromResult(new DataRequestResult { IsSuccess = false, ErrorMessage = "Covarage plan is not exists" });
                }

                // Find Rate Chart based on Age and Gender
                var rateCharts = DbCon.GetAll<RateChart>().Where(a => covaragePlanIds.Any(x => x == a.CoveragePlanId)
                    && a.CustomerGender == request.Gender && (a.CustomerAge == "<=40" ? age <= 40 : age > 40)).FirstOrDefault();

                if (rateCharts != null)
                {
                    // Insert new customer contract
                    var contract = new Contract
                    {
                        CustomerName = request.CustomerName,
                        CustomerAddress = request.CustomerAddress,
                        CustomerCountry = request.CustomerCountry,
                        CustomerGender = request.Gender,
                        CustomerDOB = request.CustomerDOB,
                        SaleDate = request.SaleDate,
                        RateChartId = rateCharts.Id
                    };

                    var contractId = DbCon.Insert<Contract>(contract);
                    return await Task.FromResult(new DataRequestResult { IsSuccess = true, ErrorMessage = string.Empty });
                }
                else
                {
                    return await Task.FromResult(new DataRequestResult { IsSuccess = false, ErrorMessage = "Rate chart is not exists" });
                }

            }
            catch (Exception ex)
            {
                return await Task.FromResult(new DataRequestResult { IsSuccess = false, ErrorMessage = ex.Message });
            }
        }

        public async Task<DataRequestResult> UpdateCustomerContractAsync(UpdateContractCommand request)
        {
            try
            {
                using IDbConnection DbCon = connection;

                var currentContract = DbCon.Get<Contract>(request.ContractId);
                if (currentContract == null)
                {
                    return await Task.FromResult(new DataRequestResult { IsSuccess = false, ErrorMessage = "Specific contract is not exists." });
                }

                var age = CalculateAge(request.CustomerDOB);

                // Find convarage Plan based on new customer country and sale date
                var covaragePlanIds = DbCon.GetAll<CoveragePlan>()
                    .Where(a => request.SaleDate.Date >= a.EligibilityDateFrom.Date
                        && request.SaleDate.Date <= a.EligibilityDateTo.Date
                        && (string.IsNullOrWhiteSpace(a.EligibilityCountry) || a.EligibilityCountry.ToLower() == request.CustomerCountry.ToLower()))
                    .Select(a => a.Id).ToList();

                if (!covaragePlanIds.Any())
                {
                    return await Task.FromResult(new DataRequestResult { IsSuccess = false, ErrorMessage = "Covarage plan is not exists." });
                }

                // Find Rate Chart based on  new Age and Gender
                var rateCharts = DbCon.GetAll<RateChart>().Where(a => covaragePlanIds.Any(x => x == a.CoveragePlanId)
                    && a.CustomerGender == request.Gender && (a.CustomerAge == "<=40" ? age <= 40 : age > 40)).FirstOrDefault();

                if (rateCharts != null)
                {
                    // Insert new customer contract
                    currentContract.CustomerName = request.CustomerName;
                    currentContract.CustomerAddress = request.CustomerAddress;
                    currentContract.CustomerGender = request.Gender;
                    currentContract.CustomerCountry = request.CustomerCountry;
                    currentContract.CustomerDOB = request.CustomerDOB;
                    currentContract.SaleDate = request.SaleDate;
                    currentContract.RateChartId = rateCharts.Id;

                    var contractId = DbCon.Update<Contract>(currentContract);
                    return await Task.FromResult(new DataRequestResult { IsSuccess = true, ErrorMessage = string.Empty });
                }
                else
                {
                    return await Task.FromResult(new DataRequestResult { IsSuccess = false, ErrorMessage = "Rate chart is not exists" });
                }

            }
            catch (Exception ex)
            {
                return await Task.FromResult(new DataRequestResult { IsSuccess = false, ErrorMessage = ex.Message });
            }
        }

        public async Task<DataRequestResult> DeleteCustomerContractAsync(DeleteContractCommand request)
        {
            try
            {
                using IDbConnection DbCon = connection;

                var deleteContract = DbCon.Get<Contract>(request.ContractId);
                if(deleteContract == null)
                    return await Task.FromResult(new DataRequestResult { IsSuccess = false, ErrorMessage = "selected contract is not exist" });

                var isItemDeleted = DbCon.DeleteAsync(deleteContract).Result;

                return await Task.FromResult(new DataRequestResult { IsSuccess = isItemDeleted, ErrorMessage = string.Empty });
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new DataRequestResult
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                });
            }
        }

        public async Task<IEnumerable<ContractDetail>> GetContractsAsync()
        {
            using IDbConnection DbCon = connection;

            var query = "SELECT c.Id as ContractId, c.CustomerName,c.CustomerAddress,c.CustomerGender,c.CustomerCountry," +
                        "CAST(c.CustomerDOB as varchar(10)) as CustomerDOB,CAST(c.SaleDate as varchar(10)) as SaleDate, cp.PlanName as CoveragePlan, rc.NetPrice as NetPrice " +
                        "FROM Contracts c " +
                        "JOIN RateCharts rc on c.RateChartId = rc.Id " +
                        "JOIN CoveragePlans cp on rc.CoveragePlanId = cp.Id ";

            return await DbCon.QueryAsync<ContractDetail>(query);
        }

        private static int CalculateAge(DateTime dateOfBirth)
        {
            int age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                age = age - 1;

            return age;
        }
                
    }
}
