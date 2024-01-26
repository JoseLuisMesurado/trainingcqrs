// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Training.Initialization;
using Training.NG.EFCommon;

Console.WriteLine($"Starting Migration Proccess");
var host = CreateHostBuilder(args).Build();
host.Run();
Environment.Exit(0);

IHostBuilder CreateHostBuilder(string[] args) =>
     Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {
                System.AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapper;
                Console.WriteLine($"Service Injection");
                var migrationConfiguration = new EFConfig
                {
                    ConnectionString = @"Server=training.mssqlserver; Database=TrainingDB; User Id=sa; Password=Testeo123!;TrustServerCertificate=True",
                    MigrationAssembly = "Training.SqlServerMigration",
                    DatabaseProviderType = DatabaseType.SqlServer
                };
                services.AddInfrastructureConfiguration(migrationConfiguration);
                services.InjectSQLServerRepositories();
                services.AddHostedService<MigrationBackgroundService>();
            });
void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e)
{
    Console.WriteLine(e.ExceptionObject.ToString());
    Console.WriteLine("Press Enter to Exit");
    Environment.Exit(0);
}

