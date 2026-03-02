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
            if (e.CommandName != "Editar" &&
                e.CommandName != "Pagar" &&
                e.CommandName != "Cancelar")
                return;

            if (string.IsNullOrEmpty(e.CommandArgument?.ToString()))
                return;

            int id = Convert.ToInt32(e.CommandArgument);

            var service = new LancamentoFinanceiroService(_repositorio);

            switch (e.CommandName)
            {
                case "Editar":
                    Response.Redirect($"~/Pages/Editar.aspx?id={id}");
                    break;

                case "Pagar":
                    service.Pagar(id);
                    Carregar();
                    break;

                case "Cancelar":
                    service.Cancelar(id);
                    Carregar();
                    break;
            }
        }
    }
}