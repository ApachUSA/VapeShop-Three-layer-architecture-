using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VapeShop.Domain.Entity.User
{
	public class Region
	{
		public int RegionID { get; set; }

		public required string RegionName { get; set; }
	}
}
