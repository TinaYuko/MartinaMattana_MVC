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
    public class MenuRepositoryEF : IMenuRepository
    {
        private readonly Context ctx;
        public MenuRepositoryEF(Context context)
        {
            ctx = context;
        }
        public Menu Add(Menu item)
        { 
            ctx.Menu.Add(item);
            ctx.SaveChanges();
            return item;
        }

        public bool Delete(Menu item)
        {
            ctx.Menu.Remove(item);
            ctx.SaveChanges();
            return true;
        }

        public List<Menu> GetAll()
        {
            return ctx.Menu.Include(x=>x.Piatti).ToList();
        }

        public bool Update(Menu item)
        {
            ctx.Menu.Update(item);
            ctx.SaveChanges();
            return true;
        }
    }
}
