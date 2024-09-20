using ServicoInWeb.Models;

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
    }
}
