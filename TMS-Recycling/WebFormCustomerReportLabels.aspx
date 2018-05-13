<%@ Page Title="" Language="C#" MasterPageFile="~/SiteCustomerRelation.master" AutoEventWireup="true" CodeBehind="WebFormCustomerReportLabels.aspx.cs" Inherits="TMS_Recycling.WebFormCustomerReportLabels" %>
<%@ Register src="WebUserControlCustomerReportLabels.ascx" tagname="WebUserControlCustomerReportLabels" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPathLink" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:WebUserControlCustomerReportLabels ID="WebUserControlCustomerReportLabels1" 
        runat="server" />
</asp:Content>
