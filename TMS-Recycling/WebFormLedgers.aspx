<%@ Page Title="" Language="C#" MasterPageFile="~/SiteBookkeeping.master" AutoEventWireup="true" CodeBehind="WebFormLedgers.aspx.cs" Inherits="TMS_Recycling.WebFormLedgers" %>
<%@ MasterType virtualpath="SiteBookkeeping.master" %>
<%@ Register src="WebUserControlBookingCodeOverview.ascx" tagname="WebUserControlBookingCodeOverview" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPathLink" runat="server">
    <span class="Header2"><asp:Label ID="LabelPath" runat="server" Text="Boekhouding \ Overzicht boekingscodes"></asp:Label></span>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:WebUserControlBookingCodeOverview ID="WebUserControlBookingCodeOverview1" 
        runat="server" />
</asp:Content>
