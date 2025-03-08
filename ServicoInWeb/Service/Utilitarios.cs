using System.IdentityModel.Tokens.Jwt;
using ServicoInWeb.Models;
using ServicoInWeb.Models.Enum;

namespace ServicoInWeb.Service
{
    public static class Utilitarios
    {
        public static bool ValidaTokenExpirado(JwtSecurityToken token)
        {
            try
            {
                var exp = token.Payload.Expiration;

                if (exp.HasValue)
                {
                    var expirationDate = DateTimeOffset.FromUnixTimeSeconds(exp.Value).UtcDateTime;

                    return expirationDate <= DateTime.UtcNow;//Se a data de expiração for menor que a atual retorna true 
                }
                return false;
            }
            catch
            {
                return true;
            }
        }
    }
}