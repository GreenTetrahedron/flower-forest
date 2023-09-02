using System.ComponentModel.DataAnnotations;

namespace PlantMicroservice.Models
{
    public class Catalogue
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid UserId { get; set; }
    }
}
