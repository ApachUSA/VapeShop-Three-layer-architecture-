using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VapeShop.Domain.ViewModels
{
	public class LoginVM
	{
		public string Email { get; set; }

		public string Password { get; set; }

		public string PasswordConfirm { get; set; }
	}
}
