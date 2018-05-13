<%@ Page Title="" Language="C#" MasterPageFile="~/SiteCustomerRelation.master" AutoEventWireup="true" CodeBehind="WebFormCustomerRelationContacts.aspx.cs" Inherits="TMS_Recycling.WebFormCustomerRelationContacts" %>
<%@ MasterType virtualpath="SiteCustomerRelation.master" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit.HTMLEditor" tagprefix="cc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="WebUserControlCustomerRelationContact.ascx" tagname="WebUserControlCustomerRelationContact" tagprefix="uc1" %>
<%@ Register src="WebUserControlCustomerRelationContactLog.ascx" tagname="WebUserControlCustomerRelationContactLog" tagprefix="uc2" %>
<%@ Register src="WebUserControlCustomerRelationContactOverview.ascx" tagname="WebUserControlCustomerRelationContactOverview" tagprefix="uc3" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPathLink" runat="server">
    <span class="Header2">
        <asp:Label ID="LabelPath" runat="server" Text="Relaties \ Relatie \ Contactpersonen en -log"></asp:Label></span>
</asp:Content>
<asp:Content ID="MainContentSection" ContentPlaceHolderID="MainContent" 
    runat="server">
    <uc3:WebUserControlCustomerRelationContactOverview ID="WebUserControlCustomerRelationContactOverview1" 
        runat="server" />
    </asp:Content>

