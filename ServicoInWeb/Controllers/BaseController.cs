using Microsoft.AspNetCore.Mvc;
using ServicoInWeb.Models;
using ServicoInWeb.Service;

namespace ServicoInWeb.Controllers
{
    public class BaseController : Controller
    {
        public SessionModel Session { get; private set; } = new();

        protected void Autenticate(ISessionService service)
        {
            Session = service.GetSection();
        }
    }
}
