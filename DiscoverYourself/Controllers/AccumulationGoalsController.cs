using DiscoverYourself.Data;
using Microsoft.AspNetCore.Mvc;
using DiscoverYourself.Models.Entities;
using DiscoverYourself.Models.RequestModels;
using DiscoverYourself.Services;

namespace DiscoverYourself.Controllers;
[Authorize]
public class AccumulationGoalsController : Controller
{
    private readonly ILogger<AccumulationGoalsController> _logger;
    private readonly DiscoverYourselfDbContext _context;
    private readonly IMailService _mailService;

    public AccumulationGoalsController(ILogger<AccumulationGoalsController> logger, DiscoverYourselfDbContext context, IMailService mailService)
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
    public IActionResult Save(AccumulationGoalInputModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            var userEmail = Convert.ToInt32(HttpContext.Session.GetString("UserEmail"));
            var investmentGoal = new InvestmentGoal
            {
                ActualSilver = model.ActualSilver,
                ActualGold = model.ActualGold,
                ActualCurrency = model.ActualCurrency,
                TargetCurrency = model.TargetCurrency,
                TargetGold = model.TargetGold,
                TargetSilver = model.TargetSilver,
                Date = model.Date,
                UserId = userId
            };
            
            _context.InvestmentGoals.Add(investmentGoal);
            _context.SaveChanges();
            _mailService.SendEmailAsync(userEmail.ToString(), "Accumulation Goal Saved", "Accumulation Saved Successfully");
            
            return RedirectToAction("Index", new { id = userId });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving accumulation goal");
            return RedirectToAction("Index");
        }
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        try
        {
            var investmentGoal = _context.InvestmentGoals.FirstOrDefault(goal => goal.Id == id);
        
            if (investmentGoal == null)
            {
                return NotFound("The accumulation goal could not be found.");
            }
            
            _context.InvestmentGoals.Remove(investmentGoal);
            _context.SaveChanges();
            
            return RedirectToAction("Index", new { id = investmentGoal.UserId });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting accumulation goal");
            return RedirectToAction("Index");
        }
    }


}