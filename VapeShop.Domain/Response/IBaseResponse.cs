using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VapeShop.Domain.Enum;

namespace VapeShop.Domain.Response
{
	public interface IBaseResponse<T>
	{
		string? Description { get; set; }

		StatusCode StatusCode { get; set; }

		T? Value { get; set; }
	}
}
