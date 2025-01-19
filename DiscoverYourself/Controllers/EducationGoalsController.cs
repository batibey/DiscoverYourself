using DiscoverYourself.Data;
using Microsoft.AspNetCore.Mvc;
using DiscoverYourself.Models.Entities;
using DiscoverYourself.Models.RequestModels;
using DiscoverYourself.Services;

namespace DiscoverYourself.Controllers;
[Authorize]
public class EducationGoalsController : Controller
{
    private readonly ILogger<EducationGoalsController> _logger;
    private readonly DiscoverYourselfDbContext _context;
    private readonly IMailService _mailService;

    public EducationGoalsController(ILogger<EducationGoalsController> logger, DiscoverYourselfDbContext context, IMailService mailService)
    {
        _logger = logger;
        _context = context;
        _mailService = mailService;
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
        var userEmail = Convert.ToInt32(HttpContext.Session.GetString("UserEmail"));
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
            IsCompleted = model.IsCompleted,
            UserId = userId
        };

        _context.EducationGoals.Add(educationGoals);
        _context.SaveChanges();
        _mailService.SendEmailAsync(userEmail.ToString(), "Education Goal Saved", "Education Goal Saved Successfully");

        return RedirectToAction("Index", new { id = userId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        try
        {
            var educationGoal = _context.EducationGoals.FirstOrDefault(goal => goal.Id == id);

            if (educationGoal == null)
            {
                return NotFound("The education goal could not be found.");
            }

            _context.EducationGoals.Remove(educationGoal);
            _context.SaveChanges();

            return RedirectToAction("Index", new { id = educationGoal.UserId });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting education goal");
            return RedirectToAction("Index");
        }
    }
}