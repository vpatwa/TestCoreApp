using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestCoreApp.Domain.Contracts.Request;
using TestCoreApp.Domain.Contracts.Response;

namespace TestCoreApp.Domain.Contracts
{
    public interface IContractRepository
    {
        Task<DataRequestResult> CreateCustomerContractAsync(CreateContractCommand request);
        Task<DataRequestResult> UpdateCustomerContractAsync(UpdateContractCommand request);
        Task<DataRequestResult> DeleteCustomerContractAsync(DeleteContractCommand request);
        Task<IEnumerable<ContractDetail>> GetContractsAsync();
    }
}
