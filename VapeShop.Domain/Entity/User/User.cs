using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VapeShop.Domain.Enum;

namespace VapeShop.Domain.Entity.User
{
	public class User
	{
		public int UserID { get; set; }

		public required string Surname { get; set; }

		public required string Name { get; set; }

		public string? Phone { get; set; }

		public required string Email { get; set; }

		public required string Password { get; set; }

		public required string PasswordConfirme { get; set; }

		public CommunicationMethod? CommunicationMethod { get; set; }

		public required DeliveryAddress DeliveryAddressID { get; set; }

		public Role Role { get; set; }
	}
}
