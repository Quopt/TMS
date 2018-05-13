<%@ Page Title="" Language="C#" MasterPageFile="~/SiteCustomerRelation.master" AutoEventWireup="true" CodeBehind="WebFormCustomerRelationLocations.aspx.cs" Inherits="TMS_Recycling.WebFormCustomerRelationLocations" %>
<%@ MasterType virtualpath="SiteCustomerRelation.master" %>
<%@ Register src="WebUserControlCustomerRelationLocationOverview.ascx" tagname="WebUserControlCustomerRelationLocationOverview" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPathLink" runat="server">
    <span class="Header2">
        <asp:Label ID="LabelPath" runat="server" Text="Relaties \ Relatie \ Lokatie overzicht"></asp:Label></span>
</asp:Content>
<asp:Content ID="MainContentSection" ContentPlaceHolderID="MainContent" 
    runat="server">
    <uc1:WebUserControlCustomerRelationLocationOverview ID="WebUserControlCustomerRelationLocationOverview1" 
        runat="server" />
    </asp:Content>
