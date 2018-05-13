<%@ Page Title="" Language="C#" MasterPageFile="~/SiteBookkeeping.master" AutoEventWireup="true" CodeBehind="WebFormBookKeepingMutations.aspx.cs" Inherits="TMS_Recycling.WebFormBookKeepingMutations" %>
<%@ MasterType virtualpath="SiteBookkeeping.master" %>
<%@ Register src="WebUserControlLedgerMutation.ascx" tagname="WebUserControlLedgerMutation" tagprefix="uc1" %>
<%@ Register src="CalendarControl.ascx" tagname="CalendarControl" tagprefix="uc2" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit.HTMLEditor" tagprefix="cc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="ComboBoxLocation.ascx" tagname="ComboBoxLocation" tagprefix="uc3" %>
<%@ Register src="WebUserControlBookKeepingMutations.ascx" tagname="WebUserControlBookKeepingMutations" tagprefix="uc4" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPathLink" runat="server">
    <span class="Header2">
        <asp:Label ID="LabelPath" runat="server" Text="Boekhouding \ Dagboek \ Overzicht mutaties"></asp:Label></span>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc4:WebUserControlBookKeepingMutations ID="WebUserControlBookKeepingMutations1" 
        runat="server" />
</asp:Content>
