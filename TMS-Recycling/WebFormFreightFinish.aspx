<%@ Page Title="" Language="C#" MasterPageFile="~/SiteFreight.master" AutoEventWireup="true" CodeBehind="WebFormFreightFinish.aspx.cs" Inherits="TMS_Recycling.WebFormFreightFinish" %>
<%@ Register src="WebUserControlFreightFinish.ascx" tagname="WebUserControlFreightFinish" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:WebUserControlFreightFinish ID="WebUserControlFreightFinish1" 
    runat="server" />
</asp:Content>
<asp:Content ID="Content2" runat="server" 
    contentplaceholderid="ContentPlaceHolderPathLink">
    <span class="Header2">
        <asp:Label ID="LabelPath" runat="server" Text="Vrachten \ Vracht/weegbon invullen"></asp:Label></span>
</asp:Content>
