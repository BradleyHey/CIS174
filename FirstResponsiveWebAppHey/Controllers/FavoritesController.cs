using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FirstResponsiveWebAppHey.Models.Olympics;
using System.Linq;

namespace FirstResponsiveWebAppHey.Controllers
{
    public class FavoritesController : Controller
    {
        private OlympicsContext context;
        public FavoritesController(OlympicsContext ctx) => context = ctx;

        [HttpGet]
        public ViewResult Index()
        {
            var session = new OlympicsSession(HttpContext.Session);
            var model = new OlympicsViewModel
            {
                ActiveGame = session.GetActiveGame(),
                ActiveCat = session.GetActiveCat(),
                Countries = session.GetMyCountries()
            };

            return View(model);
        }

        [HttpPost]
        public RedirectToActionResult Add(Country country)
        {
            country = context.Countries
                 .Include(c => c.Game)
                 .Include(c => c.Category)
                 .Where(c => c.CountryID == country.CountryID)
                 .FirstOrDefault() ?? new Country();

            var session = new OlympicsSession(HttpContext.Session);
            var countries = session.GetMyCountries();
            countries.Add(country);
            session.SetMyCountries(countries);

            TempData["message"] = $"{country.Name} added to your favorites";

            return RedirectToAction("Index", "Home", 
                new
                {
                    ActiveGame = session.GetActiveGame(),
                    ActiveCat = session.GetActiveCat()
                });
        }

        [HttpPost]
        public RedirectToActionResult Delete()
        {
            var session = new OlympicsSession(HttpContext.Session);
            session.RemoveMyCountries();

            TempData["message"] = "Favorite countries cleared";

            return RedirectToAction("Index", "Home",
                new {
                    ActiveGame = session.GetActiveGame(),
                    ActiveCat = session.GetActiveCat()
                });
        }
    }
}