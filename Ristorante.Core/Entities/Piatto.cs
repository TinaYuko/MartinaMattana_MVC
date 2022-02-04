using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ristorante.Core.Entities
{
        public class Piatto
        {
            public int Id { get; set; }
            public string Nome { get; set; }
            public string Descrizione { get; set; }
            public Tipologia Tipologia { get; set; }
            public decimal Prezzo { get; set; }


            //FK
            public int? MenuId { get; set; }
            public Menu Menu { get; set; }
        }

        public enum Tipologia
        {
            Primo,
            Secondo,
            Contorno,
            Dolce
        }
}

