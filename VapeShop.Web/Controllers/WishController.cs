﻿using Azure;
using Microsoft.AspNetCore.Mvc;
using VapeShop.Domain.Entity.Client;
using VapeShop.Domain.Entity.Functions;
using VapeShop.Infrastructure.Interfaces;
using VapeShop.Service.Implementations;
using VapeShop.Service.Interfaces;
using VapeShop.Web.Models;

namespace VapeShop.Web.Controllers
{
	public class WishController : Controller
	{

		private readonly IWishService wishService;

		public WishController(IWishService wishService)
		{
			this.wishService = wishService;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{

			var response = await wishService.GetAll(int.Parse(HttpContext.User.FindFirst("UserID").Value));

			if (response.StatusCode == Domain.Enum.StatusCode.Succes)
			{
				return View(response.Value);
			}
			return View("Error", $"{response.Description}");

		}

		
		public async Task<IActionResult> AddItem(int liquidID, int? liquid_pramaID = null)
		{
			var response = await wishService.Create(new WishList { LiquidID = liquidID, Liquid_paramID = liquid_pramaID, UserID = int.Parse(HttpContext.User.FindFirst("UserID").Value) });
			if (response.StatusCode == Domain.Enum.StatusCode.Succes)
			{
				return Ok();
			}
			return BadRequest(response.Description);
		}

		public async Task<IActionResult> DeleteItem(int wishListID)
		{
			var response = await wishService.Delete(wishListID);
			if (response.StatusCode == Domain.Enum.StatusCode.Succes)
			{
				return RedirectToAction("Index");
			}
			return View("Error", $"{response.Description}");
		}
	}
}