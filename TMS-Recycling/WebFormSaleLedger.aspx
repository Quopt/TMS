<%@ Page Title="" Language="C#" MasterPageFile="~/SiteSale.master" AutoEventWireup="true" CodeBehind="WebFormSaleLedger.aspx.cs" Inherits="TMS_Recycling.WebFormSaleLedger" %>
<%@ Register src="WebUserControlLedgerOverview.ascx" tagname="WebUserControlLedgerOverview" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPathLink" runat="server">
    <span class="Header2">
        <asp:Label ID="LabelPath" runat="server" Text="Verkopen \ Verkoopboek"></asp:Label></span>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:WebUserControlLedgerOverview ID="WebUserControlLedgerOverview1" 
        runat="server" />
</asp:Content>
