using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using VapeShop.Domain.Entity.Client;
using VapeShop.Domain.Enum;
using VapeShop.Domain.Response;
using VapeShop.Domain.ViewModels;
using VapeShop.Infrastructure.Interfaces;
using VapeShop.Service.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VapeShop.Service.Implementations
{
	public class AccountService : IAccountService
	{
		private readonly IBaseRepository<User> _userRespository;
		private readonly IBaseRepository<DeliveryAddress> _DeliveryRepository;

		public AccountService(IBaseRepository<User> userRespository, IBaseRepository<DeliveryAddress> proFileRepository)
		{
			_userRespository = userRespository;
			_DeliveryRepository = proFileRepository;
		}

		public async Task<BaseResponse<ClaimsIdentity>> Login(LoginVM model)
		{
			try
			{
				var user = _userRespository.Get().Where(x => x.Email == model.Email).FirstOrDefault();
				if (user == null)
				{
					return new BaseResponse<ClaimsIdentity>
					{
						Descrition = "User not found"
					};
				}

				if(user.Password != HashPassword(model.Password))
				{
					return new BaseResponse<ClaimsIdentity>
					{
						Descrition = "Wrong login or password"
					};
				}

				return new BaseResponse<ClaimsIdentity>
				{
					Value = Auth(user),
					StatusCode = StatusCode.Succes
				};
			}
			catch (Exception e)
			{
				return new BaseResponse<ClaimsIdentity>
				{
					Descrition = e.Message,
					StatusCode = StatusCode.InternalServerError
				};
			}
		}

		public async Task<BaseResponse<ClaimsIdentity>> Register(RegisterVM model)
		{
			try
			{
				if(_userRespository.Get().Any(x => x.Email == model.Email))
				{
					return new BaseResponse<ClaimsIdentity>()
					{
						Descrition = "User already exist"
					};
				}
			
				var NewUser = new User
				{
					Surname = model.Surname,
					Name = model.Name,
					Phone = model.Phone,
					Email = model.Email,
					Password = HashPassword(model.Password),
					PasswordConfirme = HashPassword(model.PasswordConfirme),
					CommunicationMethodID = model.CommunicationMethodID,
				};

				await _userRespository.Create(NewUser);
				var Delivery = new DeliveryAddress
				{
					UserID = NewUser.UserID,
				};

				await _DeliveryRepository.Create(Delivery);

				return new BaseResponse<ClaimsIdentity>()
				{
					Value = Auth(NewUser),
					Descrition = "User added",
					StatusCode = StatusCode.Succes

				};



			}catch(Exception e)
			{
				return new BaseResponse<ClaimsIdentity>()
				{
					Descrition = e.Message
				};
			}
		}

		private static string HashPassword(string password) => BitConverter.ToString(SHA256.HashData(Encoding.UTF8.GetBytes(password)));


		private static ClaimsIdentity Auth(User user)
		{
			var claims = new List<Claim>()
			{
				new Claim(ClaimsIdentity.DefaultNameClaimType, user.Surname + " " + user.Name),
				new Claim(ClaimsIdentity.DefaultRoleClaimType, Role.User.ToString()),

			};

			return new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

		}
	}
}
