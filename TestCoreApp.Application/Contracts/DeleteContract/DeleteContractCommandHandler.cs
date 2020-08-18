using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestCoreApp.Domain.Contracts;
using TestCoreApp.Domain.Contracts.Request;
using TestCoreApp.Domain.Contracts.Response;

namespace TestCoreApp.Application.Contracts.DeleteContract
{
    public class DeleteContractCommandHandler : IRequestHandler<DeleteContractCommand,DataRequestResult>
    {
        private readonly IContractRepository _contractRepository;
        public DeleteContractCommandHandler(IContractRepository contractRepository)
        {
            _contractRepository = contractRepository;
        }

        public Task<DataRequestResult> Handle(DeleteContractCommand request, CancellationToken cancellationToken)
        {
            return _contractRepository.DeleteCustomerContractAsync(request);
        }
    }
}
