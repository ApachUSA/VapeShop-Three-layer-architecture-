using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VapeShop.Domain.Entity.Client;
using VapeShop.Domain.Entity.Product;

namespace VapeShop.Domain.Entity.Functions
{
	public class WishList
	{
		public int WishListID { get; set; }

		public required int UserID { get; set; }

		public required int LiquidID { get; set; }

		public  int? Liquid_paramID { get; set; }

		public  User? User { get; set; }

		public  Liquid_param? Liquid_param { get; set; }

		public  Liquid? Liquid { get; set; }
	}
}
