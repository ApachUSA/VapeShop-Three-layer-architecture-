using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VapeShop.Domain.Entity.Client;
using VapeShop.Infrastructure;
using VapeShop.Infrastructure.Interfaces;
using VapeShop.Web.Models;

namespace VapeShop.Web.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return RedirectToAction("Index","Product");
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}