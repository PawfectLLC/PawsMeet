using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PawfectAppCore.Models
{
    public class Breeder 
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("breederId")]
        public string BreederId { get; set; }
        [BsonElement("org_verification")]
        public bool orgVerification { get; set; }
        [BsonElement("desc")]
        public string Desc { get; set; }
        [BsonElement("region")]
        public Region Region { get; set; }
        [BsonElement("email")]
        public string Email { get; set; }
        [BsonElement("firstName")]
        public string FirstName { get; set; }
        [BsonElement("lastName")]
        public string LastName { get; set; }

    }
    public class Region
    {
        [BsonElement("latitude")]
        public double Latitude { get; set; }
        [BsonElement("longitude")]
        public double Longitude { get; set; }
    }
}
