using Microsoft.AspNetCore.Mvc;
using ServicoInWeb.Models;
using ServicoInWeb.Service;
using ServicoInWeb.ViewModels;
using System.Net;

namespace ServicoInWeb.Controllers
{
    public class SenhaController : BaseController
    {
        public readonly IHttpBaseModel _httpBase;
        public readonly ISessionService _sessionService;
        public SenhaController(ISessionService sessionService, IHttpBaseModel httpBase) 
        {
            _sessionService = sessionService;
            _httpBase = httpBase;
            Autenticate(_sessionService);
        }

        public IActionResult Index()
        {
            if(Session is null) 
                RedirectToAction("Index", "Login");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm] AlterarSenhaViewModel model)
        {
            if(!ModelState.IsValid)
                return View(model);
            if(model.novaSenha != model.confirmarSenha)
            {
                TempData["MensagemError"] = "As senhas não conferem";
                return View(model);
            }

            try
            {
                _httpBase.Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Session.Token}");
                var request = new HttpRequestMessage(HttpMethod.Patch,$"api/v1/usuario/password?id={Session.Id}&senha={model.senha}&novaSenha={model.novaSenha}");
                HttpResponseMessage response = await _httpBase.Client.SendAsync(request);

                if(response.IsSuccessStatusCode)
                {
                    TempData["Sucesso"] = "Senha alterada com sucesso";
                    return View(model);
                }

                if(response.StatusCode == HttpStatusCode.BadRequest)
                {
                    TempData["MensagemError"] = await response.Content.ReadAsStringAsync();
                    return View(model);
                }

                TempData["MensagemErro"] = response.Content.ReadAsStringAsync();
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
