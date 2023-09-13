using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VapeShop.Domain.Enum;
using VapeShop.Domain.Response;

namespace VapeShop.Domain.Helpers
{
	public static class ResponseHelper
	{
		public static BaseResponse<T> CreateResponse<T>(T? value, string? description, StatusCode statusCode)
		{
			return new BaseResponse<T>
			{
				Value = value,
				Description = description,
				StatusCode = statusCode
			};
		}
	}
}
