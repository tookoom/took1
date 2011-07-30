<%@ Page Title="Mdo Imóveis - Detalhamento de Imóvel" Language="C#" MasterPageFile="~/Nav/Mdo/SimVendas/SimVendas.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Nav_Mdo_SimVendas_Imovel_Default" %>

<%@ Register assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" namespace="System.Web.UI.WebControls" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SimVendasHeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SimVendasMasterContent" Runat="Server">
    
    <link href="../../../../Scripts/css/basic.css" rel="stylesheet" type="text/css" />
    <link href="../../../../Scripts/css/galleriffic-3.css" rel="stylesheet" type="text/css" />

    <script src="../../../../Scripts/js/jquery-1.3.2.js" type="text/javascript"></script>
    <%--<script src="../../../../Scripts/js/jquery.history.js" type="text/javascript"></script>--%>
    <script src="../../../../Scripts/js/jquery.galleriffic.js" type="text/javascript"></script>
    <script src="../../../../Scripts/js/jquery.opacityrollover.js" type="text/javascript"></script>

    <script src="../../../../Scripts/PicGallery.js" type="text/javascript"></script>
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
                            <b><%# Eval("Site.SiteType.Name")%> bairro <%# Eval("Site.District.Name")%></b>
                        </h2>
                        <b style="line-height: 1.5em;">Código do imóvel: <%# Eval("SiteAdID")%></b>
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
        
        <table border="0" cellpadding="0" cellspacing="0" width="932px" class="siteDescription">
            <tr>
                <td class="navData">
                    <asp:Repeater ID="repeaterNavData" runat="server" DataSourceID="navDataSource">
                        <ItemTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <img src="../Imagens/<%# Eval("LogoUrl")%>" />
                                </tr>

                                <tr>
                                <p>Fone:</p>
                                </tr>

                                <tr>
                                <p><%# Eval("ContactPhone")%></p>
                                </tr>
                            </table>
                        </ItemTemplate>                                 
                    </asp:Repeater>
                    <asp:ObjectDataSource ID="navDataSource" runat="server" 
                        SelectMethod="GetNavData" 
                        TypeName="TK1.Bizz.Mdo.Data.Controller.MdoSiteController">
                        <SelectParameters>
                            <asp:QueryStringParameter DefaultValue="0" Name="customerID" 
                                QueryStringField="CustomerID" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
                <td style="width: 50%; vertical-align: top;">
                    <asp:Repeater ID="Repeater1" runat="server" 
                        DataSourceID="objectDataSourceSiteDescription" >
                        <ItemTemplate>
                            <b><h4> <%# Eval("ShortDescription")%> </h4></b>
                            <p> <b>Características do Imóvel: </b></p>
                            <p> <%# Eval("Description")%> </p>
                            <p> <b>Infraestrutura do Condomínio: </b></p>
                            <p> <%# Eval("CondDescription")%> </p>
                            <p> <b>Infraestrutura do Bairro: </b></p>
                            <p> <%# Eval("AreaDescription")%> </p>
                            <%--<div id="Div1" runat="server" visible='<%#getRentDivVisibility(Eval("AdType.AdTypeID").ToString())%>'>
                                <p> Aluguel: <%# Eval("Price", "{0:c}")%> </p>
                                <p> IPTU: <%# Eval("IPTU", "{0:c}")%> </p>
                                <p> Condomínio: <%# Eval("Cond", "{0:c}")%> </p>
                            </div>--%>
                            <br />
                            <div class="sitePrice">
                                Valor: <%# Eval("Price", "{0:c}")%>
                            </div>
                        </ItemTemplate>                                 
                    </asp:Repeater>

                </td>
                <td style="width: 5%; vertical-align: top;"></td>
                <td style="width: 20%; vertical-align: top;">
                    <p> <b>Detalhes:</b></p>
                    <asp:Repeater ID="Repeater3" runat="server" 
                        DataSourceID="objectDataSourceSiteDetail" >
                        <ItemTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" >
                                <tr style="height: 26px">
                                    <td><img src="../Imagens/Dot.png" style="vertical-align:middle; padding: 2px 3px 5px 0px;"/>
                                    </td>
                                    <td><%--<p style="padding: 3px; vertical-align:middle;"> <%# Eval("Name")%> </p>--%>
                                    <%# Eval("Name")%>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>                                 
                    </asp:Repeater>
                </td>
            </tr>
        </table>
        <div class="buttonSearchBack">
            <a href="javascript: history.go(-1)" style="margin-top:4px; font-size:medium; font-weight:bold; text-align:right;">Retornar à pesquisa</a>
        </div>
        <br />
    </div>
    <br />

    <asp:ObjectDataSource ID="objectDataSourceSiteDetail" runat="server" 
        SelectMethod="GetSiteDetail" TypeName="TK1.Bizz.Mdo.Data.Controller.MdoSiteController" >

        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="0" Name="customerID" 
                QueryStringField="CustomerID" Type="Int32" />
            <asp:QueryStringParameter DefaultValue="0" Name="siteAdID" 
                QueryStringField="ID" Type="Int32" />
        </SelectParameters>

    </asp:ObjectDataSource>

    <asp:ObjectDataSource ID="objectDataSourceSiteDescription" runat="server" 
        SelectMethod="GetSiteAd" TypeName="TK1.Bizz.Mdo.Data.Controller.MdoSiteController" 
        onselected="objectDataSourceSiteDescription_Selected">

        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="0" Name="customerID" 
                QueryStringField="CustomerID" Type="Int32" />
            <asp:QueryStringParameter DefaultValue="0" Name="siteAdID" 
                QueryStringField="ID" Type="Int32" />
        </SelectParameters>

    </asp:ObjectDataSource>

<%--    <asp:ObjectDataSource ID="objectDataSourceSitePics" runat="server" 
        SelectMethod="GetSitePics" TypeName="TK1.Bizz.Mdo.Selling.SellingSitePicHelper">
        <SelectParameters>
            <asp:QueryStringParameter Name="siteAdID" QueryStringField="ID" Type="Int32" />
            <asp:Parameter DefaultValue="AdType" Name="siteAdType" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>--%>


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

