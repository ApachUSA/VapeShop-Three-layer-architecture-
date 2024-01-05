using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VapeShop.Domain.Entity.Product;
using VapeShop.Domain.Enum;
using VapeShop.Domain.Helpers;
using VapeShop.Domain.Response;
using VapeShop.Infrastructure.Interfaces;
using VapeShop.Service.Interfaces;

namespace VapeShop.Service.Implementations
{
	public class LiquidParamService : ILiquidParamService
	{
		private readonly IBaseRepository<Liquid_param> _liquid_paramRepository;
		private readonly IBaseRepository<Nicotine> _nicotineRepository;
		private readonly IBaseRepository<PG_VG> _pg_vgRepository;
		private readonly IBaseRepository<Flavor> _flavorRepository;

		public LiquidParamService(IBaseRepository<Liquid_param> liquid_paramRepository, IBaseRepository<Nicotine> nicotineRepository, IBaseRepository<PG_VG> pg_vgRepository, IBaseRepository<Flavor> flavorRepository)
		{
			_liquid_paramRepository = liquid_paramRepository;
			_nicotineRepository = nicotineRepository;
			_pg_vgRepository = pg_vgRepository;
			_flavorRepository = flavorRepository;
		}

		public async Task<BaseResponse<bool>> Create(Liquid_param model)
		{
			try
			{
				await _liquid_paramRepository.Create(model);
				return ResponseHelper.CreateResponse(true, "Liquid_param added", StatusCode.Success);
			}
			catch (Exception ex)
			{
				return ResponseHelper.CreateResponse(false, $"[CreateLiquid_param] : {ex.Message}", StatusCode.InternalServerError);
			}
		}

		public async Task<BaseResponse<bool>> Create(List<Liquid_param> model)
		{
			try
			{
				foreach (var param in model) await _liquid_paramRepository.Create(param);

				return ResponseHelper.CreateResponse(true, "Liquid_params added", StatusCode.Success);
			}
			catch (Exception ex)
			{
				return ResponseHelper.CreateResponse(false, $"[CreateLiquid_params] : {ex.Message}", StatusCode.InternalServerError);
			}
		}

		public async Task<BaseResponse<bool>> Delete(Liquid_param model)
		{
			try
			{

				await _liquid_paramRepository.Delete(model);

				return ResponseHelper.CreateResponse(true, $"Liquid_param (id: {model.LiquidParamID}) deleted", StatusCode.Success);

			}
			catch (Exception ex)
			{
				return ResponseHelper.CreateResponse(false, $"[DeleteLiquidParam] : {ex.Message}", StatusCode.InternalServerError);
			}
		}

		public async Task<BaseResponse<bool>> Delete(int liquidParamID)
		{
			try
			{
				var liquid_param = await _liquid_paramRepository.Get().FirstOrDefaultAsync(x => x.LiquidParamID == liquidParamID);

				if (liquid_param == null)
				{
					return ResponseHelper.CreateResponse(false, $"Liquid_param (id: {liquidParamID}) not found", StatusCode.ItemNotFound);
				}

				await _liquid_paramRepository.Delete(liquid_param);
				return ResponseHelper.CreateResponse(true, $"Liquid_param (id: {liquid_param.LiquidParamID}) deleted", StatusCode.Success);
			}
			catch (Exception ex)
			{
				return ResponseHelper.CreateResponse(false, $"[DeleteLiquidParam] : {ex.Message}", StatusCode.InternalServerError);
			}
		}

		public async Task<BaseResponse<Liquid_param>> Get(int id)
		{
			try
			{
				var liquid_param = await _liquid_paramRepository.Get().FirstOrDefaultAsync(x => x.LiquidParamID == id);

				if (liquid_param == null)
				{
					return ResponseHelper.CreateResponse<Liquid_param>(null, $"Liquid_param (id: {id}) not found", StatusCode.ItemNotFound);
				}

				return ResponseHelper.CreateResponse(liquid_param, null, StatusCode.Success);
			}
			catch (Exception ex)
			{
				return ResponseHelper.CreateResponse<Liquid_param>(null, $"[GetLiquid_param (id: {id})] : {ex.Message}", StatusCode.InternalServerError);
			}
		}

		public Task<BaseResponse<IEnumerable<Liquid_param>>> GetAll()
		{
			throw new NotImplementedException();
		}

		public BaseResponse<IEnumerable<Flavor>> GetFlavors()
		{
			try
			{
				var flavors = _flavorRepository.Get().ToList();

				if (flavors == null)
				{
					return ResponseHelper.CreateResponse<IEnumerable<Flavor>>(null, $"Flavors not found", StatusCode.ItemNotFound);
				}

				return ResponseHelper.CreateResponse<IEnumerable<Flavor>>(flavors, null, StatusCode.Success);
			}
			catch (Exception ex)
			{
				return ResponseHelper.CreateResponse<IEnumerable<Flavor>>(null, $"[GetFlavors] : {ex.Message}", StatusCode.InternalServerError);
			}
		}
		public BaseResponse<IEnumerable<Nicotine>> GetNicotine()
		{
			try
			{
				var nicotines = _nicotineRepository.Get().ToList();

				if (nicotines == null)
				{
					return ResponseHelper.CreateResponse<IEnumerable<Nicotine>>(null, $"Nicotines not found", StatusCode.ItemNotFound);

				}

				return ResponseHelper.CreateResponse<IEnumerable<Nicotine>>(nicotines, null, StatusCode.Success);

			}
			catch (Exception ex)
			{
				return ResponseHelper.CreateResponse<IEnumerable<Nicotine>>(null, $"[GetNicotines] : {ex.Message}", StatusCode.InternalServerError);

			};
		}

		public Task<BaseResponse<IEnumerable<Nicotine>>> GetNicotine(int liquid_id)
		{
			throw new NotImplementedException();
		}

		public BaseResponse<IEnumerable<PG_VG>> GetPG_VG()
		{
			try
			{
				var pG_VGs = _pg_vgRepository.Get().ToList();

				if (pG_VGs == null)
				{
					return ResponseHelper.CreateResponse<IEnumerable<PG_VG>>(null, $"PG_VG not found", StatusCode.ItemNotFound);
				}

				return ResponseHelper.CreateResponse<IEnumerable<PG_VG>>(pG_VGs, null, StatusCode.Success);

			}
			catch (Exception ex)
			{
				return ResponseHelper.CreateResponse<IEnumerable<PG_VG>>(null, $"[GetPG_VGs] : {ex.Message}", StatusCode.InternalServerError);

			};
		}

		public Task<BaseResponse<IEnumerable<PG_VG>>> GetPG_VG(int liquid_id)
		{
			throw new NotImplementedException();
		}

		public Task<BaseResponse<Liquid_param>> Update(Liquid_param model)
		{
			throw new NotImplementedException();
		}
	}
}
