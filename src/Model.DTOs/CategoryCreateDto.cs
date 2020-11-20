
using System.ComponentModel.DataAnnotations;

namespace Model.DTOs
{
    public class CategoryCreateDto
    {  
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class CategoryUpdateDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
