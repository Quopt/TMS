<%@ Page Title="" Language="C#" MasterPageFile="~/SitePurchase.master" AutoEventWireup="true" CodeBehind="WebFormPurchaseInvoice.aspx.cs" Inherits="TMS_Recycling.WebFormPurchaseInvoice" %>
<%@ Register src="~/CalendarControl.ascx" tagname="CalendarControl" tagprefix="uc1" %><%@ Register src="~/WebUserControlInvoiceBase.ascx" tagname="WebUserControlInvoiceBase" tagprefix="uc3" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="WebUserControlInvoiceOverview.ascx" tagname="WebUserControlInvoiceOverview" tagprefix="uc2" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPathLink" runat="server">
    <span class="Header2"><asp:Label ID="LabelPath" runat="server" Text="Inkopen \ Inkoopfacturen overzicht"></asp:Label></span>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <uc2:WebUserControlInvoiceOverview ID="WebUserControlInvoiceOverview1" 
        runat="server" />

</asp:Content>
