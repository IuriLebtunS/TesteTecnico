using System;

namespace Financeiro.Business.Entities
{
    public class LancamentoFinanceiro
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public TipoLancamento Tipo { get; set; }
        public decimal ValorOriginal { get; set; }
        public decimal PercentualTaxa { get; set; }
        public decimal PercentualDesconto { get; set; }
        public decimal ValorCalculado { get; set; }
        public DateTime DataLancamento { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataPagamento { get; set; }
        public DateTime? DataCancelamento { get; set; }
        public string Competencia { get; set; }
        public StatusLancamento Status { get; set; }

        public DateTime? DataUltimaAcao
        {
            get
            {
                if (Status == StatusLancamento.Pago)
                    return DataPagamento;

                if (Status == StatusLancamento.Cancelado)
                    return DataCancelamento;

                return DataCriacao;
            }
        }
    }
}