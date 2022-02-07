using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ristorante.Core.BL;
using Ristorante.WCF.Helper;
using Ristorante.WCF.Models;
using System.Security.Claims;

namespace Ristorante.WCF.Controllers
{
    public class UtenteController : Controller
    {
        private readonly IBusinessLayer BL;

        public UtenteController(IBusinessLayer bl)
        {
            BL = bl;
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string returnUrl = "/")
        {
            return View(new UtenteViewModel { ReturnUrl = returnUrl });
        }
        [HttpPost]
        public async Task<IActionResult> LoginAsync(UtenteViewModel utenteVM)
        {
            if (utenteVM == null)
            {
                return View();
            }

            var utente = BL.GetAccount(utenteVM.Username);
            if (utente != null && ModelState.IsValid)
            {
                if (utente.Password == utenteVM.Password)
                {
                    var claim = new List<Claim>{

                        new Claim (ClaimTypes.Name, utente.Username),
                        new Claim (ClaimTypes.Role, utente.Role.ToString())
                    };

                    var properties = new AuthenticationProperties
                    {
                        RedirectUri = utenteVM.ReturnUrl,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddHours(2) 
                    };

                    var claimIdentity = new ClaimsIdentity(claim, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimIdentity),
                        properties
                        );
                    return Redirect("/");
                }
                else
                {
                    ModelState.AddModelError(nameof(utenteVM.Password), "Password errata");
                    return View(utenteVM);
                }
            }
            else
            {
                return View(utenteVM);
            }

            return View();
        }
        [AllowAnonymous]
        public IActionResult Forbidden()
        {
            return View();
        }

        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync();
            return Redirect("Login");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Registrazione()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Registrazione(UtenteViewModel uVM)
        {
            if (ModelState.IsValid)
            {
                var u = uVM.ToUtente();
                BL.AddUtente(u);
                return RedirectToAction(nameof(Index));
            }
            return View(uVM);
        }
    }
}
