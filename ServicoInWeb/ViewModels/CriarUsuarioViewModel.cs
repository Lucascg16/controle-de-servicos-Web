using ServicoInWeb.Models;
using System.ComponentModel.DataAnnotations;

namespace ServicoInWeb.ViewModels
{
    public class CriarUsuarioViewModel
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Nome { get; set; } = string.Empty;
        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "O email digitado não é válido")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "A senha é obrigatória")]
        public string Senha { get; set; } = string.Empty;
        [Required(ErrorMessage = "Confirmação de senha é obrigatório")]
        public string ConfirmarSenha { get; set; } = string.Empty;
        public RoleEnum Role { get; set; }
    }
}
