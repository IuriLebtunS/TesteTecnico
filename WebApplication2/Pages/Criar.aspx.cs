using Financeiro.Business.Entities;
using Financeiro.Business.Services;
using Financeiro.Data.Repositories;
using System;
using System.Web.UI;


namespace Financeiro.WebApp.Pages
{
    public partial class Criar : Page
    {
        protected void BtnSalvar_Click(object sender, EventArgs e)
        {
            var dataLancamento = DateTime.Parse(TxtDataLancamento.Text);

            var model = new LancamentoFinanceiro
            {
                Descricao = TxtDescricao.Text,
                ValorOriginal = decimal.Parse(TxtValor.Text),
                Tipo = (TipoLancamento)int.Parse(SelectTipo.SelectedValue),

                DataLancamento = dataLancamento,
                DataCriacao = DateTime.Now,
                Competencia = dataLancamento.ToString("MM/yyyy"),

                Status = StatusLancamento.Aberto,
                PercentualTaxa = 0,
                PercentualDesconto = 0,
                ValorCalculado = decimal.Parse(TxtValor.Text)
            };

            var repo = new LancamentoFinanceiroRepository();
            var service = new LancamentoFinanceiroService(repo);
            Response.Redirect("Index.aspx");
        }
    }
}