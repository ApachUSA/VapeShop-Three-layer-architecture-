using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VapeShop.Domain.Entity.Product
{
    public class Flavor
    {
        public int FlavorID { get; set; }

        public required string Flavor_name { get; set; }
    }
}
