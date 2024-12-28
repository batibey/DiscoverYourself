using DiscoverYourself.Data;
using Microsoft.AspNetCore.Mvc;
using DiscoverYourself.Models.Entities;
using DiscoverYourself.Models.RequestModels;

namespace DiscoverYourself.Controllers;

public class BusinessGoalsController : Controller
{
    private readonly ILogger<BusinessGoalsController> _logger;
    private readonly DiscoverYourselfDbContext _context;

    public BusinessGoalsController(ILogger<BusinessGoalsController> logger, DiscoverYourselfDbContext context)
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
    public IActionResult Save(BusinessGoalRequestModel request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            var businessGoals = new BusinessGoal()
            {
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                TasksCompleted = request.TasksCompleted,
                TaskType = request.TaskType,
                DailyWorkHours = request.DailyWorkHours,
                DailySelfDevelopmentHours = request.DailySelfDevelopmentHours,
                AdditionalInfo = request.AdditionalInfo,
                UserId = userId
            };
            _context.BusinessGoals.Add(businessGoals);
            _context.SaveChanges();
            
            TempData["SuccessMessage"] = "İş hedefleri başarıyla kaydedildi!";
            return RedirectToAction("Index", new { id = userId });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Hedef kaydı sırasında bir hata oluştu.");
            TempData["ErrorMessage"] = "Hedef kaydı sırasında beklenmedik bir hata oluştu. Lütfen daha sonra tekrar deneyin.";
            return RedirectToAction("Index");
        }
    }
}