<%@ Page Title="Pietá Imóveis - Página Inicial" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <script src="Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="Scripts/EasySlider/easySlider1.7.js" type="text/javascript"></script>
    <link href="Styles/Release.css" rel="stylesheet" type="text/css" />
    <link href="Styles/EasySlider/SiteRelease.css" rel="stylesheet" type="text/css" />

    	<script type="text/javascript">
		$(document).ready(function(){	
			$("#slider").easySlider({
				auto: true, 
				continuous: true,
				numeric: true,
				pause: 5000,
				controlsShow: true,
			});
		});	
	</script>
    <div class="headerBlueLine"><h1>Lançamentos</h1></div>

<%--    <div  id="slider" class="releaseViewer">
    		<ul>				
				<li>
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <div class="releaseViewerImage">
                                    <img src="Imovel/Fotos/Venda/1/foto_00001_01_Fachada_____________.jpg" />
                                </div>
                            </td>
                            <td>
                                <div class="releaseViewerInfo">
                                    <div class="headerBlueShortLine">
                                        <h1>Lançamento</h1>
                                    </div>
                                    <h3>Apartamento 2 - bairro Nonoai</h3>
                                    <p>
                                        Descrição resumida do imóvel 2</p>
                                    <h4>
                                        80m² a 100m²</h4>
                                    <h4>
                                        2 a 3 dormitórios</h4>
                                    <a href="/Imovel/Lancamentos/?ID=1"><b>Detalhes</b></a>
                                </div>
                            </td>
                        </tr>
                    </table>
                </li>
			</ul>

    </div>
--%>
   <asp:Literal id="literalSiteReleaseAds" runat="server" />

<br />
    <div class="headerBlueLine"><h1>Destaques Vendas</h1></div>
    <div class="featuredSites">

        <asp:DataList ID="dataListFeaturedSiteAds" runat="server" 
            DataSourceID="objectDataSourceFeaturedSites" RepeatDirection="Horizontal" RepeatLayout="Table">
            <ItemTemplate>
                <div style="padding:8px;">
                    <div style="padding:8px;background:#efefef;">
                    <a href="../Imovel/Default.aspx?ID=<%# Eval("Code")%>&AdTypeID=<%# Eval("AdTypeID")%>">
                        <img src="http://www.tk1.net.br/Integra/Mdo/SimVendas/Fotos/4/<%# Eval("Code") %>/<%# Eval("MainPicUrl") %>"
                            height="100px" style="margin: 6px 0px 4px 0px;" />
                    </a>
                    <h2><%# Eval("District")%></h2>
                    <h3><%# Eval("SiteType")%></h3>
                    <p><%# Eval("SiteTotalRooms")%>  <%# Eval("SiteTypeRoomName")%></p>
                    <p><%# Eval("SiteTotalArea")%>  m²</p>
                    <p><%# Eval("Value", "{0:c}")%></p>
                    <a href="../Imovel/Default.aspx?ID=<%# Eval("Code")%>&AdTypeID=<%# Eval("AdTypeID")%>">Detalhes</a>

                    </div >
                </div>
            </ItemTemplate>
        </asp:DataList>


    </div>

        <table border="0" cellpadding="0" cellspacing="0" width="932px">
            <tr>
                <td style="width: 60%; vertical-align: top; padding-right:16px;">
                    <div class="headerBlueShortLine">
                        <h1>Vantagens Pietá Imóveis</h1>
                    </div>
                </td>
                <td style="width: 40%; vertical-align: top;">
                    <div class="headerBlueShortLine">
                        <h1>Redes Sociais</h1>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="vertical-align:top;">
                    <table border="0" cellpadding="4" cellspacing="0">
                        <tr>
                            <td>
                                <img src="Images/CaixaAqui.jpg" />
                            </td>
                            <td style="vertical-align:top;">
                                <p>Os correspondentes Caixa Aqui trazem várias vantagens e serviços para os usuários.</p>
                                <a href="http://www1.caixa.gov.br/atendimento/canais_atendimento/correspondentes_bancarios.asp">Saiba mais</a>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table border="0" cellpadding="4" cellspacing="0">
                        <tr>
                            <td>
                                <a href="http://www.facebook.com/profile.php?id=100002698020190">
                                    <img src="Images/FacebookIcon.png" alt="Pietá no Facebook" 
                                    height="90px"/></a>
                            </td>
                            <td style="vertical-align:top; ">
                                <p>
                                    Novidades, informações e contato.
                                </p>
                                <a href="http://www.facebook.com/profile.php?id=100002698020190">Conheça a página da
                                    Pietá Imóveis no Facebook!</a>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

    <div class="clear releases">
    <br />
    <br />
    <br />
        <br />

        <br />
        <br />
    </div>


    <asp:ObjectDataSource ID="objectDataSourceFeaturedSites" runat="server" 
        SelectMethod="GetFeaturedSiteAds" 
        TypeName="TK1.Bizz.Mdo.Data.Controller.MdoSiteAdController">
        <SelectParameters>
            <asp:Parameter DefaultValue="pieta" Name="mdoAcronym" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>

    <asp:ObjectDataSource ID="objectDataSourceSiteReleases" runat="server" 
        SelectMethod="GetSiteReleaseAds" 
        TypeName="TK1.Bizz.Pieta.Data.Controller.PietaSiteAdController">
    </asp:ObjectDataSource>

    <%--<script type="text/javascript">
            $(document).ready(function () {
                var options1 = {
            }
            var options2 = {
                caption: false,
                navigation: 'permanent',
                direction: 'left'
            }
            var options3 = {
                caption: 'permanent',
                opacity: 1
            }

            //        $('#ppy1').popeye(options1);
            //        $('#ppy2').popeye(options2);
            $('#ppy3').popeye(options3);
        });
    </script>--%>


</asp:Content>
