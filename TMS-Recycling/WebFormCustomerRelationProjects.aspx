<%@ Page Title="" Language="C#" MasterPageFile="~/SiteCustomerRelation.master" AutoEventWireup="true" CodeBehind="WebFormCustomerRelationProjects.aspx.cs" Inherits="TMS_Recycling.WebFormCustomerRelationProjects" %>
<%@ MasterType virtualpath="SiteCustomerRelation.master" %>
<%@ Register src="WebUserControlCustomerRelationProject.ascx" tagname="WebUserControlCustomerRelationProject" tagprefix="uc1" %>
<%@ Register src="WebUserControlCustomerRelationProjectOverview.ascx" tagname="WebUserControlCustomerRelationProjectOverview" tagprefix="uc2" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPathLink" runat="server">
    <span class="Header2">
        <asp:Label ID="LabelPath" runat="server" Text="Relaties \ Relatie \ Project overzicht"></asp:Label></span>
</asp:Content>
<asp:Content ID="MainContentSection" ContentPlaceHolderID="MainContent" 
    runat="server">
    <uc2:WebUserControlCustomerRelationProjectOverview ID="WebUserControlCustomerRelationProjectOverview1" 
        runat="server" />
    </asp:Content>
