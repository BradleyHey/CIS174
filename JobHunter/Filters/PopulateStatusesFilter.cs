using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using JobHunter.Services;

namespace JobHunter.ActionFilters
{
    public class PopulateStatusesFilter : ActionFilterAttribute
    {
        private readonly IJobService _jobService;

        public PopulateStatusesFilter(IJobService jobService)
        {
            _jobService = jobService;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.Controller is Controller controller)
            {
                controller.ViewBag.Statuses = await _jobService.GetStatusesAsync();
            }
            await next();
        }
    }
}
