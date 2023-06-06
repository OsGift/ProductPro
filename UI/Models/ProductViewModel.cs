using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UI.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Product Name")]
        public string? ProductName { get; set; }
        [Required]
        public float Price { get; set; }
        [Required]
        public int Qty { get; set; }
    }
}
