namespace ServicoInWeb.Service
{
    public interface ISectionService
    {
        void CreateUserSection(string Token);
        void DesableUserSection();//Todo:pesquisar como revogar o token jwt
        object GetSection();
    }
}
