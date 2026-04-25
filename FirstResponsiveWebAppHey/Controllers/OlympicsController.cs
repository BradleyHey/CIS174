using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FirstResponsiveWebAppHey.Models.Olympics;
using System.Linq;

namespace FirstResponsiveWebAppHey.Controllers
{
    public class OlympicsController : Controller
    {
        private OlympicsContext context;

        public OlympicsController(OlympicsContext ctx)
        {
            context = ctx;
        }

        public IActionResult Index(string activeGame = "all", string activeCat = "all")
        {
            // Store active filters in session
            var session = new OlympicsSession(HttpContext.Session);
            session.SetActiveGame(activeGame);
            session.SetActiveCat(activeCat);

            // Populate view model with filters and data from database
            var model = new OlympicsViewModel
            {
                ActiveGame = activeGame,
                ActiveCat = activeCat,
                Games = context.Games.ToList(),
                Categories = context.Categories.ToList()
            };

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
            // Get current filters from session
            var session = new OlympicsSession(HttpContext.Session);
            
            // Populate model with single country data and active filters
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
    }
}