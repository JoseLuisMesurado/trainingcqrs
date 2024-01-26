using Microsoft.EntityFrameworkCore;
using Training.Core.Entities;
using Training.Infra.Contexts;
using Training.SqlServerMigration.BaseSeed;

namespace Training.SqlServerMigration.Sedding
{
    public class PermissionTypeSeed : ISqlServerMigrationSeed
    {
        public int SeedOrder { get; } = 1;

        public async Task ExecuteAsync(TrainigContext darkhorseContext)
        {
            Console.WriteLine($"Run PermissionTypeSeed ");
            var permissionTypes = GetInformationToAdd();
            foreach (var permissionType in permissionTypes)
            {
                if(!await darkhorseContext.PermissionTypes.AsNoTracking()
                    .AnyAsync(x=>x.Name == permissionType.Name))
                    darkhorseContext.Add(permissionType);
            }
        }

        private static List<PermissionType<short>> GetInformationToAdd()
        {
            return new List<PermissionType<short>> {
                new PermissionType<short> { Name = "Root", Description = "Root Description" },
                new PermissionType<short> { Name = "Admin", Description = "Root Description" },
                new PermissionType<short> { Name = "Permission 1", Description = "Root Permission 1" },
                new PermissionType<short> { Name = "Permission 2", Description = "Root Permission 2" }
                };
        }
    }
}
