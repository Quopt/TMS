<%@ Page Title="" Language="C#" MasterPageFile="~/SiteCustomerRelation.master" AutoEventWireup="true" CodeBehind="WebFormCustomerReportContract.aspx.cs" Inherits="TMS_Recycling.WebFormCustomerReportContract" %>
<%@ Register src="WebUserControlCustomerReportContract.ascx" tagname="WebUserControlCustomerReportContract" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPathLink" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:WebUserControlCustomerReportContract ID="WebUserControlCustomerReportContract1" 
        runat="server" />
</asp:Content>
