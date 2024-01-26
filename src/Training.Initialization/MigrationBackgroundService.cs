using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Training.Infra.Contexts;
using Training.SqlServerMigration.BaseSeed;


namespace Training.Initialization
{
    public class MigrationBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        public MigrationBackgroundService(IServiceProvider serviceProvider, IHostApplicationLifetime hostApplicationLifetime)
        {
            _serviceProvider = serviceProvider;
            _hostApplicationLifetime = hostApplicationLifetime;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await RunDatabaseMigration(stoppingToken);
        }
        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("DB Migrations Service Hosted is stopping.");
            _hostApplicationLifetime.StopApplication();
            await base.StopAsync(stoppingToken);
        }

        public async Task RunDatabaseMigration(CancellationToken stoppingToken)
        {
            using IServiceScope scope = _serviceProvider.CreateScope();
            var trainigContext = scope.ServiceProvider.GetRequiredService<TrainigContext>();
            Console.WriteLine($"Apply DB Migrations");
            var pendingMigrations = await trainigContext.Database.GetPendingMigrationsAsync(cancellationToken: stoppingToken);
            if (pendingMigrations.Any())
            {
                Console.WriteLine($"You have {pendingMigrations.Count()} pending migrations to apply.");
                Console.WriteLine("Applying pending migrations now");
                await trainigContext.Database.MigrateAsync(cancellationToken: stoppingToken);
            }
            var lastAppliedMigration = (await trainigContext.Database.GetAppliedMigrationsAsync(cancellationToken: stoppingToken)).Last();
            Console.WriteLine($"You're on schema version: {lastAppliedMigration}");

            Console.WriteLine($"Run Data Seed");
            await RunDataSeed(trainigContext);
            await trainigContext.SaveChangesAsync(stoppingToken);
            await StopAsync(stoppingToken);
        }

        private static async Task RunDataSeed(TrainigContext trainigContext)
        {
            var applicationAssembly = typeof(SqlServerMigration.AssemblyReference).Assembly;
            var seeds = from t in applicationAssembly.GetTypes()
                            where t.GetInterfaces().Contains(typeof(ISqlServerMigrationSeed))
                            && t.GetConstructor(Type.EmptyTypes) != null
                            && t.Namespace != null
                            select Activator.CreateInstance(t) as ISqlServerMigrationSeed;

            foreach (var seed in seeds)
            {
                await seed.ExecuteAsync(trainigContext);
            }

        }
    }
}
