using Microsoft.AspNetCore.Mvc;

namespace VapeShop.Web.Controllers
{
	public class OrderController : Controller
	{
		[HttpPost]
		public IActionResult Index(int liquid_paramID, int count)
		{
			return View();
		}
	}
}
