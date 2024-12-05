using System.ComponentModel.DataAnnotations;

namespace ServicoInWeb.ViewModels
{
    public class CriarServicoViewModel
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; } = string.Empty;
        [MaxLength(255, ErrorMessage = "A descrição não pode ter mais do que 255 caracteres")]
        public string? Descricao { get; set; } = string.Empty;
        public double? Custos { get; set; }
        public double? OrcamentoInicial { get; set; }
        public int UsuarioId { get; set; }
        public int EmpresaId {  get; set; }
    }
}
