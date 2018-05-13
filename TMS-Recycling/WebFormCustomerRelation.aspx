<%@ Page Title="Relatie overzicht" Language="C#" MasterPageFile="~/SiteCustomerRelation.master" AutoEventWireup="true" CodeBehind="WebFormCustomerRelation.aspx.cs" Inherits="TMS_Recycling.WebFormCustomerRelation" %>
<%@ MasterType virtualpath="SiteCustomerRelation.master" %>
<%@ Register src="WebUserControlCustomerRelation.ascx" tagname="WebUserControlCustomerRelation" tagprefix="uc1" %>
<%@ Register src="WebUserControlCustomerRelationOverview.ascx" tagname="WebUserControlCustomerRelationOverview" tagprefix="uc2" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPathLink" runat="server">
    <span class="Header2">
        <asp:Label ID="LabelPath" runat="server" Text="Relaties \ Relatie overzicht"></asp:Label></span>
</asp:Content>
<asp:Content ID="MainContentSection" ContentPlaceHolderID="MainContent" 
    runat="server">


    <uc2:WebUserControlCustomerRelationOverview ID="WebUserControlCustomerRelationOverview1" 
        runat="server" />


</asp:Content>
