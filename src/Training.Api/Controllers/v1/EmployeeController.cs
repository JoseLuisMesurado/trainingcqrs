using MediatR;
using Microsoft.AspNetCore.Mvc;
using Training.API.ProblemDetailsConfig;
using Training.Application;
using Training.Core;
using Training.NG.HttpResponse;
using Training.NG.KafkaHelper;

namespace Training.API;

[Route("api/[controller]")]
[ApiVersion("1")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly KafkaProducer<string, string> _producer;

    private readonly ILogger<EmployeeController> _logger;

    public EmployeeController(
            IMediator mediator ,
            KafkaProducer<string, string> producer,
            ILogger<EmployeeController> logger
            ){
                _mediator=mediator;
                _producer = producer;
                _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<ICollection<EmployeeResponse>>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(AppProblemDetails))]
    public async Task<IActionResult> Get()
    {
        _producer.Produce("trainingtopic", new Confluent.Kafka.Message<string, string> { Key = new Guid().ToString(), Value = "get-employees" });
        var toReturn = await _mediator.Send(new GetEmployeesQuery());
        return Ok( new ApiResponse<ICollection<EmployeeResponse>> { Response= toReturn });
        
    }
    [HttpGet("{employeeId}/permissions")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<ICollection<EmployeePermissionsReponse>>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(AppProblemDetails))]
    public async Task<IActionResult> GetEmployeePermissions(Guid employeeId)
    {
        _producer.Produce("trainingtopic", new Confluent.Kafka.Message<string, string> { Key = new Guid().ToString(), Value = "get-employee-permissions" });
        var toReturn = await _mediator.Send(new GetEmployeePermissionsQuery{ EmployeeId = employeeId});
        return Ok( new ApiResponse<ICollection<EmployeePermissionsReponse>> { Response=toReturn });
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ApiResponse<Employee<Guid>>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(AppProblemDetails))]
    public async Task<IActionResult> Add (AddEmployeeCommand values){

        _producer.Produce("trainingtopic", new Confluent.Kafka.Message<string, string> { Key = new Guid().ToString(), Value = "create-employee" });
        var toReturn = await _mediator.Send(values);
        return Created("Url", new ApiResponse<Employee<Guid>> { Response= toReturn} );
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(AppProblemDetails))]
    public async Task<IActionResult> Update(UpdateEmployeeCommand values)
    {
        _producer.Produce("trainingtopic", new Confluent.Kafka.Message<string, string> { Key = new Guid().ToString(), Value = "modify-employee" });
        await _mediator.Send(values);
        return Ok();
    }
}
