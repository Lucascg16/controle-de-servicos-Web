using System.ComponentModel.DataAnnotations;

namespace ServicoInWeb.ViewModels
{
    public record AlterarSenhaViewModel
    {
        [Required(ErrorMessage = "A senha atual é obrigatória")]
        public string Senha { get; set; }
        [Required(ErrorMessage = "A nova senha é obrigatória")]
        [MinLength(6, ErrorMessage = "A senha deve conter pelo menos 6 digitos")]
        public string NovaSenha { get; set; }
        [Required(ErrorMessage = "Esse campo deve ser igual ao campo de nova senha")]
        public string ConfirmarSenha { get; set; }
    }
}
