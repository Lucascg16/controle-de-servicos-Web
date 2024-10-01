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
        public async Task<IActionResult> Index(int id)
        {
            if(Session is null)
                return RedirectToAction("Index", "Login");
            if (Session.Role == "Employee")
                return RedirectToAction("Index", "Home");

            try
            {
                _httpBase.Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Session.Token}");
                HttpResponseMessage response = _httpBase.Client.GetAsync($"api/v1/empresa?id={id}").Result;

                if (response.IsSuccessStatusCode)
                {
                    var empresa = await response.Content.ReadFromJsonAsync<EmpresaModel>();//adicionar forma do cnpj e cpf serem colocados na pagina com a pontuação.
                    empresa.Cnpj = Convert.ToUInt64(empresa.Cnpj).ToString(@"00\.000\.000\/0000\-00");
                    empresa.Cpf = Convert.ToUInt64(empresa.Cpf).ToString(@"000\.000\.000\-00");
                    return View(empresa);
                }

                return View();
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
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
            }
            catch
            {
                TempData["MensagemError"] = "Algo deu errado, tente novamente mais tarde";
                return View(empresa);
            }

            return View(empresa);
        }

        public void ValidateFields(string cnpj, string cpf)
        {
            if (!string.IsNullOrEmpty(cnpj) && cnpj.Length < 18)
                ModelState.AddModelError("Cnpj", "O Cnpj digitado não é valido");

            if (!string.IsNullOrEmpty(cpf) && cpf.Length < 14)
                ModelState.AddModelError("Cpf", "O Cpf digitado não é valido");
        }
    }
}
