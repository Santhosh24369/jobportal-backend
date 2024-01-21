namespace jobportal_backend
{
    public class JobSeekDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string JobsCollectionName { get; set; } = null!;

        public string UsersCollectionName { get; set; } = null!;

        public string Log_FilePath { get; set; } = null!;
    }
}
