using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VapeShop.Domain.Entity.Client;
using VapeShop.Domain.Enum;
using VapeShop.Domain.ViewModels;
using VapeShop.Service.Interfaces;

namespace VapeShop.Web.Controllers
{
	public class AccountController : Controller
	{
		private readonly IAccountService _accountService;
		private readonly IProfileService _profileService;

		public AccountController(IAccountService accountService, IProfileService profileService)
		{
			_accountService = accountService;
			_profileService = profileService;
		}

		[HttpGet]
		public IActionResult Register()
		{
			FillSelects();
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterVM model)
		{
			if(ModelState.IsValid)
			{
				var response = await _accountService.Register(model);
				if(response.StatusCode == Domain.Enum.StatusCode.Succes)
				{
					await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new System.Security.Claims.ClaimsPrincipal(response.Value));
					return RedirectToAction("Index", "Home");

				}
				FillSelects();
				ModelState.AddModelError("", response.Descrition);
			}
			return View(model);
		}

		[HttpGet]
		public IActionResult Login() => View();

		[HttpPost]
		public async Task<IActionResult> Login(LoginVM model)
		{
			if (ModelState.IsValid)
			{
				var response = await _accountService.Login(model);
				if (response.StatusCode == Domain.Enum.StatusCode.Succes)
				{
					await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new System.Security.Claims.ClaimsPrincipal(response.Value));
					return RedirectToAction("Index", "Home");

				}
				ModelState.AddModelError("", response.Descrition);
			}
			return View(model);
		}

		[Authorize]
		[HttpGet]
		public async Task<IActionResult> Profile() 
		{
			var response = await _profileService.GetProfile(HttpContext.User.Identity.Name);
			FillSelects();
			return View(response.Value);
		}

		[HttpPost]
		public async Task<IActionResult> Profile(User model)
		{
			if (ModelState.IsValid)
			{
				var response = await _profileService.Update(model);
				if (response.StatusCode == Domain.Enum.StatusCode.Succes)
				{
					return RedirectToAction("Profile");

				}
				ModelState.AddModelError("", response.Descrition);
			}
			FillSelects();
			return View(model);
		}

		private void FillSelects()
		{
			var cities = _profileService.GetCities().Value;
			var citySelectList = cities.Select(city => new SelectListItem
			{
				Text = city.CityName,
				Value = city.CityID.ToString()
			});


			ViewBag.Cities = citySelectList;
			ViewBag.CommMethod = _profileService.GetCommunicationMethod().Value;
		}

		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Index", "Home");
		}
	}
}
