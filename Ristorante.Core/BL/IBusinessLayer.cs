using Ristorante.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ristorante.Core.BL
{
    public interface IBusinessLayer
    {
        #region Menu
        public List<Menu> GetAllMenus();
        public bool AddMenu(Menu menu);
        public bool DeleteMenu(int id);
        public bool EditMenu(Menu menu);
        #endregion

        #region Piatti
        public List<Piatto> GetAllPiatti();
        public bool AddPiatto(Piatto piatto);
        public bool DeletePiatto(int id);
        public bool EditPiatto(Piatto piatto);
        #endregion

        public Utente GetAccount(string username);
        void AddUtente(Utente u);
    }
}
