<%@ Page Title="" Language="C#" MasterPageFile="~/SiteRent.master" AutoEventWireup="true" CodeBehind="WebFormRentReportUsage.aspx.cs" Inherits="TMS_Recycling.WebFormRentReportUsage" %>
<%@ Register src="WebUserControlRentReportUsage.ascx" tagname="WebUserControlRentReportUsage" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPathLink" runat="server">
<span class="Header2
<asp:Label ID="LabelPath" runat="server" Text="Verhuur \ Overzicht gebruik verhuurmateriaal"></asp:Label>
</span>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:WebUserControlRentReportUsage ID="WebUserControlRentReportUsage1" 
        runat="server" />
</asp:Content>
