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
using VapeShop.Domain.ViewModels;
using VapeShop.Infrastructure.Interfaces;
using VapeShop.Service.Interfaces;

namespace VapeShop.Service.Implementations
{
	public class LiquidService : ILiquidService
	{

		private readonly IBaseRepository<Liquid> _liquidRepository;
		private readonly ILiquidParamService _liquid_paramRepository;

		public LiquidService(IBaseRepository<Liquid> liquidRepository, ILiquidParamService liquid_paramRepository)
		{
			_liquidRepository = liquidRepository;
			_liquid_paramRepository = liquid_paramRepository;
		}

		public async Task<BaseResponse<Liquid>> Create(CreateLiquidVM model)
		{
			var liquid = new Liquid()
			{
				Name = model.Name,
				LongName = model.LongName,
				Description = model.Description,
				Image = model.Image,
				Volume = model.Volume,
				FlavorID = model.FlavorID,
				Price = model.Price,
				LiquidType = model.LiquidType

			};

			try
			{

				await _liquidRepository.Create(liquid);

				if (model.Liquid_Params.Any())
				{
					model.Liquid_Params.ForEach(x => x.LiquidID = liquid.LiquidID);
					var response = await _liquid_paramRepository.Create(model.Liquid_Params);

					if (response.StatusCode == StatusCode.InternalServerError) throw new Exception(response.Descrition);
				}

				return new BaseResponse<Liquid>()
				{
					Value = liquid,
					Descrition = "Liquid added",
					StatusCode = StatusCode.Succes

				};
			}
			catch (Exception ex)
			{
				return new BaseResponse<Liquid>()
				{
					Value = liquid,
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

		public async Task<BaseResponse<bool>> Delete(int model_id)
		{
			try
			{
				var model = await _liquidRepository.Get().FirstOrDefaultAsync(x => x.LiquidID == model_id);
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
				var liquid = await _liquidRepository.Get()
					.Include(x => x.Flavor)
					.Include(x => x.Liquid_Params)
					.ThenInclude(lp => lp.PG_VG)
					.Include(x => x.Liquid_Params)
					.ThenInclude(lp => lp.Nicotine)
					.FirstOrDefaultAsync(x => x.LiquidID == id);


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

		public async Task<BaseResponse<IEnumerable<Liquid>>> GetAll()
		{
			try
			{
				var liquids = await _liquidRepository.Get().Include(x => x.Flavor).ToListAsync();

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
