using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowerForestAPI.Models
{
    public class Plant
    {
        public Guid Id { get; set; }

        public string Genus { get; set; }

        public string Species { get; set; }

        public double MaxHeight_metres { get; set; }

        public string? CommonName { get; set; }

        public string PhotoUrl { get; set; }
    }
}
