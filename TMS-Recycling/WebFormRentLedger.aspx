<%@ Page Title="" Language="C#" MasterPageFile="~/SiteRent.master" AutoEventWireup="true" CodeBehind="WebFormRentLedger.aspx.cs" Inherits="TMS_Recycling.WebFormRentLedger" %>
<%@ Register src="WebUserControlRentLedgerOverview.ascx" tagname="WebUserControlRentLedgerOverview" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPathLink" runat="server">
<span class="Header2
<asp:Label ID="LabelPath" runat="server" Text="Verhuur \ verhuurboek overzicht"></asp:Label>
</span>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:WebUserControlRentLedgerOverview ID="WebUserControlRentLedgerOverview1" 
        runat="server" />
</asp:Content> 
