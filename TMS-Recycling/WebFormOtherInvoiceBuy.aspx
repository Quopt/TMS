<%@ Page Title="" Language="C#" MasterPageFile="~/SiteBookkeeping.master" AutoEventWireup="true" CodeBehind="WebFormOtherInvoiceBuy.aspx.cs" Inherits="TMS_Recycling.WebFormOtherInvoiceBuy" %>
<%@ Register src="WebUserControlBookKeepingInvoiceBuyOverview.ascx" tagname="WebUserControlBookKeepingInvoiceBuyOverview" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPathLink" runat="server">
    <span class="Header2"><asp:Label ID="LabelPath" runat="server" Text="Boekhouding \ Overige facturen \ Inkoopfacturen"></asp:Label></span>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:WebUserControlBookKeepingInvoiceBuyOverview ID="WebUserControlBookKeepingInvoiceBuyOverview1" 
        runat="server" />
</asp:Content>
