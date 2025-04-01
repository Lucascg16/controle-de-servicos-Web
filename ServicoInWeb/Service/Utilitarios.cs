using System.IdentityModel.Tokens.Jwt;

namespace ServicoInWeb.Service
{
    public static class Utilitarios
    {
        /// <summary>
        /// Verifica se o Token é valido
        /// </summary>
        /// <param name="token"></param>
        /// <returns>True = invalido, False = valido</returns>
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