<%@ Page Title="" Language="C#" MasterPageFile="~/SiteBookkeeping.master" AutoEventWireup="true" CodeBehind="WebFormBookKeepingClosures.aspx.cs" Inherits="TMS_Recycling.WebFormBookKeepingClosures" %>
<%@ MasterType virtualpath="SiteBookkeeping.master" %>
<%@ Register src="WebUserControlBookKeepingClosuresOverview.ascx" tagname="WebUserControlBookKeepingClosuresOverview" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPathLink" runat="server">
    <span class="Header2"><asp:Label ID="LabelPath" runat="server" Text="Boekhouding \ Dagboek \ Overzicht sluitstanden"></asp:Label></span>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <uc1:WebUserControlBookKeepingClosuresOverview ID="WebUserControlBookKeepingClosuresOverview1" 
        runat="server" />

</asp:Content>
