using Microsoft.EntityFrameworkCore;
using Ristorante.Core.Entities;
using Ristorante.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ristorante.EF.Repositories
{
    public class PiattoRepositoryEF : IPiattoRepository
    {
        private readonly Context ctx;
        public PiattoRepositoryEF(Context context)
        {
            ctx = context;
        }
        public Piatto Add(Piatto item)
        {
            ctx.Piatti.Add(item);
            ctx.SaveChanges();
            return item;
        }

        public bool Delete(Piatto item)
        {
            ctx.Piatti.Remove(item);
            ctx.SaveChanges();
            return true;
        }

        public List<Piatto> GetAll()
        {
            return ctx.Piatti.Include(x=>x.Menu).ToList();
        }

        public bool Update(Piatto item)
        {
            ctx.Piatti.Update(item);
            ctx.SaveChanges();
            return true;
        }
    }
}
