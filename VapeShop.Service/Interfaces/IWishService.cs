using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VapeShop.Domain.Entity.Functions;
using VapeShop.Domain.Entity.Product;
using VapeShop.Domain.Response;

namespace VapeShop.Service.Interfaces
{
	public interface IWishService
	{
		Task<BaseResponse<WishList>> Get(int userID);
		Task<BaseResponse<IEnumerable<WishList>>> GetAll(int userID);

		Task<BaseResponse<bool>> Create(WishList model);

		Task<BaseResponse<bool>> Delete(int wishListID);
		Task<BaseResponse<bool>> DeleteAll(int userID);

		Task<BaseResponse<int>> GetCount(int userID);
	}
}
