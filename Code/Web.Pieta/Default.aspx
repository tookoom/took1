<%@ Page Title="Pietá Imóveis - Página Inicial" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <div class="headerBlueLine"><h2>Destaques Vendas</h2></div>

    <div class="featuredSites">

        <asp:DataList ID="dataListFeaturedSiteAds" runat="server" 
            DataSourceID="objectDataSourceFeaturedSites" RepeatDirection="Horizontal" 
            RepeatLayout="Flow">
            <ItemTemplate>
            <a href="../Imovel/Default.aspx?ID=<%# Eval("Code")%>&AdTypeID=<%# Eval("AdTypeID")%>">
                                <img src="http://www.tk1.net.br/Integra/Mdo/SimVendas/Fotos/4/<%# Eval("Code") %>/<%# Eval("MainPicUrl") %>" height="100px" style="margin: 6px 0px 4px 0px;" />
                            </a>
                <%--<table border="0" cellpadding="0" cellspacing="0" width="200px">
                    <tr >
                        <td style="vertical-align: middle; width:170px;">
                            <a href="../Imovel/Default.aspx?ID=<%# Eval("Code")%>&AdTypeID=<%# Eval("AdTypeID")%>">
                                <img src="<%# Eval("MainPicUrl") %>" height="100px" style="margin: 6px 0px 4px 0px;" />
                            </a>
                        </td>
                    </tr>
                    <tr class="center">
                        <td style="vertical-align: middle; width:130px; text-align:center;"><%# Eval("SiteType")%></td>
                    </tr>
                    <tr class="center">
                        <td id="Td1" style="vertical-align: middle; width:130px; text-align:center;" runat="server" visible='<%#Eval("IsRoomNameVisible")%>'>
                               <%# Eval("SiteTotalRooms")%>  <%# Eval("SiteTypeRoomName")%>
                        </td>
                    </tr>
                    <tr class="center">
                        <td id="Td2" style="vertical-align: middle; width:130px; text-align:center;" runat="server" visible='<%#Eval("IsAreaNameVisible")%>'>
                               <%# Eval("SiteTotalArea", "{0:0.##}")%>  m²
                        </td>
                    </tr>
                    <tr class="center">
                        <td style="vertical-align: middle; width:150px; text-align:center;">Bairro <%# Eval("District")%></td>
                    </tr>
                    <tr class="center">
                        <td style="vertical-align: middle; font-size: 1.5em; width:200px; text-align:center;"><%# Eval("Value", "{0:c}")%></td>
                    </tr>
                    <tr class="center">
                        <td style="vertical-align: middle; text-align:center; "><a href="../Imovel/Default.aspx?ID=<%# Eval("Code")%>&AdTypeID=<%# Eval("AdTypeID")%>">Detalhes</a></td>
                    </tr>
                    
                </table>--%>
            </ItemTemplate>
        </asp:DataList>

<%--        <asp:ListView ID="listViewFeaturedSiteAds" runat="server"
            onpagepropertieschanged="listViewSearchResults_PagePropertiesChanged" 
            DataSourceID="objectDataSourceFeaturedSites">
            <EmptyDataTemplate>
                <h3>Nenhum destaque cadastrado.</h3>
            </EmptyDataTemplate>
            <ItemTemplate>
                <table border="0" cellpadding="0" cellspacing="0" width="932px" style="border-bottom:2px solid #23669A; ">
                    <tr class="center">
                        <td style="vertical-align: middle; width:170px;">
                            <a href="../Imovel/Default.aspx?ID=<%# Eval("Code")%>&AdTypeID=<%# Eval("AdTypeID")%>">
                                <img src="<%# Eval("MainPicUrl") %>" height="100px" style="margin: 6px 0px 4px 0px;" />
                            </a>
                        </td>
                    </tr>
                    <tr class="center">
                        <td style="vertical-align: middle; width:130px; text-align:center;"><%# Eval("SiteType")%></td>
                    </tr>
                    <tr class="center">
                        <td id="Td1" style="vertical-align: middle; width:130px; text-align:center;" runat="server" visible='<%#Eval("IsRoomNameVisible")%>'>
                               <%# Eval("SiteTotalRooms")%>  <%# Eval("SiteTypeRoomName")%>
                        </td>
                    </tr>
                    <tr class="center">
                        <td id="Td2" style="vertical-align: middle; width:130px; text-align:center;" runat="server" visible='<%#Eval("IsAreaNameVisible")%>'>
                               <%# Eval("SiteTotalArea", "{0:0.##}")%>  m²
                        </td>
                    </tr>
                    <tr class="center">
                        <td style="vertical-align: middle; width:150px; text-align:center;">Bairro <%# Eval("District")%></td>
                    </tr>
                    <tr class="center">
                        <td style="vertical-align: middle; font-size: 1.5em; width:200px; text-align:center;"><%# Eval("Value", "{0:c}")%></td>
                    </tr>
                    <tr class="center">
                        <td style="vertical-align: middle; text-align:center; "><a href="../Imovel/Default.aspx?ID=<%# Eval("Code")%>&AdTypeID=<%# Eval("AdTypeID")%>">Detalhes</a></td>
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
--%>        


        <asp:ObjectDataSource ID="objectDataSourceFeaturedSites" runat="server" 
            SelectMethod="GetFeaturedSiteAds" 
            TypeName="TK1.Bizz.Mdo.Data.Controller.MdoSiteAdController" 
            ondatabinding="objectDataSourceFeaturedSites_DataBinding" >
            <SelectParameters>
                <asp:Parameter DefaultValue="pieta" Name="mdoAcronym" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>

    </div>

    <div class="clear releases">
<%--        <h3>
            Destaques e Lançamentos
        </h3>
--%>
    <br />
    <br />
    <br />
        <br />

        <br />
            <img src="Images/CaixaAqui.jpg" class="center" />
        <br />
    </div>

<%--    <p>
        To learn more about ASP.NET visit <a href="http://www.asp.net" title="ASP.NET Website">www.asp.net</a>.
    </p>

    <p>
        You can also find <a href="http://go.microsoft.com/fwlink/?LinkID=152368&amp;clcid=0x409"
            title="MSDN ASP.NET Docs">documentation on ASP.NET at MSDN</a>.
    </p>

    <input type="text" name="date" id="date" />
--%>
</asp:Content>
