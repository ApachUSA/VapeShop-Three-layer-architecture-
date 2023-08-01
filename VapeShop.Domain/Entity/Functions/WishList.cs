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

		public int UserID { get; set; }

		public int Liquid_paramID { get; set; }

		public required User User { get; set; }

		public required Liquid_param Liquid_param { get; set; }
	}
}
