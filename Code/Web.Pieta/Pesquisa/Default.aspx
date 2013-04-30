<%@ Page Title="Pietá Imóveis - Pesquisa" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Pesquisa_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">

    <script src="../Scripts/js/jquery-1.3.2.js" type="text/javascript"></script>
    <script src="../Scripts/Search.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.url.js" type="text/javascript"></script>

<div style="min-height:190px;">
    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="searchBar">
        <tr>
            <td style="width: 650px; vertical-align:top;">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>Quero:</td>
                         <td class="radioItem">
                            <asp:RadioButton ID="radioButtonRent" runat="server" Text="Alugar" 
                                 GroupName="radioButtonAdType"  /></td>
                        <td class="radioItem">
                            <asp:RadioButton ID="radioButtonBuy" runat="server" Text="Comprar" 
                                GroupName="radioButtonAdType" /></td>
                        <td >em:</td>
                        <td>
                            <div id="divRentCities">
                                <asp:DropDownList ID="dropDownRentCities" runat="server" Width="280px" Font-Size="Medium">
                                </asp:DropDownList>
                            </div>
                            <div id="divSellingCities">
                                <asp:DropDownList ID="dropDownSellingCities" runat="server" Width="280px" Font-Size="Medium">
                                </asp:DropDownList>
                            </div>
                        </td>                        
                        <td>Bairros:</td>
                    </tr>
                </table>
                <div id="divSellingParameters">
                    <table  border="0" cellpadding="0" cellspacing="0" width="100%" class="searchBarInternal">
                        <tr>
                            <td>Tipo:</td>
                            <td>
                                <asp:DropDownList ID="dropDownSellingPropertyType" runat="server" Width="210px" Font-Size="Medium"></asp:DropDownList></td>
                            <td>
                                <asp:DropDownList ID="dropDownSellingRoomNumber" runat="server" Width="260px" Font-Size="Medium"></asp:DropDownList></td>
                            <td style="width: 80px"></td>
                        </tr>
                    </table>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="searchBarInternal">
                        <tr>
                            <td>Preço entre:</td>
                            <td>
                                <asp:DropDownList ID="dropDownSellingPriceFrom" runat="server" Width="130px" Font-Size="Medium"></asp:DropDownList></td>
                            <td>e</td>
                            <td>
                                <asp:DropDownList ID="dropDownSellingPriceTo" runat="server" Width="210px" Font-Size="Medium"></asp:DropDownList></td>
                            <td style="width: 150px"></td>
                        </tr>
                    </table>
                    <table  border="0" cellpadding="0" cellspacing="0" width="100%" class="searchBarInternal">
                        <tr>
                            <td>Código do anúncio:</td>
                            <td>
                                <asp:TextBox ID="textBoxSellingSiteCode" runat="server" Width="70px" Font-Size="Medium"></asp:TextBox></td>
                            <td style="width: 400px"></td>                         
                            </tr>
                    </table>
                </div>
                <div id="divRentParameters">
                    <table  border="0" cellpadding="0" cellspacing="0" width="100%" class="searchBarInternal">
                        <tr>
                            <td>Tipo:</td>
                            <td>
                                <asp:DropDownList ID="dropDownRentPropertyType" runat="server" Width="210px" Font-Size="Medium"></asp:DropDownList></td>
                            <td>
                                <asp:DropDownList ID="dropDownRentRoomNumber" runat="server" Width="260px" Font-Size="Medium"></asp:DropDownList></td>
                            <td style="width: 80px"></td>
                        </tr>
                    </table>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="searchBarInternal">
                        <tr>
                            <td>Preço entre:</td>
                            <td>
                                <asp:DropDownList ID="dropDownRentPriceFrom" runat="server" Width="130px" Font-Size="Medium"></asp:DropDownList></td>
                            <td>e</td>
                            <td>
                                <asp:DropDownList ID="dropDownRentPriceTo" runat="server" Width="210px" Font-Size="Medium"></asp:DropDownList></td>
                            <td style="width: 150px"></td>
                        </tr>
                    </table>
                    <table  border="0" cellpadding="0" cellspacing="0" width="100%" class="searchBarInternal">
                        <tr>
                            <td>Código do anúncio:</td>
                            <td>
                                <asp:TextBox ID="textBoxRentSiteCode" runat="server" Width="70px" Font-Size="Medium"></asp:TextBox></td>
                            <td style="width: 400px"></td>                         
                            </tr>
                    </table>
                </div>
            </td>
            <td>
                <div style="OVERFLOW-Y:scroll; float:left; width:100%; height:140px; background-color: White; color: Black;">
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
            <td style="width: 450px"></td>
            <td style="width: auto">
                <div id="divSearchButton" class="buttonSearch">
                    <asp:LinkButton ID="buttonSearch" Text="Pesquisar" runat="server" 
                        style="margin: 2px 0px 2px 8px;"
                         OnClick="buttonSearch_Click" />
                </div>
            </td>
        </tr>
    </table>
</div>

    <script type="text/javascript">
        $("input:radio").click(function () {
            setSearchFieldsVisibility();
        });
    </script>

    <script type="text/javascript">
        $("#divSearchButton").click(function () {
            //alert('1');
           // window.location.search = 'quickSearch=no';
        });
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            //alert('1');
            //setSearchType();
            setSearchFieldsVisibility();
        });
    </script>



</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <link type="text/css" href="/Scripts/css/redmond/jquery-ui-1.8.9.custom.css" rel="Stylesheet" />	
    <script type="text/javascript" src="/Scripts/js/jquery-1.4.4.min.js"></script>
    <script type="text/javascript" src="/Scripts/js/jquery-ui-1.8.9.custom.min.js"></script>
    <script src="/Scripts/DefaultPage.js" type="text/javascript"></script>



    <div class="clear searchResult">
        <div runat="server" visible="<%#getSearchResultVisibility()%>" > <%--visible='<%#getSearchResultVisibility("bla")%>'--%>
            <div id="Div1" class="headerBlueLine" >
                <h1>Resultado da Pesquisa</h1>
            </div>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="width:180px;">Ordem de exibição:</td>
                    <td style="padding: 2px 4px 2px 4px; width:80px;">
                        <asp:DropDownList ID="dropDownListResultOrderingType" runat="server" AutoPostBack="true" Font-Size="Medium"
                            OnSelectedIndexChanged="dropDownListResultOrderingType_SelectedIndexChanged">
                            <asp:ListItem Text="Área"  Value="AREA"/>
                            <asp:ListItem Text="Código" Value="CODE" Selected="True"/>
                            <asp:ListItem Text="Preço" Value="PRICE" />
                        </asp:DropDownList>
                    </td>
                    <td style="padding: 2px 4px 2px 4px; width:70px;">
                        <asp:DropDownList ID="dropDownListResultOrdering" runat="server" AutoPostBack="true" Font-Size="Medium"
                            OnSelectedIndexChanged="dropDownListResultOrdering_SelectedIndexChanged">
                            <asp:ListItem Text="Menor para maior"  Value="ASC"/>
                            <asp:ListItem Text="Maior para menor" Value="DESC"/>
                        </asp:DropDownList>
                    </td>
                    <td style="width:500px;"></td>
<%--                    <td style="padding: 2px 4px 2px 4px"> <asp:LinkButton ID="linkButtonOrderPriceAsc" runat="server" OnClick="linkButtonOrderPriceAsc_Click">Menores valores primeiro</asp:LinkButton></td>
                    <td>|</td>
                    <td style="padding: 2px 4px 2px 4px"> <asp:LinkButton ID="linkButtonOrderPriceDec" runat="server" OnClick="linkButtonOrderPriceDesc_Click" >Maiores valores primeiro</asp:LinkButton></td>
                    <td>|</td>
                    <td style="padding: 2px 4px 2px 4px"> <asp:LinkButton ID="linkButtonOrderAreaAsc" runat="server" OnClick="linkButtonOrderAreaAsc_Click" >Menores áreas primeiro</asp:LinkButton></td>
                    <td>|</td>
                    <td style="padding: 2px 4px 2px 4px"> <asp:LinkButton ID="linkButtonOrderAreaDec" runat="server" OnClick="linkButtonOrdeAreaDesc_Click" >Maiores áreas primeiro</asp:LinkButton></td>--%>
                    <td>
                        <div style="float: right; padding:10px; margin-right:10px; background-color: #efefef;" >
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
                </tr>
            </table>
        </div>
        <%--<div id="Div2" class="headerBlueLine" >
        </div>--%>
        <hr />

        <asp:ListView ID="listViewSearchResults" runat="server" 
            EnableModelValidation="True" 
            onpagepropertieschanged="listViewSearchResults_PagePropertiesChanged">
            <EmptyDataTemplate>
                <h3>Nenhum resultado encontrado.</h3>
            </EmptyDataTemplate>
            <ItemTemplate>
                <table border="0" cellpadding="0" cellspacing="0" width="932px">
                    <tr class="center">
                        <td style="vertical-align: middle; width:170px;">
                            <a href="../Imovel/Default.aspx?ID=<%# Eval("AdCode")%>&AdType=<%# Eval("AdType")%>">
                                <img src="<%# Eval("MainPicUrl") %>" height="100px" style="margin: 6px 0px 4px 0px;" />
                            </a>
                        </td>
                        <td style="vertical-align: middle; width:80px; text-align:center;"><b>Código <%# Eval("AdCode")%></b></td>
                        <td>
                            <table>
                                <tr>
                                    <td style="vertical-align: middle; width: 140px; text-align: center;">
                                        <%# Eval("PropertyType")%>
                                    </td>
                                </tr>
                                <tr>
                                    <td id="Td2" style="vertical-align: middle; width: 140px; text-align: center;" runat="server"
                                        visible='<%#Eval("IsRoomNameVisible")%>'>
                                        <%# Eval("TotalRooms")%>
                                        <%# Eval("PropertyTypeRoomName")%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <%--<td style="vertical-align: middle; width:130px; text-align:center;"><%# Eval("PropertyType")%></td>
                        <td style="vertical-align: middle; width:130px; text-align:center;" runat="server" visible='<%#Eval("IsRoomNameVisible")%>'>
                               <%# Eval("TotalRooms")%>  <%# Eval("PropertyTypeRoomName")%>
                        </td>--%>
                        <td id="Td1" style="vertical-align: middle; width:130px; text-align:center;" runat="server" visible='<%#Eval("IsAreaNameVisible")%>'>
                               <%# Eval("InternalArea", "{0:0.##}")%>  m²
                        </td>
                        <td style="vertical-align: middle; width:150px; text-align:center;">Bairro <%# Eval("District")%></td>
                        <td style="vertical-align: middle; font-size: 1.5em; width:200px; text-align:center;"><%# Eval("Value", "{0:c}")%></td>
                        <td style="vertical-align: middle; text-align:center; "><a href="../Imovel/Default.aspx?ID=<%# Eval("AdCode")%>&AdType=<%# Eval("AdType")%>"><b>Detalhes</b></a></td>
                    </tr>
                    
                </table>
                <hr />
            </ItemTemplate>
            <LayoutTemplate>
                <div ID="itemPlaceholderContainer" runat="server" >
                    <span runat="server" id="itemPlaceholder" />
                </div>
                <div style="text-align: center;color: #000000;">
                </div>
            </LayoutTemplate>
        </asp:ListView>
        


        <asp:ObjectDataSource ID="objectDataSourceSiteSearch" runat="server" 
            SelectMethod="SearchSites" TypeName="TK1.Bizz.Mdo.Client.Data.SiteController" >
            <SelectParameters>
                <asp:Parameter Name="parameters" Type="Object" />
            </SelectParameters>
        </asp:ObjectDataSource>

        <div style="float: right; padding:10px; margin-right:10px; background-color: #efefef;" >
            <asp:DataPager ID="DataPager1" 
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
        <br />
        <br />
    </div>
</asp:Content>


