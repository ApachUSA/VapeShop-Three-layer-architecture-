using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VapeShop.Domain.Entity.Product;
using VapeShop.Domain.Enum;
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
				return new BaseResponse<bool>()
				{
					Value = true,
					Description = "Liquid_param added",
					StatusCode = StatusCode.Succes

				};
			}
			catch (Exception ex)
			{
				return new BaseResponse<bool>()
				{
					Description = $"[CreateLiquid_param] : {ex.Message}",
					StatusCode = StatusCode.InternalServerError

				};
			}
		}

		public async Task<BaseResponse<bool>> Create(List<Liquid_param> model)
		{
			try
			{
				foreach (var param in model) await _liquid_paramRepository.Create(param);
				return new BaseResponse<bool>()
				{
					Value = true,
					Description = "Liquid_params added",
					StatusCode = StatusCode.Succes

				};
			}
			catch (Exception ex)
			{
				return new BaseResponse<bool>()
				{
					Description = $"[CreateLiquid_params] : {ex.Message}",
					StatusCode = StatusCode.InternalServerError

				};
			}
		}

		public async Task<BaseResponse<bool>> Delete(Liquid_param model)
		{
			try
			{

				await _liquid_paramRepository.Delete(model);
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
					Description = $"[DeleteLiquidParam] : {ex.Message}",
					StatusCode = StatusCode.InternalServerError

				};
			}
		}

		public async Task<BaseResponse<bool>> Delete(int liquidParamID)
		{
			try
			{
				var liquid_param = await _liquid_paramRepository.Get().FirstOrDefaultAsync(x => x.LiquidParamID == liquidParamID);
				if (liquid_param != null)
				{
					await _liquid_paramRepository.Delete(liquid_param);
					return new BaseResponse<bool>()
					{
						Value = true,
						StatusCode = StatusCode.Succes

					};
				}
				return new BaseResponse<bool>()
				{
					Value = false,
					StatusCode = StatusCode.InternalServerError

				};
			}
			catch (Exception ex)
			{
				return new BaseResponse<bool>()
				{
					Description = $"[DeleteLiquidParam] : {ex.Message}",
					StatusCode = StatusCode.InternalServerError

				};
			}
		}

		public async Task<BaseResponse<Liquid_param>> Get(int id)
		{
			try
			{
				var liquid = await _liquid_paramRepository.Get().FirstOrDefaultAsync(x => x.LiquidParamID == id);

				if (liquid == null)
				{
					return new BaseResponse<Liquid_param>()
					{
						Description = "Liquid_param not found",
						StatusCode = StatusCode.InternalServerError

					};
				}

				return new BaseResponse<Liquid_param>()
				{
					Value = liquid,
					StatusCode = StatusCode.Succes
				};
			}
			catch (Exception ex)
			{

				return new BaseResponse<Liquid_param>()
				{
					Description = $"[GetLiquid_param] : {ex.Message}",
					StatusCode = StatusCode.InternalServerError

				};
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
					return new BaseResponse<IEnumerable<Flavor>>()
					{
						Description = "Flavors not found",
						StatusCode = StatusCode.InternalServerError

					};
				}

				return new BaseResponse<IEnumerable<Flavor>>()
				{
					Value = flavors,
					StatusCode = StatusCode.Succes
				};
			}
			catch (Exception ex)
			{

				return new BaseResponse<IEnumerable<Flavor>>()
				{
					Description = $"[GetFlavors] : {ex.Message}",
					StatusCode = StatusCode.InternalServerError


				};
			}
		}
		public BaseResponse<IEnumerable<Nicotine>> GetNicotine()
		{
			try
			{
				var nicotines = _nicotineRepository.Get().ToList();

				if (nicotines == null)
				{
					return new BaseResponse<IEnumerable<Nicotine>>()
					{
						Description = "Nicotines not found",
						StatusCode = StatusCode.InternalServerError

					};
				}

				return new BaseResponse<IEnumerable<Nicotine>>()
				{
					Value = nicotines,
					StatusCode = StatusCode.Succes
				};
			}
			catch (Exception ex)
			{

				return new BaseResponse<IEnumerable<Nicotine>>()
				{
					Description = $"[GetNicotine] : {ex.Message}",
					StatusCode = StatusCode.InternalServerError

				};
			}
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
					return new BaseResponse<IEnumerable<PG_VG>>()
					{
						Description = "Nicotines not found",
						StatusCode = StatusCode.InternalServerError

					};
				}

				return new BaseResponse<IEnumerable<PG_VG>>()
				{
					Value = pG_VGs,
					StatusCode = StatusCode.Succes
				};
			}
			catch (Exception ex)
			{

				return new BaseResponse<IEnumerable<PG_VG>>()
				{
					Description = $"[GetPG_VG] : {ex.Message}",
					StatusCode = StatusCode.InternalServerError

				};
			}
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
