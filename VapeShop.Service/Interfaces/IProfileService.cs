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
		Task<BaseResponse<User>> GetProfile(string name);

		Task<BaseResponse<User>> Update(User model);

		BaseResponse<IEnumerable<City>> GetCities();

		BaseResponse<IEnumerable<Region>> GetRegions();

		BaseResponse<IEnumerable<CommunicationMethod>> GetCommunicationMethod();
	}
}
