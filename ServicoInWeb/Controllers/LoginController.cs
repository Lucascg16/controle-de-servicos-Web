using Microsoft.AspNetCore.Mvc;
using ServicoInWeb.Models;
using ServicoInWeb.Service;

namespace ServicoInWeb.Controllers
{
    public class LoginController : Controller
	{
		private readonly IHttpBaseModel _httpService;
		private readonly ISectionService _sectionService;
		private readonly string url;
        public LoginController(IHttpBaseModel httpService, ISectionService sectionService)
        {
            _httpService = httpService;
            url = "api/v1/auth";
            _sectionService = sectionService;
        }

        public IActionResult Index()
		{
			if(_sectionService.GetSection() != null)
                return RedirectToAction("Index", "Home");

            return View();
		}

		[HttpPost]
		public async Task<IActionResult> Index([FromForm] LoginViewModel login)
		{
			if (!ModelState.IsValid)
				return View(login);

			HttpResponseMessage response = _httpService.Client.PostAsJsonAsync(url, login).Result;

			if (response.IsSuccessStatusCode) 
			{
                _sectionService.CreateUserSection(await response.Content.ReadAsStringAsync());
                return RedirectToAction("Index", "Home");
            }

			TempData["MensagemError"] = await response.Content.ReadAsStringAsync();

            return View(login);
        }

		public ActionResult Logout() 
		{
			_sectionService.DesableUserSection();

			return RedirectToAction("index");
		}
	}
}
