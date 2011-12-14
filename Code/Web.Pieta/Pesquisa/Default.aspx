﻿<%@ Page Title="Pietá Imóveis - Pesquisa" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Pesquisa_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">

<div style="min-height:190px;">
    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="searchBar">
        <tr>
            <td style="width: 650px; vertical-align:top;">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>Quero:</td>
                         <td class="radioItem">
                            <asp:RadioButton ID="radioButtonRent" runat="server" Text="Alugar" GroupName="radioButtonAdType" /></td>
                        <td class="radioItem">
                            <asp:RadioButton ID="radioButtonBuy" runat="server" Text="Comprar" GroupName="radioButtonAdType"/></td>
                        <td >em:</td>
                        <td ">
                            <asp:DropDownList ID="dropDownCities" runat="server" Width="280px" Font-Size="Medium"></asp:DropDownList></td>
                        <td>Bairros:</td>
                    </tr>
                </table>
                <table  border="0" cellpadding="0" cellspacing="0" width="100%" class="searchBarInternal">
                    <tr>
                        <td>Tipo:</td>
                        <td>
                            <asp:DropDownList ID="dropDownSiteType" runat="server" Width="210px" Font-Size="Medium"></asp:DropDownList></td>
                        <td>
                            <asp:DropDownList ID="dropDownRoomNumber" runat="server" Width="260px" Font-Size="Medium"></asp:DropDownList></td>
                        <td style="width: 80px"></td>
                    </tr>
                </table>
                <table border="0" cellpadding="0" cellspacing="0" width="100%" class="searchBarInternal">
                    <tr>
                        <td>Preço entre:</td>
                        <td>
                            <asp:DropDownList ID="dropDownPriceFrom" runat="server" Width="130px" Font-Size="Medium"></asp:DropDownList></td>
                        <td>e</td>
                        <td>
                            <asp:DropDownList ID="dropDownPriceTo" runat="server" Width="210px" Font-Size="Medium"></asp:DropDownList></td>
                        <td style="width: 150px"></td>
                    </tr>
                </table>
                <table  border="0" cellpadding="0" cellspacing="0" width="100%" class="searchBarInternal">
                    <tr>
                        <td>Código do anúncio:</td>
                        <td>
                            <asp:TextBox ID="textBoxSiteCode" runat="server" Width="70px" Font-Size="Medium"></asp:TextBox></td>
                        <td style="width: 400px"></td>                         
                        </tr>
                </table>
            </td>
            <td>
                <div style="OVERFLOW-Y:scroll; float:left; width:100%; height:140px; background-color: White; color: Black;">
                    <asp:CheckBoxList ID="checkBoxListDistricts" runat="server">
                    </asp:CheckBoxList>
                </div>
            </td>
        </tr>
        <tr>
            <td style="width: 450px"></td>
            <td style="width: auto">
                <div class="buttonSearch">
                    <asp:LinkButton ID="buttonSearch" Text="Pesquisar" runat="server" 
                        style="margin: 2px 0px 2px 8px;"
                         OnClick="buttonSearch_Click" />
                </div>
            </td>
        </tr>
    </table>
</div>
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
                            <a href="../Imovel/Default.aspx?ID=<%# Eval("Code")%>&AdTypeID=<%# Eval("AdTypeID")%>">
                                <img src="<%# Eval("MainPicUrl") %>" height="100px" style="margin: 6px 0px 4px 0px;" />
                            </a>
                        </td>
                        <td style="vertical-align: middle; width:80px; text-align:center;"><b>Código <%# Eval("Code")%></b></td>
                        <td>
                            <table>
                                <tr>
                                    <td style="vertical-align: middle; width: 140px; text-align: center;">
                                        <%# Eval("SiteType")%>
                                    </td>
                                </tr>
                                <tr>
                                    <td id="Td2" style="vertical-align: middle; width: 140px; text-align: center;" runat="server"
                                        visible='<%#Eval("IsRoomNameVisible")%>'>
                                        <%# Eval("SiteTotalRooms")%>
                                        <%# Eval("SiteTypeRoomName")%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <%--<td style="vertical-align: middle; width:130px; text-align:center;"><%# Eval("SiteType")%></td>
                        <td style="vertical-align: middle; width:130px; text-align:center;" runat="server" visible='<%#Eval("IsRoomNameVisible")%>'>
                               <%# Eval("SiteTotalRooms")%>  <%# Eval("SiteTypeRoomName")%>
                        </td>--%>
                        <td id="Td1" style="vertical-align: middle; width:130px; text-align:center;" runat="server" visible='<%#Eval("IsAreaNameVisible")%>'>
                               <%# Eval("SiteInternalArea", "{0:0.##}")%>  m²
                        </td>
                        <td style="vertical-align: middle; width:150px; text-align:center;">Bairro <%# Eval("District")%></td>
                        <td style="vertical-align: middle; font-size: 1.5em; width:200px; text-align:center;"><%# Eval("Value", "{0:c}")%></td>
                        <td style="vertical-align: middle; text-align:center; "><a href="../Imovel/Default.aspx?ID=<%# Eval("Code")%>&AdTypeID=<%# Eval("AdTypeID")%>"><b>Detalhes</b></a></td>
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
            SelectMethod="SearchSites" TypeName="TK1.Bizz.Mdo.Data.SiteController" >
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


