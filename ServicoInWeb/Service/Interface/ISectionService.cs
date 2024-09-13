using ServicoInWeb.Models;

namespace ServicoInWeb.Service
{
    public interface ISectionService
    {
        Task CreateUserSection(string Token);
        void DesableUserSection();//Todo:pesquisar como revogar o token jwt
        SessionModel GetSection();
    }
}
