using System.ComponentModel.DataAnnotations;

namespace ServicoInWeb.ViewModels
{
    public record AlterarSenhaViewModel
    {
        [Required(ErrorMessage = "A senha atual é obrigatória")]
        public string senha { get; set; }
        [Required(ErrorMessage = "A nova senha é obrigatória")]
        public string novaSenha { get; set; }
        [Required(ErrorMessage = "Esse campo deve ser igual ao campo de nova senha")]
        public string confirmarSenha { get; set; }
    }
}
