using System.Web.UI;
using System;
using Financeiro.Business.Entities;
using Financeiro.Data.Repositories;


namespace Financeiro.WebApp.Pages
{
    public partial class Criar : Page
    {
        protected void BtnSalvar_Click(object sender, EventArgs e)
        {
            var model = new LancamentoFinanceiro
            {
                Descricao = TxtDescricao.Text,
                ValorOriginal = decimal.Parse(TxtValor.Text),
                Tipo = (TipoLancamento)int.Parse(SelectTipo.SelectedValue),
                Competencia = TxtCompetencia.Text,
                DataLancamento = DateTime.Now,
                DataCriacao = DateTime.Now,
                Status = StatusLancamento.Aberto,
                PercentualTaxa = 0,
                PercentualDesconto = 0,
                ValorCalculado = decimal.Parse(TxtValor.Text)
            };

            var repo = new LancamentoFinanceiroRepository();
            repo.Inserir(model);

            Response.Redirect("Index.aspx");
        }
    }
}