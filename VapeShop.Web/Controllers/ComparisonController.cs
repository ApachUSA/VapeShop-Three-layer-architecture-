using Microsoft.AspNetCore.Mvc;
using VapeShop.Domain.Entity.Functions;
using VapeShop.Service.Implementations;
using VapeShop.Service.Interfaces;

namespace VapeShop.Web.Controllers
{
	public class ComparisonController : Controller
	{

		private readonly IComparisonService _comparisonService;

		public ComparisonController(IComparisonService comprasionService)
		{
			_comparisonService = comprasionService;
		}

		[HttpGet]
		public async Task<IActionResult> ComparisonIndex()
		{

			var response = await _comparisonService.GetAll(int.Parse(HttpContext.User.FindFirst("UserID").Value));
			if (response.StatusCode == Domain.Enum.StatusCode.Success)
			{
				return View(response.Value);
			}
			return View("Error", $"{response.Description}");

		}


		public async Task<IActionResult> AddItem(int liquidID)
		{
			var response = await _comparisonService.Create(new ComparisonList { LiquidID = liquidID, UserID = int.Parse(HttpContext.User.FindFirst("UserID").Value) });
			if (response.StatusCode == Domain.Enum.StatusCode.Success)
			{
				return Ok();
			}
			return BadRequest(response.Description);
		}

		public async Task<IActionResult> DeleteItem(int comparsionListID)
		{
			var response = await _comparisonService.Delete(comparsionListID);
			if (response.StatusCode == Domain.Enum.StatusCode.Success)
			{
				return RedirectToAction("ComparisonIndex");
			}
			return View("Error", $"{response.Description}");
		}

		public async Task<IActionResult> DeleteAllItems()
		{
			var response = await _comparisonService.DeleteAll(int.Parse(HttpContext.User.FindFirst("UserID").Value));
			if (response.StatusCode == Domain.Enum.StatusCode.Success)
			{
				return RedirectToAction("ComparisonIndex");
			}
			return View("Error", $"{response.Description}");
		}

		public async Task<IActionResult> GetCount()
		{
			var response = await _comparisonService.GetCount(int.Parse(HttpContext.User.FindFirst("UserID").Value));
			if (response.StatusCode == Domain.Enum.StatusCode.Success)
			{
				return Ok(response.Value);
			}
			return View("Error", $"{response.Description}");
		}
	}
}
