using Microsoft.AspNetCore.Mvc;
using Proyecto_Web.Constants;

namespace Proyecto_Web.Controllers
{
    public class LanguageController : Controller
    {
        public IActionResult SetLanguage(string Lang)
        {
            Response.Cookies.Append(CookieNames.Lang, Lang);

            return Redirect("/");
        }
    }
}
