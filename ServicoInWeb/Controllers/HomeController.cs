using Microsoft.AspNetCore.Mvc;
using ServicoInWeb.Models;
using ServicoInWeb.Service;
using System.Diagnostics;

namespace ServicoInWeb.Controllers
{
	public class HomeController : BaseController
	{
		private readonly ISectionService _sectionServices;

		public HomeController(ISectionService sectionService)
		{
			_sectionServices = sectionService;
			Autenticate(_sectionServices);
		}

		public IActionResult Index()
		{
			if(Session is null)
				return RedirectToAction("Index", "Login");

            return View();
		}

		public IActionResult Privacy()
		{
			if (Session is null)
				return RedirectToAction("Index", "Login");

			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
