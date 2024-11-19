using System.ComponentModel.DataAnnotations;

namespace ServicoInWeb.ViewModels
{
    public record EsqueciSenhaViewModel
    {
        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "O email digitado não é valido")]
        public string? Email { get; set; }
    }
}