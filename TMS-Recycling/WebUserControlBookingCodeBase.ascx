<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlBookingCodeBase.ascx.cs" Inherits="TMS_Recycling.WebUserControlBookingCodeBase" %>
<%@ Register src="URLPopUpControl.ascx" tagname="URLPopUpControl" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<table width="100%">
<tr>
 <td colspan="2">
     <asp:Label ID="LabelBasisgegevens" runat="server" 
         Text="Basisgegevens boekingscode" CssClass="SubMenuHeader"></asp:Label>
    </td>
 <td>    &nbsp;</td>
 <td>   &nbsp;</td>
</tr>
<tr>
<td>
    <asp:Label ID="LabelName" runat="server" Text="Omschrijving"></asp:Label> </td>
    <td width="300px"> 
        <asp:TextBox ID="TextBox_Description_Text" runat="server" Width="90%" 
            MaxLength="250" ></asp:TextBox>
    </td>
    <td><asp:Label ID="LabelIsActive" runat="server" Text="Actief"></asp:Label> 
        <br />
        <asp:Label ID="LabelIsDebugLedgerCode" runat="server" Text="Is debug ledger" 
            Visible="False"></asp:Label> </td>
        <td>
            <asp:CheckBox ID="CheckBox_IsActive_Checked" Text="" runat="server" 
                Checked="True" />
            <br />
            <asp:CheckBox ID="CheckBox_IsDebugLedgerCode_Checked" Text="" runat="server" 
                Checked="True" Visible="False" /></td>
</tr>

<tr>
<td>
    <asp:Label ID="LabelLedgerLevel" runat="server" Text="Niveau"></asp:Label> 
</td>
<td>
<nobr>
        <asp:TextBox ID="TextBox_LedgerLevel" runat="server"
            MaxLength="250" Enabled="False" ></asp:TextBox>
    <uc1:URLPopUpControl ID="URLPopUpControlCorrect" runat="server" Text="Corrigeer niveau" OnPopUpClosed="URLPopUpControlCorrect_OnPopupClosed" OnBeforePopUpOpened="URLPopUpControlCorrect_OnBeforePopUpOpened"  />
</nobr>
</td>
<td>
    <asp:Label ID="LabelLedgerCurrency" runat="server" Text="Munteenheid"></asp:Label> 
</td>
<td>
        <asp:DropDownList ID="DropDownList_LedgerCurrency_SelectedValue" runat="server">
            <asp:ListItem Value="Eur">Euro</asp:ListItem>
        </asp:DropDownList>
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


    





