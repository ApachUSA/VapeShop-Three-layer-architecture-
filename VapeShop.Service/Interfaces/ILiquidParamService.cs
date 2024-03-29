﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VapeShop.Domain.Entity.Client;
using VapeShop.Domain.Entity.Product;
using VapeShop.Domain.Response;

namespace VapeShop.Service.Interfaces
{
	public interface ILiquidParamService
	{
		Task<BaseResponse<Liquid_param>> Get(int id);

		Task<BaseResponse<IEnumerable<Liquid_param>>> GetAll();

		Task<BaseResponse<bool>> Create(Liquid_param model);
		Task<BaseResponse<bool>> Create(List<Liquid_param> model);

		Task<BaseResponse<Liquid_param>> Update(Liquid_param model);

		Task<BaseResponse<bool>> Delete(Liquid_param model);
		Task<BaseResponse<bool>> Delete(int liquidParamID);

		BaseResponse<IEnumerable<Nicotine>> GetNicotine();
		Task<BaseResponse<IEnumerable<Nicotine>>> GetNicotine(int liquid_id);

		BaseResponse<IEnumerable<PG_VG>> GetPG_VG();
		Task<BaseResponse<IEnumerable<PG_VG>>> GetPG_VG(int liquid_id);

		BaseResponse<IEnumerable<Flavor>> GetFlavors();

	}
}
