using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VapeShop.Domain.Entity.Client;
using VapeShop.Domain.Response;

namespace VapeShop.Service.Interfaces
{
	public interface IProfileService
	{
		Task<BaseResponse<User>> GetProfile(int id);

		Task<BaseResponse<User>> Update(int id);

		BaseResponse<IEnumerable<City>> GetCities();

		BaseResponse<IEnumerable<Region>> GetRegions();

		BaseResponse<IEnumerable<CommunicationMethod>> GetCommunicationMethod();
	}
}
