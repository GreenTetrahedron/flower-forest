using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FlowerForestAPI.Models
{
    public class Catalogue
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool IsPublic { get; set; }

        public ICollection<Plant> Plants { get; set; }


        public Guid UserId { get; set; }

        public User User { get; set; }
    }
}
