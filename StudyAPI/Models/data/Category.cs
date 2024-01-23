using System.ComponentModel.DataAnnotations;

namespace StudyAPI.Models.data
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
        [Required]
        [StringLength(50)]
        public string Img { get; set; }
        public List<Product> Products { get; set; }
    }
}
