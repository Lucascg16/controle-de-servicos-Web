using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServicoInWeb.Models;

namespace ServicoInWeb.ViewModels
{
    public class Menu : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string userSession = HttpContext.Session.GetString("sessaoUserLogged");

            if (string.IsNullOrEmpty(userSession))
                return null!;

            SessionModel session = JsonConvert.DeserializeObject<SessionModel>(userSession);

            return View(session);
        }
    }
}
