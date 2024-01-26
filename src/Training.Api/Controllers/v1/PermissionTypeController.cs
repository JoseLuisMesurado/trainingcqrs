using MediatR;
using Microsoft.AspNetCore.Mvc;
using Training.API.ProblemDetailsConfig;
using Training.Application.PermissionTypes.Queries;
using Training.Core.Responses;
using Training.NG.HttpResponse;

namespace Training.API.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiVersion("1")]
    [ApiController]
    public class PermissionTypeController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PermissionTypeController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<ICollection<PermissionTypeResponse>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(AppProblemDetails))]
        public async Task<IActionResult> Get()
        {
            var toReturn = await _mediator.Send(new GetPermissionTypeQuery());
            return Ok(toReturn);
        }
    }
}
