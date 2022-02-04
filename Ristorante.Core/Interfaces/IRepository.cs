using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ristorante.Core.Interfaces
{
    public interface IRepository<T>
    {
        public List<T> GetAll();
        public T Add(T item);
        public bool Update(T item);
        public bool Delete(T item);
    }
}
