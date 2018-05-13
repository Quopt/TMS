﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlStockReportPrices.ascx.cs" Inherits="TMS_Recycling.WebUserControlStockReportPrices" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="CalendarControl.ascx" tagname="CalendarControl" tagprefix="uc1" %>
<%@ Register src="ComboBoxLocation.ascx" tagname="ComboBoxLocation" tagprefix="uc2" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>


<table width="100%" class="filterpanel">
<tr>
<td>
    <asp:Label ID="LabelLocation" runat="server" Text="Lokatie"></asp:Label>
</td>
<td>
    <asp:Label ID="LabelPriceType" runat="server" Text="Soort prijslijst"></asp:Label>
</td>
<td>
    &nbsp;</td>
</tr>

<tr>
<td>

    <uc2:comboboxlocation ID="ComboBoxSelectedLocation" runat="server" />

</td>
<td>

    <asp:RadioButtonList ID="RadioButtonListPriceListType" runat="server">
        <asp:ListItem Selected="True" Value="Buy">Inkoop</asp:ListItem>
        <asp:ListItem Value="Sale">Verkoop</asp:ListItem>
    </asp:RadioButtonList>

</td>
<td>

    <asp:Button ID="ButtonShowReport0" runat="server" Text="Rapport tonen" 
        onclick="ButtonShowReport_Click" />

</td>
</tr>
</table>

 <table width="100%" class="resultspanel"><tr><td>
   <iframe id="FrameShowReport" scrolling="auto" runat="server" height="400px" width="100%">
  </iframe>
 </td></tr></table>

     <asp:Label ID="LabelURL" runat="server" Visible="False"></asp:Label>
     