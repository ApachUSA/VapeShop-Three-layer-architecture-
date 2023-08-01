using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VapeShop.Domain.Entity.Product;

namespace VapeShop.Domain.Entity.Client
{
	public class CommunicationMethod
	{
		public int CommunicationMethodID { get; set; }

		public required string CommunicationMethodName { get; set; }

		public ICollection<User>? Users { get; set; }
	}
}
