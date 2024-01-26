namespace Training.NG.EFCommon.AuditEntities
{
    public interface IAuditableEntity : IAuditableCreate
    {
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
