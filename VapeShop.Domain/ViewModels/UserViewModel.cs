using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VapeShop.Domain.Entity.Client;

namespace VapeShop.Domain.ViewModels
{
	public class UserViewModel
	{
		public int UserID { get; set; }

		public required string Surname { get; set; }

		public required string Name { get; set; }

		public string? Phone { get; set; }

		public required string Email { get; set; }

		public required string Password { get; set; }

		public required string PasswordConfirme { get; set; }

		public int? CommunicationMethodID { get; set; }

		public CommunicationMethod? CommunicationMethod { get; set; }

		public int? DeliveryAddressID { get; set; }

		public DeliveryAddress? DeliveryAddress { get; set; }
	}
}
