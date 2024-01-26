using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Training.Core.Entities;
using Training.Infra.Constants;

namespace Training.Infra.EntityMap
{
    internal class PermissionTypeMap : IEntityTypeConfiguration<PermissionType<short>>
    {
        public void Configure(EntityTypeBuilder<PermissionType<short>> builder)
        {
            //TableName
            builder.ToTable(SqlServerTableConstant.PermissionType);
            //Keys
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            //Properties Constrains and more
            builder.Property(x => x.Name).HasMaxLength(80).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(200).IsRequired();

            //Navigations
            builder.HasMany<Permission<Guid>>(e => e.Permissions).WithOne(c => c.PermissionType).HasForeignKey(x => x.PermissionTypeId);
        }
    }
}
