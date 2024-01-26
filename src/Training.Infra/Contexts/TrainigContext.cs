using Microsoft.EntityFrameworkCore;
using Training.Core;
using Training.Core.Contexts;
using Training.Core.Entities;
using Training.Infra.EntityMap;
using Training.NG.EFCommon.AuditEntities;

namespace Training.Infra.Contexts
{
    public class TrainigContext : DbContext, ITrainigContext
    {
        public virtual DbSet<Permission<Guid>> Permissions { get; set; }
        public virtual DbSet<PermissionType<short>> PermissionTypes { get; set; }
        public virtual DbSet<Employee<Guid>> Employees { get; set; }

        public TrainigContext() { 
        }
        public TrainigContext(DbContextOptions<TrainigContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            MapEntitites(builder);
        }

        private static void MapEntitites(ModelBuilder builder)
        {
            builder.ApplyConfiguration<Permission<Guid>>(new PermissionMap());
            builder.ApplyConfiguration<PermissionType<short>>(new PermissionTypeMap());
            builder.ApplyConfiguration<Employee<Guid>>(new EmployeeMap());
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            if (ChangeTracker.Entries<IAuditableCreate>().Any())
            {
                foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
                {
                    var currentDate = DateTime.UtcNow;
                    switch (entry.State)
                    {
                        case EntityState.Added:

                            entry.Entity.CreatedBy = "user";
                            entry.Entity.CreatedDate = currentDate;
                            entry.Entity.UpdatedBy = "user";
                            entry.Entity.UpdatedDate = currentDate;
                            break;
                        case EntityState.Modified:
                            entry.Entity.UpdatedBy = "user";
                            entry.Entity.UpdatedDate = currentDate;
                            break;
                    }
                }
            }
            else if (ChangeTracker.Entries<IAuditableCreate>().Any())
            {
                foreach (var entry in ChangeTracker.Entries<IAuditableCreate>())
                {
                    var currentDate = DateTime.UtcNow;
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.Entity.CreatedBy = "user";
                            entry.Entity.CreatedDate = currentDate;
                            break;
                    }
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
