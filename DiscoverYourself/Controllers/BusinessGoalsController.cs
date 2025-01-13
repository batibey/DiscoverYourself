using DiscoverYourself.Data;
using Microsoft.AspNetCore.Mvc;
using DiscoverYourself.Models.Entities;
using DiscoverYourself.Models.RequestModels;
using DiscoverYourself.Services;

namespace DiscoverYourself.Controllers;
[Authorize]
public class BusinessGoalsController : Controller
{
    private readonly ILogger<BusinessGoalsController> _logger;
    private readonly DiscoverYourselfDbContext _context;
    private readonly IMailService _mailService;

    public BusinessGoalsController(ILogger<BusinessGoalsController> logger, DiscoverYourselfDbContext context, IMailService mailService)
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
    public IActionResult Save(BusinessGoalRequestModel request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            var userEmail = Convert.ToInt32(HttpContext.Session.GetString("UserEmail"));
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
            _mailService.SendEmailAsync(userEmail.ToString(), "Business Goal Saved", "Business Saved Successfully");
            return RedirectToAction("Index", new { id = userId });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Hedef kaydı sırasında bir hata oluştu.");
            return RedirectToAction("Index");
        }
    }
}