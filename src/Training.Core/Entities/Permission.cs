using System;
using Training.NG.EFCommon.AuditEntities;
using Training.NG.EFCommon.BaseEntities;

namespace Training.Core.Entities
{
    public class Permission<T>: AuditableEntity, IEntityPK<T> 
    {
        //Keys
        public T Id { get; set; }
        public short PermissionTypeId { get; set; }
        public Guid EmployeeId { get; set; }

        //properties
        public DateTime GrantedDate { get; set; }
        public DateTime GrantedExpirationDate { get; set; }

        //Navigations
        public virtual PermissionType<short> PermissionType { get; set; }
        public virtual Employee<Guid> Employee { get; set; }
    }
}
