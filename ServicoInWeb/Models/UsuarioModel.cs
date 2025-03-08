namespace ServicoInWeb.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public Guid VId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public RoleEnum Role { get; set; } = RoleEnum.none;
        public int EmpresaId { get; set; }
        public bool Dono { get; set; } = false;

        public UsuarioModel() {}//construtor vazio

        public UsuarioModel(string nome, string email, string password, int empresaId, RoleEnum role)
        {
            Nome = nome;
            Email = email;
            Password = password;
            EmpresaId = empresaId;
            Role = role;
        }
    }
}
