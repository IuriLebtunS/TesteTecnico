<%@ Page Title="Editar Lançamento" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Editar.aspx.cs" Inherits="Financeiro.WebApp.Pages.Editar" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

<h2>Editar Lançamento</h2>

<asp:HiddenField ID="HiddenId" runat="server" />

<asp:TextBox ID="TxtDescricao" runat="server" /><br />
<asp:TextBox ID="TxtValor" runat="server" /><br />
<asp:TextBox ID="TxtCompetencia" runat="server" /><br />

<br />

<asp:Button ID="BtnSalvar" runat="server" Text="Salvar Alterações" OnClick="BtnSalvar_Click" />

</asp:Content>