using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace DiscoverYourself.Controllers;

public class CultureController : Controller
{
    public IActionResult SetCulture(string culture, string returnUrl)
    {
        if (string.IsNullOrEmpty(returnUrl))
        {
            returnUrl = Url.Action("Index", "Home");
        }

        Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
        );

        return LocalRedirect(returnUrl);
    }
}
