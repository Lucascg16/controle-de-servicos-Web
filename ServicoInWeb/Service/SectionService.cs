using Newtonsoft.Json;
using ServicoInWeb.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json.Serialization;

namespace ServicoInWeb.Service
{
    public class SectionService : ISectionService
    {
        private readonly IHttpContextAccessor _httpContext;

        public SectionService(IHttpContextAccessor context)
        {
            _httpContext = context;
        }

        public void CreateUserSection(string token)
        {
            var decodeToken = new JwtSecurityTokenHandler().ReadJwtToken(token);

            object jsonObj = new
            {
                Token = token,
                Id = decodeToken.Claims.FirstOrDefault(x => x.Type == "Id"),
                Role = decodeToken.Claims.FirstOrDefault(x => x.Type == "Role")
            };

            string valorSerializado = JsonConvert.SerializeObject(jsonObj);

            _httpContext.HttpContext.Session.SetString("sessaoUserLogged", valorSerializado);
        }

        public void DesableUserSection()
        {
            _httpContext.HttpContext.Session.Remove("sessaoUserLogged");
        }

        public object GetSection()
        {
            string sessaoUsuario = _httpContext.HttpContext.Session.GetString("sessaoUserLogged");

            if (string.IsNullOrEmpty(sessaoUsuario)) return null!;

            return JsonConvert.DeserializeObject<object>(sessaoUsuario);
        }
    }
}
