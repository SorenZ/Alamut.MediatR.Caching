using System.Threading.Tasks;

using Alamut.MediatR.Caching.SampleApi.Application.Commands;
using Alamut.MediatR.Caching.SampleApi.Application.Queries;
using Alamut.MediatR.Caching.SampleApi.Model;

using MediatR;
using Microsoft.AspNetCore.Mvc;
// ReSharper disable HollowTypeName
// ReSharper disable MethodNameNotMeaningful

namespace Alamut.MediatR.Caching.SampleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FooController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FooController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FooModel>> Get(int id)
        {
            return await _mediator.Send(new GetFooByIdQuery(id));
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _mediator.Send(new DeleteFooByIdCommand(id));
        }
    }
}
