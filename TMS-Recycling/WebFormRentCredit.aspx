﻿<%@ Page Title="" Language="C#" MasterPageFile="~/SiteRent.master" AutoEventWireup="true" CodeBehind="WebFormRentCredit.aspx.cs" Inherits="TMS_Recycling.WebFormRentCredit" %>
<%@ Register src="WebUserControlRentOut.ascx" tagname="WebUserControlRentOut" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPathLink" runat="server">
<span class="Header2">
<asp:Label ID="LabelPath" runat="server" Text="Verhuur \ verhuur op krediet"></asp:Label>
</span>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:WebUserControlRentOut ID="WebUserControlRentOut1" runat="server" />
</asp:Content>
