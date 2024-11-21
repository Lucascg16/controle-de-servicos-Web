using System.ComponentModel.DataAnnotations;

namespace ServicoInWeb.ViewModels
{
    public class RedefinirSenhaViewModel
    {
        public RedefinirSenhaViewModel() { }

        public RedefinirSenhaViewModel(string token, int id)
        {
            Token = token;
            Id = id;
        }

        public string Token { get; set; } = "";
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo senha é obrigatório")]
        [MinLength(6, ErrorMessage = "A senha deve conter pelo menos 6 digitos")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "A senha digitada é fraca de mais, verifique as instruções na tela")]
        public string? Senha { get; set; }
        [Required(ErrorMessage = "Confirme sua senha")]
        [Compare("Senha", ErrorMessage = "As senhas informadas não conferem")]
        public string? ConfirmarSenha { get; set; }
    }
}