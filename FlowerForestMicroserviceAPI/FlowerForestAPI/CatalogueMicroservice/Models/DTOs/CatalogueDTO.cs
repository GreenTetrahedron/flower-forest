using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace CatalogueMicroservice.Models.DTOs
{
    public class CatalogueDTO
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool IsPublic { get; set; }

        public Guid UserId { get; set; }
    }
}
