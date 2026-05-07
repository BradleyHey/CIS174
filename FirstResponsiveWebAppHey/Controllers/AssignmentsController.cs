using Microsoft.AspNetCore.Mvc;

namespace FirstResponsiveWebAppHey.Controllers
{
    public class AssignmentsController : Controller
    {
        // 1. Default routing: Matches the default pattern {controller}/{action}/{id?}
        // URL: /Assignments/DefaultRoute
        public IActionResult DefaultRoute() => View();

        // 2. Custom routing rule: Matches the pattern defined in Program.cs
        // URL: /assignments/custom-rule
        public IActionResult CustomRule() => View();

        // 3. Custom routing attribute: Matches the exact path specified in the Route attribute
        // URL: /assignments/attribute-route
        [Route("assignments/attribute-route")]
        public IActionResult AttributeRoute() => View();
    }
}