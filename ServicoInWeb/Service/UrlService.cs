namespace ServicoInWeb.Service
{
    public class UrlService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public UrlService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string GetBaseUrl() => $"{_contextAccessor.HttpContext?.Request.Scheme}://{_contextAccessor.HttpContext?.Request.Host}";
    }
}
