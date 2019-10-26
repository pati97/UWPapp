using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using PagedList;

namespace WebApplication1.Controllers
{
    public class WeatherController : Controller
    {
        MongoDbContext _dbContext = new MongoDbContext();



        // GET: Weather
        public async Task<ActionResult> Index(int? page)
        {
            int pageSize = 20;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<Weather> PagedListWeather = null;
            IEnumerable<Weather> weathers = null;
            using (IAsyncCursor<Weather> cursor = await _dbContext.GetCollection.FindAsync(new BsonDocument()))
            {
                while (await cursor.MoveNextAsync())
                {
                    weathers = cursor.Current;
                }
            }
            PagedListWeather = weathers.ToPagedList(pageIndex, pageSize);
            return View(PagedListWeather);
        }

        public ActionResult Refresh()
        {
            return RedirectToAction("Index", "Weather");
        }

        public ActionResult List()
        {
            var allData = _dbContext.SelectAll();
            return View(allData);
        }

        public ActionResult FindMessage(int id)
        {
            var weather = _dbContext.FindMessage(id);

            if (weather == null)
            {
                return HttpNotFound();
            }

            return View(weather);

        }
    }

}