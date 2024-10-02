using ServicoInWeb.Models;

namespace ServicoInWeb.ViewModels
{
    public record ServicoViewModel(IOrderedEnumerable<ServicoModel>? Servicos, bool FilterClose, List<PaginationModel> Pagination);
}
