<%@ Page Title="" Language="C#" MasterPageFile="~/SiteStock.Master" AutoEventWireup="true" CodeBehind="WebFormStockMutations.aspx.cs" Inherits="TMS_Recycling.WebFormStockMutations" %>
<%@ MasterType virtualpath="SiteStock.master" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<%@ Register src="WebUserControlStockMutationsOverview.ascx" tagname="WebUserControlStockMutationsOverview" tagprefix="uc1" %>

<asp:Content ID="Content3" runat="server" 
    contentplaceholderid="ContentPlaceHolderPathLink">
<asp:Label ID="Label1"  CssClass="Header2" runat="server" Text="Voorraad en materialen \ Materialen en uren \ Materiaal mutaties"></asp:Label>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:WebUserControlStockMutationsOverview ID="WebUserControlStockMutationsOverview1" 
        runat="server" />
</asp:Content>
