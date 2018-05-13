<%@ Page Title="" Language="C#" MasterPageFile="~/SiteFreight.master" AutoEventWireup="true" CodeBehind="WebFormFreightCollect.aspx.cs" Inherits="TMS_Recycling.WebFormFreightCollect" %>
<%@ Register src="WebUserControlFreightWeighing.ascx" tagname="WebUserControlFreightWeighing" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <uc1:WebUserControlFreightWeighing ID="WebUserControlFreightWeighing1" 
        runat="server" />

</asp:Content>
<asp:Content ID="Content2" runat="server" 
    contentplaceholderid="ContentPlaceHolderPathLink">
    <span class="Header2">
        <asp:Label ID="LabelPath" runat="server" Text="Vrachten \ Vracht halen"></asp:Label></span>
</asp:Content>
