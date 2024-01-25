using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace jobportal_backend
{
    public class Organizationmodel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("user")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string user { get; set; } = null!;

        public string phoneNumber { get; set; } = null!;

        public DateTime? createdDate { get; set; } = null;

        public decimal? totalEmployee { get; set; }

        public string industryType { get; set; } = null!;

        public string address { get; set; } = null!;

        public string description { get; set; } = null !;

        public string status { get; set; } = null!;

        public DateTime? createdAt { get; set; } = null;

        public DateTime? updatedAt { get; set; } = null;

        public decimal __v { get; set; }

    }
}
