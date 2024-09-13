using Microsoft.AspNetCore.Mvc;
using ServicoInWeb.Models;
using ServicoInWeb.Service;

namespace ServicoInWeb.Controllers
{
    public class BaseController : Controller
    {
        public SessionModel Session { get; private set; }

        protected void Autenticate(ISectionService service)
        {
            Session = service.GetSection();
        }
    }
}
