using Microsoft.AspNetCore.Mvc;
using DiscoverYourself.Models.ViewModels;

namespace DiscoverYourself.Controllers;

public class LoginController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            if (model.Username == "admin" && model.Password == "password")
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Username or password is incorrect.");
        }

        return View(model);
    }
}