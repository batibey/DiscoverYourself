using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DiscoverYourself.Models;
using DiscoverYourself.Models.Entities;
using DiscoverYourself.Services;

namespace DiscoverYourself.Controllers;
[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IMailService _mailService;

    public HomeController(ILogger<HomeController> logger, IMailService mailService)
    {
        _mailService = mailService;
        _logger = logger;
    }
    public async Task<IActionResult> Index(User user)
    {
        return View(user);
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}