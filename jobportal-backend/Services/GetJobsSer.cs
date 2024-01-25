using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace jobportal_backend.Services
{
    public class GetJobsSer
    {
        private readonly IMongoCollection<GetJobs> _jobs;
        public GetJobsSer(IOptions<JobSeekDatabaseSettings> jobSeekDatabaseSettings)
        {
            var mongoClient = new MongoClient(
               jobSeekDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                jobSeekDatabaseSettings.Value.DatabaseName);

            _jobs = mongoDatabase.GetCollection<GetJobs>(
                jobSeekDatabaseSettings.Value.JobsCollectionName);
        }
        public async Task<List<GetJobs>> GetJobs()
        {
            var pResults = _jobs.Aggregate()
                 .Lookup<GetJobs, GetJobs>("organizations", "organization", "_id", "organizationdata")
                 .Unwind("organizationdata")
                 .Lookup<GetJobs, GetJobs>("users", "organizationdata.user", "_id", "organizationdata.users")
                 .Unwind<GetJobs>("organizationdata.users")

                 .ToList();

            return pResults;
        }
        }
}
