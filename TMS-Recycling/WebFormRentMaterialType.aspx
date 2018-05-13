<%@ Page Title="" Language="C#" MasterPageFile="~/SiteRent.master" AutoEventWireup="true" CodeBehind="WebFormRentMaterialType.aspx.cs" Inherits="TMS_Recycling.WebFormRentMaterialType" %>
<%@ MasterType virtualpath="SiteRent.master" %>
<%@ Register src="WebUserControlRentMaterialTypeOverview.ascx" tagname="WebUserControlRentMaterialTypeOverview" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPathLink" runat="server">
<span class="Header2">
<asp:Label ID="LabelPath" runat="server" Text="Verhuur \ beheer \ soorten materialen"></asp:Label>
</span>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:WebUserControlRentMaterialTypeOverview ID="WebUserControlRentMaterialTypeOverview1" 
        runat="server" />
</asp:Content>
