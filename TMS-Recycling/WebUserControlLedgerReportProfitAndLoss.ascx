<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlLedgerReportProfitAndLoss.ascx.cs" Inherits="TMS_Recycling.WebUserControlLedgerReportProfitAndLoss" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="CalendarControl.ascx" tagname="CalendarControl" tagprefix="uc1" %>
<%@ Register src="ComboBoxLocation.ascx" tagname="ComboBoxLocation" tagprefix="uc2" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<table width="100%" class="filterpanel">
<tr>
<td>
    <asp:Label ID="LabelDates" runat="server" Text="Datumselectie"></asp:Label>
</td>
<td>
<nobr>
    <asp:RadioButtonList ID="BulletedListDateSelection" runat="server" RepeatColumns="4">
        <asp:ListItem Value="ThisYear" Selected="True">Dit jaar tot vandaag</asp:ListItem>
        <asp:ListItem Value="Period">Hiernaast aangegeven periode</asp:ListItem>
    </asp:RadioButtonList>
</nobr>
</td>
<td>
<nobr>
    <asp:Label ID="LabelPeriod" runat="server" Text="Periode van "></asp:Label>
    <uc1:CalendarControl ID="CalendarControlStartPeriod" runat="server" />
    <br />
    <asp:Label ID="LabelUpToAndIncluding" runat="server" Text="tot"></asp:Label>
    <uc1:CalendarControl ID="CalendarControlEndPeriod" runat="server" />
</nobr>
</td>
<td>
    <asp:Label ID="LabelLocation0" runat="server" Text="Lokatie"></asp:Label>
    <br />

    <uc2:ComboBoxLocation ID="ComboBoxSelectedLocation" runat="server" />

</td>
</tr>
<tr>
<td></td><td>
    </td>
<td align="right">
    &nbsp;</td>
<td align="right">
    <asp:Button ID="ButtonShowReport" runat="server" Text="Rapport tonen" 
        onclick="ButtonShowReport_Click" />
    </td>
</tr>
</table>

 <table width="100%" class="resultspanel"><tr><td>
   <iframe id="FrameShowReport" scrolling="auto" runat="server" height="400px" width="100%">
  </iframe>
 </td></tr></table>

     <asp:Label ID="LabelURL" runat="server" Visible="false"></asp:Label>
     
