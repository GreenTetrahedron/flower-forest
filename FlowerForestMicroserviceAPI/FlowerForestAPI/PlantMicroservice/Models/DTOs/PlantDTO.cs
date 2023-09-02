using System.ComponentModel.DataAnnotations;

namespace PlantMicroservice.Models.DTOs
{
    public class PlantDTO
    {
        [Key]
        public Guid Id { get; set; }

        public string PhotoUrl { get; set; }

        public string? CommonName { get; set; }

        public double MaxHeight_metres { get; set; }

        public string Genus { get; set; }

        public string Species { get; set; }


        public Guid CatalogueId { get; set; }
    }
}
