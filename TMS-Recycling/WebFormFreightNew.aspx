<%@ Page Title="" Language="C#" MasterPageFile="~/SiteFreight.master" AutoEventWireup="true" CodeBehind="WebFormFreightNew.aspx.cs" Inherits="TMS_Recycling.WebFormFreightNew" %>
<%@ Register src="WebUserControlFreightNewSorting.ascx" tagname="WebUserControlFreightNewSorting" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:WebUserControlFreightNewSorting ID="WebUserControlFreightNewSorting1" 
        runat="server" />
</asp:Content>
<asp:Content ID="Content2" runat="server" 
    contentplaceholderid="ContentPlaceHolderPathLink">
    <span class="Header2">
        <asp:Label ID="LabelPath" runat="server" Text="Vrachten \ Nieuwe vracht/weegbon"></asp:Label></span>
</asp:Content>
