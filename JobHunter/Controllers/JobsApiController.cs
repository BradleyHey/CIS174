using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using JobHunter.Models;
using JobHunter.Services;

namespace JobHunter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class JobsApiController : ControllerBase
    {
        private readonly IJobService _jobService;
        private readonly UserManager<User> _userManager;

        public JobsApiController(IJobService jobService, UserManager<User> userManager)
        {
            _jobService = jobService;
            _userManager = userManager;
        }

        // GET: api/jobsapi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobApplication>>> GetJobs()
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var jobs = await _jobService.GetAllAsync(userId);
            return Ok(jobs);
        }

        // GET: api/jobsapi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JobApplication>> GetJob(int id)
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var job = await _jobService.GetByIdAsync(id, userId);

            if (job == null)
            {
                return NotFound();
            }

            return Ok(job);
        }
    }
}
