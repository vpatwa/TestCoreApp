using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TestCoreApp.Domain.Contracts.Response;

namespace TestCoreApp.Domain.Contracts.Request
{
    public class DeleteContractCommand : IRequest<DataRequestResult>
    {
        public int ContractId { get; set; }
    }
}
