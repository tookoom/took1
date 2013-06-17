<%@ Page Title="Pietá Imóveis - Página Inicial" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <script src="Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="Scripts/EasySlider/easySlider1.7.js" type="text/javascript"></script>
    <script src="Scripts/Search.js" type="text/javascript"></script>
    <link href="Styles/Release.css" rel="stylesheet" type="text/css" />
    <link href="Styles/Featured.css" rel="stylesheet" type="text/css" />
    <link href="Styles/QuickSearch.css" rel="stylesheet" type="text/css" />
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

    <div class="headerBlueLine"><h1>Destaques Vendas</h1></div>
    <div class="featuredSites">

        <asp:DataList ID="dataListFeaturedSiteAds" runat="server" 
            DataSourceID="objectDataSourceFeaturedPropertyAds" RepeatDirection="Horizontal">
            <ItemTemplate>
                <div class="featureViewerOuter">
                    <div class="featureViewerInner">
                        <div class="featureViewerImage">
                            <a href="/Imovel/Default.aspx?ID=<%# Eval("AdCode")%>&AdType=<%# Eval("AdType")%>">
                                <img src="<%# Eval("MainPicUrl") %>" width="160px"/>
                            </a>
                        </div>
                        <h2><%# Eval("District")%></h2>
                        <h3><%# Eval("PropertyType")%></h3>
                        <p><%# Eval("TotalRooms")%>  <%# Eval("PropertyTypeRoomName")%></p>
                        <p><%# Eval("TotalArea")%>  m²</p>
                        <p><b><%# Eval("Value", "{0:c}")%></b></p>
                        <div class="featureViewerDetailButton">
                            <a href="/Imovel/Default.aspx?ID=<%# Eval("AdCode")%>&AdType=<%# Eval("AdType")%>">
                                Detalhes</a>
                        </div>

                    </div >
                </div>
            </ItemTemplate>
        </asp:DataList>


    </div>

    <div class="headerBlueLine"><h1>Lançamentos</h1></div>
   <asp:Literal id="literalSiteReleaseAds" runat="server" />
    <br />
    <br />
       
    <div class="headerBlueLine"><h1>Redes Sociais</h1></div>
    <table border="0" cellpadding="4" cellspacing="0">
        <tr>
            <td>
                <a target="_blank" href="http://www.facebook.com/profile.php?id=100002698020190">
                    <img src="Images/FacebookIcon.png" alt="Pietá no Facebook" 
                    height="70px"/></a>
            </td>
            <td style="vertical-align:top; ">
                <p>
                    Novidades, informações e contato.
                </p>
                <a target="_blank" href="http://www.facebook.com/profile.php?id=100002698020190">Conheça a página da
                    Pietá Imóveis no Facebook!</a>
            </td>

        </tr>
    </table>
    <br />


    <div class="clear releases">
    <br />
    <br />
    <br />
        <br />

        <br />
        <br />
    </div>


    <asp:ObjectDataSource ID="objectDataSourceFeaturedPropertyAds" runat="server" 
        SelectMethod="GetFeaturedSellingPropertyAds" 
        TypeName="TK1.Data.Bizz.Client.Binding.PropertyAdBindingSource">
        <SelectParameters>
            <asp:Parameter DefaultValue="pieta" Name="customerCode" Type="String" />
            <asp:Parameter DefaultValue="5" Name="count" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>

    <asp:ObjectDataSource ID="objectDataSourcePropertyReleaseAds" runat="server" 
        SelectMethod="GetPropertyReleaseAds" 
        TypeName="TK1.Data.Bizz.Client.Binding.PropertyAdBindingSource">
        <SelectParameters>
            <asp:Parameter DefaultValue="pieta" Name="customerCode" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>

    <script type="text/javascript">
        $("input:radio").click(function () {
            setQuickSearchFieldsVisibility();
        });
    </script>


    <script type="text/javascript">
        $(document).ready(function () {
            setQuickSearchFieldsVisibility();
        });
    </script>



</asp:Content>
