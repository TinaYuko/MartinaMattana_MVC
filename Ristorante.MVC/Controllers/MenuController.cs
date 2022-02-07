using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ristorante.Core.BL;
using Ristorante.WCF.Helper;
using Ristorante.WCF.Models;

namespace Ristorante.WCF.Controllers
{
    [Authorize]
    public class MenuController : Controller
    {
        private readonly IBusinessLayer BL;

        public MenuController(IBusinessLayer bl)
        {
            BL = bl;
        }

        #region CRUD
        [HttpGet]
        public IActionResult Index()
        {
            var menu = BL.GetAllMenus();

            List<MenuViewModel> menuViewModel = new List<MenuViewModel>();

            foreach (var item in menu)
            {
                menuViewModel.Add(item.ToMenuViewModel());
            }

            return View(menuViewModel);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var menu = BL.GetAllMenus().FirstOrDefault(m => m.Id == id);

            var menuViewModel = menu.ToMenuViewModel();
            var piatti=menuViewModel.Piatti;
            decimal tot=0;
            foreach (var p in piatti)
            {
                tot += p.Prezzo;
            }
            ViewBag.Total = tot;
            return View(menuViewModel);

        }
        [Authorize(Policy = ("Adm"))]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Policy = ("Adm"))]
        public IActionResult Create(MenuViewModel menuViewModel)
        {
            if (ModelState.IsValid)
            {
                var menu = menuViewModel.ToMenu();
                BL.AddMenu(menu);
                return RedirectToAction(nameof(Index));
            }
            return View(menuViewModel);
        }

        [Authorize(Policy = ("Adm"))]
        [HttpGet]
        public IActionResult Update(int id)
        {
            var menu = BL.GetAllMenus().FirstOrDefault(m => m.Id == id);
            var menuViewModel = menu.ToMenuViewModel();
            return View(menuViewModel);
        }

        [Authorize(Policy = ("Adm"))]
        [HttpPost]
        public IActionResult Update(MenuViewModel menuViewModel)
        {
            if (ModelState.IsValid)
            {
                var menu = menuViewModel.ToMenu();
                BL.EditMenu(menu);
                return RedirectToAction(nameof(Index));
            }
            return View(menuViewModel);
        }

        [Authorize(Policy = ("Adm"))]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var menu = BL.GetAllMenus().FirstOrDefault(m => m.Id == id);
            var menuViewModel = menu.ToMenuViewModel();
            return View(menuViewModel);
        }

        [Authorize(Policy = ("Adm"))]
        [HttpPost]
        public IActionResult Delete(MenuViewModel menuViewModel)
        {
            if (ModelState.IsValid)
            {
                var menu = menuViewModel.ToMenu();
                BL.DeleteMenu(menu.Id);
                return RedirectToAction(nameof(Index));

            }
            return View(menuViewModel);
        }

        #endregion
    }
}
