using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private MongoDbContext _db = new MongoDbContext();

        public ActionResult Index()
        {
            List<Weather> getTemp = new List<Weather>();
            List<Weather> getHumidity = new List<Weather>();
            List<Weather> getPressure = new List<Weather>();
            getTemp = _db.GetTemperature();
            getHumidity = _db.GetHumidity();
            getPressure = _db.GetPressure();

            List<DataPoint> temperature = new List<DataPoint>();
            List<DataPoint> humidity = new List<DataPoint>();
            List<DataPoint> pressure = new List<DataPoint>();

            foreach(var item in getTemp)
            {
                double temp = item.Temperature;
                DateTime date;
                DateTime.TryParse(item.DateTime, out date);
                var dataPoint = new DataPoint(date, temp);
                temperature.Add(dataPoint);
            }

            foreach (var item in getHumidity)
            {
                double hum = item.Humidity;
                DateTime date;
                DateTime.TryParse(item.DateTime, out date);
                var dataPoint = new DataPoint(date, hum);
                humidity.Add(dataPoint);
            }

            foreach (var item in getPressure)
            {
                double press = item.Pressure;
                DateTime date;
                DateTime.TryParse(item.DateTime, out date);
                var dataPoint = new DataPoint(date, press);
                pressure.Add(dataPoint);
            }
            temperature.Reverse();
            humidity.Reverse();
            pressure.Reverse();

            var jsonSettings = new JsonSerializerSettings();
            jsonSettings.DateFormatString = "dd/MM/yyyy hh:mm:ss";

            ViewBag.DataPoint1 = JsonConvert.SerializeObject(temperature, new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
            ViewBag.DataPoint2 = JsonConvert.SerializeObject(humidity, new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
            ViewBag.DataPoint3 = JsonConvert.SerializeObject(pressure, new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}