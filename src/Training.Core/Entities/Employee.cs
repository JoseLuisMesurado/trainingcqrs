using Nest;
using Training.Core.Entities;
using Training.NG.EFCommon.AuditEntities;
using Training.NG.EFCommon.BaseEntities;


namespace Training.Core;

public class Employee<T> : AuditableEntity, IEntityPK<T>
{
    //Keys
    public T Id { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime BirthDate {get; set;}

    //Navigations
    public ICollection<Permission<Guid>> Permissions { get; set; }
}
