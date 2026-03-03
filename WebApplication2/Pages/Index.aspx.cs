using Financeiro.Business.Services;
using Financeiro.Data.Repositories;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
using System.Web.UI;
using System.Linq;
using System.IO;
using System;

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

        protected void BtnExcel_Click(object sender, EventArgs e)
        {
            var lista = _repositorio.Listar().ToList();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Lançamentos");

                worksheet.Cell(1, 1).Value = "Descrição";
                worksheet.Cell(1, 2).Value = "Valor";
                worksheet.Cell(1, 3).Value = "Data";
                worksheet.Cell(1, 4).Value = "Tipo";
                worksheet.Cell(1, 5).Value = "Taxa";
                worksheet.Cell(1, 6).Value = "Desconto";

                int linha = 2;

                foreach (var item in lista)
                {
                    worksheet.Cell(linha, 1).Value = item.Descricao;
                    worksheet.Cell(linha, 2).Value = item.ValorOriginal;
                    worksheet.Cell(linha, 3).Value = item.Competencia;
                    worksheet.Cell(linha, 4).Value = item.Tipo.ToString();
                    worksheet.Cell(linha, 5).Value = item.PercentualTaxa;
                    worksheet.Cell(linha, 6).Value = item.PercentualDesconto;
                    linha++;
                }

                worksheet.Columns().AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);

                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=Lancamentos.xlsx");
                    Response.BinaryWrite(stream.ToArray());
                    Response.End();
                }
            }
        }
    }
}