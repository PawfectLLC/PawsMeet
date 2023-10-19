using MongoDB.Driver;
using PawfectAppCore.Models;

namespace PawfectAppCore.Servers
{
    public class BuyerService : IBuyerService
    {
        private readonly IMongoCollection<Buyer> _buyers;
        public BuyerService(IMongoClient mongoClient)
        {
            var db = mongoClient.GetDatabase("BaseUsers");
            _buyers = db.GetCollection<Buyer>("BuyerSignUpProfile");
        }

        public async Task<List<Buyer>> GetBuyersAsync()
        {
            return await _buyers.Find(_ => true).ToListAsync();
        }

        public async Task<Buyer> GetBuyerByIdAsync(string buyerId)
        {
            var filter = Builders<Buyer>.Filter.Eq("buyerId", buyerId);
            return await _buyers.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<Buyer> AddBuyerAsync(Buyer buyer)
        {
            await _buyers.InsertOneAsync(buyer);
            return buyer;
        }

        public async Task<Buyer> UpdateBuyerAsync(string buyerId, Buyer buyer)
        {
            var buyerFilter = Builders<Buyer>.Filter.Eq("buyerId", buyerId);
            var update = Builders<Buyer>.Update
                .Set("firstName", buyer.FirstName)
                .Set("lastName", buyer.LastName)
                .Set("auth", buyer.Auth)
                .Set("region", buyer.Region)
                .Set("what_looking_for", buyer.WhatLookingFor)
                .Set("type", buyer.Type)
                .Set("gender", buyer.Gender)
                .Set("goodWith", buyer.GoodWith)
                .Set("energyLevel", buyer.EnergyLevel)
                .Set("coatLength", buyer.CoatLength)
                .Set("careAndBehavior", buyer.CareAndBehavior)
                .Set("privileges", buyer.Privileges);
            var newbuyer = await _buyers.FindOneAndUpdateAsync(buyerFilter, update, new FindOneAndUpdateOptions<Buyer>
            {
                ReturnDocument = ReturnDocument.After
            });
            return buyer;
        }    
        public async Task DeleteBuyerAsync(string buyerId)
        {
            var filter = Builders<Buyer>.Filter.Eq("buyerId", buyerId);
            await _buyers.DeleteOneAsync(filter);
        }

        
    }
}
