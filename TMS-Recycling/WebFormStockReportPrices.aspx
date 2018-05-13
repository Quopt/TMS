<%@ Page Title="" Language="C#" MasterPageFile="~/SiteStock.Master" AutoEventWireup="true" CodeBehind="WebFormStockReportPrices.aspx.cs" Inherits="TMS_Recycling.WebFormStockReportPrices" %>
<%@ Register src="WebUserControlStockReportPrices.ascx" tagname="WebUserControlStockReportPrices" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPathLink" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:WebUserControlStockReportPrices ID="WebUserControlStockReportPrices1" 
        runat="server" />
</asp:Content>
