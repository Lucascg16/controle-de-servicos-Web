namespace ServicoInWeb.Models
{
    public record HttpBaseModel : IHttpBaseModel
    {
        public HttpClient Client { get; set; }

        public HttpBaseModel(HttpClient client)
        {
            Client = client;
            Client.BaseAddress = new Uri("https://localhost:7282/");
        }
    }
}
