using System.ComponentModel.DataAnnotations;

namespace ServicoInWeb.ViewModels
{
    public record AlterarSenhaViewModel
    {
        [Required(ErrorMessage = "A senha atual é obrigatória")]
        public string Senha { get; set; } = "";
        [Required(ErrorMessage = "A nova senha é obrigatória")]
        [MinLength(6, ErrorMessage = "A senha deve conter pelo menos 6 digitos")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "A senha digitada é fraca de mais, verifique as instruções na tela")]
        public string NovaSenha { get; set; } = "";
        [Required(ErrorMessage = "Esse campo deve ser igual ao campo de nova senha")]
        public string ConfirmarSenha { get; set; } = "";
    }
}
