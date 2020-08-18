using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TestCoreApp.Domain.Contracts.Response;

namespace TestCoreApp.Domain.Contracts.Request
{
    public class UpdateContractCommand : IRequest<DataRequestResult>
    {
        public int ContractId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerCountry { get; set; }
        public DateTime CustomerDOB { get; set; }
        public DateTime SaleDate { get; set; }
        public string Gender { get; set; }
    }
}
