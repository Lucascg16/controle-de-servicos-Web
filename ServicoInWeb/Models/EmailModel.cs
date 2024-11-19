namespace ServicoInWeb.Models
{
    public class EmailModel
    {
        public string Remetente { get; set; } = "";
        public string EmailRemetente { get; set; } = "";
        public string Senha { get; set; } = "";
        public string EnderecoServidor { get; set; } = "";
        public int Porta { get; set; }
        public bool UsarSsl { get; set; }
    }
}