using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PawfectAppCore.Models
{
    public class Pet
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("pid")]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string PetId { get; set; }
        [BsonElement("userID")]
        //[BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string UserId { get; set; }
        [BsonElement("profiles")]
        public List<string> Profiles { get; set; }
        [BsonElement("videos")]
        public List<Video> Videos { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("birthDate")]
        public DateTime BirthDate { get; set; }      
        [BsonElement("type")]
        public string Type { get; set; }
        [BsonElement("breeds")]
        public Breeds Breeds { get; set; }
        [BsonElement("colors")]
        public Colors Colors { get; set; }
        [BsonElement("size")]
        public string Size { get; set; }
        [BsonElement("about")]
        public string About { get; set; }
        [BsonElement("goodWith")]
        public List<string> GoodWith { get; set; }
        [BsonElement("energyLevel")]
        public string EnergyLevel { get; set; }
        [BsonElement("coatLength")] 
        public string CoatLength { get; set; }
        [BsonElement("careAndBehavior")]
        public List<string> CareAndBehavior { get; set; }
        [BsonElement("gender")]
        public string Gender { get; set; }
        [BsonElement("published_at")]
        public string Published { get; set; }
        [BsonElement("contact")]
        public Contact Contact { get; set; }
        [BsonElement("organization_id")]
        public string OrorganizationId { get; set; }
    }

    public class Breeds
    {
        [BsonElement("primary")]
        public string Primary { get; set; }
        [BsonElement("secondary")]
        public string Secondary { get; set; }
        [BsonElement("mixed")]
        public bool Mixed { get; set; }
        [BsonElement("unknown")]
        public bool Unknown { get; set; }
    }

    public class Colors
    {
        [BsonElement("primary")]
        public string Primary { get; set; }
        [BsonElement("secondary")]
        public string Secondary { get; set; }
        [BsonElement("tertiary")]
        public string Tertiary { get; set; }
    }

    public class Contact
    {
        [BsonElement("email")]
        public string Email { get; set; }
        [BsonElement("phone")]
        public string Phone { get; set; }
        [BsonElement("address")]
        public Address Address { get; set; }

    }
    public class Address
    {
        [BsonElement("address1")]
        public string Address1 { get; set; }
        [BsonElement("address2")]
        public string Address2 { get; set; }
        [BsonElement("city")]
        public string City { get; set; }
        [BsonElement("state")]
        public string State { get; set; }
        [BsonElement("postcode")]
        public string PostalCode { get; set; }
        [BsonElement("country")]
        public string Country { get; set; }
    }
    public class Video
    {
        [BsonElement("embed")]
        public string Embed { get; set; }
    }
}
