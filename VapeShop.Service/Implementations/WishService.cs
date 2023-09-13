using Azure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VapeShop.Domain.Entity.Functions;
using VapeShop.Domain.Entity.Product;
using VapeShop.Domain.Enum;
using VapeShop.Domain.Helpers;
using VapeShop.Domain.Response;
using VapeShop.Infrastructure.Interfaces;
using VapeShop.Service.Interfaces;

namespace VapeShop.Service.Implementations
{
	public class WishService : IWishService
	{
		private readonly IBaseRepository<WishList> _wishRepository;

		public WishService(IBaseRepository<WishList> wishRepository)
		{
			_wishRepository = wishRepository;
		}

		public async Task<BaseResponse<bool>> Create(WishList model)
		{
			try
			{
				var response = await GetAll(model.UserID);

				if(response.Value != null &&  response.StatusCode == StatusCode.Succes)
				{
					foreach (var item in response.Value)
					{
						if(item.UserID == model.UserID && item.LiquidID == model.LiquidID && item.Liquid_paramID == model.Liquid_paramID)
							return ResponseHelper.CreateResponse(false, "Item already exist in wish list", StatusCode.InternalServerError);
					}
					
				}
				await _wishRepository.Create(model);

				return ResponseHelper.CreateResponse(true, "Wish added", StatusCode.Succes);

			}
			catch (Exception ex)
			{
				return ResponseHelper.CreateResponse(false, $"[CreateWish] : {ex.Message}", StatusCode.InternalServerError);
			}
		}

		public async Task<BaseResponse<bool>> Delete(int wishListID)
		{
			try
			{
				var wishModel = await _wishRepository.Get().FirstOrDefaultAsync(x => x.WishListID.Equals(wishListID));
				if(wishModel == null)
				{
					return ResponseHelper.CreateResponse(false, "Wish not found", StatusCode.InternalServerError);

				}

				await _wishRepository.Delete(wishModel);
				return ResponseHelper.CreateResponse(true, "Wish deleted", StatusCode.Succes);

			}
			catch (Exception ex)
			{
				return ResponseHelper.CreateResponse(false, $"[DeleteWish] : {ex.Message}", StatusCode.InternalServerError);
			}
		}

		public async Task<BaseResponse<bool>> DeleteAll(int userID)
		{
			try
			{
				IEnumerable<WishList> wishLists;

				var response = await GetAll(userID);
				if (response.StatusCode == StatusCode.Succes)
				{
					wishLists = response.Value;
				}
				else
				{
					return ResponseHelper.CreateResponse(false, response.Description, StatusCode.InternalServerError);
				}

				foreach (var item in wishLists)
				{
					await _wishRepository.Delete(item);
				}

				return ResponseHelper.CreateResponse(true, "Wishs deleted", StatusCode.Succes);
			}
			catch (Exception ex)
			{
				return ResponseHelper.CreateResponse(false, $"[DeleteAllWishs] : {ex.Message}", StatusCode.InternalServerError);
			}
		}


		public async Task<BaseResponse<WishList>> Get(int userID)
		{
			try
			{
				var wishModel = await _wishRepository.Get().Include(x => x.Liquid).Include(x => x.Liquid_param).FirstOrDefaultAsync(x => x.UserID.Equals(userID));
				if (wishModel == null)
				{
					return ResponseHelper.CreateResponse<WishList>(wishModel, "Wish not found", StatusCode.InternalServerError);
				}

				return ResponseHelper.CreateResponse(wishModel, null, StatusCode.Succes);
			}
			catch (Exception ex)
			{
				return ResponseHelper.CreateResponse<WishList>(null, $"[GetWish] : {ex.Message}", StatusCode.InternalServerError);
			}
		}

		public async Task<BaseResponse<IEnumerable<WishList>>> GetAll(int userID)
		{
			try
			{
				var wishModel = await _wishRepository.Get()
					.Where(x => x.UserID.Equals(userID))
					.Include(x => x.Liquid_param)
					.ThenInclude(x => x.Nicotine)
					.Include(x => x.Liquid)
					.ThenInclude(x => x.Flavor)
					.ToListAsync();

				if (!wishModel.Any())
				{
					return ResponseHelper.CreateResponse<IEnumerable<WishList>>(null, "Wishs not found", StatusCode.Succes);
				}

				return ResponseHelper.CreateResponse<IEnumerable<WishList>>(wishModel, null, StatusCode.Succes);
			}
			catch (Exception ex)
			{
				return ResponseHelper.CreateResponse<IEnumerable<WishList>>(null, $"[GetAllWish] : {ex.Message}", StatusCode.InternalServerError);
			}
		}

	}
}