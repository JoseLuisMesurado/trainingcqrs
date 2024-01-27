using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Training.Core;
using Training.Core.Entities;
using Training.Infra.Constants;

namespace Training.Infra;

internal class EmployeeMap : IEntityTypeConfiguration<Employee<Guid>>
{
    public void Configure(EntityTypeBuilder<Employee<Guid>> builder)
    {
        //Keys
        builder.ToTable(SqlServerTableConstant.Employee);
            //Keys
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        //Properties
        builder.Property(x => x.FirstName).HasMaxLength(50).IsRequired();
        builder.Property(x => x.LastName).HasMaxLength(50).IsRequired();
        builder.Property(x=>x.BirthDate).IsRequired();

        //Navegation One to Many
        builder.HasMany<Permission<Guid>>(e=>e.Permissions).WithOne(c=>c.Employee).HasForeignKey( e=>e.EmployeeId).IsRequired();
    }
}
