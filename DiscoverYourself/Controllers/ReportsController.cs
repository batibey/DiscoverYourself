using DiscoverYourself.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiscoverYourself.Controllers;

[Authorize]
public class ReportsController : Controller
{
    private readonly DiscoverYourselfDbContext _context;

    public ReportsController(DiscoverYourselfDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> GetAccumulationReportByUserId(int userId)
    {
        var investments = await _context.InvestmentGoals
            .Where(i => i.UserId == userId)
            .ToListAsync();

        if (investments == null || !investments.Any())
        {
            return NotFound();
        }

        return View("AccumulationReport", investments);
    }
}
