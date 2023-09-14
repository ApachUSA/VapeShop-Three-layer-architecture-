using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VapeShop.Domain.Entity.Functions;
using VapeShop.Domain.Enum;
using VapeShop.Domain.Helpers;
using VapeShop.Domain.Response;
using VapeShop.Infrastructure.Interfaces;
using VapeShop.Service.Interfaces;

namespace VapeShop.Service.Implementations
{
	public class ComparisonService : IComparisonService
	{
		private readonly IBaseRepository<ComparisonList> _comparisonListRepository;

		public ComparisonService(IBaseRepository<ComparisonList> comparisonListRepository)
		{
			_comparisonListRepository = comparisonListRepository;
		}

		public async Task<BaseResponse<bool>> Create(ComparisonList model)
		{
			try
			{
				var response = await GetAll(model.UserID);

				if (response.Value != null && response.StatusCode == StatusCode.Success)
				{

					foreach (var item in response.Value)
					{
						if (item.UserID == model.UserID && item.LiquidID == model.LiquidID)
							return ResponseHelper.CreateResponse(false, "Item already exist in comparsion list", StatusCode.InternalServerError);
					}

				}
				await _comparisonListRepository.Create(model);

				return ResponseHelper.CreateResponse(true, "Item added to comparsion", StatusCode.Success);

			}
			catch (Exception ex)
			{
				return ResponseHelper.CreateResponse(false, $"[CreateComparsion] : {ex.Message}", StatusCode.InternalServerError);
			}
		}

		public async Task<BaseResponse<bool>> Delete(int comparsionListID)
		{
			try
			{
				var wishModel = await _comparisonListRepository.Get().FirstOrDefaultAsync(x => x.ComparisonListID.Equals(comparsionListID));
				if (wishModel == null)
				{
					return ResponseHelper.CreateResponse(false, "Item not found", StatusCode.ItemNotFound);

				}

				await _comparisonListRepository.Delete(wishModel);
				return ResponseHelper.CreateResponse(true, "Comparsion list deleted", StatusCode.Success);

			}
			catch (Exception ex)
			{
				return ResponseHelper.CreateResponse(false, $"[DeleteComparsion] : {ex.Message}", StatusCode.InternalServerError);
			}
		}

		public async Task<BaseResponse<bool>> DeleteAll(int userID)
		{
			try
			{
				var response = await GetAll(userID);
				if (response.StatusCode == StatusCode.Success && response.Value != null)
				{
					foreach (var item in response.Value)
					{
						await _comparisonListRepository.Delete(item);
					}
				}
				else
				{
					return ResponseHelper.CreateResponse(false, response.Description, StatusCode.InternalServerError);
				}



				return ResponseHelper.CreateResponse(true, "Comparsion list deleted", StatusCode.Success);
			}
			catch (Exception ex)
			{
				return ResponseHelper.CreateResponse(false, $"[DeleteAllComparsion] : {ex.Message}", StatusCode.InternalServerError);
			}
		}

		public async Task<BaseResponse<ComparisonList>> Get(int userID)
		{
			try
			{
				var comparsionModel = await _comparisonListRepository.Get()
					.FirstOrDefaultAsync(x => x.UserID.Equals(userID));
				if (comparsionModel == null)
				{
					return ResponseHelper.CreateResponse(comparsionModel, "Comparsion list not found", StatusCode.ItemNotFound);
				}

				return ResponseHelper.CreateResponse(comparsionModel, null, StatusCode.Success);
			}
			catch (Exception ex)
			{
				return ResponseHelper.CreateResponse<ComparisonList>(null, $"[GetComparsion] : {ex.Message}", StatusCode.InternalServerError);
			}
		}

		public async Task<BaseResponse<IEnumerable<ComparisonList>>> GetAll(int userID)
		{
			try
			{
				var comparsionModel = await _comparisonListRepository.Get()
					.Include(x => x.Liquid)
					.ThenInclude(x => x.Flavor)
					.ToListAsync();

				if (!comparsionModel.Any())
				{
					return ResponseHelper.CreateResponse<IEnumerable<ComparisonList>>(null, "Comparsion lists not found", StatusCode.Success);
				}

				return ResponseHelper.CreateResponse<IEnumerable<ComparisonList>>(comparsionModel, null, StatusCode.Success);
			}
			catch (Exception ex)
			{
				return ResponseHelper.CreateResponse<IEnumerable<ComparisonList>>(null, $"[GetAllComparsion] : {ex.Message}", StatusCode.InternalServerError);
			}
		}

		public async Task<BaseResponse<int>> GetCount(int userID)
		{
			var response = await GetAll(userID);
			if (response.StatusCode == StatusCode.Success && response.Value != null)
			{
				return ResponseHelper.CreateResponse(response.Value.Count(), null, StatusCode.Success);
			}
			return ResponseHelper.CreateResponse(0, null, StatusCode.Success);
		}
	}
}
