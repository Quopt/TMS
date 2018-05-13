<%@ Page Title="" Language="C#" MasterPageFile="~/SiteBookkeeping.master" AutoEventWireup="true" CodeBehind="WebFormLedgerReportProfitAndLoss.aspx.cs" Inherits="TMS_Recycling.WebFormLedgerReportProfitAndLoss" %>
<%@ Register src="WebUserControlLedgerReportProfitAndLoss.ascx" tagname="WebUserControlLedgerReportProfitAndLoss" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPathLink" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:WebUserControlLedgerReportProfitAndLoss ID="WebUserControlLedgerReportProfitAndLoss1" 
        runat="server" />
</asp:Content>
