using Training.Infra.Contexts;

namespace Training.SqlServerMigration.BaseSeed
{
    public interface ISqlServerMigrationSeed
    {
        int SeedOrder { get; }
        Task ExecuteAsync(TrainigContext darkhorseContext);
    }
}
