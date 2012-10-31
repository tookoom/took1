<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Contato_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h1>Contato</h1>
    <asp:RadioButtonList ID="radioButtonListContactType" runat="server" RepeatDirection="Horizontal" >
        <asp:ListItem Text="Informações Gerais" Value="Contato" Selected="True" class="radioItem"></asp:ListItem>
        <asp:ListItem Text="Vendas" Value="Vendas" class="radioItem"></asp:ListItem>
        <asp:ListItem Text="Aluguel" Value="Aluguel" class="radioItem"></asp:ListItem>
    </asp:RadioButtonList>
    <br />
    <table border="0" cellpadding="0" cellspacing="0" width="100%" >
       <tr>
            <td >Nome:</td>
            <td width="30%"><asp:TextBox ID="textBoxContactName" runat="server" TextMode="SingleLine" Width="98%"  ToolTip="Nome para contato" ></asp:TextBox></td>
            <td width="50%" rowspan="3"><asp:TextBox ID="textBoxContactMessage" Width="98%" Height="100%" runat="server" TextMode="MultiLine" Text="" ToolTip="Digite aqui a sua mensagem para a Pandolfo Imóveis"></asp:TextBox></td>
            <td rowspan="3">
                <asp:LinkButton ID="buttonSendMessage"  CssClass="lnkButton" runat="server" Height="100%" Width="90%" OnClick="buttonSendMessage_OnClick">
                        Enviar
                        <img src="../Imagens/MailSmall.png" />
                </asp:LinkButton></td>
        </tr>
        <tr>
            <td>E-mail:</td>
            <td><asp:TextBox ID="textBoxContactMail" runat="server" TextMode="SingleLine" Width="98%"  ToolTip="E-mail para contato" ></asp:TextBox></td>
        </tr>
        <tr>
            <td>Telefone:</td>
            <td><asp:TextBox ID="textBoxContactPhone" runat="server" TextMode="SingleLine" Width="98%"  ToolTip="Telefone para contato" ></asp:TextBox></td>
        </tr>
    </table>
    <br />
    <h1>Localização</h1>
    <p>
    Av Getúlio Vargas, 202, Bairro Menino Deus, Porto Alegre
    </p>
    <p>Fones: (51) 3221.9869 e (51) 3225.7116</p>
    <p>Horário de Atendimento: segunda a sexta 9h às 12h e 14h às 17h</p>
    <br />
    <iframe width="940" height="400" frameborder="0" scrolling="no" marginheight="0" marginwidth="0" src="https://maps.google.com.br/maps?q=Av+Get%C3%BAlio+Vargas,+202&amp;ie=UTF8&amp;hq=&amp;hnear=Av.+Get%C3%BAlio+Vargas,+202+-+Menino+Deus,+Porto+Alegre+-+Rio+Grande+do+Sul&amp;gl=br&amp;t=m&amp;ll=-30.041347,-51.222124&amp;spn=0.01486,0.04034&amp;z=15&amp;iwloc=A&amp;output=embed"></iframe>
    <a href="https://maps.google.com.br/maps?q=Av+Get%C3%BAlio+Vargas,+202&amp;ie=UTF8&amp;hq=&amp;hnear=Av.+Get%C3%BAlio+Vargas,+202+-+Menino+Deus,+Porto+Alegre+-+Rio+Grande+do+Sul&amp;gl=br&amp;t=m&amp;ll=-30.041347,-51.222124&amp;spn=0.01486,0.04034&amp;z=15&amp;iwloc=A&amp;source=embed"  target="_new" ><b>Crie seu itinerário</b></a>

    <br />
</asp:Content>

