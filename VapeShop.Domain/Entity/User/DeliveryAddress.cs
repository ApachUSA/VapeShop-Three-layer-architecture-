using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VapeShop.Domain.Entity.User
{
	public class DeliveryAddress
	{
		public int DeliveryAddressID { get; set; }

		public required string Surname { get; set; }

		public required string Name { get; set; }

		public required string Patronomyc { get; set; }

		public string Country { get; set; } = "Ukraine";

		public required City City { get; set; }

		public required Region Region { get; set; }
	}
}
