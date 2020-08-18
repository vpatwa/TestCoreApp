using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using TestCoreApp.Domain.Contracts.Request;
using TestCoreApp.Domain.Contracts.Response;

namespace TestCoreApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerContractController : ControllerBase
    {
        private readonly ILogger<CustomerContractController> _logger;
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        public CustomerContractController(ILogger<CustomerContractController> logger)
        {
            _logger = logger;
        }

        [Route("contracts")]
        [HttpGet]
        public async Task<IEnumerable<ContractDetail>> GetContracts(GetContractQuery getContractQuery)
        {
            return await Mediator.Send(getContractQuery);
        }

        [Route("create")]
        [HttpPost]
        public async Task<ActionResult<DataRequestResult>> CreateContract(CreateContractCommand createContractCommand)
        {
            return await Mediator.Send(createContractCommand);
        }

        [Route("update")]
        [HttpPost]
        public async Task<ActionResult<DataRequestResult>> UpdateContract(UpdateContractCommand updateContractCommand)
        {
            return await Mediator.Send(updateContractCommand);
        }

        [Route("delete")]
        [HttpPost]
        public async Task<ActionResult<DataRequestResult>> DeleteContract(DeleteContractCommand deleteContractCommand)
        {
            return await Mediator.Send(deleteContractCommand);
        }


    }
}
