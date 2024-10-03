using ServicoInWeb.Models;

namespace ServicoInWeb.ViewModels
{
    public record ServicoViewModel(List<ServicoModel>? Servicos, bool FilterClose, List<PaginationModel> Pagination);
}
