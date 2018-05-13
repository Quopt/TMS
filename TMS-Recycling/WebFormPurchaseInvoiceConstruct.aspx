<%@ Page Title="" Language="C#" MasterPageFile="~/SitePurchase.master" AutoEventWireup="true" CodeBehind="WebFormPurchaseInvoiceConstruct.aspx.cs" Inherits="TMS_Recycling.WebFormPurchaseInvoiceConstruct" %>
<%@ Register src="WebUserControlConstructInvoiceFromOrder.ascx" tagname="WebUserControlConstructInvoiceFromOrder" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPathLink" runat="server">
    <span class="Header2"><asp:Label ID="LabelPath" runat="server" Text="Inkopen \ Inkopen factureren"></asp:Label></span>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:WebUserControlConstructInvoiceFromOrder ID="WebUserControlConstructInvoiceFromOrder1" 
        runat="server" />
</asp:Content>
