<%@ Page Title="" Language="C#" MasterPageFile="~/SiteBookkeeping.master" AutoEventWireup="true" CodeBehind="WebFormBookKeeping.aspx.cs" Inherits="TMS_Recycling.WebFormBookKeeping" %>
<%@ MasterType virtualpath="SiteBookkeeping.master" %>
<%@ Register src="WebUserControlBookKeepingOverview.ascx" tagname="WebUserControlBookKeepingOverview" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPathLink" runat="server">
    <span class="Header2">
        <asp:Label ID="LabelPath" runat="server" Text="Boekhouding \ Overzicht dagboeken"></asp:Label></span>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <uc1:WebUserControlBookKeepingOverview ID="WebUserControlBookKeepingOverview1" 
    runat="server" />
    
</asp:Content>
