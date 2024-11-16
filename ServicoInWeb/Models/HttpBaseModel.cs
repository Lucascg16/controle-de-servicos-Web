namespace ServicoInWeb.Models
{
    public record HttpBaseModel : IHttpBaseModel
    {
        public HttpClient Client { get; set; }

        public HttpBaseModel(HttpClient client, IConfiguration _configuration)
        {
            Client = client;
            Client.BaseAddress = new Uri(_configuration.GetConnectionString("ApiUrl") ?? "");
        }
    }
}
