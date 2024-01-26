using MediatR;
using Microsoft.AspNetCore.Mvc;
using Training.API.ProblemDetailsConfig;
using Training.Application.Permissions.Commands;
using Training.Application.Permissions.Queries;
using Training.Core.Responses;
using Training.NG.HttpResponse;
using Training.NG.KafkaHelper;

namespace Training.API.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiVersion("1")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly KafkaProducer<string, string> _producer;
        public PermissionController(IMediator mediator, KafkaProducer<string, string> producer)
        {
            _mediator = mediator;
            _producer = producer;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<ICollection<PermissionResponse>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(AppProblemDetails))]
        public async Task<IActionResult> Get()
        {
            _producer.Produce("trainingtopic", new Confluent.Kafka.Message<string, string> { Key = new Guid().ToString(), Value = "get" });
            var toReturn = await _mediator.Send(new GetPermissionQuery());
            return Ok(toReturn);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ApiResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(AppProblemDetails))]
        public async Task<IActionResult> Add(AddPermissionCommand values)
        {
            _producer.Produce("trainingtopic", new Confluent.Kafka.Message<string, string> { Key = new Guid().ToString(), Value = "request" });
            var toReturn = await _mediator.Send(values);
            return Created("emptyUrl", toReturn);
        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(AppProblemDetails))]
        public async Task<IActionResult> Update(UpdatePermissionCommand values)
        {
            _producer.Produce("trainingtopic", new Confluent.Kafka.Message<string, string> { Key = new Guid().ToString(), Value = "modify" });
            var toReturn = await _mediator.Send(values);
            if (toReturn == null)
                return NoContent();
            return Ok(toReturn);
        }

    }
}
