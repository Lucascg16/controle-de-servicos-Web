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
        public readonly IEmailService _EmailService;

        public SenhaController(ISessionService sessionService, IHttpBaseModel httpBase, IEmailService emailService)
        {
            _sessionService = sessionService;
            _httpBase = httpBase;
            _EmailService = emailService;
            Autenticate(_sessionService);
        }

        public IActionResult Index()
        {
            if (Session is null)
                RedirectToAction("Index", "Login");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm] AlterarSenhaViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            if (model.novaSenha != model.confirmarSenha)
            {
                TempData["MensagemError"] = "As senhas não conferem";
                return View(model);
            }

            try
            {
                _httpBase.Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Session.Token}");
                var request = new HttpRequestMessage(HttpMethod.Patch, $"api/v1/usuario/password?id={Session.Id}&senha={model.senha}&novaSenha={model.novaSenha}");
                HttpResponseMessage response = await _httpBase.Client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Sucesso"] = "Senha alterada com sucesso";
                    return View(model);
                }

                if (response.StatusCode == HttpStatusCode.BadRequest)
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

        public IActionResult EsqueciSenha()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EsqueciSenha([FromForm] EsqueciSenhaViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, $"api/v1/auth/GeneratePassToken?email={model.Email}");
                HttpResponseMessage response = await _httpBase.Client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    _EmailService.SendEmailAsync(model.Email, await response.Content.ReadAsStringAsync());
                    TempData["Sucesso"] = "Um email com o link de redefinição de senha foi enviado, verifique a caixa de span";
                    return View(model);
                }
                TempData["MensagemError"] = await response.Content.ReadAsStringAsync();
                return View(model);
            }
            catch (Exception ex)
            {
                TempData["MensagemError"] = "Ocorreu um erro, tente novamente mais tarde";
                return View(model);
            }
        }
    }
}
