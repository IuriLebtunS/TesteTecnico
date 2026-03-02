using Financeiro.Business.Entities;
using Financeiro.Data.Repositories;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Financeiro.WebApp.Pages
{
    public partial class Editar : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                Carregar();
        }

        private void Carregar()
        {
            int id = int.Parse(Request.QueryString["id"]);

            var repo = new LancamentoFinanceiroRepository();
            var lista = repo.Listar();
            var item = lista.Find(x => x.Id == id);

            HiddenId.Value = item.Id.ToString();
            TxtDescricao.Text = item.Descricao;
            TxtValor.Text = item.ValorOriginal.ToString();
            TxtCompetencia.Text = item.Competencia;
        }

        protected void BtnSalvar_Click(object sender, EventArgs e)
        {
            var model = new LancamentoFinanceiro
            {
                Id = int.Parse(HiddenId.Value),
                Descricao = TxtDescricao.Text,
                ValorOriginal = decimal.Parse(TxtValor.Text),
                Competencia = TxtCompetencia.Text,
                DataLancamento = DateTime.Now
            };

            var repo = new LancamentoFinanceiroRepository();
            repo.Atualizar(model);

            Response.Redirect("Index.aspx");
        }
    }
}