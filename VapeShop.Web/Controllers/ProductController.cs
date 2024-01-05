using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VapeShop.Domain.Entity.Product;
using VapeShop.Domain.ViewModels;
using VapeShop.Service.Interfaces;

namespace VapeShop.Web.Controllers
{
	public class ProductController : Controller
	{

		private readonly ILiquidService liquidService;
		private readonly ILiquidParamService liquidParamService;

		public ProductController(ILiquidService liquidService, ILiquidParamService liquidParamService)
		{
			this.liquidService = liquidService;
			this.liquidParamService = liquidParamService;
		}

		public async Task<IActionResult> Index(int liquidtype, string flavor)
		{
			var response = await liquidService.GetAll();
			if (response.StatusCode == Domain.Enum.StatusCode.Success)
			{
				if (liquidtype != 0) response.Value = response.Value?.Where(x => x.LiquidType == (Domain.Enum.LiquidType)liquidtype);

				ViewBag.LiquidType = liquidtype;
				ViewBag.Flavor = flavor;

				return View("Catalog", response.Value);
			}
			return View("Error", $"{response.Description}");
		}

		public IActionResult Create()
		{
			FillViewData();
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreateLiquidVM model)
		{
			if (ModelState.IsValid)
			{
				var response = await liquidService.Create(model);
				if (response.StatusCode == Domain.Enum.StatusCode.Success)
				{
					return RedirectToAction("Index");
				}
				ModelState.AddModelError("", response.Description?? string.Empty);
			}
			return View(model);
		}

		public async Task<IActionResult> Delete(int liquid_id)
		{
			var response = await liquidService.Delete(liquid_id);
			if (response.StatusCode == Domain.Enum.StatusCode.Success)
			{
				return RedirectToAction("Index");
			}
			return RedirectToAction("Details", new { id = liquid_id });
		}


		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var response = await liquidService.Get(id);

			if (response.StatusCode == Domain.Enum.StatusCode.Success)
			{
				FillViewData();
				return View(response.Value);
			}
			return View("Error", $"{response.Description}");
		}

		[HttpPost]
		public async Task<IActionResult> Edit(Liquid model)
		{
			var response = await liquidService.Update(model);
			if (response.StatusCode == Domain.Enum.StatusCode.Success)
			{
				return RedirectToAction("Edit", new { id = response.Value?.LiquidID });
			}
			return View("Error", $"{response.Description}");
		}

		private async Task<IActionResult> IndexEdit(int liquid_id)
		{
			var liquid_response = await liquidService.Get(liquid_id);
			liquid_response.Value.Liquid_Params = liquid_response.Value?.Liquid_Params?.OrderBy(x => x.LiquidParamID).ToList();

			FillViewData();

			return PartialView("_EditPartial", liquid_response.Value);
		}

		[HttpGet]
		public async Task<IActionResult> CreateLiquid_Params(int liquid_id)
		{
			var response = await liquidParamService.Create(new Liquid_param { LiquidID = liquid_id });
			if (response.StatusCode == Domain.Enum.StatusCode.Success)
			{
				return await IndexEdit(liquid_id);
			}
			return View("Error", $"{response.Description}");
		}

		[HttpGet]
		public async Task<IActionResult> DeleteLiquid_Params(int liquid_id, int liquid_param_id)
		{
			var response = await liquidParamService.Delete(liquid_param_id);
			if (response.StatusCode == Domain.Enum.StatusCode.Success)
			{
				return await IndexEdit(liquid_id);
			}
			return View("Error", $"{response.Description}");
		}

		[HttpGet]
		public async Task<IActionResult> Details(int id)
		{
			var response = await liquidService.Get(id);
			if (response.StatusCode == Domain.Enum.StatusCode.Success)
			{
				return View("Details", response.Value);
			}
			return View("Error", $"{response.Description}");
		}

		[HttpGet]
		public async Task<IActionResult> DetailsWithParam(int id, int liquid_paramID)
		{
			var response = await liquidService.Get(id);
			if (response.StatusCode == Domain.Enum.StatusCode.Success)
			{
				var selected_param = response.Value?.Liquid_Params?.Where(x => x.LiquidParamID == liquid_paramID).FirstOrDefault();
				ViewBag.SelectedPG_VG = selected_param?.PG_VG?.PG_VG_ID;
				ViewBag.SelectedParamID = liquid_paramID;
				return View("Details", response.Value);
			}
			return View("Error", $"{response.Description}");
		}




		[HttpGet]
		public async Task<IActionResult> FilteredProducts(int liquidTypeID, string values, decimal minPrice, decimal maxPrice, string sortOption)
		{
			var filteredProducts = await liquidService.GetAll();

			if (filteredProducts.StatusCode == Domain.Enum.StatusCode.Success && filteredProducts.Value != null)
			{
				//liquidType
				if (liquidTypeID != 0) filteredProducts.Value = ApplyLiquidTypeFilter(filteredProducts.Value, liquidTypeID);

				//Flavors
				if (values != null) filteredProducts.Value = ApplyFlavorsFilter(filteredProducts.Value, values.Split(','));

				//Price
				if (filteredProducts.Value.Any()) filteredProducts.Value = ApplyPriceFilter(filteredProducts.Value, minPrice, maxPrice);

				//sort
				if (filteredProducts.Value.Any()) filteredProducts.Value = ApplySortOrder(filteredProducts.Value, sortOption);

				return PartialView("_CatalogPartial", filteredProducts.Value);
			}
			return View("Error", $"{filteredProducts.Description}");
		}




		//Help methods
		private static IEnumerable<Liquid> ApplyLiquidTypeFilter(IEnumerable<Liquid> products, int liquidTypeID) => products.Where(x => x.LiquidType == (Domain.Enum.LiquidType)liquidTypeID);

		private static IEnumerable<Liquid> ApplyFlavorsFilter(IEnumerable<Liquid> products, string[]? selectedFlavors) => products.Where(x => selectedFlavors.Contains(x.Flavor?.Flavor_name));

		private static IEnumerable<Liquid> ApplyPriceFilter(IEnumerable<Liquid> products, decimal minPrice, decimal maxPrice) => products.Where(x => x.Price >= minPrice && x.Price <= maxPrice);

		private static IEnumerable<Liquid> ApplySortOrder(IEnumerable<Liquid> products, string sortOption)
		{
			return sortOption switch
			{
                "New items below" => products.OrderBy(x => x.LiquidID),
                "New items above" => products.OrderByDescending(x => x.LiquidID),
                "From A to Z" => products.OrderBy(x => x.Name),
                "From Z to A" => products.OrderByDescending(x => x.Name),
                "Cheapest above" => products.OrderBy(x => x.Price),
                "Cheapest below" => products.OrderByDescending(x => x.Price),
				_ => products,
			};
		}

		private IEnumerable<PG_VG> GetPG_VGs() => liquidParamService.GetPG_VG().Value;
		private IEnumerable<Nicotine> GetNicotines() => liquidParamService.GetNicotine().Value;
		private IEnumerable<Flavor> GetFlavors() => liquidParamService.GetFlavors().Value;

		private void FillViewData()
		{
			var nicotinesWithInfo = GetNicotines().Select(n => new
			{
				NicotineID = n.NicotineID,
				NicotineInfo = $"{n.Nicotine_type} - {n.Nicotine_concentration}"
			}).ToList();

			ViewData["PG_VG"] = new SelectList(GetPG_VGs(), "PG_VG_ID", "PG_VG_name");
			ViewData["Nicotine"] = new SelectList(nicotinesWithInfo, "NicotineID", "NicotineInfo");
			ViewData["Flavor"] = new SelectList(GetFlavors(), "FlavorID", "Flavor_name");
			ViewData["Volume"] = new SelectList(Enum.GetValues(typeof(Domain.Enum.Volume)));
			ViewData["LiquidType"] = new SelectList(Enum.GetValues(typeof(Domain.Enum.LiquidType)));
		}


	}
}
