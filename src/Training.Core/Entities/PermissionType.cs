using Training.NG.EFCommon.AuditEntities;
using Training.NG.EFCommon.BaseEntities;

namespace Training.Core.Entities
{
    public class PermissionType<T> : AuditableEntity,  IEntityPK<T>
    {
        //Keys
        public T Id { get; set; }
        //Properties
        public string Name { get; set; }
        public string Description { get; set; }

        //Navigations
        public ICollection<Permission<Guid>> Permissions { get; set; }

    }
}
