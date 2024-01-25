using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudyAPI.Models.data
{
    public class Product
    {
        [Key] public Guid ProductId { get; set; }
        [Required(ErrorMessage = "Vui Long Nhap Ten")]
        public string? Name { get; set; }
        
        public string? Description { get; set; }
        [Required(ErrorMessage = "Vui Long Nhap so luong")]
        public int Quantity { get; set; }
        public string? Img { get; set; }
        [ForeignKey("Category")]
        public Guid? CategoryId { get; set; }
        public Category? Category { get; set; }  
    }
}
