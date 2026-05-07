using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using JobHunter.Models;
using JobHunter.Services;
using JobHunter.ActionFilters;

namespace JobHunter.Controllers
{
    [Authorize]
    public class JobApplicationsController : Controller
    {
        private readonly IJobService _jobService;
        private readonly UserManager<User> _userManager;

        public JobApplicationsController(IJobService jobService, UserManager<User> userManager)
        {
            _jobService = jobService;
            _userManager = userManager;
        }

        [Route("Jobs")]
        [Route("Jobs/{id}")]
        public async Task<IActionResult> Index(string id)
        {
            var filters = new Filters(id);
            ViewBag.Filters = filters;
            ViewBag.Statuses = await _jobService.GetStatusesAsync();

            var session = new JobSession(HttpContext.Session);
            ViewBag.Favorites = session.GetFavoriteIds();

            var userId = _userManager.GetUserId(User);
            var jobs = await _jobService.GetAllAsync(userId!);

            if (filters.HasStatus)
            {
                jobs = jobs.Where(j => j.StatusId == filters.StatusId);
            }

            return View(jobs);
        }

        [HttpPost]
        public IActionResult Favorite(int id)
        {
            var session = new JobSession(HttpContext.Session);
            var favs = session.GetFavoriteIds();

            if (favs.Contains(id))
                favs.Remove(id);
            else
                favs.Add(id);

            session.SetFavoriteIds(favs);

            return RedirectToAction("Index", new { id = Request.RouteValues["id"] });
        }

        [HttpPost]
        public IActionResult Filter(string[] filter)
        {
            string id = string.Join('-', filter);
            return RedirectToAction("Index", new { id = id });
        }

        [HttpGet]
        [ServiceFilter(typeof(PopulateStatusesFilter))]
        public IActionResult Create()
        {
            return View(new JobApplication());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(PopulateStatusesFilter))]
        public async Task<IActionResult> Create(JobApplication jobApplication)
        {
            ModelState.Remove("UserId");
            if (ModelState.IsValid)
            {
                jobApplication.UserId = _userManager.GetUserId(User)!;
                await _jobService.AddAsync(jobApplication);
                return RedirectToAction(nameof(Index));
            }
            return View(jobApplication);
        }

        [HttpGet]
        [ServiceFilter(typeof(PopulateStatusesFilter))]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = _userManager.GetUserId(User);
            var job = await _jobService.GetByIdAsync(id, userId!);
            if (job == null) return NotFound();

            return View(job);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(PopulateStatusesFilter))]
        public async Task<IActionResult> Edit(int id, JobApplication jobApplication)
        {
            if (id != jobApplication.Id) return NotFound();

            ModelState.Remove("UserId");
            if (ModelState.IsValid)
            {
                jobApplication.UserId = _userManager.GetUserId(User)!;
                await _jobService.UpdateAsync(jobApplication);
                return RedirectToAction(nameof(Index));
            }
            return View(jobApplication);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = _userManager.GetUserId(User);
            var job = await _jobService.GetByIdAsync(id, userId!);
            if (job == null) return NotFound();

            return View(job);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = _userManager.GetUserId(User);
            await _jobService.DeleteAsync(id, userId!);
            return RedirectToAction(nameof(Index));
        }
    }
}
