using ServicoInWeb.Models;
using ServicoInWeb.Models.Enum;

namespace ServicoInWeb.ViewModels
{
    public record ServicoViewModel(List<ServicoModel>? Servicos, ServicoFlagEnum Flag, List<PaginationModel> Pagination, string? NomeServico = null, string? Data = null);
}
