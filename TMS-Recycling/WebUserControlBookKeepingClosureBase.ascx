<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlBookKeepingClosureBase.ascx.cs" Inherits="TMS_Recycling.WebUserControlBookKeepingClosureBase" %>
<%@ Register src="URLPopUpControl.ascx" tagname="URLPopUpControl" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<table width="100%">
<tr>
 <td colspan="2">
     <asp:Label ID="LabelBasisgegevens" runat="server" 
         Text="Basisgegevens sluitstand" CssClass="SubMenuHeader"></asp:Label>
         &nbsp;<asp:Label ID="LabelLedgerDate" runat="server" Text="..."></asp:Label>
         &nbsp;<asp:Label ID="LabelLedgerName" runat="server" Text="..."></asp:Label>
    </td>
 <td>    &nbsp;</td>
 <td>   &nbsp;</td>
</tr>
<tr>
<td>
    <asp:Label ID="LabelName" runat="server" Text="Omschrijving"></asp:Label> </td>
    <td width="300px"> 
        <asp:TextBox ID="TextBox_Description_Text" runat="server" Width="90%" 
            MaxLength="250" Enabled="False" ></asp:TextBox>
    </td>
    <td><asp:Label ID="LabelIsCorrection" runat="server" Text="Is correctie"></asp:Label> </td>
        <td>
            <asp:CheckBox ID="CheckBox_IsCorrection_Checked" Text="" runat="server" 
                Checked="True" Enabled="False" /></td>
</tr>

<tr>
<td>
    <asp:Label ID="LabelLedgerLevel" runat="server" Text="Niveau"></asp:Label> 
</td>
<td>
        <asp:TextBox ID="TextBox_LedgerLevel" runat="server" Width="90%" 
            MaxLength="250" Enabled="False" ></asp:TextBox>
</td>
<td>
    <asp:Label ID="LabelLedgerLevelOrg" runat="server" Text="Niveau (voor correctie)"></asp:Label> 
</td>
<td>
        <asp:TextBox ID="TextBox_OriginalLedgerLevel" runat="server" Width="90%" 
            MaxLength="250" Enabled="False" ></asp:TextBox>
</td>
</tr>

<tr>
<td>
</td>
<td>
</td>
<td>
    <asp:Label ID="LabelLedgerLevelDelta" runat="server" 
        Text="Niveau wijziging"></asp:Label> 
</td>
<td>
        <asp:TextBox ID="TextBox_LedgerDelta" runat="server" Width="90%" 
            MaxLength="250" Enabled="False" ></asp:TextBox>
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


    





