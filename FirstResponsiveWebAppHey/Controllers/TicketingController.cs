using Microsoft.AspNetCore.Mvc;
using FirstResponsiveWebAppHey.Models.Ticketing;
using FirstResponsiveWebAppHey.Models.DataLayer;

namespace FirstResponsiveWebAppHey.Controllers
{
    public class TicketingController : Controller
    {
        private IRepository<Ticket> tickets { get; set; }
        private IRepository<Status> statuses { get; set; }

        public TicketingController(IRepository<Ticket> ticketRepo, IRepository<Status> statusRepo)
        {
            tickets = ticketRepo;
            statuses = statusRepo;
        }

        public ViewResult Index(string id)
        {
            var filters = new TicketFilters(id);
            ViewBag.Filters = filters;
            ViewBag.Statuses = statuses.List(new QueryOptions<Status> { 
                OrderBy = s => s.Name 
            });

            var options = new QueryOptions<Ticket> {
                Includes = "Status",
                OrderBy = t => t.SprintNumber
            };

            if (filters.HasStatus)
            {
                options.Where = t => t.StatusId == filters.StatusId;
            }

            var ticketList = tickets.List(options);

            return View(ticketList);
        }

        [HttpGet]
        public ViewResult Add()
        {
            ViewBag.Statuses = statuses.List(new QueryOptions<Status> { 
                OrderBy = s => s.Name 
            });
            var ticket = new Ticket { StatusId = "todo" };
            return View(ticket);
        }

        [HttpPost]
        public IActionResult Add(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                tickets.Insert(ticket);
                tickets.Save();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Statuses = statuses.List(new QueryOptions<Status> { 
                    OrderBy = s => s.Name 
                });
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
            selected = tickets.Get(selected.Id)!;
            if (selected != null)
            {
                selected.StatusId = "done";
                tickets.Update(selected);
                tickets.Save();
            }

            return RedirectToAction("Index", new { ID = id });
        }

        [HttpPost]
        public IActionResult DeleteDone(string id)
        {
            var options = new QueryOptions<Ticket> {
                Where = t => t.StatusId == "done"
            };
            var toDelete = tickets.List(options);

            foreach (var ticket in toDelete)
            {
                tickets.Delete(ticket);
            }
            tickets.Save();

            return RedirectToAction("Index", new { ID = id });
        }
    }
}
