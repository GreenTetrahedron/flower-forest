using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FlowerForestAPI.Models
{
    public class Plant
    {
        [Key]
        public Guid Id { get; set; }

        public string Genus { get; set; }

        public string Species { get; set; } 

        public double MaxHeight_metres { get; set; }
         
        public string? CommonName { get; set; }

        public string PhotoUrl { get; set; }


        public Guid CatalogueId { get; set; }

        public Catalogue catalogue { get; set; }
    }
}
