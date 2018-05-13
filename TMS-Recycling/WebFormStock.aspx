<%@ Page Title="" Language="C#" MasterPageFile="~/SiteStock.Master" AutoEventWireup="true" CodeBehind="WebFormStock.aspx.cs" Inherits="TMS_Recycling.WebFormStock" %>
<%@ MasterType virtualpath="SiteStock.master" %>
<%@ Register src="WebUserControlStockOverview.ascx" tagname="WebUserControlStockOverview" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolderPathLink">
<asp:Label ID="LabelPath" runat="server" Text="Voorraad en materialen \ Materialen en uren" CssClass="SubMenuHeader"></asp:Label>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="MainContent">
    <uc1:WebUserControlStockOverview ID="WebUserControlStockOverview1" 
        runat="server" />
</asp:Content>

