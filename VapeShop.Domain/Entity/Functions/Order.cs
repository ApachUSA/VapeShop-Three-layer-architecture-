using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VapeShop.Domain.Entity.Client;
using VapeShop.Domain.Enum;

namespace VapeShop.Domain.Entity.Functions
{
	public class Order
	{
		public int OrderID { get; set; }

		public Status Status { get; set; }

		public PaymentMethod PaymentMethod { get; set; }

		public decimal OrderAmount { get; set; }

		public int UserID { get; set; }	

		public required User User { get; set; }

		public required ICollection<OrderProduct> Products { get; set; }
	}
}
