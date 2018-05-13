<%@ Page Title="" Language="C#" MasterPageFile="~/SiteFreight.master" AutoEventWireup="true" CodeBehind="WebFormFreightLanding.aspx.cs" Inherits="TMS_Recycling.WebFormFreightLanding" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="LabelActionChoice" runat="server" 
    Text="Kies uit de iconen aan de linkerkant de actie die u uit wilt voeren."></asp:Label>
</asp:Content>
<asp:Content ID="Content2" runat="server" 
    contentplaceholderid="ContentPlaceHolderPathLink">
    <span class="Header2">
        <asp:Label ID="LabelPath" runat="server" Text="Wegen en sorteren"></asp:Label></span>
</asp:Content>

