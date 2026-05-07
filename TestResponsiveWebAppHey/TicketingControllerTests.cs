using Microsoft.AspNetCore.Mvc;
using Moq;
using FirstResponsiveWebAppHey.Controllers;
using FirstResponsiveWebAppHey.Models.Ticketing;
using FirstResponsiveWebAppHey.Models.DataLayer;
using Xunit;

namespace TestResponsiveWebAppHey
{
    public class TicketingControllerTests
    {
        [Fact]
        public void Index_ReturnsAViewResult()
        {
            // arrange
            var ticketRep = new Mock<IRepository<Ticket>>();
            var statusRep = new Mock<IRepository<Status>>();
            
            // Set up status list for the filter dropdown
            statusRep.Setup(m => m.List(It.IsAny<QueryOptions<Status>>()))
                .Returns(new List<Status>());
            
            // Set up ticket list for the main view
            ticketRep.Setup(m => m.List(It.IsAny<QueryOptions<Ticket>>()))
                .Returns(new List<Ticket>());

            var controller = new TicketingController(ticketRep.Object, statusRep.Object);

            // act
            var result = controller.Index("all");

            // assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Add_GET_ReturnsAViewResult()
        {
            // arrange
            var ticketRep = new Mock<IRepository<Ticket>>();
            var statusRep = new Mock<IRepository<Status>>();
            
            statusRep.Setup(m => m.List(It.IsAny<QueryOptions<Status>>()))
                .Returns(new List<Status>());

            var controller = new TicketingController(ticketRep.Object, statusRep.Object);

            // act
            var result = controller.Add();

            // assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Add_POST_InvalidModel_ReturnsViewResult()
        {
            // arrange
            var ticketRep = new Mock<IRepository<Ticket>>();
            var statusRep = new Mock<IRepository<Status>>();
            
            statusRep.Setup(m => m.List(It.IsAny<QueryOptions<Status>>()))
                .Returns(new List<Status>());

            var controller = new TicketingController(ticketRep.Object, statusRep.Object);
            controller.ModelState.AddModelError("Name", "Required");

            var ticket = new Ticket();

            // act
            var result = controller.Add(ticket);

            // assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Add_POST_ValidModel_ReturnsRedirectToActionResult()
        {
            // arrange
            var ticketRep = new Mock<IRepository<Ticket>>();
            var statusRep = new Mock<IRepository<Status>>();

            var controller = new TicketingController(ticketRep.Object, statusRep.Object);
            var ticket = new Ticket { Name = "Test", Description = "Test", SprintNumber = 1, PointValue = 1, StatusId = "todo" };

            // act
            var result = controller.Add(ticket);

            // assert
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }
    }
}
