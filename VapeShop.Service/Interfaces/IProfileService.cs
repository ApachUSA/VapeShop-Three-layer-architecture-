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
		Task<BaseResponse<User>> GetProfile(int userID);

		Task<BaseResponse<User>> Update(User model);

		Task<BaseResponse<IEnumerable<City>>> GetCities();

		Task<BaseResponse<IEnumerable<Region>>> GetRegions();

		Task<BaseResponse<IEnumerable<CommunicationMethod>>> GetCommunicationMethod();
	}
}
