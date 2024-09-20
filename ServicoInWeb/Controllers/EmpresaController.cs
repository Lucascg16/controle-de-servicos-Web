using Microsoft.AspNetCore.Mvc;

namespace ServicoInWeb.Controllers
{
    public class EmpresaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
