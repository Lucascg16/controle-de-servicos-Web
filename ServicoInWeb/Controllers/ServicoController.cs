using Microsoft.AspNetCore.Mvc;
using ServicoInWeb.Models;
using ServicoInWeb.Service;
using ServicoInWeb.ViewModels;

namespace ServicoInWeb.Controllers
{
    public class ServicoController : BaseController
    {
        private readonly IHttpBaseModel _httpBase;
        private readonly ISessionService _sessionService;
        public ServicoController(IHttpBaseModel httpBase, ISessionService session)
        {
            _httpBase = httpBase;
            _sessionService = session;
            Autenticate(_sessionService);
        }

        public async Task<IActionResult> Index(bool open = false, int page = 1, int itensperpage = 10)
        {
            if(Session is null)
                return RedirectToAction("Index", "Login");

            List<ServicoModel> list = new();
            try
            {
                _httpBase.Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Session.Token}");
                HttpResponseMessage response = new();
                if (!open)
                    response = _httpBase.Client.GetAsync($"api/v1/servico/all?empresaId={Session.Usuario.EmpresaId}&page={page}&itensPerPage={itensperpage}").Result;
                else
                    response = _httpBase.Client.GetAsync($"api/v1/servico/open?empresaId={Session.Usuario.EmpresaId}&page={page}&itensPerPage={itensperpage}").Result;

                if (response.IsSuccessStatusCode)
                {
                    list = await response.Content.ReadFromJsonAsync<List<ServicoModel>>() ?? new();
                    ServicoViewModel view = new(list);
                    return View(view);
                }

                return View(new ServicoViewModel([]));
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public IActionResult CriarServico()
        {
            if (Session is null)
                return RedirectToAction("Index", "Login");

            return View();
        }

        [HttpPost]
        public IActionResult CriarServico([FromForm] CriarServicoViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                model.UsuarioId = Session.Usuario.Id;
                model.EmpresaId = Session.Usuario.EmpresaId;

                _httpBase.Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Session.Token}");
                HttpResponseMessage response = _httpBase.Client.PostAsJsonAsync("api/v1/servico", model).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["Sucesso"] = "Serviço iniciado com sucesso, redirecionando para a pagina anterior";
                    return View(model);
                }

                TempData["MensagemError"] = "Algo deu errodo ao iniciar o serviço, tente novamente mais tarde";
                return View(model);
            }
            catch
            {
                TempData["MensagemError"] = "Ocorreu um erro, tente novamente mais tarde";
                return View(model);
            }
        }
    }
}
