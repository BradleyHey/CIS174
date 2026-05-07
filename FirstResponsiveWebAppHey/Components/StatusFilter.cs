using Microsoft.AspNetCore.Mvc;
using FirstResponsiveWebAppHey.Models.Ticketing;
using FirstResponsiveWebAppHey.Models.DataLayer;

namespace FirstResponsiveWebAppHey.Components
{
    public class StatusFilter : ViewComponent
    {
        private IRepository<Status> statuses { get; set; }
        public StatusFilter(IRepository<Status> rep) => statuses = rep;

        public IViewComponentResult Invoke(string activeStatus)
        {
            ViewBag.ActiveStatus = activeStatus;
            var statusList = statuses.List(new QueryOptions<Status> {
                OrderBy = s => s.Name
            });
            return View(statusList);
        }
    }
}
