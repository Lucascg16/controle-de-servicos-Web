using System.ComponentModel.DataAnnotations;

namespace ServicoInWeb.Models
{
    public class AlterarServicoModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; } = string.Empty;
        [MaxLength(255, ErrorMessage = "A descrição não pode ter mais do que 255 caracteres")]
        public string Descricao { get; set; } = string.Empty;
        public double Custos { get; set; } = 0;
    }
}
