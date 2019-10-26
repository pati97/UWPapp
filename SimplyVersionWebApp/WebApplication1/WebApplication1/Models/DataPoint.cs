using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebApplication1.Models
{
    //[DataContract]
    public class DataPoint
    {
        public DataPoint(string DateTime, double Temperature)
        {
            this.DateTime = DateTime;
            this.Temperature = Temperature;
        }

        [JsonProperty(PropertyName = "DateTime")]
        public string DateTime = null;

        [JsonProperty(PropertyName = "Temperature")]
        public double? Temperature = null;
    }
}