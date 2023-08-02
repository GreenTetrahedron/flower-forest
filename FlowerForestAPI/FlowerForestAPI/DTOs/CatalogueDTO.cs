using System;
using System.Collections.Generic;

namespace FlowerForestAPI.DTOs
{
    public class CatalogueDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public bool IsPublic { get; set; }

        public Guid UserId { get; set; }
    }
}
