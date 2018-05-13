<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WebFormReportTest.aspx.cs" Inherits="TMS_Recycling.WebFormReportTest" %>
<%@ Register src="WebUserControlShowReport.ascx" tagname="WebUserControlShowReport" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderPath" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:WebUserControlShowReport ID="WebUserControlShowReport1" runat="server" />
</asp:Content>
