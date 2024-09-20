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

        public string? Token { get; set; }
        public string? Id {  get; set; }
        public string? Role { get; set; }
        public UsuarioModel? Usuario { get; set; }
    }
}
