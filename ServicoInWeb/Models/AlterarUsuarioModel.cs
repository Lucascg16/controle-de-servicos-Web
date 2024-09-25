using System.ComponentModel.DataAnnotations;

namespace ServicoInWeb.Models
{
    public class AlterarUsuarioModel
    {
        public AlterarUsuarioModel(int id, string nome, string email, string role, int empresaId)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Role = role;
            EmpresaId = empresaId;
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Nome { get; set; } = string.Empty;
        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "O email digitado não é válido")]
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = "none";
        public int EmpresaId { get; set; }

    }
}
