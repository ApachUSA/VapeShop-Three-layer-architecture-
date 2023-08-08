using Microsoft.AspNetCore.Mvc;

namespace VapeShop.Web.Controllers
{
	public class Product : Controller
	{
		public IActionResult Index()
		{
			return View("Catalog");
		}
	}
}
