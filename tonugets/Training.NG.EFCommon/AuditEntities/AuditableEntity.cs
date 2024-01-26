namespace Training.NG.EFCommon.AuditEntities
{
    public class AuditableEntity : AuditableCreate
    {
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
        public string UpdatedBy { get; set; }
    }
}
