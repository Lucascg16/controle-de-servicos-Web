using Microsoft.AspNetCore.Mvc;

namespace ServicoInWeb.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
