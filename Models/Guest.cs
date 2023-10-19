using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PawfectAppCore.Models
{
    public class Guest
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("userId")]
        public string UserId { get; set; }
        [BsonElement("auth")]
        public Auth Auth { get; set; }
        [BsonElement("firstName")]
        public string FirstName { get; set; }
        [BsonElement("lastName")]
        public string LastName { get; set; }
        [BsonElement("privileges")]
        public bool Privileges { get; set; }
    }
    public class Auth
    {
        [BsonElement("phoneNumber")]
        public string PhoneNumber { get; set; }
        [BsonElement("verificationCode")]
        public string VerficationCod { get; set; }
        [BsonElement("email")]
        public string Email { get; set; }
        [BsonElement("password_email")]
        public string PasswordEmail { get; set; }
        [BsonElement("google")]
        public string Google { get; set; }
        [BsonElement("password_gmail")]
        public string PasswordGmail { get; set; }
    }
}
