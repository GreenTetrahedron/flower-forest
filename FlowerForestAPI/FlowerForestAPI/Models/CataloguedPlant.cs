using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FlowerForestAPI.Models
{
    public class CataloguedPlant
    {
        [Key]
        public Guid Id { get; set; }

        public string Genus { get; set; }

        public string Species { get; set; }

        public double MaxHeight_metres { get; set; }

        public string? CommonName { get; set; }

        public string PhotoUrl { get; set; }
      

        public Guid UserId { get; set; }

        public User User { get; set; }
    }
}
