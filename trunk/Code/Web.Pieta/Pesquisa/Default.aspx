<%@ Page Title="Pietá Imóveis - Pesquisa" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Pesquisa_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">

<div style="min-height:190px;">
    <table border="0" cellpadding="0" cellspacing="0" width="100%" >
        <tr>
            <td style="width: 80px; vertical-align:top;">
                <h3>Quero:
                </h3>
            </td>
             <td style="width: 120px; vertical-align:top;">
                <asp:RadioButton ID="radioButtonRent" runat="server" Text="Alugar" GroupName="radioButtonAdType" />
            </td>
            <td style="width: 220px; vertical-align:top;">
                <asp:RadioButton ID="radioButtonBuy" runat="server" Text="Comprar" GroupName="radioButtonAdType"/>
            </td>
            <td style="width: 30px; vertical-align:top;"><h3>Em:</h3></td>
            <td style="width: auto; vertical-align:top;">
                <asp:DropDownList ID="dropDownCities" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="width: 450px; vertical-align:top;">
                <table border="0">
                    <tr>
                        <td style="width: 72px"></td>
                        <td style="width: 120px"><asp:RadioButton ID="radioButtonSiteBusiness" runat="server" Text="Comercial" GroupName="radioButtonSiteCategory" /></td>
                        <td style="width: 216px"><asp:RadioButton ID="radioButtonSiteResidence" runat="server" Text="Residencial" GroupName="radioButtonSiteCategory"/></td>
                    </tr>
                </table>
                <table border="0">
                    <tr>
                        <td style="width: 80px"><h3>Tipo:</h3></td>
                         <td style="width: 220px"><asp:DropDownList ID="dropDownSiteType" runat="server" Width="100%"></asp:DropDownList></td>
                        <td style="width: 220px"><asp:DropDownList ID="dropDownRoomNumber" runat="server" Width="100%"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="width: 72px"><h3>Valor:</h3></td>
                        <td style="width: 220px">
                            <p style="float:left">De:</p>
                            <asp:DropDownList ID="dropDownPriceFrom" runat="server" style="float:left; ">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 220px">
                            <p style="float:left">Até:</p>
                            <asp:DropDownList ID="dropDownPriceTo" runat="server" style="float:left">
                            </asp:DropDownList>
                        </td>
                    </tr>
<%--                    <tr>
                        <td style="width: 80px"></td>
                         <td style="width: 120px"></td>
                        <td style="width: 120px"></td>
                    </tr>
--%>
                </table>
            </td>
            <td style="width: auto">
                <div style="OVERFLOW-Y:scroll; float:left; height:120px; background-color: White; color: Black">
                    <asp:CheckBoxList ID="checkBoxListDistricts" runat="server">
                    </asp:CheckBoxList>
                </div>
            </td>
        </tr>
        <tr>
            <td style="width: 450px"></td>
            <td style="width: auto">
                <div style="margin:0px 0px 0px 0px; float: right; width: 130px; height:32px; text-align: center;" >
                    <img src="../Images/IconSearch.png" style="float: left; width: 24px; text-align: center; margin: 4px" />
                    <asp:LinkButton ID="buttonSearch" Text="Pesquisar" runat="server"  
                        class="buttonSearch" OnClick="buttonSearch_Click" />
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
        <div class="headerBlueLine">
            <h2>Resultado da Pesquisa</h2>
        </div>
        <%--<asp:GridView ID="gridViewSearchResult" runat="server" 
            DataSourceID="objectDataSourceSiteSearch" AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" EnableModelValidation="True">
            <Columns>
                <asp:ImageField DataImageUrlField="ImageUrl" 
                    NullImageUrl="~/Images/PicNotFound.jpg">
                </asp:ImageField>
                <asp:BoundField DataField="SiteAdID" HeaderText="Código" 
                    SortExpression="SiteAdID" Visible="False" />
                <asp:BoundField DataField="Title" HeaderText="Anúncio" SortExpression="Title" />
                <asp:BoundField DataField="Price" DataFormatString="{0:C}" 
                    HeaderText="Price" SortExpression="Price" />
                <asp:TemplateField>
                    <ItemTemplate><img src="../Images/PicNotFound.jpg" height="100px" /></ItemTemplate>
                    
                </asp:TemplateField>
            </Columns>
        </asp:GridView>--%>

        <%--DataSourceID="objectDataSourceSiteSearch" --%>
        <asp:ListView ID="listViewSearchResults" runat="server" 
            EnableModelValidation="True">
            <EmptyDataTemplate>
                <span>Nenhum resultado encontrado.</span>
            </EmptyDataTemplate>
            <ItemTemplate>
                <table border="0" cellpadding="0" cellspacing="0" width="932px" style="border-bottom:2px solid #23669A; ">
                    <tr class="center">
                        <td style="vertical-align: middle;">
                            <img src="../<%# Eval("ImageUrl") %>" height="100px" />
                        </td>
                        <td style="vertical-align: middle;">Código <%# Eval("SiteAdID")%></td>
                        <td style="vertical-align: middle;"><%# Eval("Site.SiteType.Name")%></td>
                        <td style="vertical-align: middle;"><%# Eval("Site.TotalRooms")%> dormitório(s)</td>
                        <td style="vertical-align: middle;">Bairro <%# Eval("Site.District.Name")%></td>
                        <td class="center"><h1> <%# Eval("Price", "{0:c}")%> </h1></td>
                        <td style="vertical-align: middle;"><a href="../Imovel/Default.aspx?ID=<%# Eval("SiteAdID")%>&AdType=<%# Eval("AdTypeID")%>">Detalhes</a></td>
                    </tr>
                    
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
        


        <asp:ObjectDataSource ID="objectDataSourceSiteSearch" runat="server" 
            SelectMethod="SearchSites" TypeName="TK1.Bizz.Pieta.Data.SiteController" >
            <SelectParameters>
                <asp:Parameter Name="parameters" Type="Object" />
            </SelectParameters>
        </asp:ObjectDataSource>

        <asp:DataPager ID="DataPager1" runat="server" PagedControlID="listViewSearchResults" 
            PageSize="5">
            <Fields>
                <asp:NextPreviousPagerField ButtonType="Button" FirstPageText="Primeira" 
                    LastPageText="Última" NextPageText="Próxima" PreviousPageText="Anterior" 
                    ShowFirstPageButton="True" ShowLastPageButton="True" />
                <asp:NumericPagerField />
            </Fields>
        </asp:DataPager>
    </div>

</asp:Content>


