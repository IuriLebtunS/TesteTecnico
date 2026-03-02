<%@ Page Title="Lançamentos" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="Index.aspx.cs"
    Inherits="Financeiro.WebApp.Pages.Index" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Lançamentos Financeiros</h2>

    <div class="d-flex justify-content-end mb-3">
        <asp:Button ID="BtnNovo"
            runat="server"
            Text="Novo Lançamento"
            PostBackUrl="~/Pages/Criar.aspx"
            CssClass="btn btn-success" />
    </div>

    <div class="alert alert-info">
        Saldo Atual:
        <asp:Label ID="LblSaldo" runat="server" Font-Bold="true" />
    </div>

    <asp:GridView ID="GridLancamentos"
        runat="server"
        AutoGenerateColumns="false"
        CssClass="table table-striped table-bordered"
        GridLines="None"
        DataKeyNames="Id"
        OnRowCommand="GridLancamentos_RowCommand">

        <Columns>

            <asp:BoundField DataField="Id" HeaderText="Código" />
            <asp:BoundField DataField="Descricao" HeaderText="Descrição" />
            <asp:BoundField DataField="Tipo" HeaderText="Tipo" />

            <asp:BoundField DataField="ValorOriginal"
                HeaderText="Valor Original (R$)"
                DataFormatString="{0:N2}" />

            <asp:BoundField DataField="ValorCalculado"
                HeaderText="Valor Final (R$)"
                DataFormatString="{0:N2}" />

            <asp:BoundField DataField="Status"
                HeaderText="Situação" />

            <asp:TemplateField HeaderText="Ações">
                <ItemTemplate>
                    <div class="d-flex gap-2">

                        <asp:Button ID="BtnEditar"
                            runat="server"
                            Text="Editar"
                            CommandName="Editar"
                            CommandArgument='<%# Eval("Id") %>'
                            CssClass="btn btn-primary btn-sm"
                            CausesValidation="false"
                            Visible='<%# Eval("Status").ToString() == "Aberto" %>' />

                        <asp:Button ID="BtnPagar"
                            runat="server"
                            Text="Pagar"
                            CommandName="Pagar"
                            CommandArgument='<%# Eval("Id") %>'
                            CssClass="btn btn-success btn-sm"
                            CausesValidation="false"
                            Visible='<%# Eval("Status").ToString() == "Aberto" %>' />

                        <asp:Button ID="BtnCancelar"
                            runat="server"
                            Text="Cancelar"
                            CommandName="Cancelar"
                            CommandArgument='<%# Eval("Id") %>'
                            CssClass="btn btn-danger btn-sm"
                            CausesValidation="false"
                            Visible='<%# Eval("Status").ToString() == "Aberto" %>' />

                    </div>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>

    </asp:GridView>

</asp:Content>
