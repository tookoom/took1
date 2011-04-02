<%@ Page Title="Pietá Imóveis - Pesquisa" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Pesquisa_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">

<div style="min-height:190px;">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="width: 650px; vertical-align:top;">
                <table border="0" cellpadding="0" cellspacing="0" width="100%" >
                    <tr>
                        <td style="width: 60px; vertical-align:middle; padding-left:8px;"><p>Quero:</p></td>
                         <td style="width: 80px; vertical-align:middle; padding-top:7px; font-size:1.3em;">
                            <asp:RadioButton ID="radioButtonRent" runat="server" Text="Alugar" GroupName="radioButtonAdType" /></td>
                        <td style="width: 100px; vertical-align:middle; padding-top:7px; font-size:1.3em;">
                            <asp:RadioButton ID="radioButtonBuy" runat="server" Text="Comprar" GroupName="radioButtonAdType"/></td>
                        <td style="width: 40px; vertical-align:middle; padding-left:8px;"><p>Em:</p></td>
                        <td style="width: 290px; vertical-align:middle; padding-top:8px;">
                            <asp:DropDownList ID="dropDownCities" runat="server" Width="280px"></asp:DropDownList></td>
                        <td style="width: auto; vertical-align:middle; padding-left:8px;"><p>Bairros:</p></td>
                    </tr>
                </table>
                <%--<table border="0">
                    <tr>
                        <td style="width: 72px"></td>
                        <td style="width: 120px"><asp:RadioButton ID="radioButtonSiteBusiness" runat="server" Text="Comercial" GroupName="radioButtonSiteCategory" /></td>
                        <td style="width: 216px"><asp:RadioButton ID="radioButtonSiteResidence" runat="server" Text="Residencial" GroupName="radioButtonSiteCategory"/></td>
                    </tr>
                </table>--%>
                <table  border="0" cellpadding="0" cellspacing="0" width="100%" >
                    <tr>
                        <td style="width: 60px; vertical-align:middle; padding-left:8px;"><p>Tipo:</p></td>
                        <td style="width: 228px; padding-top:8px;">
                            <asp:DropDownList ID="dropDownSiteType" runat="server" Width="210px"></asp:DropDownList></td>
                        <td style="width: 290px; padding-top:8px;">
                            <asp:DropDownList ID="dropDownRoomNumber" runat="server" Width="280px"></asp:DropDownList></td>
                        <td style="width: auto; vertical-align:middle; padding-left:8px;"></td>
                    </tr>
                </table>
                <table border="0" cellpadding="0" cellspacing="0" width="100%" >
                    <tr>
                        <td style="width: 100px; vertical-align:middle; padding-left:8px;"><p>Preço entre:</p></td>
                        <td style="width: 140px; vertical-align:middle; padding-top:8px;">
                            <asp:DropDownList ID="dropDownPriceFrom" runat="server" Width="130px"></asp:DropDownList></td>
                        <td style="width: 30px; vertical-align:middle; padding-left:8px;"><p>e</p></td>
                        <td style="width: 220px; vertical-align:middle; padding-top:8px;">
                            <asp:DropDownList ID="dropDownPriceTo" runat="server" Width="210px"></asp:DropDownList></td>
                        <td style="width: auto; vertical-align:middle; padding-left:8px;"></td>
                    </tr>
                </table>
            </td>
            <td style="width: auto; padding:12px">
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
                        <td style="vertical-align: middle; width:170px;">
                            <img src="../<%# Eval("ImageUrl") %>" height="100px" style="margin: 6px 0px 4px 0px;" />
                        </td>
                        <td style="vertical-align: middle; width:70px; text-align:center;">Código <%# Eval("SiteAdID")%></td>
                        <td style="vertical-align: middle; width:130px; text-align:center;"><%# Eval("Site.SiteType.Name")%></td>
                        <td style="vertical-align: middle; width:130px; text-align:center;"><%# Eval("Site.TotalRooms")%> dormitório(s)</td>
                        <td style="vertical-align: middle; width:150px; text-align:center;">Bairro <%# Eval("Site.District.Name")%></td>
                        <td style="vertical-align: middle; font-size: 1.5em; width:200px; text-align:center;"><%# Eval("Price", "{0:c}")%></td>
                        <td style="vertical-align: middle; text-align:center; "><a href="../Imovel/Default.aspx?ID=<%# Eval("SiteAdID")%>&AdType=<%# Eval("AdTypeID")%>">Detalhes</a></td>
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


