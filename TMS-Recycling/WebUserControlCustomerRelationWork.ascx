<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlCustomerRelationWork.ascx.cs" Inherits="TMS_Recycling.WebUserControlCustomerRelationWork" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit.HTMLEditor" tagprefix="cc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="CalendarControl.ascx" tagname="CalendarControl" tagprefix="uc1" %>
<%@ Register src="URLPopUpControl.ascx" tagname="URLPopUpControl" tagprefix="uc2" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<style type="text/css">
    .style1
    {
        height: 30px;
    }
</style>
<table width="100%">
<tr>
 <td colspan="4">
     <asp:Label ID="LabelBasisgegevens" runat="server" 
         Text="Basisgegevens werk derden" CssClass="SubMenuHeader"></asp:Label></td>
 <td>    &nbsp;</td>
 <td>   &nbsp;</td>
</tr>

<tr>
<td>
    <asp:Label ID="LabelName" runat="server" Text="Naam"></asp:Label> </td>
    <td colspan="3" width="300px"> <asp:TextBox ID="TextBox_Description_Text" runat="server" Width="90%" 
            MaxLength="250" ></asp:TextBox>
    </td>
    <td> 
        <br />
        <br />
    </td>
        <td><asp:CheckBox ID="CheckBox_IsActive_Checked" runat="server" Text="Actief" /><br />
            <asp:CheckBox ID="CheckBox_IsTreatedAsAdvancePayment_Checked" runat="server" 
                Text="Behandelen als voorschot" /><br />
            <asp:CheckBox ID="CheckBox_IsVATApplicable_Checked" runat="server" 
                Text="BTW van toepassing" />
    </td>
</tr>

<tr>
<td>
    <asp:Label ID="LabelWorkType" runat="server" Text="Werk derden soort"></asp:Label>
    </td>
<td>

    <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" 
        ID="DropDownList_WorkType_SelectedValue" runat="server">
    </cc11:ClassComboBox>

    </td>
<td></td>
<td></td>
<td></td>
<td></td>
</tr>
<tr>
<td class="style1">
    <asp:Label ID="LabelContactDate" runat="server" Text="Overeenkomst datum"></asp:Label>
    </td>
<td class="style1">
    <uc1:CalendarControl ID="CalendarControl_AgreementDateTime_SelectedDate" 
        runat="server" />
    </td>
<td class="style1">
    <asp:Label ID="LabelFollowHours" runat="server" Text="Aantal uren"></asp:Label>
</td>
<td class="style1">
    <asp:TextBox ID="TextBox_Hours" runat="server" 
            MaxLength="10" ></asp:TextBox>
</td>
<td class="style1">
    <asp:Label ID="LabelHourlyRate" runat="server" Text="Uurtarief"></asp:Label>
</td>
<td class="style1">
    <asp:TextBox ID="TextBox_HourlyRate" runat="server"
            MaxLength="10" ></asp:TextBox>
    </td>
</tr>

<tr>
<td>
    <asp:Label ID="LabelBookingCode" runat="server" Text="Boekingscode"></asp:Label>
</td>
<td>

    <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" ID="DropDownList_LedgerBookingCode" runat="server" 
        DataSourceID="EntityDataSourceLedgerBookingCodes" DataTextField="Description" 
        DataValueField="Id">
    </cc11:ClassComboBox>

</td>

<td>
    <asp:Label ID="LabelVATPercentage" runat="server" Text="BTW%"></asp:Label>
    </td>
<td>

    <asp:TextBox ID="TextBox_VATPercentage" runat="server" 
            MaxLength="10" ></asp:TextBox>
    </td>
<td>
    <asp:Label ID="LabelAmountPaidBack" runat="server" Text="Reeds terugbetaald"></asp:Label>
</td>
<td>

    <asp:Label ID="Label_AmountPaidBack" runat="server" Text="..."></asp:Label>

</td>
</tr>

<tr>
<td>
    <asp:Label ID="LabelAmountEXVat" runat="server" Text="Bedrag ex BTW"></asp:Label>
</td>
<td>
    <asp:Label ID="Label_AmountEXVat" runat="server" Text="..."></asp:Label>
</td>

<td>
    <asp:Label ID="LabelVAT" runat="server" Text="BTW"></asp:Label>
</td>
<td>
    <asp:Label ID="Label_VATAmount" runat="server" Text="..."></asp:Label>
</td>
<td>
    <asp:Label ID="LabelTotalAmount" runat="server" Text="Totaal bedrag (incl BTW)"></asp:Label>
</td>
<td>

    <asp:Label ID="Label_TotalAmount" runat="server" Text="..."></asp:Label>

</td>
</tr>

<tr>
    <td><asp:Label ID="Label15" runat="server" Text="Opmerkingen"></asp:Label> </td>
    <td colspan="5"> 
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
<asp:Button ID="ButtonDelete" runat="server" onclick="ButtonDelete_Click" OnClientClick="return confirm('Weet u het zeker dat u dit wilt?');"
    Text="Werk derden verwijderen" />
</nobr>
</td>
<td style="text-align:right;width:90%;">

    <uc2:URLPopUpControl ID="URLPopUpControlShowContract" Text="Toon contract" 
        runat="server" />

    <uc2:URLPopUpControl ID="URLPopUpControlLink" Text="Toon gekoppelde facturen" 
        runat="server" />

</td></tr></table>

<cc11:ClassEntityDataSource ID="EntityDataSourceLedgerBookingCodes" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EntitySetName="LedgerBookingCodeSet" 
    Select="it.[Id], it.[IsActive], it.[Description]" 
    Where="it.IsActive" OrderBy="it.Description">
</cc11:ClassEntityDataSource>

    








    




