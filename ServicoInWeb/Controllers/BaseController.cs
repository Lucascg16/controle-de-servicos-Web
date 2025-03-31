using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
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

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var decodeToken = new JwtSecurityTokenHandler().ReadJwtToken(Session.Token);
            
            if(Utilitarios.ValidaTokenExpirado(decodeToken)){
               context.Result = new RedirectToActionResult("Index", "Login", null);
            }

            base.OnActionExecuting(context);
        }
    }
}
