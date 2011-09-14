<%@ Page Title="Pietá Imóveis - Página Inicial" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <link href="Styles/popeye/jquery.popeye.css" rel="stylesheet" type="text/css" />
    <link href="Styles/popeye/jquery.popeye.style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.6.2/jquery.min.js"></script>
    <script src="Scripts/popeye/jquery.popeye-2.1.js" type="text/javascript"></script>


    <div class="headerBlueLine"><h2>Lançamentos</h2></div>

    <div class="ppy" id="ppy3">
        <ul class="ppy-imglist">
            <li>
                <a href="http://www.tk1.net.br/Integra/Mdo/SimVendas/Fotos/3/101/foto_00101_01_____________________.jpg">
                    <img class="ppy-forcedthumb" src="http://www.tk1.net.br/Integra/Mdo/SimVendas/Fotos/3/101/foto_00101_01_____________________.jpg" alt="" />
                </a>
                <span class="ppy-extcaption">
                    <h1>Teste 1</h1>
                    <p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Cras euismod risus non diam.</p>
                </span>
            </li>
            <li>
                <a href="http://www.tk1.net.br/Integra/Mdo/SimVendas/Fotos/3/101/foto_00101_02_____________________.jpg">
                    <img class="ppy-forcedthumb" src="http://www.tk1.net.br/Integra/Mdo/SimVendas/Fotos/3/101/foto_00101_02_____________________.jpg" alt="" />
                </a>
                <span class="ppy-extcaption">
                    <h1>Teste 2</h1>
                    <p>In tempus consequat purus. Curabitur in massa nec purus semper adipiscing. Donec convallis odio nec nibh. Nam viverra, tortor nec cursus accumsan, purus purus ultricies mauris, in dignissim orci dui at est. Morbi non quam at ante cursus posuere. Sed ornare sem vitae diam tempor blandit. Nam risus diam, faucibus eget, fermentum et, dignissim eu, tellus. Praesent non justo. Proin vestibulum diam at mi. Cras molestie eleifend mi. Morbi massa enim, auctor at, rhoncus nec, consequat nec, diam. In laoreet lacus vel velit. Sed vulputate commodo ligula.</p>
                </span>
            </li>
            <li>
                <a href="http://www.tk1.net.br/Integra/Mdo/SimVendas/Fotos/3/4/foto_00004_05_____________________.jpg">
                    <img class="ppy-forcedthumb" src="http://www.tk1.net.br/Integra/Mdo/SimVendas/Fotos/3/4/foto_00004_05_____________________.jpg" alt="" />
                </a>
                <span class="ppy-extcaption">
                    <h1>Teste 3</h1>
                    <p>In tempus consequat purus. Curabitur in massa nec purus semper adipiscing. Donec convallis odio nec nibh. Nam viverra, tortor nec cursus accumsan, purus purus ultricies mauris, in dignissim orci dui at est. Morbi non quam at ante cursus posuere. Sed ornare sem vitae diam tempor blandit. Nam risus diam, faucibus eget, fermentum et, dignissim eu, tellus. Praesent non justo. Proin vestibulum diam at mi. Cras molestie eleifend mi. Morbi massa enim, auctor at, rhoncus nec, consequat nec, diam. In laoreet lacus vel velit. Sed vulputate commodo ligula.</p>
                </span>
            </li>
        </ul>
            
        <div class="ppy-outer">
            <div class="ppy-stage">
                <div class="ppy-nav">
                    <div class="nav-wrap">
                        <a class="ppy-prev" title="Previous image">Anterior</a>
                        <a class="ppy-play" title="Play Slideshow">Play</a>
                        <a class="ppy-pause" title="Stop Slideshow">Stop</a>
                        <a class="ppy-switch-enlarge" title="Enlarge">Aumentar</a>
                        <a class="ppy-switch-compact" title="Close">Reduzir</a>
                        <a class="ppy-next" title="Next image">Próxima</a>
                    </div>
                </div>
                <div class="ppy-counter">
                    <strong class="ppy-current"></strong> / <strong class="ppy-total"></strong> 
                </div>
            </div>
            <div class="ppy-caption">
                <span class="ppy-text"></span>
            </div>
        </div>
    </div>

                <%--<asp:Literal id="literalSitePics" runat="server" />--%>

<%--    <div>
        <asp:DataList ID="dataListSiteReleaseAds" runat="server" DataSourceID="objectDataSourceSiteReleases"
            RepeatDirection="Horizontal">
            <ItemTemplate>
                <div style="padding: 8px;">
                    <div style="padding: 8px; background: #efefef;">
                        <a href="../Imovel/Default.aspx?ID=<%# Eval("Code")%>&AdTypeID=<%# Eval("AdTypeID")%>">
                            <img src="http://www.tk1.net.br/Integra/Mdo/SimVendas/Fotos/4/<%# Eval("Code") %>/<%# Eval("MainPicUrl") %>"
                                height="100px" style="margin: 6px 0px 4px 0px;" />
                        </a>
                        <h2>
                            <%# Eval("District")%></h2>
                        <h3>
                            <%# Eval("SiteType")%></h3>
                        <p>
                            <%# Eval("SiteTotalRooms")%>
                            <%# Eval("SiteTypeRoomName")%></p>
                        <a href="../Imovel/Default.aspx?ID=<%# Eval("Code")%>&AdTypeID=<%# Eval("AdTypeID")%>">
                            Detalhes</a>
                    </div>
                </div>
            </ItemTemplate>
        </asp:DataList>
    </div>
--%>    
<br />
<div class="headerBlueLine"><h2>Destaques Vendas</h2></div>
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
                    <a href="../Imovel/Default.aspx?ID=<%# Eval("Code")%>&AdTypeID=<%# Eval("AdTypeID")%>">Detalhes</a>
                    </div >
                </div>
            </ItemTemplate>
        </asp:DataList>


    </div>

    <div class="clear releases">
    <br />
    <br />
    <br />
        <br />

        <br />
            <img src="Images/CaixaAqui.jpg" class="center" />
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

    <script type="text/javascript">
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
    </script>


</asp:Content>
