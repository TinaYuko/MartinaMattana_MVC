using Ristorante.Core.Entities;
using Ristorante.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ristorante.EF.Repositories
{
    public class UtenteRepositoryEF : IUtenteRepository
    {
        private readonly Context ctx;
        public UtenteRepositoryEF(Context context)
        {
            ctx = context;
        }
        public Utente Add(Utente item)
        {
            ctx.Utenti.Add(item);
            ctx.SaveChanges();
            return item;
        }

        public bool Delete(Utente item)
        {
            ctx.Utenti.Remove(item);
            ctx.SaveChanges();
            return true;
        }

        public List<Utente> GetAll()
        {
            return ctx.Utenti.ToList();
        }

        public Utente? GetByUsername(string username)
        {
            return ctx.Utenti.FirstOrDefault(x=>x.Username == username);
        }

        public bool Update(Utente item)
        {
            ctx.Utenti.Update(item);
            ctx.SaveChanges();
            return true;
        }
    }
}
