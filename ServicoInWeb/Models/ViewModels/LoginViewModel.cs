using System.ComponentModel.DataAnnotations;

namespace ServicoInWeb.Models
{
    public record LoginViewModel
    {
        [Required(ErrorMessage = "O campo e-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "O formato do e-mail digitado é inválido")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "O campo senha é obrigatório")]
        public string Password { get; set; } = string.Empty;
    }
}
