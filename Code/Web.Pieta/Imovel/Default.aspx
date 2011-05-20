<%@ Page Title="Pietá Imóveis - Detalhamento de Imóvel" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Imovel_Default" %>

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

    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/js/jquery.easing.1.3.js" type="text/javascript"></script>
    <script src="../Scripts/js/jquery.timers-1.2.js" type="text/javascript"></script>
    <script src="../Scripts/js/jquery.galleryview-3.0.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            var dotCounter = 0;
            (function waitPicLoad() {
                setTimeout(function () {
                    if (dotCounter++ < 5) {
                        waitPicLoad();
                    }
                    else {
                        $('#picGallery').galleryView({ pause_on_hover: true,
                            panel_scale: 'nocrop',
                            panel_width: 754,
                            panel_heigth: 620,
                            panel_animation: 'none',
                            frame_opacity: 0.4,
                            show_panel_nav: true,
                            show_overlays: true,
                            frame_width: 160,
                            frame_height: 50,
                            filmstrip_size: 1,
                            filmstrip_position: 'right',
                            show_filmstrip_nav: false,
                            frame_gap: 6
                        });
                        //$('#divSitePicGallery').show('slow');
                    }
                }, 100);
            })();

        });

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
            <br />
            <div id="divSitePicGallery">
                <asp:Literal id="literalSitePics" runat="server" />
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
                            <h4> <%# Eval("Title")%> </h4>
                            <p> <%# Eval("Description")%> </p>
                            <div runat="server" visible='<%#getRentDivVisibility(Eval("AdType.AdTypeID").ToString())%>'>
                                <p> Aluguel: <%# Eval("Price", "{0:c}")%> </p>
                                <p> IPTU: <%# Eval("IPTU", "{0:c}")%> </p>
                                <p> Condomínio: <%# Eval("Cond", "{0:c}")%> </p>
                            </div>
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

</asp:Content>

