<%@ Page Title="Mdo Imóveis - Pesquisa" Language="C#" MasterPageFile="~/Nav/Mdo/SimVendas/SimVendas.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Nav_Mdo_SimVendas_Pesquisa_Default" %>


<asp:Content ID="Content1" ContentPlaceHolderID="SimVendasHeadContent" Runat="Server">

    <div Runat="Server">
        <table border="0" cellpadding="0" cellspacing="0" class="searchBarTop">
            <tr>
                <td style="padding-right:16px">
                    <asp:RadioButton ID="radioButtonComercial" runat="server" Text="Comercial" GroupName="radioButtonSearchType" />
                </td>
                <td style="padding-right:16px">
                    <asp:RadioButton ID="radioButtonResidencial" runat="server" Text="Residencial" GroupName="radioButtonSearchType" />
                </td>
                <td style="padding-right:16px">
                    <asp:RadioButton ID="radioButtonCode" runat="server" Text="Busca pelo Código:" GroupName="radioButtonSearchType" />
                </td>
                <td>
                    <asp:TextBox ID="textBoxSiteCode" runat="server" Width="100%" Font-Size="Large"></asp:TextBox>
                </td>
            </tr>
        </table>
        <hr />
        <table  border="0" cellpadding="0" cellspacing="0" width="100%" class="searchBar">
            <tr>
                <td>Tipo de Imóvel:</td>
                <td>
                    <asp:DropDownList ID="dropDownSiteType" runat="server" Width="280px" Font-Size="Medium"></asp:DropDownList></td>
                <td></td>
                <td>Cidade:</td>
                <td>
                    <asp:DropDownList ID="dropDownCities" runat="server" Width="280px" Font-Size="Medium"></asp:DropDownList></td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:DropDownList ID="dropDownRoomNumber" runat="server" Width="280px" Font-Size="Medium"></asp:DropDownList></td>
                <td></td>
                <td>Bairros:</td>
                <td rowspan="3" style="width: 280px; padding-top:16px;">
                    <div style="overflow-y: scroll; float: left; width: 100%; height: 150px; background-color: White;
                        color: Black;">
                        <asp:CheckBoxList ID="checkBoxListDistricts" runat="server">
                        </asp:CheckBoxList>
                    </div>
                </td>
            </tr>
            <tr>
                <td>Preço Mínimo:</td>
                <td>
                    <asp:DropDownList ID="dropDownPriceFrom" runat="server" Width="280px" Font-Size="Medium"></asp:DropDownList></td>
                <td></td>
            </tr>
            <tr>
                <td>Preço Máximo:</td>                            
                <td>
                    <asp:DropDownList ID="dropDownPriceTo" runat="server" Width="280px" Font-Size="Medium"></asp:DropDownList>
                    </td>
                <td></td>
            </tr>

        </table> 
        <table border="0" cellpadding="0" cellspacing="0" width="100%" class="searchBar">
            <tr>
                <td>
                     <asp:Button ID="buttonSearch" Text="Pesquisar" runat="server" 
                        OnClick="buttonSearch_Click"  Font-Size="Medium" 
                        style="float:right;width: 280px; "/>
                </td>
            </tr>
        </table>
       
    </div>

</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="SimVendasMasterContent">

    <link type="text/css" href="/Scripts/css/redmond/jquery-ui-1.8.9.custom.css" rel="Stylesheet" />	
    <script type="text/javascript" src="/Scripts/js/jquery-1.4.4.min.js"></script>
    <script type="text/javascript" src="/Scripts/js/jquery-ui-1.8.9.custom.min.js"></script>
    <script src="/Scripts/DefaultPage.js" type="text/javascript"></script>


    

    <div class="searchResult" runat="server">
        
        <div runat="server" visible="<%#getSearchResultVisibility(1)%>" > 
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
                            <a href="../Imovel/Default.aspx?ID=<%# Eval("Code")%>&CustomerID=<%# Eval("CustomerID")%>">
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
                        <td style="vertical-align: middle; text-align:center; width:80px;"><a href="../Imovel/Default.aspx?ID=<%# Eval("Code")%>&CustomerID=<%# Eval("CustomerID")%>"><b>Detalhes</b></a></td>
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

    <div>
        <asp:Literal ID="literalDebugResult" Text="Debug" runat="server"/>
    </div>

</asp:Content>


