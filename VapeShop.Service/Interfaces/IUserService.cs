using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VapeShop.Domain.Response;
using VapeShop.Domain.ViewModels;

namespace VapeShop.Service.Interfaces
{
	public interface IUserService
	{
		Task<BaseResponse<IEnumerable<UserViewModel>>> GetAll();

		Task<BaseResponse<UserViewModel>> Get(int id);

		Task<BaseResponse<bool>> Delete(int id);
	}
}
