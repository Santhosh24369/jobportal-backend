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

        public string avatar { get; set; } = null!;     

        public string province { get; set; } = null!;
        public string city { get; set; } = null!;  
        public string country { get; set; } = null!;
        public string postalCode { get; set; } = null!;

        public string type { get; set; } = null!;

        public string role { get; set; } = null!;

        public DateTime? createdAt { get; set; } = null;

        public DateTime? updatedAt { get; set; } = null;

        public decimal __v { get; set; }

        public bool blockUser { get; set; }
    }
}
