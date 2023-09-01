using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VapeShop.Domain.Entity.Product;
using VapeShop.Domain.Enum;
using VapeShop.Domain.Response;
using VapeShop.Infrastructure.Interfaces;
using VapeShop.Service.Interfaces;

namespace VapeShop.Service.Implementations
{
	public class LiquidService : ILiquidService
	{

		private readonly IBaseRepository<Liquid> _liquidRepository;

		public LiquidService(IBaseRepository<Liquid> liquidRepository)
		{
			_liquidRepository = liquidRepository;
		}

		public async Task<BaseResponse<Liquid>> Create(Liquid model)
		{
			try
			{
				await _liquidRepository.Create(model);
				return new BaseResponse<Liquid>()
				{
					Value = model,
					Descrition = "Liquid added",
					StatusCode = StatusCode.Succes

				};
			}
			catch (Exception ex)
			{
				return new BaseResponse<Liquid>()
				{
					Value = model,
					Descrition = $"[CreateLiquid] : {ex.Message}",
					StatusCode = StatusCode.InternalServerError

				};
			}
			

			
		}

		public async Task<BaseResponse<bool>> Delete(Liquid model)
		{
			try
			{
				await _liquidRepository.Delete(model);
				return new BaseResponse<bool>()
				{
					Value = true,
					StatusCode = StatusCode.Succes

				};
			}
			catch (Exception ex)
			{
				return new BaseResponse<bool>()
				{
					Descrition = $"[DeleteLiquid] : {ex.Message}",
					StatusCode = StatusCode.InternalServerError

				};
			}
			

			
		}

		public async Task<BaseResponse<Liquid>> Get(int id)
		{
			try
			{
				var liquid = await _liquidRepository.Get().FirstOrDefaultAsync(x => x.LiquidID == id);

				if (liquid == null)
				{
					return new BaseResponse<Liquid>()
					{
						Descrition = "Liquid not found",
						StatusCode = StatusCode.InternalServerError

					};
				}

				return new BaseResponse<Liquid>()
				{
					Value = liquid,
					StatusCode = StatusCode.Succes
				};
			}
			catch (Exception ex)
			{

				return new BaseResponse<Liquid>()
				{
					Descrition = $"[GetLiquid] : {ex.Message}",
					StatusCode = StatusCode.InternalServerError

				};
			}

		}

		public  BaseResponse<IEnumerable<Liquid>> GetAll()
		{
			try
			{
				var liquids =  _liquidRepository.Get().Include(x => x.Flavor).ToList();

				if (liquids == null)
				{
					return new BaseResponse<IEnumerable<Liquid>>()
					{
						Descrition = "Liquids not found",
						StatusCode = StatusCode.InternalServerError

					};
				}

				return new BaseResponse<IEnumerable<Liquid>>()
				{
					Value = liquids,
					StatusCode = StatusCode.Succes
				};
			}
			catch (Exception ex)
			{

				return new BaseResponse<IEnumerable<Liquid>>()
				{
					Descrition = $"[GetLiquids] : {ex.Message}",
					StatusCode = StatusCode.InternalServerError

				};
			}
		}

		public async Task<BaseResponse<Liquid>> Update(Liquid model)
		{
			try
			{
				await _liquidRepository.Update(model);

				return new BaseResponse<Liquid>()
				{
					Value = model,
					StatusCode = StatusCode.Succes
				};
			}
			catch (Exception ex)
			{
				return new BaseResponse<Liquid>()
				{
					Descrition = $"[UpdateLiquids] : {ex.Message}",
					StatusCode = StatusCode.InternalServerError

				};
			}
		}
	}
}
