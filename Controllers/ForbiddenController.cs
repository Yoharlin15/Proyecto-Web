using Microsoft.AspNetCore.Mvc;

namespace Proyecto_Web.Controllers
{
    public class ForbiddenController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
