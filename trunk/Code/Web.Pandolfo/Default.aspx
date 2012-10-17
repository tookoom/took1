<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="Scripts/Search.js" type="text/javascript"></script>
    <link href="Styles/QuickSearch.css" rel="stylesheet" type="text/css" />
    <link href="Styles/Featured.css" rel="stylesheet" type="text/css" />
    <div class="quickSearch">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td width="20%">
                Estou procurando:
                </td>
                <td class="radioItem" width="15%">
                    <asp:RadioButton ID="radioButtonRent" runat="server" Text="Aluguel"  TextStyle="bold"
                            GroupName="radioButtonAdType"  />
                </td>
                <td width="40%">
                    <div id="divRentCities">
                        <asp:DropDownList ID="dropDownRentCities" runat="server" Font-Size="Medium" Width="100%">
                        </asp:DropDownList>
                    </div>
                    <div id="divSellingCities">
                        <asp:DropDownList ID="dropDownSellingCities" runat="server"  Font-Size="Medium" Width="100%">
                        </asp:DropDownList>
                    </div>
                </td>
                <td width=20%" rowspan=3>
                    <asp:LinkButton ID="buttonSearch" CssClass="lnkButton" runat="server"  Height="100%" Width="100%" OnClick="buttonSearch_Click" >
                        Pesquisar
                        <img src="Imagens/SearchSmall.png" />
                    </asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td> </td>
                <td class="radioItem">
                    <asp:RadioButton ID="radioButtonBuy" runat="server" Text="Venda" TextStyle="bold"
                                GroupName="radioButtonAdType" />
                </td>
                <td>
                    <div id="divRentDistricts">
                        <asp:DropDownList ID="dropDownRentDistricts" runat="server" Font-Size="Medium" Width="100%">
                        </asp:DropDownList>
                    </div>
                    <div id="divSellingDistricts">
                        <asp:DropDownList ID="dropDownSellingDistricts" runat="server"  Font-Size="Medium" Width="100%">
                        </asp:DropDownList>
                    </div>
                </td>
            </tr>
            <tr>
                <td> </td>
                <td> </td>
                <td>
                    <div id="divRentSiteTypes">
                        <asp:DropDownList ID="dropDownRentSiteType" runat="server"  Font-Size="Medium" Width="100%"></asp:DropDownList>
                    </div>
                    <div id="divSellingSiteTypes">
                        <asp:DropDownList ID="dropDownSellingSiteType" runat="server"  Font-Size="Medium" Width="100%"></asp:DropDownList>
                    </div>
                </td>
            </tr>
        </table>
    </div>

</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h1>
        Destaques Vendas
    </h1>
    <div class="featuredSites">

        <asp:DataList ID="dataListFeaturedSiteAds" runat="server" 
            DataSourceID="objectDataSourceFeaturedSellingSites" RepeatDirection="Horizontal">
            <ItemTemplate>
                <div class="featureViewerOuter">
                    <div class="featureViewerInner">
                        <div class="featureViewerImage">
                            <a href="../Imovel/Default.aspx?ID=<%# Eval("Code")%>&AdTypeID=<%# Eval("AdTypeID")%>">
                                <img src="http://www.tk1.net.br/Integra/Mdo/SimVendas/Fotos/4/<%# Eval("Code") %>/<%# Eval("MainPicUrl") %>" width="160px"/>
                            </a>
                        </div>
                        <h2><%# Eval("District")%></h2>
                        <h3><%# Eval("SiteType")%></h3>
                        <p><%# Eval("SiteTotalRooms")%>  <%# Eval("SiteTypeRoomName")%></p>
                        <p><%# Eval("SiteTotalArea")%>  m²</p>
                        <p><b><%# Eval("Value", "{0:c}")%></b></p>
                        <div class="featureViewerDetailButton">
                            <a href="../Imovel/Default.aspx?ID=<%# Eval("Code")%>&AdTypeID=<%# Eval("AdTypeID")%>">
                                Detalhes</a>
                        </div>

                    </div >
                </div>
            </ItemTemplate>
        </asp:DataList>


    </div>
    <br />

    <h1>
        Destaques Aluguel
    </h1>
    <div class="featuredSites">

        <asp:DataList ID="dataList1" runat="server" 
            DataSourceID="objectDataSourceFeaturedRentSites" RepeatDirection="Horizontal">
            <ItemTemplate>
                <div class="featureViewerOuter">
                    <div class="featureViewerInner">
                        <div class="featureViewerImage">
                            <a href="../Imovel/Default.aspx?ID=<%# Eval("Code")%>&AdTypeID=<%# Eval("AdTypeID")%>">
                                <img src="http://www.tk1.net.br/Integra/Mdo/SimVendas/Fotos/4/<%# Eval("Code") %>/<%# Eval("MainPicUrl") %>" width="160px"/>
                            </a>
                        </div>
                        <h2><%# Eval("District")%></h2>
                        <h3><%# Eval("SiteType")%></h3>
                        <p><%# Eval("SiteTotalRooms")%>  <%# Eval("SiteTypeRoomName")%></p>
                        <p><%# Eval("SiteTotalArea")%>  m²</p>
                        <p><b><%# Eval("Value", "{0:c}")%></b></p>
                        <div class="featureViewerDetailButton">
                            <a href="../Imovel/Default.aspx?ID=<%# Eval("Code")%>&AdTypeID=<%# Eval("AdTypeID")%>">
                                Detalhes</a>
                        </div>

                    </div >
                </div>
            </ItemTemplate>
        </asp:DataList>
        <br />

    </div>

    <asp:ObjectDataSource ID="objectDataSourceFeaturedRentSites" runat="server" 
        SelectMethod="GetFeaturedRentSiteAds" 
        TypeName="TK1.Bizz.Data.Controller.SiteAdController">
        <SelectParameters>
            <asp:Parameter DefaultValue="Pandolfo" Name="customerName" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="objectDataSourceFeaturedSellingSites" runat="server" 
        SelectMethod="GetFeaturedSellingSiteAds" 
        TypeName="TK1.Bizz.Data.Controller.SiteAdController">
        <SelectParameters>
            <asp:Parameter DefaultValue="Pandolfo" Name="customerName" Type="String" />
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
