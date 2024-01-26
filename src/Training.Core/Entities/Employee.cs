using Nest;
using Training.Core.Entities;
using Training.NG.EFCommon.AuditEntities;
using Training.NG.EFCommon.BaseEntities;


namespace Training.Core;

public class Employee<T> : AuditableEntity, IEntityPK<T>
{
    //Keys
    public T Id { get; set; }

    public string EmployeeFirstName { get; set; }
    public string EmployeeLastName { get; set; }
    public DateTime DateOfBirth {get; set;}

    //Navigations
    public ICollection<Permission<Guid>> Permissions { get; set; }
}
