<%@ Page Title="" Language="C#" MasterPageFile="~/SiteRent.master" AutoEventWireup="true" CodeBehind="WebFormRentVAT.aspx.cs" Inherits="TMS_Recycling.WebFormRentVAT" %>
<%@ MasterType virtualpath="SiteRent.master" %>
<%@ Register src="WebUserControlRentVATOverview.ascx" tagname="WebUserControlRentVATOverview" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPathLink" runat="server">
<span class="Header2">
<asp:Label ID="LabelPath" runat="server" Text="Verhuur \ verhuur \ BTW tarieven verhuurmateriaal"></asp:Label>
</span>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:WebUserControlRentVATOverview ID="WebUserControlRentVATOverview1" 
        runat="server" />
</asp:Content>
