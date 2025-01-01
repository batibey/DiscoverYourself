using DiscoverYourself.Data;
using Microsoft.AspNetCore.Mvc;
using DiscoverYourself.Models.Entities;
using DiscoverYourself.Models.RequestModels;

namespace DiscoverYourself.Controllers;

public class EducationGoalsController : Controller
{
    private readonly ILogger<EducationGoalsController> _logger;
    private readonly DiscoverYourselfDbContext _context;

    public EducationGoalsController(ILogger<EducationGoalsController> logger, DiscoverYourselfDbContext context)
    {
        _logger = logger;
        _context = context;
    }
    [Authorize]
    public IActionResult Index(int id)
    {
        ViewBag.UserId = id;
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Save(EducationGoalRequestModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
        var educationGoals = new EducationGoal()
        {
            Title = model.Title,
            Description = model.Description,
            StartDate = model.StartDate,
            EndDate = model.EndDate,
            Difficulty = model.Difficulty,
            Topics = model.Topics,
            TargetAudience = model.TargetAudience,
            Milestones = model.Milestones,
            SuccessCriteria = model.SuccessCriteria,
            IsCompleted = model.IsCompleted
        };
        _context.EducationGoals.Add(educationGoals);
        _context.SaveChanges();

        return RedirectToAction("Index", new { id = userId });
    }

}