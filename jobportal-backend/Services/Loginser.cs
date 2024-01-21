using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace jobportal_backend.Services
{
    public class Loginser
    {
        private readonly IMongoCollection<Loginmodel> _usersCollection;
        public Loginser(
            IOptions<JobSeekDatabaseSettings> jobSeekDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                jobSeekDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                jobSeekDatabaseSettings.Value.DatabaseName);

            _usersCollection = mongoDatabase.GetCollection<Loginmodel>(
                jobSeekDatabaseSettings.Value.UsersCollectionName);
        }
        public async Task<Loginmodel?> GetAsync(string id) =>
           await _usersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();



        public async Task<Loginmodel?> Login(Loginmodel user) =>
          await _usersCollection.Find(x => x.email == user.email).FirstOrDefaultAsync();


        public async Task CreateAsync(Loginmodel newuser) =>
           await _usersCollection.InsertOneAsync(newuser);
    }
}
