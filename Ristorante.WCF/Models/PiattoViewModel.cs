using System.ComponentModel.DataAnnotations;

namespace Ristorante.WCF.Models
{
    public class PiattoViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Descrizione { get; set; }
        [Required]
        public Tipologia Tipologia { get; set; }
        [Required]
        public decimal Prezzo { get; set; }


        //FK
        public int? MenuId { get; set; }
        public MenuViewModel Menu { get; set; }
    }

    public enum Tipologia
    {
        Primo,
        Secondo,
        Contorno,
        Dolce
    }
}
