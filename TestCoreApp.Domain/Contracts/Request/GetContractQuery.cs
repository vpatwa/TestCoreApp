using MediatR;
using System.Collections.Generic;
using TestCoreApp.Domain.Contracts.Response;

namespace TestCoreApp.Domain.Contracts.Request
{
    public class GetContractQuery : IRequest<IEnumerable<ContractDetail>>
    {
    }
}
