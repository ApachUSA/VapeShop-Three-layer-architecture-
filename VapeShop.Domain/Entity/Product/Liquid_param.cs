using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VapeShop.Domain.Entity.Functions;

namespace VapeShop.Domain.Entity.Product
{
    public class Liquid_param
    {
        public int LiquidParamID { get; set; }

		public int LiquidID { get; set; }

		public int PG_VGID { get; set; }

		public int NicotineID { get; set; }

        public int Available_quantity { get; set; }

		public required Liquid Liquid { get; set; }

        public required PG_VG PG_VG { get; set; }

        public required Nicotine Nicotine { get; set; }

		public ICollection<WishList>? WishLists { get; set; }
		public ICollection<Cart>? CartLists { get; set; }
		public ICollection<OrderProduct>? OrderProducts { get; set; }

	}
}
