<%@ Page Title="" Language="C#" MasterPageFile="~/SiteRent.master" AutoEventWireup="true" CodeBehind="WebFormRentMaterials.aspx.cs" Inherits="TMS_Recycling.WebFormRentMaterials" %>
<%@ MasterType virtualpath="SiteRent.master" %>
<%@ Register src="WebUserControlRentMaterialOverview.ascx" tagname="WebUserControlRentMaterialOverview" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPathLink" runat="server">
<span class="Header2">
<asp:Label ID="LabelPath" runat="server" Text="Verhuur \ beheer \ Soorten materialen \ Geregistreerde materialen"></asp:Label>
</span>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:WebUserControlRentMaterialOverview ID="WebUserControlRentMaterialOverview1" 
        runat="server" />
</asp:Content>
