using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebApplication1.Models
{
    [DataContract]
    public class DataPoint
    {
        public DataPoint(DateTime DateTime, double YValue)
        {
            this.DateTime = DateTime;
            this.YValue = YValue;
        }

        [DataMember(Name = "label")]    
        public DateTime? DateTime = null;

        [DataMember(Name = "y")]
        public double? YValue = null;
    }
}