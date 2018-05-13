<%@ Page Title="" Language="C#" MasterPageFile="~/SiteStock.Master" AutoEventWireup="true" CodeBehind="WebFormStockClosures.aspx.cs" Inherits="TMS_Recycling.WebFormStockClosures" %>
<%@ MasterType virtualpath="SiteStock.master" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="WebUserControlStockClosuresOverview.ascx" tagname="WebUserControlStockClosuresOverview" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPathLink" runat="server">
    <asp:Label ID="Label1"  CssClass="Header2" runat="server" Text="Voorraad en materialen \ Materialen en uren \ Voorraad sluitstanden"></asp:Label>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <uc1:WebUserControlStockClosuresOverview ID="WebUserControlStockClosuresOverview1" 
        runat="server" />

</asp:Content>
