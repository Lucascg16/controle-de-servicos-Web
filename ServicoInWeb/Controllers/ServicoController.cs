﻿using Microsoft.AspNetCore.Mvc;
using ServicoInWeb.Models;
using ServicoInWeb.Service;
using ServicoInWeb.ViewModels;
using System.Net;

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

        public async Task<IActionResult> Index(bool filterClose = false, int page = 1, int itensperpage = 10)
        {
            if(Session is null)
                return RedirectToAction("Index", "Login");

            List<ServicoModel> list = new();
            try
            {
                _httpBase.Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Session.Token}");
                HttpResponseMessage response = new();
                if (filterClose)
                    response = _httpBase.Client.GetAsync($"api/v1/servico/close?empresaId={Session.Usuario.EmpresaId}&page={page}&itensPerPage={itensperpage}").Result;
                else
                    response = _httpBase.Client.GetAsync($"api/v1/servico/open?empresaId={Session.Usuario.EmpresaId}&page={page}&itensPerPage={itensperpage}").Result;

                if (response.IsSuccessStatusCode)
                {
                    list = await response.Content.ReadFromJsonAsync<List<ServicoModel>>() ?? [];
                    ServicoViewModel view = new(list, filterClose);
                    return View(view);
                }

                return View(new ServicoViewModel([], filterClose));
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
                    TempData["Sucesso"] = "Serviço iniciado com sucesso, redirecionando para a página anterior";
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

        public async Task<IActionResult> AlterarServico(int id)
        {
            if(Session is null)
                return RedirectToAction("Index", "Login");

            try
            {
                _httpBase.Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Session.Token}");
                HttpResponseMessage response = _httpBase.Client.GetAsync($"api/v1/servico?id={id}").Result;

                var servico = await response.Content.ReadFromJsonAsync<AlterarServicoModel>() ?? new();

                return View(servico);
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public IActionResult AlterarServico([FromForm]AlterarServicoModel model)
        {
            if(!ModelState.IsValid)
                return View(model);

            try
            {
                _httpBase.Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Session.Token}");
                HttpResponseMessage response = _httpBase.Client.PatchAsJsonAsync("api/v1/servico", model).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["Sucesso"] = "Servico alterado com sucesso, redirecionando para a página anterior";
                }

                if (response.StatusCode == HttpStatusCode.BadRequest)
                    TempData["MensagemError"] = "Ocorreu um erro ao atualizar o serviço.";

                return View(model);
            }
            catch
            {
                TempData["MensagemError"] = "Algo deu errado, tente novamente mais tarde";
                return View(model);
            }
        }

        [HttpPost]
        public IActionResult FinalizarServico(int Id, double Faturamento) 
        {
            try
            {
                _httpBase.Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Session.Token}");
                var request = new HttpRequestMessage(HttpMethod.Patch, $"api/v1/servico/finalizar?servicoId={Id}&faturamento={Faturamento}");
                _httpBase.Client.SendAsync(request);
                return Ok();
            }
            catch
            { 
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult CancelarServico(int Id)
        {
            try
            {
                _httpBase.Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Session.Token}");
                var request = new HttpRequestMessage(HttpMethod.Patch, $"api/v1/servico/cancel?servicoId={Id}");
                _httpBase.Client.SendAsync(request);
                return Ok();
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
