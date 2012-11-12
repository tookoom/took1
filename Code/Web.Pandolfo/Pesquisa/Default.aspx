<%@ Page Title="Pandolfo Imóveis - Pesquisa" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Pesquisa_Default" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.url.js" type="text/javascript"></script>
    <script src="../Scripts/Search.js" type="text/javascript"></script>
    <link href="../Styles/QuickSearch.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/Featured.css" rel="stylesheet" type="text/css" />

    <div class="quickSearch">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td width="6%" class="radioItem">
                    <asp:RadioButton ID="radioButtonRent" runat="server" Text="Aluguel"  TextStyle="bold"
                            GroupName="radioButtonAdType"  />
                </td>
                <td width="27%" colspan="2">
                    <div id="divRentCities">
                        <asp:DropDownList ID="dropDownRentCities" runat="server" Font-Size="Medium" Width="96%">
                        </asp:DropDownList>
                    </div>
                    <div id="divSellingCities">
                        <asp:DropDownList ID="dropDownSellingCities" runat="server"  Font-Size="Medium" Width="96%">
                        </asp:DropDownList>
                    </div>
                </td>
                <td width="15%" rowspan="6">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="vertical-align:top;">
                                <div style="overflow-y: scroll; float: left; width: 99%; height:116px; background-color: White;
                                    color: Black; border:1px solid #d3d3d3; font-size:medium; font-weight:normal; margin-bottom:6px;margin-top:4px;">
                                    <div id="divRentDistricts">
                                        <asp:CheckBoxList ID="checkBoxListRentDistricts" runat="server">
                                        </asp:CheckBoxList>
                                    </div>
                                    <div id="divSellingDistricts">
                                        <asp:CheckBoxList ID="checkBoxListSellingDistricts" runat="server">
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align:bottom;">
                                <div class="button">
                                    <asp:LinkButton ID="buttonSearch" runat="server" OnClick="buttonSearch_Click">
                                    <img src="http://pandolfo.tk1.net.br/Imagens/Search.png" />
                                    </asp:LinkButton>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="radioItem">
                    <asp:RadioButton ID="radioButtonBuy" runat="server" Text="Venda" TextStyle="bold"
                                GroupName="radioButtonAdType" />
                </td>
                <td colspan="2">
                    <div id="divRentSiteTypes">
                        <asp:DropDownList ID="dropDownRentSiteType" runat="server"  Font-Size="Medium" Width="96%"></asp:DropDownList>
                    </div>
                    <div id="divSellingSiteTypes">
                        <asp:DropDownList ID="dropDownSellingSiteType" runat="server"  Font-Size="Medium" Width="96%"></asp:DropDownList>
                    </div>
                </td>
            </tr>
            <tr>
                <td></td>
                <td colspan="2"><asp:DropDownList ID="dropDownRoomNumber" runat="server" Width="96%" Font-Size="Medium"></asp:DropDownList></td>
            </tr>
            <tr>
                <td></td>
                <td>Preço Mínimo:</td>
                <td>
                    <div id="divRentPriceFrom">
                        <asp:DropDownList ID="dropDownRentPriceFrom" runat="server" Width="94%" Font-Size="Medium"></asp:DropDownList>
                    </div>
                    <div id="divSellingPriceFrom">
                        <asp:DropDownList ID="dropDownSellingPriceFrom" runat="server" Width="94%" Font-Size="Medium">
                        </asp:DropDownList>
                    </div>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>Preço Máximo:</td>                            
                <td width="18%">
                    <div id="divRentPriceTo">
                        <asp:DropDownList ID="dropDownRentPriceTo" runat="server" Width="94%" Font-Size="Medium"></asp:DropDownList>
                    </div>
                    <div id="divSellingPriceTo">
                        <asp:DropDownList ID="dropDownSellingPriceTo" runat="server" Width="94%" Font-Size="Medium">
                        </asp:DropDownList>
                    </div>
                </td>
            </tr>
            <tr>
                <%--<td></td>--%>
                <td></td>
                <td>Busca pelo código:</td>
                <td><asp:TextBox ID="textBoxSiteCode" runat="server" Width="93%" Font-Size="Large"></asp:TextBox></td>
            </tr>
            </table>
    </div>


    <script type="text/javascript">
        $("input:radio").click(function () {
            setSearchFieldsVisibility();
        });
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            setSearchType();
            setSearchFieldsVisibility();
        });
    </script>

</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <link type="text/css" href="/Scripts/css/redmond/jquery-ui-1.8.9.custom.css" rel="Stylesheet" />	
    <script type="text/javascript" src="/Scripts/js/jquery-1.4.4.min.js"></script>
    <script type="text/javascript" src="/Scripts/js/jquery-ui-1.8.9.custom.min.js"></script>
    <script src="/Scripts/DefaultPage.js" type="text/javascript"></script>


    

    <div class="searchResult" runat="server">
        
        <div runat="server" visible="<%#getSearchResultVisibility()%>" > 
            <h2>Resultados</h2>

            <table border="0" cellpadding="0" cellspacing="0" width="100%" runat="server">
                <tr>
                    <td style="padding: 2px 4px 2px 4px; width:150px;">Ordem de exibição:</td>
                    <td style="padding: 2px 4px 2px 4px; width:150px;">
                        <asp:DropDownList ID="dropDownListResultOrdering" runat="server" AutoPostBack="true" Font-Size="Medium"
                            OnSelectedIndexChanged="dropDownListResultOrdering_SelectedIndexChanged">
                            <asp:ListItem Text="Escolha" Value="NONE"/>
                            <asp:ListItem Text="Menores valores primeiro" Value="PRICE_ASC"/>
                            <asp:ListItem Text="Maiores valores primeiro" Value="PRICE_DESC" />
                            <asp:ListItem Text="Menores áreas primeiro"  Value="AREA_ASC"/>
                            <asp:ListItem Text="Maiores áreas primeiro" Value="AREA_DESC" />
                        </asp:DropDownList>
                    </td>
                    <td>
                        <div style="float: right; padding:10px; background-color: #efefef;" >
                            <asp:DataPager ID="siteSearchDataPagerTop" 
                                runat="server" PagedControlID="listViewSearchResults" PageSize="20">
                                <Fields>
                                    <asp:NumericPagerField ButtonCount="10" 
                                        CurrentPageLabelCssClass="searchResultPagerActiveButton" 
                                        NextPageText="&gt;&gt;" NumericButtonCssClass="searchResultPagerButton" 
                                        PreviousPageText="&lt;&lt;" />
                                    <asp:NextPreviousPagerField FirstPageText="Primeira" 
                                        LastPageText="Última" NextPageText="Próxima" PreviousPageText="Anterior" 
                                        ButtonCssClass="searchResultPagerNestPrevButton" />
                                </Fields>
                            </asp:DataPager>
                        </div>
                    </td>
                    <%--<td style="padding: 2px 4px 2px 4px"> <asp:LinkButton ID="linkButtonOrderPriceAsc" runat="server" OnClick="linkButtonOrderPriceAsc_Click">Menores valores primeiro</asp:LinkButton></td>
                    <td>|</td>
                    <td style="padding: 2px 4px 2px 4px"> <asp:LinkButton ID="linkButtonOrderPriceDec" runat="server" OnClick="linkButtonOrderPriceDesc_Click" >Maiores valores primeiro</asp:LinkButton></td>
                    <td>|</td>
                    <td style="padding: 2px 4px 2px 4px"> <asp:LinkButton ID="linkButtonOrderAreaAsc" runat="server" OnClick="linkButtonOrderAreaAsc_Click" >Menores áreas primeiro</asp:LinkButton></td>
                    <td>|</td>
                    <td style="padding: 2px 4px 2px 4px"> <asp:LinkButton ID="linkButtonOrderAreaDec" runat="server" OnClick="linkButtonOrdeAreaDesc_Click" >Maiores áreas primeiro</asp:LinkButton></td>--%>
                </tr>
            </table>
        </div>
        

        <asp:ListView ID="listViewSearchResults" runat="server" 
            onpagepropertieschanged="listViewSearchResults_PagePropertiesChanged">
            <EmptyDataTemplate>
                <h3>Nenhum resultado encontrado.</h3>
            </EmptyDataTemplate>
            <ItemTemplate>
                <table class="searchResultTable" border="0" cellpadding="0px 0px 4px 0px;" cellspacing="0" width="100%">
                    <tr class="center">
                        <td style="vertical-align: middle;">
                            <a href="../Imovel/Default.aspx?ID=<%# Eval("Code")%>&AdTypeID=<%# Eval("AdTypeID")%>">
                                <img src="<%# Eval("MainPicUrl") %>" height="110px" width="120px" style="margin: 4px 0px 4px 0px;" />
                            </a>
                        <td style="vertical-align: middle; width:80px; text-align:center;"><b>Código <%# Eval("Code")%></b></td>
                        <td>
                            <table>
                                <tr>
                                    <td style="vertical-align: middle; width:140px; text-align:center;"><%# Eval("SiteType")%></td>
                                </tr>
                                <tr>
                                    <td id="Td2" style="vertical-align: middle; width:140px; text-align:center;" runat="server" visible='<%#Eval("IsRoomNameVisible")%>'>
                                           <%# Eval("SiteTotalRooms")%>  <%# Eval("SiteTypeRoomName")%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td id="Td1" style="vertical-align: middle; width:80px; text-align:center;" runat="server">
                               <%# Eval("SiteTotalArea", "{0:0.##}")%>  m²
                        </td>
                        <td style="vertical-align: middle; width:100px; text-align:center;">Bairro <%# Eval("District")%></td>
                        <td style="vertical-align: middle; font-size: 1.5em; width:200px; text-align:center;"><%# Eval("Value", "{0:c}")%></td>
                        <td style="vertical-align: middle; text-align:center; width:80px;"><a href="../Imovel/Default.aspx?ID=<%# Eval("Code")%>&AdTypeID=<%# Eval("AdTypeID")%>"><b>Detalhes</b></a></td>
                    </tr>
                    <hr />
                </table>
            </ItemTemplate>
            <LayoutTemplate>
                <div ID="itemPlaceholderContainer" runat="server" >
                    <span runat="server" id="itemPlaceholder" />
                </div>
                <div style="text-align: center;color: #000000;">
                </div>
            </LayoutTemplate>
        </asp:ListView>
        <hr />
<%--        <div style="height: 50px;">
            <asp:DataPager ID="siteSearchDataPagerBottom" style="float: right; padding:10px; font-size:medium; font-weight: bold;"
                runat="server" PagedControlID="listViewSearchResults" PageSize="20">
                <Fields>
                    <asp:NextPreviousPagerField ButtonType="Button" FirstPageText="Primeira" 
                        LastPageText="Última" NextPageText="Próxima" PreviousPageText="Anterior" />
                    <asp:NumericPagerField />
                </Fields>
            </asp:DataPager>
        </div>--%>
        <div style="float: right; padding:10px; background-color: #efefef;" >
            <asp:DataPager ID="siteSearchDataPagerBottom" 
                runat="server" PagedControlID="listViewSearchResults" PageSize="20">
                <Fields>
                    <asp:NumericPagerField ButtonCount="10" 
                        CurrentPageLabelCssClass="searchResultPagerActiveButton" 
                        NextPageText="&gt;&gt;" NumericButtonCssClass="searchResultPagerButton" 
                        PreviousPageText="&lt;&lt;" />
                    <asp:NextPreviousPagerField FirstPageText="Primeira" 
                        LastPageText="Última" NextPageText="Próxima" PreviousPageText="Anterior" 
                        ButtonCssClass="searchResultPagerNestPrevButton" />
                </Fields>
            </asp:DataPager>
        </div>
    </div>
    <br />
<%--    <div>
        <asp:Literal ID="literalDebugResult" Text="Debug" runat="server"/>
    </div>
--%>
</asp:Content>


