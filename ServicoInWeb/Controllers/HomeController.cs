using Microsoft.AspNetCore.Mvc;
using ServicoInWeb.Service;
using ServicoInWeb.ViewModels;
using System.Diagnostics;

namespace ServicoInWeb.Controllers
{
	public class HomeController : BaseController
	{
		private readonly ISessionService _sectionServices;

		public HomeController(ISessionService sectionService)
		{
			_sectionServices = sectionService;
			Autenticate(_sectionServices);
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
