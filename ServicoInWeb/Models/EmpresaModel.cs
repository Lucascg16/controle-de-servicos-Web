namespace ServicoInWeb.Models
{
    public class EmpresaModel
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public  string Cnpj { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;

        public EmpresaModel() {}
    }
}
