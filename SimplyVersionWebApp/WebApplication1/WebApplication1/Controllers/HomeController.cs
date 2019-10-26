using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private MongoDbContext _db = new MongoDbContext();

        public ActionResult Index()
        {       
            List<DataPoint> temperature = new List<DataPoint>();

            List<DataPoint> humidity = new List<DataPoint>();

            List<DataPoint> pressure = new List<DataPoint>();

            temperature = _db.GetTemp();

            return View(temperature);
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