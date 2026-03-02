using Financeiro.Business.Services;
using Financeiro.Data.Repositories;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Financeiro.WebApp.Pages
{
    public partial class Index : Page
    {
        private readonly LancamentoFinanceiroRepository _repositorio = new LancamentoFinanceiroRepository();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                Carregar();
        }

        private void Carregar()
        {
            var lista = _repositorio.Listar();

            GridLancamentos.DataSource = lista;
            GridLancamentos.DataBind();

            var service = new LancamentoFinanceiroService(_repositorio);
            LblSaldo.Text = service.CalcularSaldo().ToString("C2");
        }

        protected void GridLancamentos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);

            var service = new LancamentoFinanceiroService(_repositorio);

            if (e.CommandName == "Editar")
                Response.Redirect($"~/Pages/Editar.aspx?id={id}");

            if (e.CommandName == "Pagar")
            {
                service.Pagar(id);
                Carregar();
            }

            if (e.CommandName == "Cancelar")
            {
                service.Cancelar(id);
                Carregar();
            }
        }
    }
}