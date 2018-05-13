<%@ Page Title="" Language="C#" MasterPageFile="~/SiteFreight.master" AutoEventWireup="true" CodeBehind="WebFormFreightControl.aspx.cs" Inherits="TMS_Recycling.WebFormFreightControl" %>
<%@ Register src="WebUserControlFreightOverview.ascx" tagname="WebUserControlFreightOverview" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:WebUserControlFreightOverview ID="WebUserControlFreightOverview1" 
        runat="server" />
</asp:Content>
<asp:Content ID="Content2" runat="server" 
    contentplaceholderid="ContentPlaceHolderPathLink">
    <span class="Header2">
        <asp:Label ID="LabelPath" runat="server" 
        Text="Vrachten \ Overzicht weegbonnen en vrachten"></asp:Label></span>
</asp:Content>

