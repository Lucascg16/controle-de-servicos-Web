using ServicoInWeb.Models;

namespace ServicoInWeb.Service
{
    public interface IEmailService
    {
        void SendEmailAsync(string emailDestinatario, string token);
    }
}