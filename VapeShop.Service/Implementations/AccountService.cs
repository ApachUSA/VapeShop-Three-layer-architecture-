using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using VapeShop.Domain.Entity.Client;
using VapeShop.Domain.Enum;
using VapeShop.Domain.Helpers;
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
				var user = await _userRespository.Get().Where(x => x.Email == model.Email).FirstOrDefaultAsync();
				if (user == null)
				{
					return ResponseHelper.CreateResponse<ClaimsIdentity>(null, "User not found", StatusCode.UserNotFound);
				}

				if(user.Password != HashPassword(model.Password))
				{
					return ResponseHelper.CreateResponse<ClaimsIdentity>(null, "Wrong login or password", StatusCode.InternalServerError);
				}

				return ResponseHelper.CreateResponse(Auth(user),null, StatusCode.Success);
			}
			catch (Exception e)
			{
				return ResponseHelper.CreateResponse<ClaimsIdentity>(null, $"[Login] : {e.Message}", StatusCode.InternalServerError);
			}
		}

		public async Task<BaseResponse<ClaimsIdentity>> Register(RegisterVM model)
		{
			try
			{
				if(_userRespository.Get().Any(x => x.Email == model.Email))
				{
					return ResponseHelper.CreateResponse<ClaimsIdentity>(null, "User already exist", StatusCode.UserAlreadyExists);
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

				return ResponseHelper.CreateResponse(Auth(NewUser), "User added", StatusCode.Success);



			}catch(Exception e)
			{
				return ResponseHelper.CreateResponse<ClaimsIdentity>(null, $"[Register] : {e.Message}", StatusCode.InternalServerError);
			}
		}

		public static string HashPassword(string password) => BitConverter.ToString(SHA256.HashData(Encoding.UTF8.GetBytes(password)));


		private static ClaimsIdentity Auth(User user)
		{
			var claims = new List<Claim>()
			{
				new Claim(ClaimsIdentity.DefaultNameClaimType, user.Surname + " " + user.Name),
				new Claim(ClaimsIdentity.DefaultRoleClaimType, Role.User.ToString()),
				new Claim("UserId", user.UserID.ToString())
			};

			return new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

		}
	}
}
