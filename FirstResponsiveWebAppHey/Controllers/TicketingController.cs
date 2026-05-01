using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FirstResponsiveWebAppHey.Models.Ticketing;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FirstResponsiveWebAppHey.Controllers
{
    public class TicketingController : Controller
    {
        private TicketingContext context;
        public TicketingController(TicketingContext ctx) => context = ctx;

        public ViewResult Index(string id)
        {
            var filters = new TicketFilters(id);
            ViewBag.Filters = filters;
            ViewBag.Statuses = context.Statuses.ToList();

            IQueryable<Ticket> query = context.Tickets
                .Include(t => t.Status);

            if (filters.HasStatus)
            {
                query = query.Where(t => t.StatusId == filters.StatusId);
            }

            var tickets = query.OrderBy(t => t.SprintNumber).ToList();

            return View(tickets);
        }

        [HttpGet]
        public ViewResult Add()
        {
            ViewBag.Statuses = context.Statuses.ToList();
            var ticket = new Ticket { StatusId = "todo" };
            return View(ticket);
        }

        [HttpPost]
        public IActionResult Add(Ticket ticket)
        {
            string key = nameof(ticket.PointValue);
            var value = ModelState.GetValidationState(key);
            if (value == ModelValidationState.Valid)
            {
                if (ticket.PointValue < 0)
                {
                    ModelState.AddModelError(key, "Point value must be greater than or equal to zero");
                }
            }
            
            
            if (ModelState.IsValid)
            {
                context.Tickets.Add(ticket);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Statuses = context.Statuses.ToList();
                return View(ticket);
            }
        }

        [HttpPost]
        public IActionResult Filter(string[] filter)
        {
            string id = string.Join('-', filter);
            return RedirectToAction("Index", new { ID = id });
        }

        [HttpPost]
        public IActionResult MarkDone([FromRoute] string id, Ticket selected)
        {
            selected = context.Tickets.Find(selected.Id)!;
            if (selected != null)
            {
                selected.StatusId = "done";
                context.SaveChanges();
            }

            return RedirectToAction("Index", new { ID = id });
        }

        [HttpPost]
        public IActionResult DeleteDone(string id)
        {
            var toDelete = context.Tickets
                .Where(t => t.StatusId == "done").ToList();

            foreach (var ticket in toDelete)
            {
                context.Tickets.Remove(ticket);
            }
            context.SaveChanges();

            return RedirectToAction("Index", new { ID = id });
        }
    }
}
