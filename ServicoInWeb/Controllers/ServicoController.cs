using Microsoft.AspNetCore.Mvc;

namespace ServicoInWeb.Controllers
{
    public class ServicoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
