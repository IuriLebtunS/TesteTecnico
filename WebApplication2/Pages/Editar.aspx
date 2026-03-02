<%@ Page Title="Editar Lançamento" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Editar.aspx.cs" Inherits="Financeiro.WebApp.Pages.Editar" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Editar Lançamento</h2>

    <asp:HiddenField ID="HiddenId" runat="server" />

    <asp:TextBox ID="TxtDescricao" runat="server" /><br />
    <asp:TextBox ID="TxtValor" runat="server" /><br />
    <asp:TextBox ID="TxtCompetencia" runat="server" /><br />


    <asp:DropDownList ID="SelectTipo"
        runat="server"
        AutoPostBack="true"
        OnSelectedIndexChanged="SelectTipo_SelectedIndexChanged">

        <asp:ListItem Text="Crédito" Value="1" />
        <asp:ListItem Text="Débito" Value="2" />
    </asp:DropDownList>

    <asp:Panel ID="PanelTaxa" runat="server" Visible="false">
        <div class="mb-3">
            <label>Percentual Taxa</label>
            <asp:TextBox ID="TxtTaxa" runat="server" CssClass="form-control" />
        </div>
    </asp:Panel>

    <asp:Panel ID="PanelDesconto" runat="server" Visible="false">
        <div class="mb-3">
            <label>Percentual Desconto</label>
            <asp:TextBox ID="TxtDesconto" runat="server" CssClass="form-control" />
        </div>
    </asp:Panel>

    <br />

    <asp:Button ID="BtnSalvar" runat="server" Text="Salvar Alterações" OnClick="BtnSalvar_Click" />

</asp:Content>
