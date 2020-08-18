using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestCoreApp.Domain.Contracts;
using TestCoreApp.Domain.Contracts.Request;
using TestCoreApp.Domain.Contracts.Response;

namespace TestCoreApp.Application.Contracts.CreateContract
{
    public class CreateContractCommandHandler : IRequestHandler<CreateContractCommand, DataRequestResult>
    {
        private readonly IContractRepository _contractRepository;
        public CreateContractCommandHandler(IContractRepository contractRepository)
        {
            _contractRepository = contractRepository;
        }

        public Task<DataRequestResult> Handle(CreateContractCommand request, CancellationToken cancellationToken)
        {
            return _contractRepository.CreateCustomerContractAsync(request);
        }
    }
}
