using System.ComponentModel.DataAnnotations;

namespace PlantMicroservice.Models
{
    public class Plant
    {
        [Key]
        public Guid Id { get; set; }

        public string PhotoUrl { get; set; }

        public string? CommonName { get; set; }

        public double MaxHeight_metres { get; set; }

        public string Genus { get; set; }

        public string Species { get; set; }


        public Guid CatalogueId { get; set; }

        public Catalogue Catalogue { get; set; }
    }
}
