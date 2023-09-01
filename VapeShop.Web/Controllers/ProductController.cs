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

		public IActionResult Index()
		{
			var response = liquidService.GetAll();
			if (response.StatusCode == Domain.Enum.StatusCode.Succes)
			{
				return View("Catalog",response.Value);
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
			return  View(model);
		}

		[HttpGet]
		public IActionResult Details(int id) => View("Details", liquidService.Get(id));


		[HttpGet]
		public IActionResult FilteredProducts(string filter)
		{
			// Здесь выполняйте логику фильтрации товаров на основе переданного фильтра
			var filteredProducts = liquidService.GetAll().Value.Where(x => x.FlavorID == 2);

			return PartialView("_CatalogPartial", filteredProducts);
		}
	}
}
