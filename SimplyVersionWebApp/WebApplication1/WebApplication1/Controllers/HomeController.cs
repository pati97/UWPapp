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

        public ActionResult Charts24h()
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

            int counter = 0;
            double averangeValue = 0;
            int SizeOfMeasurement = 20;
            
            foreach (var item in getTemp)
            {
                if (counter < 40)
                {
                    var temp = item.Temperature;
                    averangeValue += temp; 
                }
                if (counter == 40)
                {
                    double tempValue = averangeValue / 40;
                    DateTime date;
                    DateTime.TryParse(item.DateTime, out date);
                    var dataPoint = new DataPoint(date, tempValue);
                    temperature.Add(dataPoint);
                    counter = 0;
                    averangeValue = 0;
                }
                counter++;
            }
            averangeValue = 0;
            counter = 0;
            foreach (var item in getHumidity)
            {
                if (counter < 20)
                {
                    var hum = item.Humidity;
                    averangeValue += hum; 
                }
                if(counter == 20)
                {
                    double humValue = averangeValue / 20;
                    DateTime date;
                    DateTime.TryParse(item.DateTime, out date);
                    var dataPoint = new DataPoint(date, humValue);
                    humidity.Add(dataPoint);
                    counter = 0;
                    averangeValue = 0;
                }
                counter++;
            }
            averangeValue = 0;
            counter = 0;
            foreach (var item in getPressure)
            {
                if(counter < 20)
                {
                    var press = item.Pressure;
                    averangeValue += press;
                }
                if (counter == 20)
                {
                    double pressValue = item.Pressure;
                    DateTime date;
                    DateTime.TryParse(item.DateTime, out date);
                    var dataPoint = new DataPoint(date, pressValue);
                    pressure.Add(dataPoint);
                    counter = 0;
                    averangeValue = 0;
                }
                counter++;
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

        public ActionResult Charts7days()
        {
            List<Weather> getTemp7days = new List<Weather>();
            List<Weather> getHumidity7days = new List<Weather>();
            List<Weather> getPressure7days = new List<Weather>();
            getTemp7days = _db.GetTemperatureLast7Days();
            getHumidity7days = _db.GetHumidityLast7Days();
            getPressure7days = _db.GetPressureLast7Days();

            List<DataPoint> temperature = new List<DataPoint>();
            List<DataPoint> humidity = new List<DataPoint>();
            List<DataPoint> pressure = new List<DataPoint>();

            int counter = 0;
            double averangeValue = 0;
            int SizeOfMeasurement = 720;

            foreach (var item in getTemp7days)
            {
                if (counter < SizeOfMeasurement)
                {
                    var temp = item.Temperature;
                    averangeValue += temp;
                }
                if (counter == SizeOfMeasurement)
                {
                    double tempValue = averangeValue / SizeOfMeasurement;
                    DateTime date;
                    DateTime.TryParse(item.DateTime, out date);
                    var dataPoint = new DataPoint(date, tempValue);
                    temperature.Add(dataPoint);
                    counter = 0;
                    averangeValue = 0;
                }
                counter++;
            }
            averangeValue = 0;
            counter = 0;
            foreach (var item in getHumidity7days)
            {
                if (counter < SizeOfMeasurement)
                {
                    var hum = item.Humidity;
                    averangeValue += hum;
                }
                if (counter == SizeOfMeasurement)
                {
                    double humValue = averangeValue / SizeOfMeasurement;
                    DateTime date;
                    DateTime.TryParse(item.DateTime, out date);
                    var dataPoint = new DataPoint(date, humValue);
                    humidity.Add(dataPoint);
                    counter = 0;
                    averangeValue = 0;
                }
                counter++;
            }
            averangeValue = 0;
            counter = 0;
            foreach (var item in getPressure7days)
            {
                if (counter < SizeOfMeasurement)
                {
                    var press = item.Pressure;
                    averangeValue += press;
                }
                if (counter == SizeOfMeasurement)
                {
                    double pressValue = item.Pressure;
                    DateTime date;
                    DateTime.TryParse(item.DateTime, out date);
                    var dataPoint = new DataPoint(date, pressValue);
                    pressure.Add(dataPoint);
                    counter = 0;
                    averangeValue = 0;
                }
                counter++;
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