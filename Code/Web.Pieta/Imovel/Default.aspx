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
                            panel_heigth: 600,
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
        <div class="headerBlueLine">
            <h2>Detalhes</h2>
        </div>

        <div class="searchDetailHeader">
             <asp:Repeater ID="Repeater2" runat="server" 
                DataSourceID="objectDataSourceSiteDescription" >
                <ItemTemplate>
                    <div style="float:left;">
                        <h2>
                            <%# Eval("AdType.Name")%> de imóvel <%# Eval("Category.Name")%> em <%# Eval("Site.City.Name")%>, no bairro <%# Eval("Site.District.Name")%>
                        </h2>
                        Código <%# Eval("SiteAdID")%>
                    </div>
                    <div style="float:right;">
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
                        </ItemTemplate>                                 
                    </asp:Repeater>

                </td>
                <td style="width: 30%; vertical-align: top;">
                    <asp:Repeater ID="Repeater3" runat="server" 
                        DataSourceID="objectDataSourceSiteDetail" >
                        <ItemTemplate>
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td><img src="http://www.pietaimoveis.com.br/Images/Check.png" style="vertical-align:middle;"/>
                                    </td>
                                    <td><p style="padding: 3px; vertical-align:middle;"> <%# Eval("Name")%> </p>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>                                 
                    </asp:Repeater>
                </td>
                <td style="width: 20%; ">
                    <img src="http://www.pietaimoveis.com.br/Images/CallNow.png" style="float:right; margin-top: 8px;"/>
                </td>
            </tr>
        </table>
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

