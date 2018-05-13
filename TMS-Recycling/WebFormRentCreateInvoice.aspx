<%@ Page Title="" Language="C#" MasterPageFile="~/SiteRent.master" AutoEventWireup="true" CodeBehind="WebFormRentCreateInvoice.aspx.cs" Inherits="TMS_Recycling.WebFormRentCreateInvoice" %>
<%@ Register src="WebUserControlRentCreateInvoice.ascx" tagname="WebUserControlRentCreateInvoice" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPathLink" runat="server">
<span class="Header2
<asp:Label ID="LabelPath" runat="server" Text="Verhuur \ Verhuringen factureren"></asp:Label>
</span>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:WebUserControlRentCreateInvoice ID="WebUserControlRentCreateInvoice1" 
        runat="server" />
</asp:Content>
