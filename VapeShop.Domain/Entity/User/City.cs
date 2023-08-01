using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VapeShop.Domain.Entity.User
{
	public class City
	{
		public int CityID { get; set; }

		public required string CityName { get; set; }

		public ICollection<DeliveryAddress> DeliveryAddress { get; set; }
	}
}
