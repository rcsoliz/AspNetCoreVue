
using System.ComponentModel.DataAnnotations;

namespace Model.DTOs
{
    public class ClientCreateDto
    {  
        [Required]
        public string Name { get; set; }
        public string SurNames { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }
        public int CountryId { get; set; }
    }
    public class ClientDto
    {
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string SurNames { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }
        public int CountryId { get; set; }
    }
    public class ClientUpdateDto
    {
        [Required]
        public string Name { get; set; }
        public string SurNames { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }
        public int CountryId { get; set; }
    }
}
