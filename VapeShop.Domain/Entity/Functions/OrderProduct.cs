using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VapeShop.Domain.Entity.Product;

namespace VapeShop.Domain.Entity.Functions
{
	public class OrderProduct
	{
		public int OrderProductId { get; set; }

		public int OrderId { get; set; }

		public int Liquid_paramID { get; set; }

		public int Count { get; set; }

		public required Liquid_param Liquid_Param { get; set; }

		public required Order Order { get; set; }


	}
}
