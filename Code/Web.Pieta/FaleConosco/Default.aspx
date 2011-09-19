<%@ Page Title="Piet� Im�veis - Contato" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="FaleConosco_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <div class="headerBlueLine">
        <h1>Envio de Mensagem</h1>
    </div>
    <img src="http://www.pietaimoveis.com.br/Images/FaleConosco.png" style="float:right; position:absolute; margin-left:456px; margin-top:120px; " />

        <table border="0" cellpadding="0" cellspacing="0" width="480px" >
            <tr>
                <td style="vertical-align:top;"><p>Quero:</p></td>
                <td style="padding-top:16px;">
                    <asp:RadioButtonList ID="radioButtonListContactType" runat="server">
                        <asp:ListItem Text="Informa��es gerais" Value="Informa��es gerais" Selected="True" class="radioItem"></asp:ListItem>
                        <asp:ListItem Text="Informa��es sobre venda de im�veis" Value="Vendas" class="radioItem"></asp:ListItem>
                        <asp:ListItem Text="Informa��es sobre loca��o de im�veis" Value="Aluguel" class="radioItem"></asp:ListItem>
                        <asp:ListItem Text="Trabalhar na Piet�" Value="Trabalhar na Piet�" class="radioItem"></asp:ListItem>
                        <asp:ListItem Text="Deixar uma sugest�o ou reclama��o" Value="Sugest�o/Reclama��o" class="radioItem"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td><p>Nome:</p></td>
                <td>
                    <asp:TextBox ID="textBoxContactName" runat="server" TextMode="SingleLine" 
                        Width="100%"  ToolTip="Nome para contato" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td><p>E-mail:</p></td>
                <td>
                    <asp:TextBox ID="textBoxContactMail" runat="server" TextMode="SingleLine" Width="100%"  ToolTip="E-mail para contato"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td><p>Telefone:</p></td>
                <td>
                    <asp:TextBox ID="textBoxContactPhone" runat="server" TextMode="SingleLine" Width="100%" ToolTip="Telefone para contato" ></asp:TextBox>
                </td>
            </tr>
        </table>
        <br />
        <table  border="0" cellpadding="0" cellspacing="0" width="480px" >
            <tr>
                <asp:TextBox ID="textBoxContactMessage"  class="input" runat="server" TextMode="MultiLine" Width="480px" Text="Digite aqui a sua mensagem" ToolTip="Digite aqui a sua mensagem para a Piet� Im�veis" Height="100px" ></asp:TextBox>
            </tr>
            <tr>
                <td style="width: 150px; vertical-align:top;"><p>Prefiro ser contactado:</p></td>
                <td style="width: 180px; vertical-align:top;padding-top:16px;">
                    <asp:RadioButtonList ID="radioButtonListResponseType" runat="server">
                        <asp:ListItem Text="Indiferente" Value="Indiferente" Selected="True" class="radioItem"></asp:ListItem>
                        <asp:ListItem Text="Por telefone" Value="Telefone" class="radioItem"></asp:ListItem>
                        <asp:ListItem Text="Por e-mail" Value="E-mail" class="radioItem"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td style="vertical-align:top; padding-left:8px; padding-top:12px; width:98px">
                    <div class="buttonSend" ><asp:LinkButton ID="buttonSendMessage" Text="Enviar" runat="server" 
                        style="margin: -4px 0px 0px 8px;"
                            OnClick="buttonSendMessage_OnClick" /></div>
                    
                </td>
            </tr>
        </table>
        <br />

</asp:Content>

