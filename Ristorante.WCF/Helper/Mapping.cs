using Ristorante.Core.Entities;
using Ristorante.WCF.Models;

namespace Ristorante.WCF.Helper
{
    public static class Mapping
    {
        public static MenuViewModel ToMenuViewModel(this Menu menu)
        {
            List<PiattoViewModel> piattiViewModel = new List<PiattoViewModel>();
            if (menu.Piatti != null)
            {
                foreach (var item in menu.Piatti)
                {
                    piattiViewModel.Add(item?.ToPiattoViewModel());
                }
            }

            return new MenuViewModel
            {
                Id = menu.Id,
                Nome = menu.Nome,
                Piatti = piattiViewModel
            };
        }

        public static Menu ToMenu(this MenuViewModel menuViewModel)
        {
            List<Piatto> piatti = new List<Piatto>();
            foreach (var item in menuViewModel.Piatti)
            {
                piatti.Add(item?.ToPiatto());
            }

            return new Menu
            {
                Id = menuViewModel.Id,
                Nome = menuViewModel.Nome,
                Piatti = piatti
            };
        }
        public static PiattoViewModel ToPiattoViewModel(this Piatto piatto)
        {
            return new PiattoViewModel
            {
                Id = piatto.Id,
                Nome = piatto.Nome,
                Descrizione = piatto.Descrizione,
                Prezzo = piatto.Prezzo,
                Tipologia = (Models.Tipologia)piatto.Tipologia,
                MenuId = piatto.MenuId
            };
        }

        public static Piatto ToPiatto(this PiattoViewModel piattoViewModel)
        {
            return new Piatto
            {
                Id = piattoViewModel.Id,
                Nome = piattoViewModel.Nome,
                Descrizione = piattoViewModel.Descrizione,
                Prezzo = piattoViewModel.Prezzo,
                Tipologia = (Core.Entities.Tipologia)piattoViewModel.Tipologia,
                MenuId = piattoViewModel.MenuId
            };
        }

        public static Utente ToUtente(this UtenteViewModel uViewModel)
        {
            return new Utente
            {
                Username=uViewModel.Username,
                Password=uViewModel.Password

            };
        }
    }
}
