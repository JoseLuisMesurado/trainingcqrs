using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Training.Core.Entities;
using Training.Infra.Constants;

namespace Training.Infra.EntityMap
{
    internal class PermissionMap : IEntityTypeConfiguration<Permission<Guid>>
    {
        public void Configure(EntityTypeBuilder<Permission<Guid>> builder)
        {
            //Tablename
            builder.ToTable(SqlServerTableConstant.Permission);
            //Keys
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            //Properties Constrains and more
            
            builder.Property(x => x.GrantedDate).IsRequired();

            


        }
    }
}

