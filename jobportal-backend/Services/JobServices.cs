using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace jobportal_backend.Services
{
    public class JobServices
    {
        private readonly IMongoCollection<JobModel> _jobsCollection;

        public JobServices(
        IOptions<JobSeekDatabaseSettings> jobSeekDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                jobSeekDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                jobSeekDatabaseSettings.Value.DatabaseName);

            _jobsCollection = mongoDatabase.GetCollection<JobModel>(
                jobSeekDatabaseSettings.Value.JobsCollectionName);
        }

        public async Task<List<JobModel>> GetAsync() =>
        await _jobsCollection.Find(_ => true).ToListAsync();

        public async Task<JobModel?> GetAsync(string id) =>
            await _jobsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(JobModel newBook) =>
            await _jobsCollection.InsertOneAsync(newBook);

        public async Task UpdateAsync(string id, JobModel updatedBook) =>
            await _jobsCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

        public async Task RemoveAsync(string id) =>
            await _jobsCollection.DeleteOneAsync(x => x.Id == id);
    }
}
