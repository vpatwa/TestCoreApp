using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestCoreApp.Domain.Contracts;
using TestCoreApp.Domain.Contracts.Request;
using TestCoreApp.Domain.Contracts.Response;

namespace TestCoreApp.Application.Contracts.GetContract
{
    public class GetContractQueryHandler : IRequestHandler<GetContractQuery, IEnumerable<ContractDetail>>
    {
        private readonly IContractRepository _contractRepository;

        public GetContractQueryHandler(IContractRepository contractRepository)
        {
            _contractRepository = contractRepository;
        }        

        public Task<IEnumerable<ContractDetail>> Handle(GetContractQuery request, CancellationToken cancellationToken)
        {
            return _contractRepository.GetContractsAsync();
        }
    }
}
