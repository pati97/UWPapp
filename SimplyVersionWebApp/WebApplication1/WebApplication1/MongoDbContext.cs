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

        public MongoDbContext()
        {
            MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));
            settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };

            var mongoClient = new MongoClient(settings);
            Database = mongoClient.GetDatabase(_databaseName);
            var collection = Database.GetCollection<BsonDocument>("weather");
        }

        public IMongoCollection<Weather> GetWeathers
        {
            get
            {
                return Database.GetCollection<Weather>(_collectionName);
            }
        }

        public Weather FindMessage(int id)
        {
            return GetWeathers.Find(new BsonDocument { { "MessageId", id } }).FirstAsync().Result;
        }
        public List<Weather> SelectAll()
        {
            var query = GetWeathers.Find(new BsonDocument()).ToListAsync();
            return query.Result;
        }
    }

}