<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="User_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login TK1</title>
    <link href="../Styles/Login.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="page">
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <img src="../Images/LogoTK1_Small.jpg" alt="TK1" />
                </td>
                <td>
                    <div>
                        <h1>
                            Login
                        </h1>
                        <p>
                            Digite seu e-mail e senha.
                        </p>
                        <table width="240px">
                            <tr>
                                <td>Usuário: </td>
                                <td><asp:TextBox ID="textBoxUserName" runat="server" Width="100%" /></td>
                            </tr>
                            <tr>
                                <td>Senha: </td>
                                <td><asp:TextBox ID="textBoxUserPassword" runat="server" TextMode="Password"  Width="100%" /></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td><asp:LinkButton Text="Entrar" runat="server" ID="linkButtonLogin" 
                                        onclick="linkButtonLogin_Click" /></td>
                            </tr>
                            <tr>
                                <td colspan="2" style="color:Maroon">
                                    <div id="divLoginFail" runat="server" visible="false">Falha ao conectar.<br /> Verifique e-mail e senha.</div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </div>    
    </form>
</body>
</html>
