<%@ Page Title="" Language="C#" MasterPageFile="~/SiteBookkeeping.master" AutoEventWireup="true" CodeBehind="WebFormLedgerChecks.aspx.cs" Inherits="TMS_Recycling.WebFormLedgerChecks" %>
<%@ MasterType virtualpath="SiteBookkeeping.master" %>
<%@ Register src="WebUserControlBookKeepingChecks.ascx" tagname="WebUserControlBookKeepingChecks" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPathLink" runat="server">
   <span class="Header2"><asp:Label ID="LabelPath" runat="server" Text="Boekhouding \ Boekingscodes \ Controles"></asp:Label></span>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:WebUserControlBookKeepingChecks ID="WebUserControlBookKeepingChecks1" 
        runat="server" />
</asp:Content>
