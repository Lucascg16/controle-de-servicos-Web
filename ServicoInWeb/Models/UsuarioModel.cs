namespace ServicoInWeb.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int EmpresaId { get; set; }

        public UsuarioModel(int id, string nome, string email, int empresaId) 
        {
            Id = id;
            Nome = nome;
            Email = email;
            EmpresaId = empresaId;
        }
    }
}
