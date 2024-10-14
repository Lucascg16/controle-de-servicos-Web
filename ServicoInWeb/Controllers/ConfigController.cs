using Microsoft.AspNetCore.Mvc;
using ServicoInWeb.Service;

namespace ServicoInWeb.Controllers
{
    public class ConfigController : BaseController
    {
        public readonly ISessionService _sessionService;

        public ConfigController(ISessionService sessionService)
        {
            _sessionService = sessionService;
            Autenticate(_sessionService);
        }

        public IActionResult Index()
        {
            return View(Session);
        }
    }
}
