using Financeiro.Business.Entities;
using Financeiro.Business.Services;
using Financeiro.Data.Repositories;
using System.Web.UI;
using System;

namespace Financeiro.WebApp.Pages
{
    public partial class Editar : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                Carregar();
        }
        protected void SelectTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            AtualizarCamposTipo();
        }

        private void Carregar()
        {
            int id = int.Parse(Request.QueryString["id"]);

            var repo = new LancamentoFinanceiroRepository();
            var item = repo.ObterPorId(id);

            if (item == null)
                throw new Exception("Lançamento não encontrado");

            HiddenId.Value = item.Id.ToString();
            TxtDescricao.Text = item.Descricao;
            TxtValor.Text = item.ValorOriginal.ToString("N2");
            TxtCompetencia.Text = item.Competencia;

            SelectTipo.SelectedValue = ((int)item.Tipo).ToString();
            TxtTaxa.Text = item.PercentualTaxa.ToString();
            TxtDesconto.Text = item.PercentualDesconto.ToString();
            AtualizarCamposTipo();
        }

        protected void BtnSalvar_Click(object sender, EventArgs e)
        {
            var model = new LancamentoFinanceiro
            {
                Id = int.Parse(HiddenId.Value),
                Descricao = TxtDescricao.Text,
                ValorOriginal = decimal.Parse(TxtValor.Text),
                Competencia = TxtCompetencia.Text,
                Tipo = (TipoLancamento)int.Parse(SelectTipo.SelectedValue),
                PercentualTaxa = string.IsNullOrEmpty(TxtTaxa.Text) ? 0 : decimal.Parse(TxtTaxa.Text),
                PercentualDesconto = string.IsNullOrEmpty(TxtDesconto.Text) ? 0 : decimal.Parse(TxtDesconto.Text)
            };


            var repo = new LancamentoFinanceiroRepository();
            var service = new LancamentoFinanceiroService(repo);

            service.Editar(model);

            Response.Redirect("Index.aspx");
        }

        private void AtualizarCamposTipo()
        {
            int tipo = int.Parse(SelectTipo.SelectedValue);

            if (tipo == 1) 
            {
                PanelDesconto.Visible = true;
                PanelTaxa.Visible = false;
                TxtTaxa.Text = "0";
            }
            else 
            {
                PanelDesconto.Visible = false;
                PanelTaxa.Visible = true;
                TxtDesconto.Text = "0";
            }
        }
    }
}