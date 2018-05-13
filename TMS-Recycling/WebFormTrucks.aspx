<%@ Page Title="" Language="C#" MasterPageFile="~/SiteFreight.master" AutoEventWireup="true" CodeBehind="WebFormTrucks.aspx.cs" Inherits="TMS_Recycling.WebFormTrucks" %>
<%@ Register src="WebUserControlTruckOverview.ascx" tagname="WebUserControlTruckOverview" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPathLink" runat="server">
    <span class="Header2">
        <asp:Label ID="LabelPath" runat="server" Text="Wegen en sorteren \ Beheer \ Vrachtwagens"></asp:Label>
    </span>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <uc1:WebUserControlTruckOverview ID="WebUserControlTruckOverview1" 
        runat="server" />

</asp:Content>
