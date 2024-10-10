namespace ServicoInWeb.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public Guid VId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = "none";
        public int EmpresaId { get; set; }

        public UsuarioModel() {}//construtor vazio

        public UsuarioModel(string nome, string email, string password, int empresaId, string role)
        {
            Nome = nome;
            Email = email;
            Password = password;
            EmpresaId = empresaId;
            Role = role;
        }
    }
}
