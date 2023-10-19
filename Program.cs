using Firebase.Auth;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PawfectAppCore.Models;
using PawfectAppCore.Servers;

namespace PawfectAppCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var firebaseApp = FirebaseApp.Create(new AppOptions
            {
                Credential = GoogleCredential.FromFile("firebase-config.json"),
            });

            builder.Services.Configure<MongoDBSettings>(
                           builder.Configuration.GetSection(nameof(MongoDB)));
            builder.Services.AddSingleton<MongoDBSettings>(sp =>
                           sp.GetRequiredService<IOptions<MongoDBSettings>>().Value);

            builder.Services.AddSingleton<IMongoClient>(s =>
                           new MongoClient(builder.Configuration.GetValue<string>("MongoDB:ConnectionURI")));
          

            builder.Services.AddScoped<IBaseUserService, BaseUserService>();
            builder.Services.AddScoped<IBreederUserService, BreederUserService>();
            builder.Services.AddScoped<IPetService, PetService>();
            builder.Services.AddScoped<IBuyerService, BuyerService>();


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}