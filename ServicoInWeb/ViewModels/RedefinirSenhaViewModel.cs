using System.ComponentModel.DataAnnotations;

namespace ServicoInWeb.ViewModels
{
    public class RedefinirSenhaViewModel
    {
        public RedefinirSenhaViewModel(){}

        public RedefinirSenhaViewModel(string token, int id)
        {
            Token = token;
            Id = id;
        }

        public string? Token { get; set; }
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo senha é obrigatório")]
        public string? Senha { get; set; }
        [Required(ErrorMessage = "Confirme sua senha")]
        [Compare("Senha", ErrorMessage = "As senhas informadas não conferem")]
        public string? ConfirmarSenha { get; set; }
    }
}