using Ristorante.Core.Entities;
using Ristorante.Core.Interfaces;

namespace Ristorante.Core.BL
{
    public class BusinessLayer : IBusinessLayer
    {
        private readonly IMenuRepository menuRepo;
        private readonly IPiattoRepository piattiRepo;
        private readonly IUtenteRepository utentiRepo;

        public BusinessLayer(IMenuRepository menu, IPiattoRepository piatti, IUtenteRepository utenti)
        {
            menuRepo = menu;
            piattiRepo = piatti;
            utentiRepo = utenti;
        }

        public bool AddMenu(Menu menu)
        {
            Menu menuEsistente = menuRepo.GetAll().FirstOrDefault(x => x.Id == menu.Id);
            if (menuEsistente != null)
            {
                return false;
                Console.WriteLine("Errore: Id già presente");
            }

            menuRepo.Add(menu);
            return true;
            Console.WriteLine("Nuovo menu aggiunto con successo");
        }

        public bool AddPiatto(Piatto piatto)
        {
            Menu menuEsistente = menuRepo.GetAll().FirstOrDefault(x => x.Id == piatto.MenuId);
            if (menuEsistente == null)
                menuEsistente = new Menu();


            piatto.Menu = menuEsistente;
            piattiRepo.Add(piatto);
            return true;
            Console.WriteLine("Nuovo piatto aggiunto con successo");

        }

        public void AddUtente(Utente u)
        {
            utentiRepo.Add(u);        }

        public bool DeleteMenu(int id)
        {
            Menu menuEsistente = menuRepo.GetAll().FirstOrDefault(x => x.Id == id);
            if (menuEsistente == null)
            {
                return false;
                Console.WriteLine("Menu non trovato.");
            }

            var PiattiDelMenu = GetAllPiatti().FirstOrDefault(p => p.MenuId == menuEsistente.Id);
            if (PiattiDelMenu != null)
            {
                return false;
                Console.WriteLine("Impossibile cancellare il menu perchè risulta associato ad almeno un piatto");
            }
            menuRepo.Delete(menuEsistente);
            return true;
            Console.WriteLine("Menu eliminato con successo");
        }

        public bool DeletePiatto(int id)
        {
            var piattoToDelete = piattiRepo.GetAll().FirstOrDefault(x => x.Id == id);
            if (piattoToDelete == null)
            {
                return false;
                Console.WriteLine("Id piatto non trovato");
            }

            piattiRepo.Delete(piattoToDelete);
            return true;
            Console.WriteLine("Piatto eliminato con successo");
        }

        public bool EditMenu(Menu menu)
        {
            Menu menuToUpdate = menuRepo.GetAll().FirstOrDefault(x => x.Id == menu.Id);
            if (menuToUpdate == null)
            {
                return false;
                Console.WriteLine("Errore: menu non trovato");
            }
            menuToUpdate.Nome = menu.Nome;
            menuRepo.Update(menuToUpdate);
            return true;
            Console.WriteLine("Menu aggiornato con successo");
        }

        public bool EditPiatto(Piatto piatto)
        {
            Piatto piattoToUpdate = piattiRepo.GetAll().FirstOrDefault(x => x.Id == piatto.Id);
            if (piattoToUpdate == null)
            {
                return false;
                Console.WriteLine("Errore: piatto non trovato");
            }


            piattoToUpdate.Nome = piatto.Nome;
            piattoToUpdate.Descrizione = piatto.Descrizione;
            piattoToUpdate.Tipologia = piatto.Tipologia;
            piattoToUpdate.Prezzo = piatto.Prezzo;
            piattoToUpdate.MenuId = piatto.MenuId;
            piattoToUpdate.Menu = menuRepo.GetAll().FirstOrDefault(x => x.Id == piatto.MenuId);
            piattiRepo.Update(piattoToUpdate);
            return true;
            Console.WriteLine("Piatto aggiornato con successo");
        }

        public Utente GetAccount(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return null;
            }
            return utentiRepo.GetByUsername(username);
        }

        public List<Menu> GetAllMenus()
        {
            return menuRepo.GetAll();
        }

        public List<Piatto> GetAllPiatti()
        {
            return piattiRepo.GetAll();
        }
    }
}