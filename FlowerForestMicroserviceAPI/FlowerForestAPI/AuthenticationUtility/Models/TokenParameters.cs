using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationUtility.Models
{
    public class TokenParameters
    {
        public string Secret { get; set; }

        public int Lifetime_minutes { get; set; }
    }
}
