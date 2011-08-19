<%@ Page Title="Pietá Imóveis - Contato" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="FaleConosco_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

        <div class="headerBlueLine">
            <h2>Envio de Mensagem</h2>
        </div>
        <img src="http://www.pietaimoveis.com.br/Images/FaleConosco.png" style="float:right; position:absolute; margin-left:456px; margin-top:120px; " />

        <table border="0" cellpadding="0" cellspacing="0" width="480px" >
            <tr>
                <td style="width: 70px; vertical-align:top; padding-left:8px;"><p>Quero:</p></td>
                    <td style="width: 410px; vertical-align:top; padding-top:12px; font-size:1.3em;">
                        <asp:RadioButtonList ID="radioButtonListContactType" runat="server">
                            <asp:ListItem Text="Informações gerais" Value="Informações gerais" Selected="True" class="radioItem"></asp:ListItem>
                            <asp:ListItem Text="Informações sobre venda de imóveis" Value="Vendas" class="radioItem"></asp:ListItem>
                            <asp:ListItem Text="Informações sobre locação de imóveis" Value="Aluguel" class="radioItem"></asp:ListItem>
                            <asp:ListItem Text="Trabalhar na Pietá" Value="Trabalhar na Pietá" class="radioItem"></asp:ListItem>
                            <asp:ListItem Text="Deixar uma sugestão ou reclamação" Value="Sugestão/Reclamação" class="radioItem"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
            </tr>
            <tr>
                <td style="width: 70px; vertical-align:middle; padding-left:8px;"><p>Nome:</p></td>
                <td style="width: 410px; vertical-align:middle; padding-top:0px; font-size:1.3em;">
                    <asp:TextBox ID="textBoxContactName" runat="server" TextMode="SingleLine" 
                        Width="100%"  ToolTip="Nome para contato" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 70px; vertical-align:middle; padding-left:8px;"><p>E-mail:</p></td>
                <td style="width: 410px; vertical-align:middle; padding-top:7px; font-size:1.3em;">
                    <asp:TextBox ID="textBoxContactMail" runat="server" TextMode="SingleLine" Width="100%"  ToolTip="E-mail para contato"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 70px; vertical-align:middle; padding-left:8px;"><p>Telefone:</p></td>
                <td style="width: 410px; vertical-align:middle; padding-top:7px; font-size:1.3em;">
                    <asp:TextBox ID="textBoxContactPhone" runat="server" TextMode="SingleLine" Width="100%" ToolTip="Telefone para contato" ></asp:TextBox>
                </td>
            </tr>
        </table>
        <br />
        <table  border="0" cellpadding="0" cellspacing="0" width="480px" >
            <tr>
                <asp:TextBox ID="textBoxContactMessage" runat="server" TextMode="MultiLine" Width="458px" Text="Digite aqui a sua mensagem" ToolTip="Digite aqui a sua mensagem para a Pietá Imóveis" Height="100px" 
                    style="font-family: Arial, Helvetica, Verdana, sans-serif; padding-left:16px; margin-left:8px;"></asp:TextBox>
            </tr>
            <tr>
                <td style="width: 150px; vertical-align:top; padding-left:8px;"><p>Prefiro ser contactado:</p></td>
                <td style="width: 180px; vertical-align:top; padding-top:12px; font-size:1.3em;">
                    <asp:RadioButtonList ID="radioButtonListResponseType" runat="server">
                        <asp:ListItem Text="Indiferente" Value="Indiferente" Selected="True" class="radioItem"></asp:ListItem>
                        <asp:ListItem Text="Por telefone" Value="Telefone" class="radioItem"></asp:ListItem>
                        <asp:ListItem Text="Por e-mail" Value="E-mail" class="radioItem"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td style="vertical-align:top; padding-left:8px; padding-top:12px; width:98px">
                    <div class="buttonSend" ><asp:LinkButton ID="buttonSendMessage" Text="Enviar" runat="server" 
                        style="margin: 2px 0px 2px 12px;"
                            OnClick="buttonSendMessage_OnClick" /></div>
                    
                </td>
            </tr>
        </table>
        <br />

</asp:Content>

