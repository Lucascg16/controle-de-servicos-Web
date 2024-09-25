using System.ComponentModel.DataAnnotations;

namespace ServicoInWeb.Models
{
    public class EmpresaModel
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; } = string.Empty;
        public string? Cnpj { get; set; } 
        public string? Cpf { get; set; }

        public EmpresaModel() {}
    }
}
