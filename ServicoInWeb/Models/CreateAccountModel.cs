using ServicoInWeb.Service;
using ServicoInWeb.ViewModels;

namespace ServicoInWeb.Models
{
    public class CreateAccountModel
    {
        public EmpresaModel Empresa { get; set; } = new();
        public UsuarioModel Usuario { get; set; } = new();

        public CreateAccountModel(CreateAccountViewModel model) 
        {
            Usuario.Nome = model.Nome;
            Usuario.Email = model.Email;
            Usuario.Password = model.Password;
            Usuario.Role = Utilitarios.GetRoleString(RoleEnum.Admin);
            Empresa.Nome = model.NomeEmpresa;
            Empresa.Cnpj = model.Cnpj ?? string.Empty;
            Empresa.Cpf = model.Cpf ?? string.Empty;
        }
    }
}
