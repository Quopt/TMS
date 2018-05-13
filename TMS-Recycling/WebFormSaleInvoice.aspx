<%@ Page Title="" Language="C#" MasterPageFile="~/SiteSale.master" AutoEventWireup="true" CodeBehind="WebFormSaleInvoice.aspx.cs" Inherits="TMS_Recycling.WebFormSaleInvoice" %>
<%@ Register src="WebUserControlInvoiceBase.ascx" tagname="WebUserControlInvoiceBase" tagprefix="uc1" %>
<%@ Register src="WebUserControlInvoiceOverview.ascx" tagname="WebUserControlInvoiceOverview" tagprefix="uc2" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPathLink" runat="server">
    <span class="Header2">
        <asp:Label ID="LabelPath" runat="server" Text="Verkopen \ Verkoopfacturen overzicht"></asp:Label></span>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc2:WebUserControlInvoiceOverview ID="WebUserControlInvoiceOverview1" 
        runat="server" />
</asp:Content>
