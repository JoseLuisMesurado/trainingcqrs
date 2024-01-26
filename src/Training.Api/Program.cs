using Training.NG.EFCommon;
using Training.NG.KafkaHelper;
using Application = Training.Application;

var builder = WebApplication.CreateBuilder(args);

var currentEnviroment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

var configurationBuilder = new ConfigurationBuilder()
             .SetBasePath(AppContext.BaseDirectory)
             .AddJsonFile($"appsettings.{currentEnviroment}.json", optional: false, reloadOnChange: true)
             .AddEnvironmentVariables();

IConfiguration configuration = configurationBuilder.Build();
var efOptions = new EFConfig();
configuration.GetSection(EFConfig.Position).Bind(efOptions);
var kafkaOptions = new KafkaConfig();
configuration.GetSection(KafkaConfig.Position).Bind(kafkaOptions);

builder.Services.AddControllers();
builder.Services.AddInfrastructureConfiguration(efOptions);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen();
builder.Services.AddHealthchecksConfig();
var applicationAssembly = typeof(Application.AssemblyReference).Assembly;
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(applicationAssembly);
});

builder.Services.AddKafkaConfiguration(kafkaOptions);

builder.Services.FluentValidationsModelsConfiguration(applicationAssembly);
builder.Services.ProblemDetailsConfiguration();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", cfg =>
    {
        cfg.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();
app.SetAppConfiguration();
app.Run();
