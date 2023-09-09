using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VapeShop.Domain.Entity.Client;
using VapeShop.Domain.Entity.Product;
using VapeShop.Domain.Response;
using VapeShop.Domain.ViewModels;

namespace VapeShop.Service.Interfaces
{
	public interface ILiquidService
	{
		Task<BaseResponse<Liquid>> Get(int id);

		BaseResponse<IEnumerable<Liquid>> GetAll();

		Task<BaseResponse<Liquid>> Create(CreateLiquidVM model);

		Task<BaseResponse<Liquid>> Update(Liquid model);

		Task<BaseResponse<bool>> Delete(Liquid model);
		Task<BaseResponse<bool>> Delete(int model_id);

	}
}
