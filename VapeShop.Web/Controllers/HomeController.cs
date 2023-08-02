﻿using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VapeShop.Infrastructure;
using VapeShop.Web.Models;

namespace VapeShop.Web.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly VapeShopDbContext _context;

		public HomeController(ILogger<HomeController> logger, VapeShopDbContext context)
		{
			_logger = logger;
			_context = context;
		}

		public IActionResult Index()
		{
			return View();
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