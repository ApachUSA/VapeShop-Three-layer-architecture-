using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VapeShop.Domain.Entity.Client
{
	public class Region
	{
		public int RegionID { get; set; }

		public required string RegionName { get; set; }

		public ICollection<DeliveryAddress>? DeliveryAddress { get; set; }
	}
}
