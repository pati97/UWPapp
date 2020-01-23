using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Weather
    {
        [JsonProperty(PropertyName = "_id")]
        //[BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [JsonProperty(PropertyName = "DeviceId")]
        public string DeviceId { get; set;}

        [JsonProperty(PropertyName = "MessageId")]
        public int MessageId { get; set; }

        [JsonProperty(PropertyName = "DateTime")]
        public string  DateTime { get; set; }

        [JsonProperty(PropertyName = "Temperature")]
        public double Temperature { get; set; }

        [JsonProperty(PropertyName = "Humidity")]
        public float Humidity { get; set; }

        [JsonProperty(PropertyName = "Pressure")]
        public float Pressure { get; set; }
    }
}