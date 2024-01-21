using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace jobportal_backend
{
    public class Loginmodel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string name { get; set; } = null!;
        public string email { get; set; } = null!;

        public string password { get; set; } = null!;

        public string role { get; set; } = null!;
    }
}
