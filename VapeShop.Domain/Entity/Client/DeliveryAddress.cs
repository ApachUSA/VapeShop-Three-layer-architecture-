using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VapeShop.Domain.Entity.Client
{
	public class DeliveryAddress
	{
		public int DeliveryAddressID { get; set; }

		public string? Surname { get; set; }

		public string? Name { get; set; }

		public string? Patronomyc { get; set; }

		public string Country { get; set; } = "Ukraine";

		public int? CityID { get; set; }

		public City? City { get; set; }

		public int? RegionID { get; set; }

		public Region? Region { get; set; }

		public int UserID { get; set; }

		public User? User { get; set; }
	}
}
