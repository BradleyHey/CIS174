using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FirstResponsiveWebAppHey.Models;
using FirstResponsiveWebAppHey.Models.Olympics;
using System.Linq;

namespace FirstResponsiveWebAppHey.Controllers
{
    public class HomeController : Controller
    {
        private OlympicsContext context;

        public HomeController(OlympicsContext ctx)
        {
            context = ctx;
        }

        public ViewResult Index(OlympicsViewModel model)
        {
            // Store active filters in session
            var session = new OlympicsSession(HttpContext.Session);
            session.SetActiveGame(model.ActiveGame);
            session.SetActiveCat(model.ActiveCat);

            // Populate view model with filters and data from database
            model.Games = context.Games.ToList();
            model.Categories = context.Categories.ToList();

            // Filter countries based on selection
            IQueryable<Country> query = context.Countries
                .Include(c => c.Game)
                .Include(c => c.Category)
                .OrderBy(c => c.Name);

            if (model.ActiveGame != "all")
            {
                query = query.Where(c => c.GameID.ToLower() == model.ActiveGame.ToLower());
            }

            if (model.ActiveCat != "all")
            {
                query = query.Where(c => c.CategoryID.ToLower() == model.ActiveCat.ToLower());
            }

            model.Countries = query.ToList();

            return View(model);
        }

        public IActionResult Details(string id)
        {
            var session = new OlympicsSession(HttpContext.Session);
            var model = new OlympicsViewModel
            {
                Country = context.Countries
                    .Include(c => c.Game)
                    .Include(c => c.Category)
                    .FirstOrDefault(c => c.CountryID == id) ?? new Country(),
                ActiveGame = session.GetActiveGame(),
                ActiveCat = session.GetActiveCat()
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult AgeCalculator()
        {
            return View(new FirstResponsiveWebAppModel());
        }

        [HttpPost]
        public IActionResult AgeCalculator(FirstResponsiveWebAppModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ViewBag.Age = model.AgeThisYear();
            return View(model);
        }
    }
}
