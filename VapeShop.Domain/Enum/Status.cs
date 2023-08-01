using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VapeShop.Domain.Enum
{
	public enum Status
	{
		Waiting_for_confirmation = 0,
		Waiting_for_payment = 1,
		Sended = 2,
		Retention = 3,
	}
}
