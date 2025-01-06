using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using DiscoverYourself.Data;
using DiscoverYourself.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

public class AccountController : Controller
{
    private readonly DiscoverYourselfDbContext _context;

    public AccountController(DiscoverYourselfDbContext context)
    {
        _context = context;
    }

    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(string email, string password)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
        
        if (user == null || !VerifyPasswordHash(password, user.PasswordHash))
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View();
        }

        // Set session or token
        HttpContext.Session.SetString("UserId", user.Id.ToString());
        Log.Information($"{user.Email} logged in.");
        return RedirectToAction("Index", "Home", user);
    }

    public IActionResult Register() => View();

    [HttpPost]
    public async Task<IActionResult> Register(string username, string email, string password)
    {
        if (_context.Users.Any(u => u.Email == email))
        {
            ModelState.AddModelError(string.Empty, "Email is already taken.");
            return View();
        }

        var user = new User()
        {
            UserName = username,
            Email = email,
            PasswordHash = CreatePasswordHash(password)
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        Log.Information($"{user.Email} registered.");
        return RedirectToAction("Login");
    }

    private static string CreatePasswordHash(string password)
    {
        using var sha256 = SHA256.Create();
        return Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(password)));
    }

    private static bool VerifyPasswordHash(string password, string storedHash)
    {
        var hash = CreatePasswordHash(password);
        return hash == storedHash;
    }
    
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        
        if (HttpContext.Request.Cookies != null)
        {
            foreach (var cookie in HttpContext.Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }
        }
        
        return RedirectToAction("Login", "Account");
    }

}