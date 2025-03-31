namespace ServicoInWeb.Models
{
    public class SessionModel
    {
        public SessionModel(string token, string id, string role, UsuarioModel usuario)
        {
            Token = token;
            Id = id;
            Role = role;
            Usuario = usuario;
        }

        public SessionModel(){}

        public string Token { get; set; } = string.Empty;
        public string Id {  get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public UsuarioModel Usuario { get; set; } = new();
    }
}
