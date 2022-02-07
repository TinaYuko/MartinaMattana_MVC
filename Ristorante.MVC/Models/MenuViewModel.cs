using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ristorante.WCF.Models
{
    public class MenuViewModel
    {
        [DisplayName("Id Menù")]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        public List<PiattoViewModel> Piatti { get; set; } = new List<PiattoViewModel>();
    }
}
