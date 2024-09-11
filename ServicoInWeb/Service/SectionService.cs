namespace ServicoInWeb.Service
{
    public class SectionService : ISectionService
    {
        private readonly IHttpContextAccessor _httpContext;

        public SectionService(IHttpContextAccessor context)
        {
            _httpContext = context;
        }

        public void CreateUserSection(string Token)
        {
            _httpContext.HttpContext.Session.SetString("token", Token);
        }

        public void DesableUserSection()
        {
            throw new NotImplementedException();
        }

        public string GetSection()
        {
            throw new NotImplementedException();
        }
    }
}
