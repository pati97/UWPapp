using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Authentication;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1
{
    public class MongoDbContext
    {
        private readonly string _username = ConfigurationManager.AppSettings.Get("CosmosDbAccountUsername");
        private readonly string _password = ConfigurationManager.AppSettings.Get("CosmosDbAccountPassword");
        private readonly string _host = ConfigurationManager.AppSettings.Get("CosmosDbAccountHost");
        private readonly int _port = Convert.ToInt32(ConfigurationManager.AppSettings.Get("CosmosDbAccountPort"));
        private readonly string _databaseName = ConfigurationManager.AppSettings.Get("DatabaseName");
        private readonly string _collectionName = ConfigurationManager.AppSettings.Get("CollectionName");
        private readonly string connectionString = @"mongodb://telemetrycosmosdb:I4EZoXM4sEo3OaNN72Sh72UPxszKfCacgAfLx4hQWs6T4H5vtfzOJe8WwTGCcaz0dXmG3EOVu8huW0fK9NcFIA==@telemetrycosmosdb.documents.azure.com:10255/?ssl=true&replicaSet=globaldb";

        public IMongoDatabase Database { get; set; }
        public IMongoCollection<Weather> collection { get; set; }

        public MongoDbContext()
        {
            MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));
            settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };

            var mongoClient = new MongoClient(settings);
            Database = mongoClient.GetDatabase(_databaseName);
            collection = Database.GetCollection<Weather>("weather");
        }

        public IMongoCollection<Weather> GetCollection
        {
            get
            {
                return Database.GetCollection<Weather>(_collectionName);
            }
        }

        public Weather FindMessage(int id)
        {
            return GetCollection.Find(new BsonDocument { { "MessageId", id } }).FirstAsync().Result;
        }

        public List<Weather> SelectAll()
        {
            var query = GetCollection.Find(new BsonDocument()).ToListAsync();
            return query.Result;
        }

        public List<Weather> GetParameters24h()
        {
            IMongoCollection<Weather> collectionTemperature = Database.GetCollection<Weather>("weather");

            var filter = Builders<Weather>.Filter.Empty;
            var projection = Builders<Weather>.Projection.Include("Temperature").Include("Humidity").Include("Pressure").Include("DateTime");
            var query = collectionTemperature.Find(filter).Project<Weather>(projection).SortByDescending(i => i.Id).Limit(5760).ToListAsync().Result;
            return query;
        }

        public List<Weather> GetParametersLast7Days()
        {
            IMongoCollection<Weather> collectionTemperature = Database.GetCollection<Weather>("weather");

            var filter = Builders<Weather>.Filter.Empty;
            var projection = Builders<Weather>.Projection.Include("Temperature").Include("DateTime").Include("Humidity").Include("Pressure");
            var query = collectionTemperature.Find(filter).Project<Weather>(projection).SortByDescending(i => i.Id).Limit(40320).ToListAsync().Result;
            return query;
        }

        public Weather GetLastWeather()
        {
            IMongoCollection<Weather> collectionTemperature = Database.GetCollection<Weather>("weather");

            var filter = Builders<Weather>.Filter.Empty;
            var projection = Builders<Weather>.Projection.Include("Temperature").Include("DateTime").Include("Humidity").Include("Pressure");
            var query = collectionTemperature.Find(filter).Project<Weather>(projection).SortByDescending(i => i.Id).Limit(1).FirstAsync().Result;
            return query;
        }

        public Weather GetMinTemperatureLast24h()
        {
            IMongoCollection<Weather> collectionTemperature = Database.GetCollection<Weather>("weather");

            var filter = Builders<Weather>.Filter.Empty;
            var projection = Builders<Weather>.Projection.Include("Temperature").Include("DateTime");
            var query = collectionTemperature.Find(filter).Project<Weather>(projection).SortByDescending(i => i.Id).Limit(5760).ToListAsync().Result;
            var result = query.OrderBy(p => p.Temperature).FirstOrDefault();
            return result;
        }

        public Weather GetMaxTemperatureLast24h()
        {
            IMongoCollection<Weather> collectionTemperature = Database.GetCollection<Weather>("weather");

            var filter = Builders<Weather>.Filter.Empty;
            var projection = Builders<Weather>.Projection.Include("Temperature").Include("DateTime");
            var query = collectionTemperature.Find(filter).Project<Weather>(projection).SortByDescending(i => i.Id).Limit(5760).ToListAsync().Result;
            var result = query.OrderBy(p => p.Temperature).LastOrDefault();
            return result;
        }

        public Weather GetMinHumidityLast24h()
        {
            IMongoCollection<Weather> collectionTemperature = Database.GetCollection<Weather>("weather");

            var filter = Builders<Weather>.Filter.Empty;
            var projection = Builders<Weather>.Projection.Include("Humidity").Include("DateTime");
            var query = collectionTemperature.Find(filter).Project<Weather>(projection).SortByDescending(i => i.Id).Limit(5760).ToListAsync().Result;
            var result = query.OrderBy(p => p.Humidity).FirstOrDefault();
            return result;
        }

        public Weather GetMaxHumidityLast24h()
        {
            IMongoCollection<Weather> collectionTemperature = Database.GetCollection<Weather>("weather");

            var filter = Builders<Weather>.Filter.Empty;
            var projection = Builders<Weather>.Projection.Include("Humidity").Include("DateTime");
            var query = collectionTemperature.Find(filter).Project<Weather>(projection).SortByDescending(i => i.Id).Limit(5760).ToListAsync().Result;
            var result = query.OrderBy(p => p.Humidity).LastOrDefault();
            return result;
        }

        public Weather GetMinPressureLast24h()
        {
            IMongoCollection<Weather> collectionTemperature = Database.GetCollection<Weather>("weather");

            var filter = Builders<Weather>.Filter.Empty;
            var projection = Builders<Weather>.Projection.Include("Pressure").Include("DateTime");
            var query = collectionTemperature.Find(filter).Project<Weather>(projection).SortByDescending(i => i.Id).Limit(5760).ToListAsync().Result;
            var result = query.OrderBy(p => p.Pressure).FirstOrDefault();
            return result;
        }

        public Weather GetMaxPressureLast24h()
        {
            IMongoCollection<Weather> collectionTemperature = Database.GetCollection<Weather>("weather");

            var filter = Builders<Weather>.Filter.Empty;
            var projection = Builders<Weather>.Projection.Include("Pressure").Include("DateTime");
            var query = collectionTemperature.Find(filter).Project<Weather>(projection).SortByDescending(i => i.Id).Limit(5760).ToListAsync().Result;
            var result = query.OrderBy(p => p.Pressure).LastOrDefault();
            return result;
        }

    }

}