using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VapeShop.Domain.Entity.Client;
using VapeShop.Domain.Entity.Product;

namespace VapeShop.Domain.Entity.Functions
{
	public class ComparisonList
	{
		public int ComparsionListID { get; set; }

		public int UserID { get; set; }

		public int LiquidID { get; set; }

		public required User User { get; set; }

		public required Liquid Liquid { get; set; }

	}
}
