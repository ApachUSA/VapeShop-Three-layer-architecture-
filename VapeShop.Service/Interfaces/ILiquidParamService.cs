using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VapeShop.Domain.Entity.Product;
using VapeShop.Domain.Response;

namespace VapeShop.Service.Interfaces
{
	public interface ILiquidParamService
	{
		Task<BaseResponse<Liquid_param>> Get(int id);

		Task<BaseResponse<IEnumerable<Liquid_param>>> GetAll();

		Task<BaseResponse<Liquid_param>> Create(Liquid_param model);

		Task<BaseResponse<Liquid_param>> Update(Liquid_param model);

		Task<BaseResponse<bool>> Delete(Liquid_param model);
	}
}
