using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VapeShop.Domain.Entity.Product;
using VapeShop.Domain.Enum;
using VapeShop.Domain.Helpers;
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

				if (model.Liquid_Params != null && model.Liquid_Params.Any())
				{
					model.Liquid_Params.ForEach(x => x.LiquidID = liquid.LiquidID);
					var response = await _liquid_paramRepository.Create(model.Liquid_Params);

					if (response.StatusCode == StatusCode.InternalServerError) 
						return ResponseHelper.CreateResponse<Liquid>(null, response.Description, StatusCode.InternalServerError); 
				}

				return ResponseHelper.CreateResponse(liquid, "Liquid added", StatusCode.Success);
			}
			catch (Exception ex)
			{
				return ResponseHelper.CreateResponse<Liquid>(null, $"[CreateLiquid] : {ex.Message}", StatusCode.InternalServerError);
			}



		}

		public async Task<BaseResponse<bool>> Delete(Liquid model)
		{
			try
			{
				await _liquidRepository.Delete(model);
				return ResponseHelper.CreateResponse(true, $"Liquid (id : {model.LiquidID}) deleted", StatusCode.Success);
			}
			catch (Exception ex)
			{
				return ResponseHelper.CreateResponse(false, $"[DeleteLiquid] : {ex.Message}", StatusCode.InternalServerError);
			}



		}

		public async Task<BaseResponse<bool>> Delete(int model_id)
		{
			try
			{
				var model = await _liquidRepository.Get().FirstOrDefaultAsync(x => x.LiquidID == model_id);
				if(model == null) return ResponseHelper.CreateResponse(false, $"Liquid (id : {model_id}) not found", StatusCode.ItemNotFound);
				await _liquidRepository.Delete(model);
				return ResponseHelper.CreateResponse(true, $"Liquid (id : {model.LiquidID}) deleted", StatusCode.Success);
			}
			catch (Exception ex)
			{
				return ResponseHelper.CreateResponse(false, $"[DeleteLiquid] : {ex.Message}", StatusCode.InternalServerError);
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


				if (liquid == null) return ResponseHelper.CreateResponse<Liquid>(null, $"Liquid (id : {id}) not found", StatusCode.ItemNotFound);

				return ResponseHelper.CreateResponse(liquid, null, StatusCode.Success);
			}
			catch (Exception ex)
			{
				return ResponseHelper.CreateResponse<Liquid>(null, $"[GetLiquid] : {ex.Message}", StatusCode.InternalServerError);

			}

		}

		public async Task<BaseResponse<IEnumerable<Liquid>>> GetAll()
		{
			try
			{
				var liquids = await _liquidRepository.Get().Include(x => x.Flavor).Include(x => x.Liquid_Params).ToListAsync();

				if (liquids == null)
				{
					return ResponseHelper.CreateResponse<IEnumerable<Liquid>>(null, "Liquids not found", StatusCode.ItemNotFound);
				}

				return ResponseHelper.CreateResponse<IEnumerable<Liquid>>(liquids, null, StatusCode.Success);

			}
			catch (Exception ex)
			{
				return ResponseHelper.CreateResponse<IEnumerable<Liquid>>(null, $"[GetLiquids] : {ex.Message}", StatusCode.InternalServerError);
			}
		}

		public async Task<BaseResponse<Liquid>> Update(Liquid model)
		{
			try
			{
				await _liquidRepository.Update(model);

				return ResponseHelper.CreateResponse(model, null, StatusCode.Success);
			}
			catch (Exception ex)
			{
				return ResponseHelper.CreateResponse<Liquid>(null, $"[UpdateLiquids (id {model.LiquidID})] : {ex.Message}", StatusCode.InternalServerError);
			}
		}
	}
}
