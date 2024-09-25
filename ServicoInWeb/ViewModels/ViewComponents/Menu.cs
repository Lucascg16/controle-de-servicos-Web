using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServicoInWeb.Models;

namespace ServicoInWeb.ViewModels
{
    public class Menu : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userSession = HttpContext.Session.GetString("sessaoUserLogged");

            if (userSession == null) 
                return View(new SessionModel());

            var session = JsonConvert.DeserializeObject<SessionModel>(userSession);

            return View(session);
        }
    }
}
