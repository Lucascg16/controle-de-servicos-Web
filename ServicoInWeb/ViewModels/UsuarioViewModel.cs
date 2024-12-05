using ServicoInWeb.Models;

namespace ServicoInWeb.ViewModels
{
    public record UsuarioViewModel(List<UsuarioModel>? Usuarios, List<PaginationModel> Pagination, string? NomeUsuario = "");
    
}
