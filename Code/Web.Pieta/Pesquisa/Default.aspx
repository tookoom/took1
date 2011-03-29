<%@ Page Title="Pietá Imóveis - Pesquisa" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Pesquisa_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">

    <div class="clear search">
        <div style="margin:0px 0px 0px 0px; float: left; position:absolute; width: 100px;text-align: left;">
            <h3>Quero:
            </h3>
            <h3>Tipo:
            </h3>
            <h3>Valor:
            </h3>
        </div>
        <div style="margin:0px 0px 0px 104px; float: left; position:absolute; width: 500px;text-align: left;">
            <div style="margin:8px 0px 0px 0px;">
                <asp:RadioButton ID="radioButtonRent" runat="server" Text="Alugar" GroupName="radioButtonAdType" />
                <asp:RadioButton ID="radioButtonBuy" runat="server" Text="Comprar" GroupName="radioButtonAdType" style="margin:0px 0px 0px 60px; position:absolute;"/>
            </div>
            <div style="margin:0px 0px 0px 0px;">
                <asp:RadioButton ID="radioButtonSiteBusiness" runat="server" Text="Comercial" GroupName="radioButtonSiteCategory" />
                <asp:RadioButton ID="radioButtonSiteResidence" runat="server" Text="Residencial" GroupName="radioButtonSiteCategory" style="margin:0px 0px 0px 60px; position:absolute;"/>
            </div>
            <div style="margin:20px 0px 0px 0px;">
                <asp:DropDownList ID="dropDownSiteType" runat="server">
                </asp:DropDownList>
                <asp:DropDownList ID="dropDownRoomNumber" runat="server">
                </asp:DropDownList>
            </div>
            <div style="margin:20px 0px 0px 0px; position:absolute;">
                <p style="float:left">De:</p>
                <asp:DropDownList ID="dropDownPriceFrom" runat="server" style="float:left">
                </asp:DropDownList>
                <p style="float:left">Até:</p>
                <asp:DropDownList ID="dropDownPriceTo" runat="server" style="float:left">
                </asp:DropDownList>
            </div>
        </div>
        <div style="margin:0px 0px 0px 508px; float: left; position:absolute; width: 300px;text-align: left; position:absolute;">
            <div>
                <asp:DropDownList ID="dropDownCities" runat="server">
                </asp:DropDownList>
            </div>
            <div>
                <asp:CheckBoxList ID="checkBoxListDistricts" runat="server">
                </asp:CheckBoxList>
            </div>
            <div>
                <asp:Button ID="buttonSearch" runat="server" Text="Pesquisar" 
                    onclick="buttonSearch_Click" />
            </div>
        </div>

    </div>

</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <link type="text/css" href="/Scripts/css/redmond/jquery-ui-1.8.9.custom.css" rel="Stylesheet" />	
    <script type="text/javascript" src="/Scripts/js/jquery-1.4.4.min.js"></script>
    <script type="text/javascript" src="/Scripts/js/jquery-ui-1.8.9.custom.min.js"></script>
    <script src="/Scripts/DefaultPage.js" type="text/javascript"></script>



    <div class="clear searchResult">
        <h3>
            Resultados da Pesquisa
        </h3>
        <%--DataSourceID="objectDataSourceSiteSearch"--%> 
        <asp:ListView ID="listViewSearchResults" runat="server" 
            EnableModelValidation="True">
            <AlternatingItemTemplate>
                <span style="background-color: #FFF8DC;">Title:
                <asp:Label ID="TitleLabel" runat="server" Text='<%# Eval("Title") %>' />
                <br />
                Price:
                <asp:Label ID="PriceLabel" runat="server" Text='<%# Eval("Price") %>' />
                <br />
                Description:
                <asp:Label ID="DescriptionLabel" runat="server" 
                    Text='<%# Eval("Description") %>' />
                <br />
<br /></span>
            </AlternatingItemTemplate>
            <EditItemTemplate>
                <span style="background-color: #008A8C;color: #FFFFFF;">Title:
                <asp:TextBox ID="TitleTextBox" runat="server" Text='<%# Bind("Title") %>' />
                <br />
                Price:
                <asp:TextBox ID="PriceTextBox" runat="server" Text='<%# Bind("Price") %>' />
                <br />
                Description:
                <asp:TextBox ID="DescriptionTextBox" runat="server" 
                    Text='<%# Bind("Description") %>' />
                <br />
                <asp:Button ID="UpdateButton" runat="server" CommandName="Update" 
                    Text="Update" />
                <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" 
                    Text="Cancel" />
                <br /><br /></span>
            </EditItemTemplate>
            <EmptyDataTemplate>
                <span>Nenhum resultado encontrado.</span>
            </EmptyDataTemplate>
            <InsertItemTemplate>
                <span style="">Title:
                <asp:TextBox ID="TitleTextBox" runat="server" Text='<%# Bind("Title") %>' />
                <br />Price:
                <asp:TextBox ID="PriceTextBox" runat="server" Text='<%# Bind("Price") %>' />
                <br />Description:
                <asp:TextBox ID="DescriptionTextBox" runat="server" 
                    Text='<%# Bind("Description") %>' />
                <br />
                <asp:Button ID="InsertButton" runat="server" CommandName="Insert" 
                    Text="Insert" />
                <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" 
                    Text="Clear" />
                <br /><br /></span>
            </InsertItemTemplate>
            <ItemTemplate>
                <span style="background-color: #DCDCDC;color: #000000;">Title:
                <asp:Label ID="TitleLabel" runat="server" Text='<%# Eval("Title") %>' />
                <br />
                Price:
                <asp:Label ID="PriceLabel" runat="server" Text='<%# Eval("Price") %>' />
                <br />
                Description:
                <asp:Label ID="DescriptionLabel" runat="server" 
                    Text='<%# Eval("Description") %>' />
                <a target="_top" href="http//www.pietaimoveis.com.br/Imovel/Default.aspx?ID=<%# Eval("SiteAdID") %>&AdType=<%# Eval("AdType.AdTypeID") %>">Detalhes</a>
                
                <br />
<br /></span>
            </ItemTemplate>
            <LayoutTemplate>
                <div ID="itemPlaceholderContainer" runat="server" 
                    style="font-family: Verdana, Arial, Helvetica, sans-serif;">
                    <span runat="server" id="itemPlaceholder" />
                </div>
                <div style="text-align: center;background-color: #CCCCCC;font-family: Verdana, Arial, Helvetica, sans-serif;color: #000000;">
                </div>
            </LayoutTemplate>
            <SelectedItemTemplate>
                <span style="background-color: #008A8C;font-weight: bold;color: #FFFFFF;">Title:
                <asp:Label ID="TitleLabel" runat="server" Text='<%# Eval("Title") %>' />
                <br />
                Price:
                <asp:Label ID="PriceLabel" runat="server" Text='<%# Eval("Price") %>' />
                <br />
                Description:
                <asp:Label ID="DescriptionLabel" runat="server" 
                    Text='<%# Eval("Description") %>' />
                <br />
<br /></span>
            </SelectedItemTemplate>
        </asp:ListView>

<%--        <asp:Repeater ID="searchResultRepeater" runat="server" 
            DataSourceID="ObjectDataSource1">
                <ItemTemplate>
                    <h3> <%# Eval("Title")%> </h3>
                    <h5> <%# Eval("Price")%> </h5>
                    <h5> <%# Eval("Description")%> </h5>
                                       
                </ItemTemplate>                                 

        </asp:Repeater>
--%>

        <asp:ObjectDataSource ID="objectDataSourceSiteSearch" runat="server" 
            SelectMethod="SearchSites" TypeName="TK1.Bizz.Pieta.Data.SiteController" >
            <SelectParameters>
                <asp:Parameter Name="parameters" Type="Object" />
            </SelectParameters>
        </asp:ObjectDataSource>

<%--        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
            SelectMethod="GetData" TypeName="TestDataSource"></asp:ObjectDataSource>
--%>
        <asp:DataPager ID="DataPager1" runat="server" PagedControlID="listViewSearchResults" 
            PageSize="5">
            <Fields>
                <asp:NextPreviousPagerField ButtonType="Button" FirstPageText="Primeira" 
                    LastPageText="Ultima" NextPageText="Proxima" PreviousPageText="Anterior" 
                    ShowFirstPageButton="True" ShowLastPageButton="True" />
                <asp:NumericPagerField />
            </Fields>
        </asp:DataPager>
    </div>

</asp:Content>


