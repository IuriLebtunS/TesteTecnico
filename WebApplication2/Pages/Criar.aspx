
<%@ Page Title="Novo Lançamento" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Criar.aspx.cs" Inherits="Financeiro.WebApp.Pages.Criar" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

<h2>Novo Lançamento</h2>

<asp:TextBox ID="TxtDescricao" runat="server" Placeholder="Descrição" /><br />
<asp:TextBox ID="TxtValor" runat="server" Placeholder="Valor" /><br />
<asp:TextBox ID="TxtCompetencia" runat="server" Placeholder="Competência (MM/AAAA)" /><br />

<asp:DropDownList ID="SelectTipo" runat="server">
    <asp:ListItem Text="Crédito" Value="1" />
    <asp:ListItem Text="Débito" Value="2" />
</asp:DropDownList>

<br /><br />

<asp:Button ID="BtnSalvar" runat="server" Text="Salvar" OnClick="BtnSalvar_Click" />

</asp:Content>