using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VapeShop.Domain.Enum;

namespace VapeShop.Domain.Entity.Product
{
    public class Liquid
    {
        public int LiquidID { get; set; }

        public required string Name { get; set; }

        public string? LongName { get; set; }

        public string? Description { get; set; }

        public required string Image { get; set; }

        public required Volume Volume { get; set; }

        public required Flavor Flavor { get; set; }

        public  SqlMoney Price { get; set; }

        public required LiquidType LiquidType { get; set; }

    }
}
