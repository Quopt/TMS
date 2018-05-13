<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlCustomerReport.ascx.cs" Inherits="TMS_Recycling.WebUserControlCustomerReport" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<table width="100%" class="filterpanel">
<tr>
<td>
    <asp:Label ID="LabelAdressType" runat="server" Text="Soort adres"></asp:Label>
</td>
<td>


</td>
<td>


</td>
</tr>
<tr>
<td>
    <cc11:ClassComboBox ID="ComboBoxAdressType" runat="server" AutoCompleteMode="SuggestAppend" 
        DropDownStyle="DropDownList">
    </cc11:ClassComboBox>
    </td><td>
    </td>
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
<asp:Label ID="LabelURL" runat="server" Text="Label" Visible="False"></asp:Label>
