using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VapeShop.Domain.Entity.Client;
using VapeShop.Domain.Enum;
using VapeShop.Domain.Helpers;
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

		public async Task<BaseResponse<IEnumerable<City>>> GetCities()
		{
			 return ResponseHelper.CreateResponse<IEnumerable<City>>(await _cityRepository.Get().OrderBy(x => x.CityName).ToListAsync(), null, StatusCode.Success);
		}

		public async Task<BaseResponse<IEnumerable<CommunicationMethod>>> GetCommunicationMethod()
		{
			return ResponseHelper.CreateResponse<IEnumerable<CommunicationMethod>>(await _communicationMethodRepository.Get().ToListAsync(), null, StatusCode.Success);
		}


		public async Task<BaseResponse<IEnumerable<Region>>> GetRegions()
		{
			return ResponseHelper.CreateResponse<IEnumerable<Region>>(await _regionRepository.Get().ToListAsync(), null, StatusCode.Success);
		}

		public async Task<BaseResponse<User>> GetProfile(int userID)
		{
			try
			{
				var user = await _userRepository.Get()
					.Where(x => x.UserID == userID)
					.Include(x => x.CommunicationMethod)
					.Include(x => x.DeliveryAddress)
					.FirstOrDefaultAsync();

				if(user == null)
				{
					return ResponseHelper.CreateResponse<User>(null, "User not found", StatusCode.UserNotFound);
				}

				return ResponseHelper.CreateResponse(user, null, StatusCode.Success);

			}
			catch (Exception ex)
			{
				return ResponseHelper.CreateResponse<User>(null, $"[GetProfile] : {ex.Message}", StatusCode.InternalServerError);
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
				if(model.Password.Length < 30)
				{
					model.Password = AccountService.HashPassword(model.Password);
					model.PasswordConfirme = AccountService.HashPassword(model.PasswordConfirme);
				}
				await _userRepository.Update(model);

				return ResponseHelper.CreateResponse(model, null, StatusCode.Success);

			}
			catch (Exception ex)
			{
				return ResponseHelper.CreateResponse<User>(null, $"[UpdateProfile] : {ex.Message}", StatusCode.InternalServerError);
			}
		}
	}
}
