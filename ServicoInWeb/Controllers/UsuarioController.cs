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
            if (Session.Role == "Employee")
                return RedirectToAction("AlterarUsuario", "Usuario");

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
        public async Task<IActionResult> CriarUsuario([FromForm] CriarUsuarioViewModel model)
        {
            if (!ModelState.IsValid) 
                return View(model);
            if (model.Senha != model.ConfirmarSenha)
            {
                TempData["MensagemError"] = "Os campos de senha não coincidem";
                return View(model);
            }

            UsuarioModel novoUsuario = new(model.Nome, model.Email, model.Senha, Session.Usuario.EmpresaId, Utilitarios.GetRoleString(model.Role));

            try
            {
                _httpBase.Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Session.Token}");
                HttpResponseMessage response = _httpBase.Client.PostAsJsonAsync("api/v1/Usuario", novoUsuario).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["Sucesso"] = "Usuário criado com sucesso";
                    return View();
                }


                if (response.StatusCode == HttpStatusCode.BadRequest)
                    TempData["MensagemError"] = "Ocorreu um erro ao criar usuário, verifique se os campos estão preenchidos corretamente";
                if(response.StatusCode == HttpStatusCode.Unauthorized)
                    TempData["MensagemError"] = await response.Content.ReadAsStringAsync();

                return View(model);
            }
            catch 
            {
                TempData["MensagemError"] = "Ocorreu um erro, tente novamente mais tarde";
                return View(model);
            }
        }

        public async Task<IActionResult> AlterarUsuario(int id)
        {
            if(Session is null)
                return RedirectToAction("Index", "Login");

            try
            {
                _httpBase.Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Session.Token}");
                HttpResponseMessage response = _httpBase.Client.GetAsync($"api/v1/usuario?id={id}").Result;

                var user = await response.Content.ReadFromJsonAsync<UsuarioModel>();

                return View(new AlterarUsuarioViewModel(user.Id, user.Nome, user.Email, Utilitarios.GetRoleEnum(user.Role), user.EmpresaId, Session.Role));
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public IActionResult AlterarUsuario([FromForm]AlterarUsuarioViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            AlterarUsuarioModel user = new(model.Id, model.Nome, model.Email, Utilitarios.GetRoleString(model.Role), model.EmpresaId);
            try
            {
                _httpBase.Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Session.Token}");
                HttpResponseMessage response = _httpBase.Client.PatchAsJsonAsync("api/v1/usuario", user).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["Sucesso"] = "Usuário alterado com sucesso";
                    return View();
                }

                if(response.StatusCode == HttpStatusCode.BadRequest)
                    TempData["MensagemError"] = "Ocorreu um erro ao alterar usuário, verifique se os campos estão preenchidos corretamente";

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
