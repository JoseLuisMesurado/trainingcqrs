namespace Training.NG.EFCommon.AuditEntities
{
    public class AuditableCreate : IAuditableCreate,  IAuditableDelete 
    {
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string DeletedBy { get; set; }
    }
}
