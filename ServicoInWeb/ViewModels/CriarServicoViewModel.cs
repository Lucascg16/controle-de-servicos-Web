using System.ComponentModel.DataAnnotations;

namespace ServicoInWeb.ViewModels
{
    public class CriarServicoViewModel
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double? Custos { get; set; }
        public double? OrcamentoInicial { get; set; }
        public int UsuarioId { get; set; }
        public int EmpresaId {  get; set; }
    }
}
