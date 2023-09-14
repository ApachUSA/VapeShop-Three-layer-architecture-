using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VapeShop.Domain.Entity.Functions;
using VapeShop.Domain.Response;

namespace VapeShop.Service.Interfaces
{
	public interface IComparisonService
	{
		Task<BaseResponse<ComparisonList>> Get(int userID);
		Task<BaseResponse<IEnumerable<ComparisonList>>> GetAll(int userID);

		Task<BaseResponse<bool>> Create(ComparisonList model);

		Task<BaseResponse<bool>> Delete(int comparsionListID);
		Task<BaseResponse<bool>> DeleteAll(int userID);

		Task<BaseResponse<int>> GetCount(int userID);
	}
}
