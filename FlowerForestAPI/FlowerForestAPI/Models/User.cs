using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowerForestAPI.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string Username { get; set; }

        [MinLength(8)]
        [Required]
        public string Password { get; set; }

        public ICollection<Catalogue> Catalogues { get; set; }
    }
}
