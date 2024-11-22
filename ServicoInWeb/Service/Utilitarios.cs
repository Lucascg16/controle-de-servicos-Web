using System.IdentityModel.Tokens.Jwt;
using ServicoInWeb.Models;
using ServicoInWeb.Models.Enum;

namespace ServicoInWeb.Service
{
    public static class Utilitarios
    {
        public static string GetRoleString(RoleEnum role)
        {
            switch (role)
            {
                case RoleEnum.Admin:
                    return "Admin";
                case RoleEnum.Funcionario:
                    return "Employee";
                default:
                    return "None";
            }
        }

        public static RoleEnum GetRoleEnum(string role)
        {
            switch (role)
            {
                case "Admin":
                    return RoleEnum.Admin;
                case "Employee":
                    return RoleEnum.Funcionario;
                default:
                    return RoleEnum.none;
            }
        }

        public static string GetFlagString(ServicoFlagEnum flag)
        {
            switch (flag)
            {
                case ServicoFlagEnum. Ativo:
                    return "Ativo";
                case ServicoFlagEnum.Cancelado:
                    return "Cancelado";
                case ServicoFlagEnum.Finalizado:
                    return "Finalizado";
                default:
                    return "Ativo";
            }
        }

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