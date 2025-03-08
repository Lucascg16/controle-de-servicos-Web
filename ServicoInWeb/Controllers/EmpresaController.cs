using Microsoft.AspNetCore.Mvc;
using ServicoInWeb.Models;
using ServicoInWeb.Service;

namespace ServicoInWeb.Controllers
{
    public class EmpresaController : BaseController
    {
        private readonly IHttpBaseModel _httpBase;
        private readonly ISessionService _sessionService;

        public EmpresaController(IHttpBaseModel httpBase, ISessionService session)
        {
            _httpBase = httpBase;
            _sessionService = session;
            Autenticate(_sessionService);
        }
        public async Task<IActionResult> Index()
        {
            if (Session is null)
                return RedirectToAction("Index", "Login");
            if (Session.Role == "Employee")
                return RedirectToAction("Index", "Home");

            try
            {
                _httpBase.Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Session.Token}");
                HttpResponseMessage response = _httpBase.Client.GetAsync($"api/v1/empresa?id={Session.Usuario.EmpresaId}").Result;

                if (response.IsSuccessStatusCode)
                {
                    var empresa = await response.Content.ReadFromJsonAsync<EmpresaModel>() ?? new();
                    return View(FormatDocumentos(empresa));
                }

                return View();
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public EmpresaModel FormatDocumentos(EmpresaModel empresa){
            if(!string.IsNullOrEmpty(empresa.Cnpj)) empresa.Cnpj = Convert.ToUInt64(empresa.Cnpj).ToString(@"00\.000\.000\/0000\-00");
            if(!string.IsNullOrEmpty(empresa.Cpf)) empresa.Cpf = Convert.ToUInt64(empresa.Cpf).ToString(@"000\.000\.000\-00");

            return empresa;
        }

        [HttpPost]
        public IActionResult Index([FromForm] EmpresaModel empresa)
        {
            ValidateFields(empresa.Cnpj, empresa.Cpf);

            if (!ModelState.IsValid)
                return View(empresa);

            try
            {
                _httpBase.Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Session.Token}");
                HttpResponseMessage response = _httpBase.Client.PatchAsJsonAsync("api/v1/empresa", empresa).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["Sucesso"] = "Os dados da empresa foram alterados com sucesso";
                    return View(empresa);
                }
                
                TempData["MensagemError"] = response.StatusCode.ToString();
                return View(empresa);
            }
            catch
            {
                TempData["MensagemError"] = "Algo deu errado, tente novamente mais tarde";
                return View(empresa);
            }
        }

        public void ValidateFields(string cnpj, string cpf)
        {
            if (!string.IsNullOrEmpty(cnpj) && cnpj.Length < 18)
                ModelState.AddModelError("Cnpj", "O Cnpj digitado não é valido");

            if (!string.IsNullOrEmpty(cpf) && cpf.Length < 14)
                ModelState.AddModelError("Cpf", "O Cpf digitado não é valido");
        }

        [HttpPost]
        public IActionResult DesativarConta(int id)
        {
            _httpBase.Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Session.Token}");
            var request = new HttpRequestMessage(HttpMethod.Delete, $"/api/v1/empresa?id={id}");
            var response = _httpBase.Client.Send(request);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode(500);
            }

            return RedirectToAction("Logout", "Login");
        }
    }
}
