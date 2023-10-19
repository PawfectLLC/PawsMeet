using MongoDB.Bson.Serialization.Attributes;

namespace PawfectAppCore.Models
{
    public class Buyer 
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("buyerId")]
        public string BuyerId { get; set; }
        [BsonElement("firstName")]
        public string FirstName { get; set; }
        [BsonElement("lastName")]
        public string LastName { get; set; }
        [BsonElement("auth")]
        public Auth Auth { get; set; }
        [BsonElement("region")]
        public Region Region { get; set; }
        [BsonElement("what_looking_for")]
        public string WhatLookingFor { get; set; }
        [BsonElement("type")]
        public string Type { get; set; }
        [BsonElement("gender")]
        public string Gender { get; set; }
        [BsonElement("goodWith")]
        public List<string> GoodWith { get; set; }
        [BsonElement("energyLevel")]
        public string EnergyLevel { get; set; }
        [BsonElement("coatLength")]
        public string CoatLength { get; set; }
        [BsonElement("careAndBehavior")]
        public List<string> CareAndBehavior { get; set; }
        [BsonElement("privileges")]
        public bool Privileges { get; set; }

    }
}
