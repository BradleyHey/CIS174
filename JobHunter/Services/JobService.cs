using JobHunter.Models;
using Microsoft.EntityFrameworkCore;

namespace JobHunter.Services
{
    public interface IJobService
    {
        Task<IEnumerable<JobApplication>> GetAllAsync(string userId);
        Task<JobApplication?> GetByIdAsync(int id, string userId);
        Task AddAsync(JobApplication jobApplication);
        Task UpdateAsync(JobApplication jobApplication);
        Task DeleteAsync(int id, string userId);
        Task<IEnumerable<Status>> GetStatusesAsync();
    }

    public class JobService : IJobService
    {
        private readonly ApplicationDbContext _context;

        public JobService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<JobApplication>> GetAllAsync(string userId)
        {
            return await _context.JobApplications
                .Include(j => j.Status)
                .Where(j => j.UserId == userId)
                .OrderByDescending(j => j.ApplicationDate)
                .ToListAsync();
        }

        public async Task<JobApplication?> GetByIdAsync(int id, string userId)
        {
            return await _context.JobApplications
                .Include(j => j.Status)
                .FirstOrDefaultAsync(j => j.Id == id && j.UserId == userId);
        }

        public async Task AddAsync(JobApplication jobApplication)
        {
            _context.JobApplications.Add(jobApplication);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(JobApplication jobApplication)
        {
            _context.Entry(jobApplication).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id, string userId)
        {
            var job = await GetByIdAsync(id, userId);
            if (job != null)
            {
                _context.JobApplications.Remove(job);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Status>> GetStatusesAsync()
        {
            return await _context.Statuses.OrderBy(s => s.Name).ToListAsync();
        }
    }
}
