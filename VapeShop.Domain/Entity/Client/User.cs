using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VapeShop.Domain.Entity.Functions;
using VapeShop.Domain.Enum;

namespace VapeShop.Domain.Entity.Client
{
	public class User
	{
		public int UserID { get; set; }

		public required string Surname { get; set; }

		public required string Name { get; set; }

		public string? Phone { get; set; }

		public required string Email { get; set; }

		[Required(ErrorMessage = "Password is required")]
		public required string Password { get; set; }

		[Required(ErrorMessage = "Password confirmation is required")]
		[Compare("Password", ErrorMessage = "Passwords do not match")]
		public required string PasswordConfirme { get; set; }

		public int? CommunicationMethodID { get; set; }

		public CommunicationMethod? CommunicationMethod { get; set; }

		public DeliveryAddress? DeliveryAddress { get; set; }

		public Role Role { get; set; }

		public ICollection<ComparisonList>? ComparisonLists { get; set; }

		public ICollection<WishList>? WishLists { get; set; }

		public ICollection<Cart>? CartLists { get; set; }

		public ICollection<Order>? Orders { get; set; }
	}
}
