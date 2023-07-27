using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VapeShop.Domain.Entity.Product
{
    public class Liquid_param
    {
        public int LiquidParamID { get; set; }

        public required Liquid Liquid { get; set; }

        public required PG_VG PG_VG { get; set; }

        public required Nicotine Nicotine { get; set; }

        public int Available_quantity { get; set; }

	}
}
