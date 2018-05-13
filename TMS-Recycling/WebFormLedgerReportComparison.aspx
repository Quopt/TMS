<%@ Page Title="" Language="C#" MasterPageFile="~/SiteBookkeeping.master" AutoEventWireup="true" CodeBehind="WebFormLedgerReportComparison.aspx.cs" Inherits="TMS_Recycling.WebFormLedgerReportComparison" %>
<%@ Register src="WebUserControlLedgerReportComparison.ascx" tagname="WebUserControlLedgerReportComparison" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPathLink" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:WebUserControlLedgerReportComparison ID="WebUserControlLedgerReportComparison1" 
        runat="server" />
</asp:Content>
