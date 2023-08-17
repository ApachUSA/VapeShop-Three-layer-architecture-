using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VapeShop.Domain.Entity.Client;
using VapeShop.Domain.Response;
using VapeShop.Infrastructure.Interfaces;
using VapeShop.Service.Interfaces;

namespace VapeShop.Service.Implementations
{
	public class ProfileService : IProfileService
	{
		private readonly IBaseRepository<User> _userRepository;
		private readonly IBaseRepository<City> _cityRepository;
		private readonly IBaseRepository<Region> _regionRepository;
		private readonly IBaseRepository<CommunicationMethod> _communicationMethodRepository;
		private readonly IBaseRepository<DeliveryAddress> _deliveryAddressRepository;

		public ProfileService(IBaseRepository<User> userRepository, IBaseRepository<City> cityRepository, IBaseRepository<Region> regionRepository, IBaseRepository<CommunicationMethod> communicationMethodRepository, IBaseRepository<DeliveryAddress> deliveryAddressRepository)
		{
			_userRepository = userRepository;
			_cityRepository = cityRepository;
			_regionRepository = regionRepository;
			_communicationMethodRepository = communicationMethodRepository;
			_deliveryAddressRepository = deliveryAddressRepository;
		}

		public BaseResponse<IEnumerable<City>> GetCities()
		{
			return new BaseResponse<IEnumerable<City>> { Value =  _cityRepository.Get().ToList().OrderBy(x => x.CityName) };
		}

		public BaseResponse<IEnumerable<CommunicationMethod>> GetCommunicationMethod()
		{
			return new BaseResponse<IEnumerable<CommunicationMethod>> { Value = _communicationMethodRepository.Get().ToList() };
		}

		public async Task<BaseResponse<User>> GetProfile(string name)
		{
			try
			{
				var surname_name = name.Split(' ');
				var user = _userRepository.Get()
					.Where(x => x.Surname == surname_name[0] && x.Name == surname_name[1])
					.Include(x => x.CommunicationMethod)
					.Include(x => x.DeliveryAddress)
					.FirstOrDefault();
				if(user == null)
				{
					return new BaseResponse<User>
					{
						StatusCode = Domain.Enum.StatusCode.UserNotFound,
						Descrition = "User not found",
					};
				}

				return new BaseResponse<User>
				{
					Value = user,
					StatusCode = Domain.Enum.StatusCode.Succes,
				};

			}
			catch (Exception ex)
			{
				return new BaseResponse<User>()
				{
					Descrition = ex.Message
				};
			}
		}

		public BaseResponse<IEnumerable<Region>> GetRegions()
		{
			return new BaseResponse<IEnumerable<Region>> { Value = _regionRepository.Get().ToList() };
		}

		public async Task<BaseResponse<User>> Update(User model)
		{
			try
			{
				
				await _userRepository.Update(model);

				return new BaseResponse<User>
				{
					Value = model,
					StatusCode = Domain.Enum.StatusCode.Succes,
				};

			}
			catch (Exception ex)
			{
				return new BaseResponse<User>()
				{
					Descrition = ex.Message
				};
			}
		}
	}
}
