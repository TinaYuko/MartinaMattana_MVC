using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ristorante.Core.BL;
using Ristorante.WCF.Helper;
using Ristorante.WCF.Models;

namespace Ristorante.WCF.Controllers
{
    [Authorize]
    public class PiattoController : Controller
    {
        private readonly IBusinessLayer BL;

        public PiattoController(IBusinessLayer bl)
        {
            BL = bl;
        }

        #region CRUD
        [HttpGet]
        public IActionResult Index()
        {
            var piatti = BL.GetAllPiatti();

            List<PiattoViewModel> piattiViewModel = new List<PiattoViewModel>();

            foreach (var item in piatti)
            {
                piattiViewModel.Add(item.ToPiattoViewModel());
            }

            return View(piattiViewModel);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var piatto = BL.GetAllPiatti().FirstOrDefault(p => p.Id == id);

            var piattoViewModel = piatto.ToPiattoViewModel();

            return View(piattoViewModel);

        }

        [Authorize(Policy = ("Adm"))]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Policy = ("Adm"))]
        public IActionResult Create(PiattoViewModel piattoViewModel)
        {
            if (ModelState.IsValid)
            {
                var piatto = piattoViewModel.ToPiatto();
                BL.AddPiatto(piatto);
                return RedirectToAction(nameof(Index));
            }
            return View(piattoViewModel);
        }

        [Authorize(Policy = ("Adm"))]
        [HttpGet]
        public IActionResult Update(int id)
        {
            var piatto = BL.GetAllPiatti().FirstOrDefault(p => p.Id == id);
            var piattoViewModel = piatto.ToPiattoViewModel();
            return View(piattoViewModel);
        }

        [Authorize(Policy = ("Adm"))]
        [HttpPost]
        public IActionResult Update(PiattoViewModel piattoViewModel)
        {
            if (ModelState.IsValid)
            {
                var piatto = piattoViewModel.ToPiatto();
                BL.EditPiatto(piatto);
                return RedirectToAction(nameof(Index));
            }
            return View(piattoViewModel);
        }

        [Authorize(Policy = ("Adm"))]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var piatto = BL.GetAllPiatti().FirstOrDefault(p => p.Id == id);
            var piattoViewModel = piatto.ToPiattoViewModel();
            return View(piattoViewModel);
        }

        [Authorize(Policy = ("Adm"))]
        [HttpPost]
        public IActionResult Delete(PiattoViewModel piattoViewModel)
        {
            if (ModelState.IsValid)
            {
                var piatto = piattoViewModel.ToPiatto();
                BL.DeletePiatto(piatto.Id);
                return RedirectToAction(nameof(Index));
            }
            return View(piattoViewModel); ;
        }

        #endregion
    }
}
