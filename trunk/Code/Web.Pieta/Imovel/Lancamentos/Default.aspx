<%@ Page Title="Pietá Imóveis - Detalhamento de Imóvel" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Imovel_Lancamento_Default" %>

<%@ Register assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" namespace="System.Web.UI.WebControls" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <%--
    <script src="http://www.pietaimoveis.com.br/Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="http://www.pietaimoveis.com.br/Scripts/js/jquery.easing.1.3.js" type="text/javascript"></script>
    <script src="http://www.pietaimoveis.com.br/Scripts/js/jquery.timers-1.2.js" type="text/javascript"></script>
    <script src="http://www.pietaimoveis.com.br/Scripts/js/jquery.galleryview-3.0.js" type="text/javascript"></script>
    --%>
    <link href="../../Scripts/css/basic.css" rel="stylesheet" type="text/css" />
    <link href="../../Scripts/css/galleriffic-3.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/js/jquery-1.3.2.js" type="text/javascript"></script>
    <script src="../../Scripts/js/jquery.galleriffic.js" type="text/javascript"></script>
    <script src="../../Scripts/js/jquery.opacityrollover.js" type="text/javascript"></script>
    <script src="../../Scripts/PicGallery.js" type="text/javascript"></script>

    <script type="text/javascript">
            document.write('<style>.noscript { display: none; }</style>');
	</script>


    <div id="divSiteNotFound" runat="server" class="center">
        
        <h2>Imóvel não cadastrado em nosso sistema
        </h2>
    </div>

    <div id="divSiteDetails" runat="server">
        <div class="searchDetailHeader">
             <asp:Repeater ID="Repeater2" runat="server" 
                DataSourceID="objectDataSourceSiteDescription" >
                <ItemTemplate>
                    <div style="float:left; padding-top: 2px;">
                        <h2>
                            <%# Eval("PropertyType")%> 
                        </h2>
                        <h3><%# Eval("City")%>, bairro <%# Eval("District")%></h3>
                        <b style="line-height: 1.5em;">Código do anúncio: <%# Eval("AdCode")%></b>
                        <br />
                    </div>
               </ItemTemplate>                                 

            </asp:Repeater>
        </div>
        <br />
        <div id="divSitePics" class="divSitePics" style="height:650px">
		    <div id="page">
			    <div id="container">
				    <!-- Start Advanced Gallery Html Containers -->
				    <div id="gallery" class="content">
					    <div id="controls" class="controls"></div>
					    <div class="slideshow-container">
						    <div id="loading" class="loader"></div>
						    <div id="slideshow" class="slideshow"></div>
					    </div>
					    <div id="caption" class="caption-container"></div>
				    </div>
				    <div id="thumbs" class="navigation">

                        <asp:Literal id="literalSitePics" runat="server" />
				    </div>
				    <div style="clear: both;"></div>
			    </div>
		    </div>

        </div>
        
        <p style="float:right; margin-right: 12px;">* Fotos ilustrarivas</p>

        <table border="0" cellpadding="8" cellspacing="0" width="932px">
            <tr>
                <td style="width: 80%; vertical-align: top;">
                    <div class="headerBlueShortLine">
                        <h1>Características</h1>
                    </div>
                    <asp:Repeater ID="Repeater4" runat="server" 
                        DataSourceID="objectDataSourceSiteDescription" >
                        <ItemTemplate>
                            <p> <%# Eval("FullDescription")%> </p>
                            <p id="P1" runat="server" visible='<%#Eval("IsCondoDescriptionVisible")%>'> <b>Infraestrutura do Condomínio: </b></p>
                            <p> <%# Eval("CondoDescription")%> </p>
                            <p id="P2" runat="server" visible='<%#Eval("IsAreaDescriptionVisible")%>'> <b>Infraestrutura do Bairro: </b></p>
                            <p> <%# Eval("AreaDescription")%> </p>
                            <div class="darkBlueHighlight">
                                <h2>Consulte-nos sobre o valor</h2>
                            </div>
                            
                        </ItemTemplate>                                 
                    </asp:Repeater>

                </td>
<%--                <td style="width: 30%; vertical-align: top;">
                    <div class="headerBlueShortLine">
                        <h1>Detalhes</h1>
                    </div>
                    <div style="margin-top:12px;">
                        <asp:Repeater ID="Repeater5" runat="server" DataSourceID="objectDataSourceSiteDetail">
                            <ItemTemplate>
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr style="height: 26px">
                                        <td>
                                            <img src="http://www.pietaimoveis.com.br/Images/Check.png" style="vertical-align: middle;
                                                padding: 2px 3px 5px 0px;" />
                                        </td>
                                        <td>
                                            <%# Eval("Value")%>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </td>--%>
                <td style="width: 20%; vertical-align: top;">
                    <div class="headerBlueShortLine">
                        <h1>Contato</h1>
                    </div>
                    <img src="http://www.pietaimoveis.com.br/Images/CallNow.png" style="float:right; margin-top: 8px; margin-bottom: auto; vertical-align:top;display: block;"/>
                </td>
            </tr>
        </table>

        <div class="buttonSearchBack">
            <a href="javascript: history.go(-1)" style="margin-left:26px; margin-top:3px">Retornar</a>
        </div>

        <br />
    </div>
    <br />

    <asp:ObjectDataSource ID="objectDataSourceSiteDetail" runat="server" 
        SelectMethod="GetPropertyAdDetails" 
        TypeName="TK1.Data.Bizz.Client.Binding.PropertyAdBindingSource" >

        <SelectParameters>
            <asp:Parameter DefaultValue="pieta" Name="customerCode" Type="String" />
            <asp:Parameter DefaultValue="Release" Name="adType" Type="String" />
            <asp:QueryStringParameter DefaultValue="0" Name="adCode" 
                QueryStringField="ID" Type="Int32" />
        </SelectParameters>

    </asp:ObjectDataSource>


    <asp:ObjectDataSource ID="objectDataSourceSiteDescription" runat="server" 
        SelectMethod="GetPropertyReleaseAd" 
        TypeName="TK1.Data.Bizz.Client.Binding.PropertyAdBindingSource" 
        onselected="objectDataSourceSiteDescription_Selected">

        <SelectParameters>
            <asp:Parameter DefaultValue="pieta" Name="customerCode" Type="String" />
            <asp:QueryStringParameter DefaultValue="0" Name="adCode" 
                QueryStringField="ID" Type="Int32" />
        </SelectParameters>

    </asp:ObjectDataSource>

        <script type="text/javascript">
            $(document).ready(function () {
                var dotCounter = 0;
                (function waitPicLoad() {
                    setTimeout(function () {
                        if (dotCounter++ < 10) {
                            waitPicLoad();
                        }
                        else {
                            jQuery(document).ready(loadGallery($));
                        }
                    }, 100);
                })();

            });
		    </script>


</asp:Content>

