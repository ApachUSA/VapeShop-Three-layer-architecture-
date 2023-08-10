using Microsoft.AspNetCore.Mvc;
using VapeShop.Domain.ViewModels;

namespace VapeShop.Web.Controllers
{
	public class ProductController : Controller
	{
		public IActionResult Index()
		{
			return View("Details");
		}

		public IActionResult Create() => View();

		[HttpPost]
		public IActionResult Create(CreateLiquidVM model)
		{

			return Ok();
		}
	}
}
