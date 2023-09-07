using Microsoft.AspNetCore.Mvc;
using VapeShop.Domain.Entity.Product;
using VapeShop.Domain.ViewModels;
using VapeShop.Service.Interfaces;

namespace VapeShop.Web.Controllers
{
	public class ProductController : Controller
	{

		private readonly ILiquidService liquidService;

		public ProductController(ILiquidService liquidService)
		{
			this.liquidService = liquidService;
		}

		public IActionResult Index(int liquidtype, string flavor)
		{
			var response = liquidService.GetAll();
			if (response.StatusCode == Domain.Enum.StatusCode.Succes)
			{
				if (liquidtype != 0) response.Value = response.Value.Where(x => x.LiquidType == (Domain.Enum.LiquidType)liquidtype);

				ViewBag.LiquidType = liquidtype;
				ViewBag.Flavor = flavor;

				return View("Catalog", response.Value);
			}
			return View("Error", $"{response.Descrition}");
		}

		public IActionResult Create() => View();

		[HttpPost]
		public async Task<IActionResult> Create(Liquid model)
		{
			if (ModelState.IsValid)
			{
				var response = await liquidService.Create(model);
				if (response.StatusCode == Domain.Enum.StatusCode.Succes)
				{
					return RedirectToAction("Index");
				}
				ModelState.AddModelError("", response.Descrition);
			}
			return View(model);
		}

		[HttpGet]
		public IActionResult Details(int id) => View("Details", liquidService.Get(id));


		[HttpGet]
		public IActionResult FilteredProducts(int liquidTypeID, string values, decimal minPrice, decimal maxPrice, string sortOption)
		{
			IEnumerable<Liquid> filteredProducts = liquidService.GetAll().Value; ;

			//liquidType
			if (liquidTypeID != 0) filteredProducts = ApplyLiquidTypeFilter(filteredProducts, liquidTypeID);

			//Flavors
			if (values != null) filteredProducts = ApplyFlavorsFilter(filteredProducts, values.Split(','));

			//Price
			if (filteredProducts.Any()) filteredProducts = ApplyPriceFilter(filteredProducts, minPrice, maxPrice);

			//sort
			if (filteredProducts.Any()) filteredProducts = ApplySortOrder(filteredProducts, sortOption);

			return PartialView("_CatalogPartial", filteredProducts);
		}




		//Help methods
		private static IEnumerable<Liquid> ApplyLiquidTypeFilter(IEnumerable<Liquid> products, int liquidTypeID) => products.Where(x => x.LiquidType == (Domain.Enum.LiquidType)liquidTypeID);

		private static IEnumerable<Liquid> ApplyFlavorsFilter(IEnumerable<Liquid> products, string[]? selectedFlavors) => products.Where(x => selectedFlavors.Contains(x.Flavor.Flavor_name));

		private static IEnumerable<Liquid> ApplyPriceFilter(IEnumerable<Liquid> products, decimal minPrice, decimal maxPrice) => products.Where(x => x.Price >= minPrice && x.Price <= maxPrice);

		private static IEnumerable<Liquid> ApplySortOrder(IEnumerable<Liquid> products, string sortOption)
		{
			return sortOption switch
			{
				"Новинки ниже" => products.OrderBy(x => x.LiquidID),
				"Новинки выше" => products.OrderByDescending(x => x.LiquidID),
				"От А до Я" => products.OrderBy(x => x.Name),
				"От Я до А" => products.OrderByDescending(x => x.Name),
				"Дешевые выше" => products.OrderBy(x => x.Price),
				"Дешевые ниже" => products.OrderByDescending(x => x.Price),
				_ => products,
			};
		}


	}
}
