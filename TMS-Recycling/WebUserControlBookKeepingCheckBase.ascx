<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlBookKeepingCheckBase.ascx.cs" Inherits="TMS_Recycling.WebUserControlBookKeepingCheckBase" %>
<%@ Register src="URLPopUpControl.ascx" tagname="URLPopUpControl" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>
<%@ Register src="CalendarWithTimeControl.ascx" tagname="CalendarWithTimeControl" tagprefix="uc2" %>

<table width="100%">
<tr>
 <td colspan="2">
     <asp:Label ID="LabelBasisgegevens" runat="server" 
         Text="Basisgegevens controle" CssClass="SubMenuHeader"></asp:Label>
    &nbsp;<asp:Label ID="LabelCurrentLevel" runat="server" Text="(Huidige kasniveau {0})"></asp:Label>
    <asp:Label ID="LabelCurrentLevelBase" runat="server" Text="(Huidige kasniveau {0})" Visible="false"></asp:Label>
    </td>
 <td>    &nbsp;</td>
 <td>   &nbsp;</td>
</tr>
<tr>
<td>
    <asp:Label ID="LabelName" runat="server" Text="Omschrijving"></asp:Label> </td>
    <td width="300px"> 
        <asp:TextBox ID="TextBox_Description" runat="server" Width="90%" 
            MaxLength="250" ></asp:TextBox>
    </td>
    <td><asp:Label ID="LabelIsCorrected" runat="server" Text="Is gecorrigeerd"></asp:Label> 
        <br />
    </td>
        <td>
            <asp:CheckBox ID="CheckBox_IsLedgerCorrected_Checked" Text="" runat="server" 
                Checked="True" Enabled="False"  />
            <br />
            </td>
</tr>

<tr>
<td>
    <asp:Label ID="LabelCheckDateTime" runat="server" Text="Datum van deze controle"></asp:Label> 
</td>
<td>
    <uc2:CalendarWithTimeControl ID="CalendarWithTimeControl_CheckDate_SelectedDateTime" runat="server" />
</td>
<td>
    <asp:Label ID="LabelLedgerCorrectionAmount" runat="server" Text="Te corrigeren bedrag"></asp:Label> 
</td>
<td>
<nobr>
        <asp:TextBox ID="TextBox_CorrectionAmount" runat="server"
            MaxLength="250" Enabled="False" ></asp:TextBox>
</nobr>
    <asp:Button ID="ButtonCorrectNow" runat="server" Text="Correctie doorvoeren" 
        onclick="ButtonCorrectNow_Click" />
</td>
</tr>

<tr>
    <td><asp:Label ID="Label15" runat="server" Text="Opmerkingen"></asp:Label> </td>
    <td colspan="3"> 
        <asp:TextBox ID="TextBox_Comments" runat="server" Width="90%" 
            MaxLength="2000" Rows="4" TextMode="MultiLine" ></asp:TextBox>
    </td>


</tr>

</table>
<table style="width:100%;">
<tr>
<td>
<nobr>
<asp:Button ID="ButtonCancel" runat="server" Text="Wijzigingen annuleren" 
    onclick="ButtonCancel_Click" />
<asp:Button ID="ButtonSave" runat="server" Text="Wijzigingen opslaan" 
    onclick="ButtonSave_Click" />
</nobr>
</td>
<td style="width:90%;text-align:right;">
    &nbsp;</td></tr></table>


    






