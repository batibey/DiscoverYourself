using DiscoverYourself.Data;
using Microsoft.AspNetCore.Mvc;
using DiscoverYourself.Models.Entities;
using DiscoverYourself.Models.RequestModels;
using DiscoverYourself.Services;

namespace DiscoverYourself.Controllers;
[Authorize]
public class DevelopmentGoalsController : Controller
{
    private readonly ILogger<DevelopmentGoalsController> _logger;
    private readonly DiscoverYourselfDbContext _context;
    private readonly IMailService _mailService;

    public DevelopmentGoalsController(ILogger<DevelopmentGoalsController> logger, DiscoverYourselfDbContext context, IMailService mailService)
    {
        _logger = logger;
        _context = context;
        _mailService = mailService;
    }
    public IActionResult Index(int id)
    {
        ViewBag.UserId = id;
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(DevelopmentGoalRequestModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            var userEmail = Convert.ToInt32(HttpContext.Session.GetString("UserEmail"));
            var developmentGoal = new DevelopmentGoal()
            {
                Title = model.Title,
                Description = model.Description,
                Category = model.Category,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Progress = model.Progress,
                IsCompleted = model.IsCompleted,
                PriorityLevel = model.PriorityLevel,
                ResourcesNeeded = model.ResourcesNeeded,
                Feedback = model.Feedback,
                UpdatedDate = DateTime.Now,
                UserId = userId
            };
            
            _context.DevelopmentGoals.Add(developmentGoal);
            _context.SaveChanges();
            _mailService.SendEmailAsync(userEmail.ToString(), "Development Goal Saved", "Development Goal Saved Successfully");
            return RedirectToAction("Index", new { id = userId });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Hedef kaydı sırasında bir hata oluştu.");
            return RedirectToAction("Index");
        }
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        try
        {
            var developmentGoal = _context.DevelopmentGoals.FirstOrDefault(goal => goal.Id == id);
        
            if (developmentGoal == null)
            {
                return NotFound("The development goal could not be found.");
            }
            
            _context.DevelopmentGoals.Remove(developmentGoal);
            _context.SaveChanges();
            
            return RedirectToAction("Index", new { id = developmentGoal.UserId });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting development goal");
            return RedirectToAction("Index");
        }
    }

}