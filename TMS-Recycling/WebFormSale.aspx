<%@ Page Title="" Language="C#" MasterPageFile="~/SiteSale.master" AutoEventWireup="true" CodeBehind="WebFormSale.aspx.cs" Inherits="TMS_Recycling.WebFormSale" %>
<%@ Register src="WebUserControlCashPurchase.ascx" tagname="WebUserControlCashPurchase" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPathLink" runat="server">
    <span class="Header2">
        <asp:Label ID="LabelPath" runat="server" Text="Verkopen \ Verkoop per kas "></asp:Label></span>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:WebUserControlCashPurchase ID="WebUserControlCashPurchase1" 
        runat="server" />
</asp:Content>
