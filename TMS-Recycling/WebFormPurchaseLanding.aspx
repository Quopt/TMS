<%@ Page Title="" Language="C#" MasterPageFile="~/SitePurchase.master" AutoEventWireup="true" CodeBehind="WebFormPurchaseLanding.aspx.cs" Inherits="TMS_Recycling.WebFormPurchaseLanding" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPathLink" runat="server">
    <span class="Header2">
        <asp:Label ID="LabelPath" runat="server" Text="Inkopen"></asp:Label></span>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="LabelActionChoice" runat="server" 
    Text="Kies uit de iconen aan de linkerkant de actie die u uit wilt voeren."></asp:Label>
</asp:Content>
