<%@ Page Title="Piet� Im�veis - Contato" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="FaleConosco_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <div class="clear contact">
        <h2>
            Envio de Mensagem
        </h2>

        <table border="0" cellpadding="0" cellspacing="0" width="98%">
            <tr>
                <td>
                    <h3>
                        Quero:
                    </h3>
                    <asp:RadioButtonList ID="radioButtonListContactType" runat="server">
                        <asp:ListItem Text="Informa��es gerais" Value="Informa��es gerais" Selected="True" class="radioItem"></asp:ListItem>
                        <asp:ListItem Text="Informa��es sobre venda de im�veis" Value="Vendas" class="radioItem"></asp:ListItem>
                        <asp:ListItem Text="Informa��es sobre loca��o de im�veis" Value="Aluguel" class="radioItem"></asp:ListItem>
                        <asp:ListItem Text="Trabalhar na Piet�" Value="Trabalhar na Piet�" class="radioItem"></asp:ListItem>
                        <asp:ListItem Text="Deixar uma sugest�o ou reclama��o" Value="Sugest�o/Reclama��o" class="radioItem"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>
                    <h3>
                        Meus dados:
                    </h3>

                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td><p>Nome:</p></td>
                            <td>
                                <asp:TextBox ID="textBoxContactName" runat="server" TextMode="SingleLine" Width="96%"  ToolTip="Nome para contato" Font-Names=' "Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif'></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td><p>Telefone:</p></td>
                            <td>
                                <asp:TextBox ID="textBoxContactPhone" runat="server" TextMode="SingleLine" Width="96%" ToolTip="Telefone para contato" Font-Names=' "Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif'></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td><p>E-mail:</p></td>
                            <td>
                                <asp:TextBox ID="textBoxContactMail" runat="server" TextMode="SingleLine" Width="96%"  ToolTip="E-mail para contato" Font-Names=' "Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif'></asp:TextBox>
                            </td>
                        </tr>
                    </table>                
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="0" cellspacing="0" width="98%">
            <tr>
                <br />
                <h3>
                    Prefiro ser contactado:
                </h3>
                <asp:RadioButtonList ID="radioButtonListResponseType" runat="server">
                    <asp:ListItem Text="Indiferente" Value="Indiferente" Selected="True" class="radioItem"></asp:ListItem>
                    <asp:ListItem Text="Por telefone" Value="Telefone" class="radioItem"></asp:ListItem>
                    <asp:ListItem Text="Por e-mail" Value="E-mail" class="radioItem"></asp:ListItem>
                </asp:RadioButtonList>

            </tr>
        </table>
        <br />
        <table border="0" cellpadding="0" cellspacing="0" width="100%" Margin="9px">
            <tr>
                <asp:TextBox ID="textBoxContactMessage" runat="server" TextMode="MultiLine" Width="98%" Text="Digite aqui a sua mensagem" ToolTip="Digite aqui a sua mensagem para a Piet� Im�veis" Height="100px" Font-Names=' "Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif'></asp:TextBox>
            </tr>
            <tr>
                <asp:Button ID="buttonSendMessage" runat="server" Text="Enviar" style="float: right; margin:9px 18px 9px 18px" OnClick="buttonSendMessage_OnClick"/>
            </tr>
        </table>


    </div>

</asp:Content>

