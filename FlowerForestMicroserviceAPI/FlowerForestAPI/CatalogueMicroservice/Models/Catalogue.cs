using System.ComponentModel.DataAnnotations;

namespace CatalogueMicroservice.Models
{
    public class Catalogue
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool IsPublic { get; set; }


        public Guid UserId { get; set; }

        public User User { get; set; }
    }
}
