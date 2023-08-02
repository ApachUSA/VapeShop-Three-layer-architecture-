using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VapeShop.Domain.Response;
using VapeShop.Domain.ViewModels;

namespace VapeShop.Service.Interfaces
{
	public interface IAccountService
	{
		Task<BaseResponse<ClaimsIdentity>> Register(RegisterVM model);

		Task<BaseResponse<ClaimsIdentity>> Login(LoginVM model);
	}
}
