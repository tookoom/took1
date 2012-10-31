<%@ Page Title="Pandolfo Imóveis - Detalhamento de Imóvel" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Imovel_Default" %>

<%@ Register assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" namespace="System.Web.UI.WebControls" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    
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
                    <div class="siteHeaderPrice">   
                        <h2><%# Eval("AdTypeName")%>: <%# Eval("Value", "{0:c}")%></h2>
                        
                    </div>
                    <div >
                        <h2>
                            <b><%# Eval("SiteType")%> bairro <%# Eval("District")%></b>
                        </h2>
                        <b style="line-height: 1.5em;">Código do imóvel: <%# Eval("Code")%></b>
                      
                    </div>
                    
               </ItemTemplate>                                 

            </asp:Repeater>
        </div>
        <br />
        <div id="divSitePics" class="divSitePics" style="height:650px">
		    <div id="page">
			    <div id="container">
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
        
        <table border="0" cellpadding="0" cellspacing="0" width="100%" class="siteDescription">
            <tr>
                <td style="width: 50%; vertical-align: top;">
                    <asp:Repeater ID="Repeater1" runat="server" 
                        DataSourceID="objectDataSourceSiteDescription" >
                        <ItemTemplate>
                            <b><h4> <%# Eval("ShortDescription")%> </h4></b>
                            <p> <b>Características do Imóvel: </b></p>
                            <p> <%# Eval("FullDescription")%> </p>
                            <p runat="server" visible='<%#Eval("IsCondoDescriptionVisible")%>'> <b>Infraestrutura do Condomínio: </b></p>
                            <p> <%# Eval("CondoDescription")%> </p>
                            <p runat="server" visible='<%#Eval("IsAreaDescriptionVisible")%>'> <b>Infraestrutura do Bairro: </b></p>
                            <p> <%# Eval("AreaDescription")%> </p>
                            <br />
                            <div class="sitePrice">
                                Valor: <%# Eval("Value", "{0:c}")%>
                                <div id="Div1" runat="server" visible='<%#getRentDivVisibility(Eval("AdTypeID").ToString())%>'>
                                    <p> IPTU: <%# Eval("CityTaxes", "{0:c}")%> </p>
                                    <p> Condomínio: <%# Eval("CondoTaxes", "{0:c}")%> </p>
                                </div>

                            </div>
                        </ItemTemplate>                                 
                    </asp:Repeater>

                </td>
                <td style="width: 5%; vertical-align: top;"></td>
                <td style="width: 20%; vertical-align: top;">
                    <p> <b>Detalhes:</b></p>
                    <asp:Repeater ID="Repeater3" runat="server" DataSourceID="objectDataSourceSiteDetail" >
                        <ItemTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" >
                                <tr style="height: 26px">
                                    <td><img src="../Imagens/Dot.png" style="vertical-align:middle; padding: 2px 3px 5px 0px;" /></td>
                                    <td><%# Eval("Name")%></td>
                                </tr>
                            </table>
                        </ItemTemplate>                                 
                    </asp:Repeater>
                </td>
                <td style="width: 20%; vertical-align: top;">
                    <p> <b>Solicite Contato</b></p>
                    Preencha seus dados e a Pandolfo Imóveis entra em contato com você.
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
        SelectMethod="GetSiteDetail" TypeName="TK1.Bizz.Data.Controller.SiteAdController" >

        <SelectParameters>
            <asp:Parameter DefaultValue="Pandolfo" Name="customerCodename" Type="String" />
            <asp:QueryStringParameter DefaultValue="0" Name="siteAdTypeID" QueryStringField="AdTypeID" Type="Int32" />
            <asp:QueryStringParameter DefaultValue="0" Name="siteAdID" QueryStringField="ID" Type="Int32" />
        </SelectParameters>

    </asp:ObjectDataSource>

    <asp:ObjectDataSource ID="objectDataSourceSiteDescription" runat="server" 
        SelectMethod="GetSiteAdView" TypeName="TK1.Bizz.Data.Controller.SiteAdController" 
        onselected="objectDataSourceSiteDescription_Selected">

        <SelectParameters>
            <asp:Parameter DefaultValue="Pandolfo" Name="customerCodename" Type="String" />
            <asp:QueryStringParameter DefaultValue="0" Name="siteAdTypeID" QueryStringField="AdTypeID" Type="Int32" />
            <asp:QueryStringParameter DefaultValue="0" Name="siteAdID" QueryStringField="ID" Type="Int32" />
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

