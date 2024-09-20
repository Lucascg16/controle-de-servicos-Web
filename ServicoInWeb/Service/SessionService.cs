using Newtonsoft.Json;
using ServicoInWeb.Models;
using System.IdentityModel.Tokens.Jwt;

namespace ServicoInWeb.Service
{
    public class SessionService : ISessionService
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly IHttpBaseModel _httpBase;

        public SessionService(IHttpContextAccessor context, IHttpBaseModel httpBase)
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

            SessionModel sessaoJson = new(token, id, role, user);

            string valorSerializado = JsonConvert.SerializeObject(sessaoJson);

            _httpContext.HttpContext.Session.SetString("sessaoUserLogged", valorSerializado);
        }

        public void DesableUserSection()
        {
            _httpContext.HttpContext.Session.Remove("sessaoUserLogged");
        }

        public SessionModel GetSection()
        {
            string? sessaoUsuario = _httpContext.HttpContext.Session.GetString("sessaoUserLogged");

            if (string.IsNullOrEmpty(sessaoUsuario)) 
                return null!;

            var sessao = JsonConvert.DeserializeObject<SessionModel>(sessaoUsuario);

            return sessao;
        }

        private async Task<UsuarioModel> GetUser(string token, int id)
        {
            try
            {
                _httpBase.Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                HttpResponseMessage response = _httpBase.Client.GetAsync($"api/v1/usuario?id={id}").Result;

                return await response.Content.ReadFromJsonAsync<UsuarioModel>();
            }
            catch 
            {
                return new();//retorna um usuario vazio
            }
        }
    }
}
