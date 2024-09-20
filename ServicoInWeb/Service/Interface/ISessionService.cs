using ServicoInWeb.Models;

namespace ServicoInWeb.Service
{
    public interface ISessionService
    {
        Task CreateUserSection(string Token);
        void DesableUserSection();
        SessionModel GetSection();
    }
}
