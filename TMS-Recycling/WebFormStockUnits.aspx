<%@ Page Title="" Language="C#" MasterPageFile="~/SiteStock.Master" AutoEventWireup="true" CodeBehind="WebFormStockUnits.aspx.cs" Inherits="TMS_Recycling.WebFormStockUnits" %>
<%@ Register src="WebUserControlStockUnitsOverview.ascx" tagname="WebUserControlStockUnitsOverview" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPathLink" runat="server">
<span class="Header2">    <asp:Label ID="LabelPath" runat="server" Text="Voorraad en materialen \ Materiaal eenheden"></asp:Label>     </span>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:WebUserControlStockUnitsOverview ID="WebUserControlStockUnitsOverview1" 
        runat="server" />
</asp:Content>
