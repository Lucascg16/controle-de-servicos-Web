using Microsoft.AspNetCore.Mvc;
using ServicoInWeb.Models;
using ServicoInWeb.Service;
using ServicoInWeb.ViewModels;
using System.Net;

namespace ServicoInWeb.Controllers
{
    public class UsuarioController : BaseController
    {
        private readonly IHttpBaseModel _httpBase;
        private readonly ISessionService _sectionService;

        public UsuarioController(IHttpBaseModel httpBase, ISessionService sectionService)
        {
            _httpBase = httpBase;
            _sectionService = sectionService;
            Autenticate(_sectionService);
        }

        public async Task<IActionResult> Index(int page = 1, int itensperpage = 10)
        {
            if(Session is null)
                return RedirectToAction("Index", "Login");

            List<UsuarioModel>? userList = [];
            try
            {
                _httpBase.Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Session.Token}");
                HttpResponseMessage response = _httpBase.Client.GetAsync($"api/v1/usuario/All?empresaId={Session.Usuario.EmpresaId}&page={page}&itensPerPage={itensperpage}").Result;

                if (response.IsSuccessStatusCode)
                {
                    userList = await response.Content.ReadFromJsonAsync<List<UsuarioModel>>();
                }

                UsuarioViewModel view = new(userList);

                return View(view);
            }
            catch
            { 
                return RedirectToAction("Error", "Home");
            }
        }

        public IActionResult CriarUsuario()
        {
            if (Session is null)
                return RedirectToAction("Index", "Login");

            return View();
        }

        [HttpPost]
        public IActionResult CriarUsuario([FromForm] CriarUsuarioViewModel criarUsuario)
        {
            if (!ModelState.IsValid) 
                return View(criarUsuario);
            if (criarUsuario.Senha != criarUsuario.ConfirmarSenha)
            {
                TempData["MensagemError"] = "Os campos de senha não coincidem";
                return View(criarUsuario);
            }

            UsuarioModel novoUsuario = new(criarUsuario.Nome, criarUsuario.Email, criarUsuario.Senha, Session.Usuario.EmpresaId, Utilitarios.GetRoleString(criarUsuario.Role));

            try
            {
                _httpBase.Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Session.Token}");
                HttpResponseMessage response = _httpBase.Client.PostAsJsonAsync("api/v1/Usuario", novoUsuario).Result;

                if (response.IsSuccessStatusCode)//adicionar regra de negocio para verificar se o email ja existe na empresa
                {
                    TempData["Sucesso"] = "Usuário criado com sucesso";
                    return View();
                }

                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    TempData["MensagemError"] = "Ocorreu um erro ao criar usuário, verifique se os campos estão preenchidos corretamente";
                }
                return View(criarUsuario);
            }
            catch 
            {
                TempData["MensagemError"] = "Ocorreu um erro, tente novamente mais tarde";
                return View(criarUsuario);
            }
        }
    }
}
