<%@ Page Title="" Language="C#" MasterPageFile="~/Bizz/BizzApp.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Bizz_User_Settings_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BizzContentPlaceHolder" Runat="Server">

    <div class="page">
        <h1>Configurações do usuário</h1>
        <p> Nome do usuário: 
            <asp:Literal ID="literalUserName" Text="Usuário" runat="server" /></p>
        <p> Empresa: 
            <asp:Literal ID="literalCustomerName" Text="Empresa" runat="server" /></p>
        <h2>Troca de Senha:</h2>
        <table border="0" cellpadding="4" cellspacing="0">
            <tr>
                <td>Digite a senha atual: </td>
                <td><asp:TextBox runat="server" ID="textBoxUserPassword" TextMode="Password"/></td>
            </tr>
            <tr>
                <td>Digite a nova senha:</td>
                <td><asp:TextBox runat="server" ID="textBoxUserNewPassword" TextMode="Password"/></td>
            </tr>
            <tr>
                <td>Digite novamente a nova senha:</td>
                <td><asp:TextBox runat="server" ID="textBoxUserNewPasswordTest" TextMode="Password"/></td>
            </tr>
        </table>
            
        <div id="divLoginFail" runat="server" visible="false" style="color:Maroon;">Falha ao conectar.<br /> Verifique a senha.</div>
        <div id="divPasswordNotMatching" runat="server" visible="false" style="color:Maroon;">Não foi possível trocar a senha.<br /> Digite duas vezes uma nova senha válida nos campos indicados.</div>
        
        <hr />
        <div class="darkLinkButton">
            <asp:LinkButton Text="Salvar" runat="server" ID="linkButtonSave" OnClick="linkButtonSave_Click" />
            |
            <asp:LinkButton Text="Cancelar" runat="server" ID="linkButtonCancel" OnClick="linkButtonCancel_Click" />
        </div>
    </div>
</asp:Content>

