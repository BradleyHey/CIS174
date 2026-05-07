using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Moq;
using JobHunter.Controllers;
using JobHunter.Models;
using JobHunter.Services;
using System.Security.Claims;

namespace JobHunter.Tests
{
    public class JobApplicationsControllerTests
    {
        private Mock<IJobService> GetMockService()
        {
            var mock = new Mock<IJobService>();
            mock.Setup(m => m.GetAllAsync(It.IsAny<string>()))
                .ReturnsAsync(new List<JobApplication>());
            mock.Setup(m => m.GetStatusesAsync())
                .ReturnsAsync(new List<Status>());
            return mock;
        }

        private Mock<UserManager<User>> GetMockUserManager()
        {
            var store = new Mock<IUserStore<User>>();
            var mock = new Mock<UserManager<User>>(store.Object, null!, null!, null!, null!, null!, null!, null!, null!);
            mock.Setup(m => m.GetUserId(It.IsAny<ClaimsPrincipal>()))
                .Returns("test-user-id");
            return mock;
        }

        [Fact]
        public async Task Index_ReturnsViewResultWithModel()
        {
            // arrange
            var mockService = GetMockService();
            var mockUserMgr = GetMockUserManager();
            var controller = new JobApplicationsController(mockService.Object, mockUserMgr.Object);
            
            // Mock HttpContext for Session
            var mockSession = new Mock<ISession>();
            var context = new DefaultHttpContext();
            context.Session = mockSession.Object;
            controller.ControllerContext = new ControllerContext { HttpContext = context };

            // act
            var result = await controller.Index("all");

            // assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<IEnumerable<JobApplication>>(viewResult.ViewData.Model);
        }

        [Fact]
        public async Task Create_POST_ReturnsRedirectIfModelIsValid()
        {
            // arrange
            var mockService = GetMockService();
            var mockUserMgr = GetMockUserManager();
            var controller = new JobApplicationsController(mockService.Object, mockUserMgr.Object);
            var job = new JobApplication { CompanyName = "Test", JobTitle = "Dev" };

            // act
            var result = await controller.Create(job);

            // assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
        }

        [Fact]
        public async Task Create_POST_ReturnsViewIfModelIsInvalid()
        {
            // arrange
            var mockService = GetMockService();
            var mockUserMgr = GetMockUserManager();
            var controller = new JobApplicationsController(mockService.Object, mockUserMgr.Object);
            controller.ModelState.AddModelError("CompanyName", "Required");
            var job = new JobApplication();

            // act
            var result = await controller.Create(job);

            // assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(job, viewResult.Model);
        }
    }
}
