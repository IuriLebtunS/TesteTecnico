<%@ Page Title="Novo Lançamento" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Criar.aspx.cs" Inherits="Financeiro.WebApp.Pages.Criar" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-4">

        <div>
            <h2>
                <i class="fa-solid fa-circle-plus"></i>
                Novo Lançamento
            </h2>
        </div>

        <asp:ValidationSummary
            ID="ValidationSummary1"
            runat="server"
            CssClass="text-danger" />

        <div class="card shadow-sm p-4">

            <div class="row">

                <div class="col-md-6 mb-3">
                    <label class="form-label">Descrição</label>
                    <asp:TextBox
                        ID="TxtDescricao"
                        runat="server"
                        CssClass="form-control" />
                </div>

                <div class="col-md-6 mb-3">
                    <label class="form-label">Valor</label>
                    <asp:TextBox
                        ID="TxtValor"
                        runat="server"
                        CssClass="form-control" />
                </div>

            </div>

            <div class="row">

                <div class="col-md-6 mb-3">
                    <label class="form-label">Competência</label>
                    <asp:TextBox
                        ID="TxtCompetencia"
                        runat="server"
                        CssClass="form-control"
                        Placeholder="MM/AAAA" />
                </div>

                <div class="col-md-6 mb-3">
                    <label class="form-label">Tipo</label>
                    <asp:DropDownList
                        ID="SelectTipo"
                        runat="server"
                        CssClass="form-select">
                        <asp:ListItem Text="Crédito" Value="1" />
                        <asp:ListItem Text="Débito" Value="2" />
                    </asp:DropDownList>
                </div>

            </div>


            <div class="d-flex justify-content-end mt-3">

                <asp:Button
                    ID="BtnVoltar"
                    runat="server"
                    Text="Voltar"
                    PostBackUrl="~/Pages/Index.aspx"
                    CssClass="btn btn-secondary" />

                <asp:Button
                    ID="BtnSalvar"
                    runat="server"
                    Text="Salvar"
                    CssClass="btn btn-primary ms-2"
                    OnClick="BtnSalvar_Click" />

            </div>

        </div>
    </div>

</asp:Content>
