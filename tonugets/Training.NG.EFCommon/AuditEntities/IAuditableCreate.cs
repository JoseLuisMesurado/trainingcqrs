namespace Training.NG.EFCommon.AuditEntities
{
    public interface IAuditableCreate
    {
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
