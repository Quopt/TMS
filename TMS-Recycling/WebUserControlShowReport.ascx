<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlShowReport.ascx.cs" Inherits="TMS_Recycling.WebUserControlShowReport" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit.HTMLEditor" tagprefix="cc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=14.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<rsweb:ReportViewer ID="ReportViewerShow" runat="server" Font-Names="Verdana" 
    Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
    WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%" 
    Height="" ShowBackButton="False" 
    ShowRefreshButton="False" ZoomPercent="80" SizeToReportContent="True">
    <LocalReport ReportPath="Reports\ReportInvoiceA4.rdlc">
    </LocalReport>
</rsweb:ReportViewer>


