using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Web.Data;
using Proyecto_Web.Models;
using Proyecto_Web.ViewModels.Account;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Proyecto_Web.Constants;

namespace Proyecto_Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly NewsContext _newsContext;

        public AccountController(NewsContext newsContext) 
        {
            this._newsContext = newsContext;
        }
        [AllowAnonymous]
        public IActionResult Login()
        {

            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(AccountLoginViewModel viewModel, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var user = _newsContext.Users.FirstOrDefault(a => a.Email == viewModel.UserName);

                if (user.Password == viewModel.Password)
                {
                    var claims = new List<Claim>{new Claim(ClaimTypes.Name, user.Email),
                        new Claim("FullName", $"{user.FirstName} {user.LastName}")};

                    if (user.RoleId == 1)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, RoleNames.Administrator));
                    }
                    else if (user.RoleId == 2)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, RoleNames.Editor));
                    }

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties();

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    return Redirect(returnUrl ?? "/");
                }

            }
            return View(viewModel);
        }


        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            // Clear the existing external cookie
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return Redirect(returnUrl ?? "/");
        }

        [AllowAnonymous]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult SignUp(AccountSignUpViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new User();
                user.FirstName = viewModel.FirtName;
                user.LastName = viewModel.LastName;
                user.Email = viewModel.Email;
                user.Password = viewModel.Password;
                user.RoleId = GetRoleId(viewModel.Role);
                _newsContext.Users.Add(user);
                _newsContext.SaveChanges();
            }
            return View(viewModel);
        }
        private int? GetRoleId(string roleName)
        {
            // Busca el RoleId basado en el nombre del rol
            return _newsContext.Roles.FirstOrDefault(r => r.RoleName == roleName)?.RoleD;
        }
    }
}
