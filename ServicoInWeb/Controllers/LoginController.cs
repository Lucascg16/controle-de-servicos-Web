using Microsoft.AspNetCore.Mvc;
using ServicoInWeb.Models;
using ServicoInWeb.Service;
using ServicoInWeb.ViewModels;
using System.Net;

namespace ServicoInWeb.Controllers
{
    public class LoginController : Controller
	{
		private readonly IHttpBaseModel _httpService;
		private readonly ISessionService _sectionService;
        public LoginController(IHttpBaseModel httpService, ISessionService sectionService)
        {
            _httpService = httpService;
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

			try
			{
                HttpResponseMessage response = _httpService.Client.PostAsJsonAsync("api/v1/auth", login).Result;

                if (response.IsSuccessStatusCode)
                {
                    await _sectionService.CreateUserSection(await response.Content.ReadAsStringAsync());
                    return RedirectToAction("Index", "Home");
                }
                TempData["MensagemError"] = await response.Content.ReadAsStringAsync();
            }
            catch(Exception ex)
			{
				TempData["MensagemError"] = "Há algo de errado com o sistema, por favor entre em contato com o administrador" + ex.Message;
			}

            return View(login);
        }

		public IActionResult CreateAccount()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreateAccount([FromForm] CreateAccountViewModel model)
		{
			if(!ModelState.IsValid)
				return View(model);

			CreateAccountModel accountModel = new(model);
			try
			{
                HttpResponseMessage response = _httpService.Client.PostAsJsonAsync("api/v1/auth/create", accountModel).Result;
                if (response.IsSuccessStatusCode)
                    TempData["Sucesso"] = "Conta criada com sucesso, redirecionando para o login.";
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                    TempData["MensagemError"] = await response.Content.ReadAsStringAsync();
            }
            catch
			{
                TempData["MensagemError"] = "Algo deu errado, tente novamente mais tarde";
            }

            return View(model);
		}
        public ActionResult Logout() 
		{
			_sectionService.DesableUserSection();

			return RedirectToAction("index");
		}
	}
}
