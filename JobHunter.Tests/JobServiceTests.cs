using JobHunter.Models;
using JobHunter.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace JobHunter.Tests
{
    public class JobServiceTests
    {
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var context = new ApplicationDbContext(options);
            context.Database.EnsureCreated(); // This will trigger OnModelCreating and seed data
            return context;
        }

        [Fact]
        public async Task GetAllAsync_ReturnsOnlyUserJobs()
        {
            // Arrange
            var context = GetDbContext();
            var service = new JobService(context);
            var userId1 = "user1";
            var userId2 = "user2";

            context.JobApplications.AddRange(
                new JobApplication { UserId = userId1, CompanyName = "C1", JobTitle = "T1", StatusId = "applied" },
                new JobApplication { UserId = userId1, CompanyName = "C2", JobTitle = "T2", StatusId = "applied" },
                new JobApplication { UserId = userId2, CompanyName = "C3", JobTitle = "T3", StatusId = "applied" }
            );
            await context.SaveChangesAsync();

            // Act
            var result = await service.GetAllAsync(userId1);

            // Assert
            Assert.Equal(2, result.Count());
            Assert.All(result, j => Assert.Equal(userId1, j.UserId));
        }

        [Fact]
        public async Task AddAsync_SavesJob()
        {
            // Arrange
            var context = GetDbContext();
            var service = new JobService(context);
            var job = new JobApplication { UserId = "u1", CompanyName = "Test", JobTitle = "Dev", StatusId = "applied" };

            // Act
            await service.AddAsync(job);

            // Assert
            Assert.Equal(1, await context.JobApplications.CountAsync());
            Assert.Equal("Test", (await context.JobApplications.FirstAsync()).CompanyName);
        }

        [Fact]
        public async Task DeleteAsync_RemovesJob()
        {
            // Arrange
            var context = GetDbContext();
            var service = new JobService(context);
            var job = new JobApplication { Id = 1, UserId = "u1", CompanyName = "Test", JobTitle = "Dev", StatusId = "applied" };
            context.JobApplications.Add(job);
            await context.SaveChangesAsync();

            // Act
            await service.DeleteAsync(1, "u1");

            // Assert
            Assert.Equal(0, await context.JobApplications.CountAsync());
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNullForIncorrectUser()
        {
            // Arrange
            var context = GetDbContext();
            var service = new JobService(context);
            var job = new JobApplication { Id = 1, UserId = "u1", CompanyName = "Test", JobTitle = "Dev", StatusId = "applied" };
            context.JobApplications.Add(job);
            await context.SaveChangesAsync();

            // Act
            var result = await service.GetByIdAsync(1, "other-user");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateAsync_ModifiesExistingJob()
        {
            // Arrange
            var context = GetDbContext();
            var service = new JobService(context);
            var job = new JobApplication { Id = 1, UserId = "u1", CompanyName = "Old Name", JobTitle = "Dev", StatusId = "applied" };
            context.JobApplications.Add(job);
            await context.SaveChangesAsync();

            // Detach to simulate a fresh update
            context.Entry(job).State = EntityState.Detached;

            // Act
            job.CompanyName = "New Name";
            await service.UpdateAsync(job);

            // Assert
            var updatedJob = await context.JobApplications.FindAsync(1);
            Assert.Equal("New Name", updatedJob?.CompanyName);
        }

        [Fact]
        public async Task GetStatusesAsync_ReturnsSeededStatuses()
        {
            // Arrange
            var context = GetDbContext(); // GetDbContext calls EnsureCreated which seeds statuses
            var service = new JobService(context);

            // Act
            var result = await service.GetStatusesAsync();

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, s => s.Name == "Applied");
        }
    }
}
