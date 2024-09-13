using Newtonsoft.Json;
using ServicoInWeb.Models;
using System.IdentityModel.Tokens.Jwt;

namespace ServicoInWeb.Service
{
    public class SectionService : ISectionService
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly IHttpBaseModel _httpBase;

        public SectionService(IHttpContextAccessor context, IHttpBaseModel httpBase)
        {
            _httpContext = context;
            _httpBase = httpBase;
        }

        public async Task CreateUserSection(string token)
        {
            var decodeToken = new JwtSecurityTokenHandler().ReadJwtToken(token);

            var id = decodeToken.Claims.FirstOrDefault(x => x.Type == "id").Value;
            var role = decodeToken.Claims.FirstOrDefault(x => x.Type == "role").Value;
            var user = await GetUser(token, int.Parse(id));

            SessionModel jsonObj = new(token, id, role, user);

            string valorSerializado = JsonConvert.SerializeObject(jsonObj);

            _httpContext.HttpContext.Session.SetString("sessaoUserLogged", valorSerializado);
        }

        public void DesableUserSection()
        {
            _httpContext.HttpContext.Session.Remove("sessaoUserLogged");
        }

        public SessionModel GetSection()
        {
            string sessaoUsuario = _httpContext.HttpContext.Session.GetString("sessaoUserLogged");

            if (string.IsNullOrEmpty(sessaoUsuario)) return null!;

            var sessaode = JsonConvert.DeserializeObject<SessionModel>(sessaoUsuario);

            return sessaode;
        }

        private async Task<UsuarioModel> GetUser(string token, int id)
        {
            var url = $"api/v1/usuario?id={id}";

            _httpBase.Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            HttpResponseMessage response = _httpBase.Client.GetAsync(url).Result;

            return await response.Content.ReadFromJsonAsync<UsuarioModel>();
        }
    }
}
