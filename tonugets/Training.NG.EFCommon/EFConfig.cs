namespace Training.NG.EFCommon
{
    public class EFConfig
    {
        
        public const string Position = "EFConfiguration";
        public string ConnectionString { get; set; }
        public string MigrationAssembly { get; set; }
        public string DatabaseProviderType { get; set; }
    }
}
