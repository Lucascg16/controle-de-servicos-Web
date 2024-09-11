using Microsoft.AspNetCore.Mvc;
using ServicoInWeb.Models;

namespace ServicoInWeb.Controllers
{
    public class LoginController : Controller
	{
		private readonly IHttpBaseModel _httpService;
		private readonly string url;
        public LoginController(IHttpBaseModel httpService)
        {
            _httpService = httpService;
            url = "api/v1/auth";
        }

        public IActionResult Index()
		{
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
                var token = await response.Content.ReadAsStringAsync();
                return RedirectToAction("Index", "Home");
            }

			login.Mensagem = await response.Content.ReadAsStringAsync();

            return View(login);
        }
	}
}
