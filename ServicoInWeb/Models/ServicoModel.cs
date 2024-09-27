namespace ServicoInWeb.Models
{
    public class ServicoModel
    {
        public ServicoModel(int id, string nome, string descricao, double? custos,
            double? orcamentoInicial, double? valorFaturado, double? lucroLiquido,
            bool excluido, DateTime dataCriacao, DateTime dataFinalizado)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            Custos = custos ?? 0;
            OrcamentoInicial = orcamentoInicial ?? 0;
            ValorFaturado = valorFaturado ?? 0;
            LucroLiquido = lucroLiquido ?? 0;
            Excluido = excluido;
            DataCriacao = dataCriacao;
            DataFinalizado = dataFinalizado;
        }

        public ServicoModel() { }

        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public double? Custos { get; set; }
        public double? OrcamentoInicial { get; set; }
        public double? ValorFaturado { get; set; }
        public double? LucroLiquido { get; set; }
        public bool Excluido { get; set; }
        public DateTime? DataCriacao { get; set; }
        public DateTime? DataFinalizado { get; set; }
    }
}
