using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VapeShop.Domain.Entity.Product;
using VapeShop.Domain.Enum;

namespace VapeShop.Domain.ViewModels
{
	public class CreateLiquidVM
	{
		public required string Name { get; set; }

		public string? LongName { get; set; }

		public string? Description { get; set; }

		public string? Image { get; set; }

		public Volume Volume { get; set; }

		public int? FlavorID { get; set; }

		public Flavor? Flavor { get; set; }

		public decimal Price { get; set; }

		public LiquidType LiquidType { get; set; }

		public List<Liquid_param>? Liquid_Params { get; set; } = new();
	}
}
