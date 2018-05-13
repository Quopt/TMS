<%@ Page Title="" Language="C#" MasterPageFile="~/SiteSetting.master" AutoEventWireup="true" CodeBehind="WebFormSettingUserProfile.aspx.cs" Inherits="TMS_Recycling.WebFormSettingUserProfile" %>
<%@ Register src="WebUserControlSecurityRoleOverview.ascx" tagname="WebUserControlSecurityRoleOverview" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPathLink" runat="server">
    <asp:Label ID="LabelPath" runat="server" 
        Text="Systeembeheer \ Gebruikersrollen beheren" CssClass="Header2"></asp:Label>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:WebUserControlSecurityRoleOverview ID="WebUserControlSecurityRoleOverview1" 
        runat="server" />
</asp:Content>
