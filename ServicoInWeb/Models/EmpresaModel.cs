namespace ServicoInWeb.Models
{
    public class EmpresaModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public  string Cnpj { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;

        public EmpresaModel(int id, string nome, string? cnpj, string? cpf) 
        {
            Id = id;
            Nome = nome;
            Cnpj = cnpj ?? string.Empty;
            Cpf = cpf ?? string.Empty;
        }
    }
}
