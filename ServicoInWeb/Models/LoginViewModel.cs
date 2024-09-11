using System.ComponentModel.DataAnnotations;

namespace ServicoInWeb.Models
{
    public record LoginViewModel
    {
        [Required(ErrorMessage = "O campo e-mail é obrigatório")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "O campo senha é obrigatório")]
        public string Password { get; set; } = string.Empty;
    }
}
