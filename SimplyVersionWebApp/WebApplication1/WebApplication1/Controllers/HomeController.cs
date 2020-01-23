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
            List<Weather> getParams24h = new List<Weather>();
            getParams24h = _db.GetParameters24h();
            List<DataPoint> temperature = new List<DataPoint>();
            List<DataPoint> humidity = new List<DataPoint>();
            List<DataPoint> pressure = new List<DataPoint>();

            int counter = 0;
            double averangeValue = 0;
            int SizeOfMeasurement = 20;

            foreach (var item in getParams24h)
            {
                if (counter < SizeOfMeasurement)
                {
                    var temp = item.Temperature;
                    averangeValue += temp;
                    counter++;
                }
                if (counter == SizeOfMeasurement)
                {
                    double tempValue = averangeValue / SizeOfMeasurement;
                    DateTime.TryParse(item.DateTime, out DateTime date);
                    var dataPoint = new DataPoint(date, tempValue);
                    temperature.Add(dataPoint);
                    counter = 0;
                    averangeValue = 0;
                }

            }
            averangeValue = 0;
            counter = 0;
            foreach (var item in getParams24h)
            {
                if (counter < SizeOfMeasurement)
                {
                    var hum = item.Humidity;
                    averangeValue += hum;
                    counter++;
                }
                if (counter == SizeOfMeasurement)
                {
                    double humValue = averangeValue / SizeOfMeasurement;
                    DateTime.TryParse(item.DateTime, out DateTime date);
                    var dataPoint = new DataPoint(date, humValue);
                    humidity.Add(dataPoint);
                    counter = 0;
                    averangeValue = 0;
                }
            }
            averangeValue = 0;
            counter = 0;
            foreach (var item in getParams24h)
            {
                if (counter < SizeOfMeasurement)
                {
                    var press = item.Pressure;
                    averangeValue += press;
                    counter++;
                }
                if (counter == SizeOfMeasurement)
                {
                    double pressValue = averangeValue / SizeOfMeasurement;
                    DateTime.TryParse(item.DateTime, out DateTime date);
                    var dataPoint = new DataPoint(date, pressValue);
                    pressure.Add(dataPoint);
                    counter = 0;
                    averangeValue = 0;
                }
            }

            temperature.Reverse();
            humidity.Reverse();
            pressure.Reverse();

            var jsonSettings = new JsonSerializerSettings();
            jsonSettings.DateFormatString = "dd/MM/yyyy hh:mm:ss";

            ViewBag.Temperature24 = JsonConvert.SerializeObject(temperature, new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
            ViewBag.Humidity24 = JsonConvert.SerializeObject(humidity, new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
            ViewBag.Pressure24 = JsonConvert.SerializeObject(pressure, new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });

            return View();
        }

        public ActionResult Charts7days()
        {
            List<Weather> getParamsLast7days = new List<Weather>();
            getParamsLast7days = _db.GetParametersLast7Days();
    
            List<DataPoint> temperature = new List<DataPoint>();
            List<DataPoint> humidity = new List<DataPoint>();
            List<DataPoint> pressure = new List<DataPoint>();

            int counter = 0;
            double averangeValue = 0;
            int SizeOfMeasurement = 600;

            foreach (var item in getParamsLast7days)
            {
                if (counter < SizeOfMeasurement)
                {
                    var temp = item.Temperature;
                    averangeValue += temp;
                    counter++;
                }
                if (counter == SizeOfMeasurement)
                {
                    double tempValue = averangeValue / SizeOfMeasurement;
                    DateTime.TryParse(item.DateTime, out DateTime date);
                    var dataPoint = new DataPoint(date, tempValue);
                    temperature.Add(dataPoint);
                    counter = 0;
                    averangeValue = 0;
                }
                
            }
            averangeValue = 0;
            counter = 0;
            foreach (var item in getParamsLast7days)
            {
                if (counter < SizeOfMeasurement)
                {
                    var hum = item.Humidity;
                    averangeValue += hum;
                    counter++;
                }
                if (counter == SizeOfMeasurement)
                {
                    double humValue = averangeValue / SizeOfMeasurement;
                    DateTime.TryParse(item.DateTime, out DateTime date);
                    var dataPoint = new DataPoint(date, humValue);
                    humidity.Add(dataPoint);
                    counter = 0;
                    averangeValue = 0;
                }
                
            }
            averangeValue = 0;
            counter = 0;

            foreach (var item in getParamsLast7days)
            {
                if (counter < SizeOfMeasurement)
                {
                    var press = item.Pressure;
                    averangeValue += press;
                    counter++;
                }
                if (counter == SizeOfMeasurement)
                {
                    double pressValue = averangeValue / SizeOfMeasurement;
                    DateTime.TryParse(item.DateTime, out DateTime date);
                    var dataPoint = new DataPoint(date, pressValue);
                    pressure.Add(dataPoint);
                    counter = 0;
                    averangeValue = 0;
                }
                
            }

            temperature.Reverse();
            humidity.Reverse();
            pressure.Reverse();

            var jsonSettings = new JsonSerializerSettings();
            jsonSettings.DateFormatString = "dd/MM/yyyy hh:mm:ss";

            ViewBag.Temperature7 = JsonConvert.SerializeObject(temperature, new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
            ViewBag.Humidity7 = JsonConvert.SerializeObject(humidity, new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
            ViewBag.Pressure7 = JsonConvert.SerializeObject(pressure, new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });

            return View();
        }

        public ActionResult ActualData()
        {
            Weather getParamsLastDate;
            getParamsLastDate = _db.GetLastWeather();

            var temp = getParamsLastDate.Temperature;
            var hum = getParamsLastDate.Humidity;
            var press = getParamsLastDate.Pressure;
            DateTime.TryParse(getParamsLastDate.DateTime, out DateTime date);

            temp = Math.Round(temp, 2);
            hum = (float)Math.Round(hum, 2);
            press = (float)Math.Round(press, 2);

            ViewBag.DataPoint1 = JsonConvert.SerializeObject(temp);
            ViewBag.DataPoint2 = JsonConvert.SerializeObject(hum);
            ViewBag.DataPoint3 = JsonConvert.SerializeObject(press);
            ViewBag.DateTime = JsonConvert.SerializeObject(date, new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd HH:mm:ss"});
            return View();
        }

        public ActionResult MinMax()
        {
            var minTemperature = _db.GetMinTemperatureLast24h();
            var maxTemperature = _db.GetMaxTemperatureLast24h();
            var minHumidity = _db.GetMinHumidityLast24h();
            var maxHumidity = _db.GetMaxHumidityLast24h();
            var minPressure = _db.GetMinPressureLast24h();
            var maxPressure = _db.GetMaxPressureLast24h();

            ViewBag.TempMin = JsonConvert.SerializeObject(Math.Round(minTemperature.Temperature,2));
            ViewBag.TempMinDate = JsonConvert.SerializeObject(minTemperature.DateTime, new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
            ViewBag.TempMax = JsonConvert.SerializeObject(Math.Round(maxTemperature.Temperature,2));
            ViewBag.TempMaxDate = JsonConvert.SerializeObject(maxTemperature.DateTime, new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });

            ViewBag.HumMin = JsonConvert.SerializeObject(Math.Round(minHumidity.Humidity,2));
            ViewBag.HumMinDate = JsonConvert.SerializeObject(minHumidity.DateTime, new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
            ViewBag.HumMax = JsonConvert.SerializeObject(Math.Round(maxHumidity.Humidity,2));
            ViewBag.HumMaxDate = JsonConvert.SerializeObject(maxHumidity.DateTime, new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });

            ViewBag.PressureMin = JsonConvert.SerializeObject(Math.Round(minPressure.Pressure,2));
            ViewBag.PressureMinDate = JsonConvert.SerializeObject(minPressure.DateTime, new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
            ViewBag.PressureMax = JsonConvert.SerializeObject(Math.Round(maxPressure.Pressure,2));
            ViewBag.PressureMaxDate = JsonConvert.SerializeObject(maxPressure.DateTime, new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });

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