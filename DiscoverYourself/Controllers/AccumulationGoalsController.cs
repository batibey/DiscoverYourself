using DiscoverYourself.Data;
using Microsoft.AspNetCore.Mvc;
using DiscoverYourself.Models.Entities;
using DiscoverYourself.Models.RequestModels;

namespace DiscoverYourself.Controllers;

public class AccumulationGoalsController : Controller
{
    private readonly ILogger<AccumulationGoalsController> _logger;
    private readonly DiscoverYourselfDbContext _context;

    public AccumulationGoalsController(ILogger<AccumulationGoalsController> logger, DiscoverYourselfDbContext context)
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
    public IActionResult Save(AccumulationGoalInputModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
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
            
            // Veritabanına kaydet
            _context.InvestmentGoals.Add(investmentGoal);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Birikim hedefleri başarıyla kaydedildi!";
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