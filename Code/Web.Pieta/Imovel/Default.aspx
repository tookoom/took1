<%@ Page Title="Pietá Imóveis - Detalhamento de Imóvel" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Imovel_Default" %>

<%@ Register assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" namespace="System.Web.UI.WebControls" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <script src="http://www.pietaimoveis.com.br/Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="http://www.pietaimoveis.com.br/Scripts/js/jquery.easing.1.3.js" type="text/javascript"></script>
    <script src="http://www.pietaimoveis.com.br/Scripts/js/jquery.timers-1.2.js" type="text/javascript"></script>
    <script src="http://www.pietaimoveis.com.br/Scripts/js/jquery.galleryview-3.0.js" type="text/javascript"></script>
    <%--
    <script src="~/Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="~/Scripts/js/jquery.easing.1.3.js" type="text/javascript"></script>
    <script src="~/Scripts/js/jquery.timers-1.2.js" type="text/javascript"></script>
    <script src="~/Scripts/js/jquery.galleryview-3.0.js" type="text/javascript"></script>--%>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#divSiteDetailPopup').hide();
            //            $('#divSitePicGallery').animate({ heigth: '0px' });
            var dotCounter = 0;
            (function waitPicLoad() {
                setTimeout(function () {
                    if (dotCounter++ < 5) {
                        waitPicLoad();
                    }
                    else {
                        $('#picGallery').galleryView({ pause_on_hover: true,
                            filmstrip_position: 'right',
                            panel_scale: 'nocrop',
                            panel_width: 800,
                            panel_heigth: 500,
                            frame_opacity: 0.7,
                            show_panel_nav: true,
                            show_overlays: true,
                            frame_width: 120,
                            frame_height: 80,
                            show_filmstrip_nav: false
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

         <asp:Repeater ID="Repeater2" runat="server" 
            DataSourceID="objectDataSourceSiteDetail" >
            <ItemTemplate>
                <h2>
                    <%# Eval("AdType.Name")%> de imóvel <%# Eval("Category.Name")%> em <%# Eval("Site.City.Name")%>, no bairro <%# Eval("Site.District.Name")%>
                </h2>
           </ItemTemplate>                                 

        </asp:Repeater>
        <div id="divSitePics">
            <h3>Imagens do imóvel
            </h3>
            <br />
            <div id="divSitePicGallery">
                <asp:Literal id="literalSitePics" runat="server" />
            </div>
            
        </div>
        
        <asp:Repeater ID="Repeater1" runat="server" 
            DataSourceID="objectDataSourceSiteDetail" >
            <ItemTemplate>
                <h3>Detalhes do imóvel
                </h3>

                <table border="0" cellpadding="0" cellspacing="0" width="98%">
                    <tr>
                        <td>
                               <h4> <%# Eval("Title")%> </h4>
                            <p> <%# Eval("Description")%> </p>
                            <a id="linkSiteDetailPopup"> Conheça as características do imóvel</a>
                        </td>
                        <td style="text-align: right; font-size:1.7em; font-weight:bold">
                                <h1> R$ <%# Eval("Price")%> </h1>
                        </td>
                    </tr>
                </table>

                <div id="divSiteDetailPopup" style="position:relative; width: 600px;text-align: left; border:2px solid Gray; min-height: 120px;" >
                    Mais detalhes do imóvel, de acordo com o cadastro
                </div>
<%--                <div style="float: left; margin:0px 0px 0px 0px; position:relative; width: 600px;text-align: left" >
                </div>
                <div style="float: right; margin:0px 0px 0px 700px; position:absolute; width: 200px;text-align: right; font-size:1.5em; font-weight:bold">
                </div>
                <br />--%>

           </ItemTemplate>                                 

        </asp:Repeater>
    </div>


    <asp:ObjectDataSource ID="objectDataSourceSiteDetail" runat="server" 
        SelectMethod="GetSiteAd" TypeName="TK1.Bizz.Pieta.Data.SiteController" 
        onselected="objectDataSourceSiteDetail_Selected">

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
        $('#linkSiteDetailPopup').hover(function () {
            $('#divSiteDetailPopup').fadeIn('slow');
        });
        $('#linkSiteDetailPopup').mouseleave(function () {
            $('#divSiteDetailPopup').fadeOut('slow');
        });

    </script>

</asp:Content>

