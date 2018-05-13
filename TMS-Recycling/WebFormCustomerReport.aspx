<%@ Page Title="" Language="C#" MasterPageFile="~/SiteCustomerRelation.master" AutoEventWireup="true" CodeBehind="WebFormCustomerReport.aspx.cs" Inherits="TMS_Recycling.WebFormCustomerReport" %>
<%@ Register src="WebUserControlCustomerReport.ascx" tagname="WebUserControlCustomerReport" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPathLink" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:WebUserControlCustomerReport ID="WebUserControlCustomerReport1" 
        runat="server" />
</asp:Content>
