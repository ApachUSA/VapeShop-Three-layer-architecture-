using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VapeShop.Domain.Enum
{
	public enum StatusCode
	{
		UserNotFound = 0,
		UserAlreadyExists = 1,

		ItemNotFound = 20,
		ItemAlreadyExist = 21,

		Success = 200,
		InternalServerError = 500
	}
}
