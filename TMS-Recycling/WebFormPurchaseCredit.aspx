<%@ Page Title="" Language="C#" MasterPageFile="~/SitePurchase.master" AutoEventWireup="true" CodeBehind="WebFormPurchaseCredit.aspx.cs" Inherits="TMS_Recycling.WebFormPurchaseCredit" %>
<%@ Register src="WebUserControlNonCashPurchase.ascx" tagname="WebUserControlNonCashPurchase" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPathLink" runat="server">
    <span class="Header2">
        <asp:Label ID="LabelPath" runat="server" Text="Inkopen \ Inkoop op krediet"></asp:Label></span>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:WebUserControlNonCashPurchase ID="WebUserControlNonCashPurchase1" 
        runat="server" />
</asp:Content>
