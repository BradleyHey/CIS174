using Microsoft.AspNetCore.Mvc;
using FirstResponsiveWebAppHey.Models;

namespace FirstResponsiveWebAppHey.Controllers
{
    public class Assignment6_1Controller : Controller
    {
        // This page accepts a parameter between 1 and 10
        [Route("Assignment6_1/{accessLevel:int:range(1,10)}")]
        public IActionResult Index(int accessLevel)
        {
            var model = new StudentViewModel
            {
                AccessLevel = accessLevel,
                Students = new List<Student>
                {
                    new Student { FirstName = "John", LastName = "Wayne", Grade = "A" },
                    new Student { FirstName = "Kobe", LastName = "Bryant", Grade = "B" },
                    new Student { FirstName = "Lebron", LastName = "James", Grade = "C" },
                    new Student { FirstName = "Peter", LastName = "Parker", Grade = "A" }
                }
            };
            return View(model);
        }
    }
}
