<%@ Page Title="Mdo Imóveis - Pesquisa" Language="C#" MasterPageFile="~/Nav/Mdo/SimVendas/SimVendas.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Nav_Mdo_SimVendas_Pesquisa_Default" %>


<asp:Content ID="Content1" ContentPlaceHolderID="SimVendasHeadContent" Runat="Server">

    <div class="searchBar" Runat="Server">
        <table border="0" cellpadding="4" cellspacing="0">
            <tr>
                <td style="padding-right:16px">
                    <asp:RadioButton ID="radioButtonComercial" runat="server" Text="Comercial" GroupName="radioButtonSearchType" />
                </td>
                <td style="padding-right:16px">
                    <asp:RadioButton ID="radioButtonResidencial" runat="server" Text="Residencial" GroupName="radioButtonSearchType" />
                </td>
                <td>
                    <asp:RadioButton ID="radioButtonCode" runat="server" Text="Busca pelo Código:" GroupName="radioButtonSearchType" />
                </td>
                <td>
                    <asp:TextBox ID="textBoxSiteCode" runat="server" Width="100%" Font-Size="Medium"></asp:TextBox>
                </td>
            </tr>
        </table>
        <hr />
        <table  border="0" cellpadding="4" cellspacing="0" width="100%" >
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
                <td rowspan="3" style="width: 280px; padding-right:8px;">
                    <div style="overflow-y: scroll; float: left; width: 100%; height: 140px; background-color: White;
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
                    <asp:DropDownList ID="dropDownPriceTo" runat="server" Width="280px" Font-Size="Medium"></asp:DropDownList></td>
                <td></td>
            </tr>

        </table> 
        <asp:Button ID="buttonSearch" Text="Pesquisar" runat="server" OnClick="buttonSearch_Click"  Font-Size="Medium" Width="200px"/>
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
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>Ordem de exibição:</td>
                    <td style="padding: 2px 4px 2px 4px"> <asp:LinkButton ID="linkButtonOrderPriceAsc" runat="server" OnClick="linkButtonOrderPriceAsc_Click">Menores valores primeiro</asp:LinkButton></td>
                    <td>|</td>
                    <td style="padding: 2px 4px 2px 4px"> <asp:LinkButton ID="linkButtonOrderPriceDec" runat="server" OnClick="linkButtonOrderPriceDesc_Click" >Maiores valores primeiro</asp:LinkButton></td>
                    <td>|</td>
                    <td style="padding: 2px 4px 2px 4px"> <asp:LinkButton ID="linkButtonOrderAreaAsc" runat="server" OnClick="linkButtonOrderAreaAsc_Click" >Menores áreas primeiro</asp:LinkButton></td>
                    <td>|</td>
                    <td style="padding: 2px 4px 2px 4px"> <asp:LinkButton ID="linkButtonOrderAreaDec" runat="server" OnClick="linkButtonOrdeAreaDesc_Click" >Maiores áreas primeiro</asp:LinkButton></td>
                </tr>
            </table>
        </div>


        <asp:ListView ID="listViewSearchResults" runat="server" 
            onpagepropertieschanged="listViewSearchResults_PagePropertiesChanged">
            <EmptyDataTemplate>
                <h3>Nenhum resultado encontrado.</h3>
            </EmptyDataTemplate>
            <ItemTemplate>
                <table border="0" cellpadding="0" cellspacing="0" width="932px" style="border-bottom:0px solid #23669A; ">
                    <tr class="center">
                        <td style="vertical-align: middle; width:170px;">
                            <a href="../Imovel/Default.aspx?ID=<%# Eval("SiteAdID")%>&CustomerID=<%# Eval("CustomerID")%>">
                                <img src="<%# Eval("ImageUrl") %>" height="100px" style="margin: 6px 0px 4px 0px;" />
                            </a>
                        <td style="vertical-align: middle; width:70px; text-align:center;"><b>Código <%# Eval("SiteAdID")%></b></td>
                        <td style="vertical-align: middle; width:130px; text-align:center;"><%# Eval("Site.SiteType.Name")%></td>
                        <td style="vertical-align: middle; width:130px; text-align:center;" runat="server" visible='<%#getSiteRoomNameVisibility(Eval("Site.SiteType.Category.CategoryID").ToString())%>'>
                               <%# Eval("Site.TotalRooms")%>  <%# Eval("Site.SiteType.RoomDisplayName")%>
                        </td>
                        <td id="Td1" style="vertical-align: middle; width:130px; text-align:center;" runat="server">
                               <%# Eval("Site.TotalArea", "{0:0.##}")%>  m²
                        </td>
                        <td style="vertical-align: middle; width:150px; text-align:center;">Bairro <%# Eval("Site.District.Name")%></td>
                        <td style="vertical-align: middle; font-size: 1.5em; width:200px; text-align:center;"><%# Eval("Price", "{0:c}")%></td>
                        <td style="vertical-align: middle; text-align:center; "><a href="../Imovel/Default.aspx?ID=<%# Eval("SiteAdID")%>&CustomerID=<%# Eval("CustomerID")%>">Detalhes</a></td>
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
        <div style="height: 50px;">
            <asp:DataPager ID="siteSearchDataPager" style="float: right; padding:10px; font-size:medium; font-weight: bold;"
                runat="server" PagedControlID="listViewSearchResults" PageSize="20">
                <Fields>
                    <asp:NextPreviousPagerField ButtonType="Button" FirstPageText="Primeira" 
                        LastPageText="Última" NextPageText="Próxima" PreviousPageText="Anterior" />
                    <asp:NumericPagerField />
                </Fields>
            </asp:DataPager>
        </div>
    </div>

</asp:Content>


