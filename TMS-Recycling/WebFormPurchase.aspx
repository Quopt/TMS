<%@ Page Title="" Language="C#" MasterPageFile="~/SitePurchase.master" AutoEventWireup="true" CodeBehind="WebFormPurchase.aspx.cs" Inherits="TMS_Recycling.WebFormPurchase" %>
<%@ Register src="WebUserControlCashPurchase.ascx" tagname="WebUserControlCashPurchase" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="MainContent">

    <uc1:WebUserControlCashPurchase ID="WebUserControlCashPurchase1" 
        runat="server" />
&nbsp;
</asp:Content>

<asp:Content ID="Content2" runat="server" 
    contentplaceholderid="ContentPlaceHolderPathLink">
    <span class="Header2">
        <asp:Label ID="LabelPath" runat="server" Text="Inkopen \ Inkoop per kas"></asp:Label></span>
</asp:Content>






