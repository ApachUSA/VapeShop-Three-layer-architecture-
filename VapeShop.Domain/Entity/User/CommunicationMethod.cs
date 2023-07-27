using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VapeShop.Domain.Entity.User
{
	public class CommunicationMethod
	{
		public int CommunicationMethodID { get; set; }

		public required string CommunicationMethodName { get; set; }
	}
}
