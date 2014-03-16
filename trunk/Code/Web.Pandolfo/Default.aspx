<%@ Page Title="Pandolfo Imóveis" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
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
                <td width="35%">
                    <div id="divRentCities">
                        <asp:DropDownList ID="dropDownRentCities" runat="server" Font-Size="Medium" Width="90%">
                        </asp:DropDownList>
                    </div>
                    <div id="divSellingCities">
                        <asp:DropDownList ID="dropDownSellingCities" runat="server"  Font-Size="Medium" Width="90%">
                        </asp:DropDownList>
                    </div>
                </td>
                <td width=25%" rowspan=3 style="vertical-align:top;">
                    <div class="button" style="margin-right:20px; margin-top:4px;">
                        <asp:LinkButton ID="buttonSearch" runat="server" OnClick="buttonSearch_Click">
                        <img src="Imagens/Search.png" />
                        </asp:LinkButton>
                    </div>
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
                        <asp:DropDownList ID="dropDownRentDistricts" runat="server" Font-Size="Medium" Width="90%">
                        </asp:DropDownList>
                    </div>
                    <div id="divSellingDistricts">
                        <asp:DropDownList ID="dropDownSellingDistricts" runat="server"  Font-Size="Medium" Width="90%">
                        </asp:DropDownList>
                    </div>
                </td>
            </tr>
            <tr>
                <td> </td>
                <td> </td>
                <td>
                    <div id="divRentSiteTypes">
                        <asp:DropDownList ID="dropDownRentSiteType" runat="server"  Font-Size="Medium" Width="90%"></asp:DropDownList>
                    </div>
                    <div id="divSellingSiteTypes">
                        <asp:DropDownList ID="dropDownSellingSiteType" runat="server"  Font-Size="Medium" Width="90%"></asp:DropDownList>
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

        <asp:DataList ID="dataListFeaturedSellingSiteAds" runat="server" 
            DataSourceID="objectDataSourceFeaturedSellingSites" RepeatDirection="Horizontal">
            <ItemTemplate>
                <div class="featureViewerOuter">
                    <div class="featureViewerInner">
                        <div class="featureViewerImage">
                            <a href="../Imovel/Default.aspx?ID=<%# Eval("Code")%>&AdTypeID=<%# Eval("AdTypeID")%>">
                                <img src="<%# Eval("MainPicUrl") %>" width="160px"/>
                            </a>
                        </div>
                        <h2><%# Eval("District")%></h2>
                        <h3><%# Eval("SiteType")%></h3>
                        <p runat="server" visible='<%# Eval("IsRoomNameVisible")%>'><%# Eval("SiteTotalRooms")%> dormitórios</p>
                        <p runat="server" visible='<%# Eval("IsAreaNameVisible")%>'><%# Eval("SiteTotalArea")%>  m²</p>
                        <h3><%# Eval("Value", "{0:c}")%></h3>
                        <div class="featureViewerDetailButton">
                            <a href="../Imovel/Default.aspx?ID=<%# Eval("Code")%>&AdTypeID=<%# Eval("AdTypeID")%>">
                                Ver mais detalhes</a>
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
                                <img src="<%# Eval("MainPicUrl") %>" width="160px"/>
                            </a>
                        </div>
                        <h2><%# Eval("District")%></h2>
                        <h3><%# Eval("SiteType")%></h3>
                        <p runat="server" visible='<%# Eval("IsRoomNameVisible")%>'><%# Eval("SiteTotalRooms")%> dormitórios</p>
                        <p runat="server" visible='<%# Eval("IsAreaNameVisible")%>'><%# Eval("SiteTotalArea")%>  m²</p>
                        <h3><%# Eval("Value", "{0:c}")%></h3>
                        <div class="featureViewerDetailButton">
                            <a href="../Imovel/Default.aspx?ID=<%# Eval("Code")%>&AdTypeID=<%# Eval("AdTypeID")%>">
                                Ver mais detalhes</a>
                        </div>

                    </div >
                </div>
            </ItemTemplate>
        </asp:DataList>
        <br />
        <hr />
        <table width="100%">
            <tr>
                <td width="40%" style="vertical-align:top">
                    <h2>Parceiros</h2>
                    
                    <table border="0" cellpadding="4" cellspacing="0">
                        <tr>
                            <td>
                                <a target="_blank" href="http://www.lojadez.net/">
                                    <img src="http://www.pandolfoimoveis.com.br/Imagens/LogoLojaDez.jpg" alt="Loja Dez" 
                                    height="50px"/></a>
                            </td>
                            <td>
                                <a target="_blank" href="http://www.lojadez.net/">Loja Dez Móveis</a> 
                            </td>

                        </tr>
                        <tr>
                            <td colspan="2">
                                <a target="_blank" href="mailto:debora@safetycorretora.com.br">
                                    <img src="http://www.pandolfoimoveis.com.br/Imagens/LogoSafety.jpg" alt="" 
                                    height="50px"/></a>
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="40%" style="vertical-align:top">
                    <h2>Redes Sociais</h2>
                    <table border="0" cellpadding="4" cellspacing="0">
                        <tr>
                            <td>
                                <a target="_blank" href="https://www.facebook.com/PandolfoImoveis?ref=ts&fref=ts">
                                    <img src="http://www.pandolfoimoveis.com.br/Imagens/FacebookIcon.png" alt="Pandolfo no Facebook" 
                                    height="40px"/></a>
                            </td>
                            <td style="vertical-align:top; ">
                                <a target="_blank" href="https://www.facebook.com/PandolfoImoveis?ref=ts&fref=ts">Visite a página da Pandolfo Imóveis no Facebook</a>
                            </td>

                        </tr>
                    </table>

                </td>
                <td width="20%" style="vertical-align:top; text-align:right;">
                    <img src="http://www.pandolfoimoveis.com.br/Imagens/LogoEficiencia.jpg" alt="Pandolfo" width="150px"/>
                </td>
            </tr>
        </table>
    </div>

    <asp:ObjectDataSource ID="objectDataSourceFeaturedRentSites" runat="server"  OnSelected="objectDataSourceFeaturedRentSites_OnSelected"
        SelectMethod="GetFeaturedRentSiteAds" 
        TypeName="TK1.Bizz.Data.Controller.SiteAdController">
        <SelectParameters>
            <asp:Parameter DefaultValue="Pandolfo" Name="customerName" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>

    <asp:ObjectDataSource ID="objectDataSourceFeaturedSellingSites" runat="server"   OnSelected="objectDataSourceFeaturedRentSites_OnSelected"
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
