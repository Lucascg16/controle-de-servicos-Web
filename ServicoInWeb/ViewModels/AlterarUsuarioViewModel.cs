using ServicoInWeb.Models;
using System.ComponentModel.DataAnnotations;

namespace ServicoInWeb.ViewModels
{
    public class AlterarUsuarioViewModel
    {
        public int Id {  get; set; }
        [Required(ErrorMessage = "Nome é obriagtório")]
        public string? Nome { get; set; } = string.Empty;
        [Required(ErrorMessage = "Email é obrigatório")]
        public string? Email { get; set; } = string.Empty;
        public RoleEnum Role { get; set; } = RoleEnum.none;
        public int EmpresaId { get; set; }
        public string UserLogedRole { get; set; } = string.Empty;
        public int UserLogedId { get; set; } 

        public AlterarUsuarioViewModel(int id, string? nome, string? email, RoleEnum role, int empresaId, string userLogedRole, int userLogedId)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Role = role;
            EmpresaId = empresaId;
            UserLogedRole = userLogedRole;
            UserLogedId = userLogedId;
        }

        public AlterarUsuarioViewModel() { }
    }
}
