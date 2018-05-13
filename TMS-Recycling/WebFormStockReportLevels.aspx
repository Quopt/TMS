<%@ Page Title="" Language="C#" MasterPageFile="~/SiteStock.Master" AutoEventWireup="true" CodeBehind="WebFormStockReportLevels.aspx.cs" Inherits="TMS_Recycling.WebFormStockReportLevels" %>
<%@ Register src="WebUserControlStockReportLevels.ascx" tagname="WebUserControlStockReportLevels" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPathLink" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:WebUserControlStockReportLevels ID="WebUserControlStockReportLevels1" 
        runat="server" />
</asp:Content>
