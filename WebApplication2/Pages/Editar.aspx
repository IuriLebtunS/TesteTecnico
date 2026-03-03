<%@ Page Title="Editar Lançamento" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Editar.aspx.cs" Inherits="Financeiro.WebApp.Pages.Editar" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-4">

        <div>
            <h2>
                <i class="fa-solid fa-pen-to-square"></i>
                Editar Lançamento
            </h2>
        </div>

        <asp:HiddenField ID="HiddenId" runat="server" />

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
                    <label class="form-label">Data</label>
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
                        CssClass="form-select"
                        AutoPostBack="true"
                        OnSelectedIndexChanged="SelectTipo_SelectedIndexChanged">

                        <asp:ListItem Text="Crédito" Value="1" />
                        <asp:ListItem Text="Débito" Value="2" />

                    </asp:DropDownList>
                </div>

            </div>

            <asp:Panel ID="PanelTaxa" runat="server" Visible="false">
                <div class="row">
                    <div class="col-md-6 mb-3">
                        <label class="form-label">Percentual Taxa</label>
                        <asp:TextBox
                            ID="TxtTaxa"
                            runat="server"
                            CssClass="form-control" />
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="PanelDesconto" runat="server" Visible="false">
                <div class="row">
                    <div class="col-md-6 mb-3">
                        <label class="form-label">Percentual Desconto</label>
                        <asp:TextBox
                            ID="TxtDesconto"
                            runat="server"
                            CssClass="form-control" />
                    </div>
                </div>
            </asp:Panel>

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
                    Text="Salvar Alterações"
                    CssClass="btn btn-primary ms-2"
                    OnClick="BtnSalvar_Click" />

            </div>

        </div>

    </div>

</asp:Content>
