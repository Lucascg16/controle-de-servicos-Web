using System.ComponentModel.DataAnnotations;

namespace ServicoInWeb.ViewModels
{
    public class CreateAccountViewModel
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [MaxLength(255)]
        public string Nome {  get; set; } = string.Empty;
        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "O email digitado não é valido")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "A senha é obrigatória")]
        [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 dígitos")]
        [MaxLength(500)]
        public string Password { get; set; } = string.Empty;
        [Required(ErrorMessage = "O nome da empresa é obrigatório")]
        [MaxLength(255)]
        public string NomeEmpresa {  get; set; } = string.Empty;
        public string? Cnpj { get; set; }
        public string? Cpf {  get; set; }
    }
}
