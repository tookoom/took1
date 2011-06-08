<%@ Page Title="Pietá Imóveis - Detalhamento de Imóvel" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Imovel_Novo_Default" %>

<%@ Register assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" namespace="System.Web.UI.WebControls" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    
    <link href="../../Scripts/css/basic.css" rel="stylesheet" type="text/css" />
    <link href="../../Scripts/css/galleriffic-3.css" rel="stylesheet" type="text/css" />

    <script src="../../Scripts/js/jquery-1.3.2.js" type="text/javascript"></script>
    <script src="../../Scripts/js/jquery.history.js" type="text/javascript"></script>
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
        <div class="headerBlueLine">
            <h2>Detalhes</h2>
        </div>

        <div class="searchDetailHeader">
             <asp:Repeater ID="Repeater2" runat="server" 
                DataSourceID="objectDataSourceSiteDescription" >
                <ItemTemplate>
                    <div style="float:left; padding-top: 2px;">
                        <h2>
                            <%# Eval("Site.SiteType.Name")%> - <%# Eval("AdType.Name")%> 
                        </h2>
                        <h3><%# Eval("Site.City.Name")%>, bairro <%# Eval("Site.District.Name")%></h3>
                        <b style="line-height: 1.5em;">Código do anúncio: <%# Eval("SiteAdID")%></b>
                        <br />
                    </div>
                    <div style="float:right; padding-top: 14px;">
                        <h1> <%# Eval("Price", "{0:c}")%> </h1>
                    </div>
               </ItemTemplate>                                 

            </asp:Repeater>
        </div>
        <br />
        <div id="divSitePics" class="divSitePics">
            
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
					    <%--<ul class="thumbs noscript">
						    <li>
							    <a class="thumb" name="leaf" href="http://farm4.static.flickr.com/3261/2538183196_8baf9a8015.jpg" title="Title #0">
								    <img src="http://farm4.static.flickr.com/3261/2538183196_8baf9a8015_s.jpg" alt="Title #0" />
							    </a>
							    <div class="caption">
								    <div class="download">
									    <a href="http://farm4.static.flickr.com/3261/2538183196_8baf9a8015_b.jpg">Download Original</a>
								    </div>
								    <div class="image-title">Title #0</div>
								    <div class="image-desc">Description</div>
							    </div>
						    </li>

						    <li>
							    <a class="thumb" name="drop" href="http://farm3.static.flickr.com/2404/2538171134_2f77bc00d9.jpg" title="Title #1">
								    <img src="http://farm3.static.flickr.com/2404/2538171134_2f77bc00d9_s.jpg" alt="Title #1" />
							    </a>
							    <div class="caption">
								    Any html can be placed here ...
							    </div>
						    </li>
					    </ul>--%>
                        <asp:Literal id="literalSitePics" runat="server" />
				    </div>
				    <div style="clear: both;"></div>
			    </div>
		    </div>

        </div>
        
        <div class="headerBlueLine">
            <h3>Descrição</h3>
        </div>

        <table border="0" cellpadding="0" cellspacing="0" width="932px">
            <tr>
                <td style="width: 50%; vertical-align: top;">
                    <asp:Repeater ID="Repeater1" runat="server" 
                        DataSourceID="objectDataSourceSiteDescription" >
                        <ItemTemplate>
                            <b><h4> <%# Eval("Title")%> </h4></b>
                            <p> <b>Características do Imóvel: </b></p>
                            <p> <%# Eval("Description")%> </p>
                            <p> <b>Infraestrutura do Condomínio: </b></p>
                            <p> Informações sobre o condomínio </p>
                            <p> <b>Infraestrutura do Bairro: </b></p>
                            <p> Informações sobre o bairro </p>
                            <div id="Div1" runat="server" visible='<%#getRentDivVisibility(Eval("AdType.AdTypeID").ToString())%>'>
                                <p> Aluguel: <%# Eval("Price", "{0:c}")%> </p>
                                <p> IPTU: <%# Eval("IPTU", "{0:c}")%> </p>
                                <p> Condomínio: <%# Eval("Cond", "{0:c}")%> </p>
                            </div>
                            <br />
                        </ItemTemplate>                                 
                    </asp:Repeater>

                </td>
                <td style="width: 30%; vertical-align: top;">
                    <asp:Repeater ID="Repeater3" runat="server" 
                        DataSourceID="objectDataSourceSiteDetail" >
                        <ItemTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" >
                                <tr style="height: 26px">
                                    <td><img src="http://www.pietaimoveis.com.br/Images/Check.png" style="vertical-align:middle; padding: 2px 3px 5px 0px;"/>
                                    </td>
                                    <td><%--<p style="padding: 3px; vertical-align:middle;"> <%# Eval("Name")%> </p>--%>
                                    <%# Eval("Name")%>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>                                 
                    </asp:Repeater>
                </td>
                <td style="width: 20%; vertical-align: top;">
                    <img src="http://www.pietaimoveis.com.br/Images/CallNow.png" style="float:right; margin-top: 8px; margin-bottom: auto; vertical-align:top;display: block;"/>
                </td>
            </tr>
        </table>
        <div class="buttonSearchBack">
            <a href="javascript: history.go(-1)" style="margin-left:26px; margin-top:3px">Retornar à pesquisa</a>
        </div>

        <br />
    </div>
    <br />

    <asp:ObjectDataSource ID="objectDataSourceSiteDetail" runat="server" 
        SelectMethod="GetSiteDetail" TypeName="TK1.Bizz.Pieta.Data.SiteController" >

        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="0" Name="siteAdID" 
                QueryStringField="ID" Type="Int32" />
            <asp:QueryStringParameter DefaultValue="0" Name="siteAdType" 
                QueryStringField="AdType" Type="Int32" />
        </SelectParameters>

    </asp:ObjectDataSource>


    <asp:ObjectDataSource ID="objectDataSourceSiteDescription" runat="server" 
        SelectMethod="GetSiteAd" TypeName="TK1.Bizz.Pieta.Data.SiteController" 
        onselected="objectDataSourceSiteDescription_Selected">

        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="0" Name="siteAdID" 
                QueryStringField="ID" Type="Int32" />
            <asp:QueryStringParameter DefaultValue="0" Name="siteAdType" 
                QueryStringField="AdType" Type="Int32" />
        </SelectParameters>

    </asp:ObjectDataSource>

    <asp:ObjectDataSource ID="objectDataSourceSitePics" runat="server" 
        SelectMethod="GetSitePics" TypeName="TK1.Bizz.Pieta.SitePicHelper">
        <SelectParameters>
            <asp:QueryStringParameter Name="siteAdID" QueryStringField="ID" Type="Int32" />
            <asp:Parameter DefaultValue="AdType" Name="siteAdType" Type="Int32" />
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

