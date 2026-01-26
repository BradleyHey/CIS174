using FirstResponsiveWebAppHey.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstResponsiveWebAppHey.Controllers;

public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View(new FirstResponsiveWebAppModel());
    }

    [HttpPost]
    public IActionResult Index(FirstResponsiveWebAppModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        ViewBag.Age = model.AgeThisYear();
        return View(model);
    }
    
}