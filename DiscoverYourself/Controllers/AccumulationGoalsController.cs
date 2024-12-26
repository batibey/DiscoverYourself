using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DiscoverYourself.Models;

namespace DiscoverYourself.Controllers;

public class AccumulationGoalsController : Controller
{
    private readonly ILogger<AccumulationGoalsController> _logger;

    public AccumulationGoalsController(ILogger<AccumulationGoalsController> logger)
    {
        _logger = logger;
    }
    [Authorize]
    public IActionResult Index()
    {
        return View();
    }
}