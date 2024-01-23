using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StudyAPI.Models.Request
{
    public class ProductRequest
    {
         public Guid? ProductId { get; set; }
        [Required(ErrorMessage = "Vui Long Nhap Ten")]
        public string? Name { get; set; }

        public string? Description { get; set; }
        [Required(ErrorMessage = "Vui Long Nhap so luong")]
        public int Quantity { get; set; }
        public string? Img { get; set; }
        [ForeignKey("Category")]
        public Guid? CategoryId { get; set; }
    }
}
