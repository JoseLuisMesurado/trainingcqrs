using MediatR;
using Microsoft.AspNetCore.Mvc;
using Training.API.ProblemDetailsConfig;
using Training.Application.PermissionTypes.Queries;
using Training.Core.Responses;
using Training.NG.HttpResponse;
using Training.NG.KafkaHelper;

namespace Training.API.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiVersion("1")]
    [ApiController]
    public class PermissionTypeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly KafkaProducer<string, string> _producer;
        public PermissionTypeController(IMediator mediator,KafkaProducer<string, string> producer)  {
            _mediator = mediator; 
            _producer = producer;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<ICollection<PermissionTypeResponse>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(AppProblemDetails))]
        public async Task<IActionResult> Get()
        {
            _producer.Produce("trainingtopic", new Confluent.Kafka.Message<string, string> { Key = new Guid().ToString(), Value = "get-permission-types" });
            var toReturn = await _mediator.Send(new GetPermissionTypesQuery());
            return Ok(toReturn);
        }
    }
}
