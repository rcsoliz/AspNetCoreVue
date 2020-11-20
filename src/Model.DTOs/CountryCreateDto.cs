
using System.ComponentModel.DataAnnotations;

namespace Model.DTOs
{
    public class CountryCreateDto
    {  
        [Required]
        public string Name { get; set; }
    }
    public class CountryDto
    {
        public int CountryId { get; set; }
        public string Name { get; set; }
    }
    public class CountryUpdateDto
    {
        [Required]
        public string Name { get; set; }
    }
}
