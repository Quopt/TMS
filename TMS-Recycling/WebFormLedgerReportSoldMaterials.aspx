<%@ Page Title="" Language="C#" MasterPageFile="~/SiteBookkeeping.master" AutoEventWireup="true" CodeBehind="WebFormLedgerReportSoldMaterials.aspx.cs" Inherits="TMS_Recycling.WebFormLedgerReportSoldMaterials" %>
<%@ Register src="WebUserControlLedgerReportMaterialMovement.ascx" tagname="WebUserControlLedgerReportMaterialMovement" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPathLink" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:WebUserControlLedgerReportMaterialMovement ID="WebUserControlLedgerReportMaterialMovement1" 
        runat="server" />
</asp:Content>
