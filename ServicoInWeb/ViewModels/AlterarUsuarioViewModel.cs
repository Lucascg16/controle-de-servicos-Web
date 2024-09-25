using ServicoInWeb.Models;

namespace ServicoInWeb.ViewModels
{
    public record AlterarUsuarioViewModel(int Id, string Nome, string Email, RoleEnum Role, int EmpresaId, string UserLogedRole);
}
