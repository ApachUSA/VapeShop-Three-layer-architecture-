using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VapeShop.Domain.Enum;

namespace VapeShop.Domain.Response
{
	public class BaseResponse<T> : IBaseResponse<T>
	{
		public string Description { get; set; }
		public StatusCode StatusCode { get; set; }
		public T Value { get; set; }
	}
}
