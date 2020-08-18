using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TestCoreApp.Domain.Contracts;
using TestCoreApp.Domain.Contracts.Request;
using TestCoreApp.Domain.Contracts.Response;

namespace TestCoreApp.Application.Contracts.UpdateContract
{
    public class UpdateContractCommandHandler : IRequestHandler<UpdateContractCommand, DataRequestResult>
    {
        private readonly IContractRepository _contractRepository;
        public UpdateContractCommandHandler(IContractRepository contractRepository)
        {
            _contractRepository = contractRepository;
        }

        public Task<DataRequestResult> Handle(UpdateContractCommand request, CancellationToken cancellationToken)
        {
            return _contractRepository.UpdateCustomerContractAsync(request);
        }
    }
}
