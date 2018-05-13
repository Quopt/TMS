<%@ Page Title="" Language="C#" MasterPageFile="~/SiteRent.master" AutoEventWireup="true" CodeBehind="WebFormRentReturn.aspx.cs" Inherits="TMS_Recycling.WebFormRentReturn" %>
<%@ Register src="WebUserControlRentReturn.ascx" tagname="WebUserControlRentReturn" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPathLink" runat="server">
    <span class="Header2">
<asp:Label ID="LabelPath" runat="server" Text="Verhuur \ Materialen verhuren \ Materialen innemen"></asp:Label>
</span>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:WebUserControlRentReturn ID="WebUserControlRentReturn1" runat="server" />
</asp:Content>
