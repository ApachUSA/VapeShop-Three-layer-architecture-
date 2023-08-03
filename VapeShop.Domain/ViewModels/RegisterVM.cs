using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VapeShop.Domain.Entity.Client;

namespace VapeShop.Domain.ViewModels
{
	public class RegisterVM
	{
		public string Surname { get; set; }

		public string Name { get; set; }

		public string Phone { get; set; }

		public string Email { get; set; }

		public string Password { get; set; }

		public string PasswordConfirme { get; set; }

		public int CommunicationMethodID { get; set; } = 1;
	}
}
