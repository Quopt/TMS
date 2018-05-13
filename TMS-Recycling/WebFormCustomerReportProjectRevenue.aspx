<%@ Page Title="" Language="C#" MasterPageFile="~/SiteCustomerRelation.master" AutoEventWireup="true" CodeBehind="WebFormCustomerReportProjectRevenue.aspx.cs" Inherits="TMS_Recycling.WebFormCustomerReportProjectRevenue" %>
<%@ Register src="WebUserControlCustomerReportProjectRevenue.ascx" tagname="WebUserControlCustomerReportProjectRevenue" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPathLink" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:WebUserControlCustomerReportProjectRevenue ID="WebUserControlCustomerReportProjectRevenue1" 
        runat="server" />
</asp:Content>
