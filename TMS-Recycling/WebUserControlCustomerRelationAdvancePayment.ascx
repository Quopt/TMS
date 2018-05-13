<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlCustomerRelationAdvancePayment.ascx.cs" Inherits="TMS_Recycling.WebUserControlCustomerRelationAdvancePayment" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="CalendarControl.ascx" tagname="CalendarControl" tagprefix="uc1" %>
<%@ Register src="URLPopUpControl.ascx" tagname="URLPopUpControl" tagprefix="uc2" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<table width="100%">
<tr>
 <td colspan="4">
     <asp:Label ID="LabelBasisgegevens" runat="server" 
         Text="Basisgegevens voorschot" CssClass="SubMenuHeader"></asp:Label></td>
 <td>    &nbsp;</td>
 <td>   &nbsp;</td>
</tr>

<tr>
<td>
    <asp:Label ID="LabelName" runat="server" Text="Naam"></asp:Label> </td>
    <td colspan="3" width="300px"> <asp:TextBox ID="TextBox_Description_Text" runat="server" Width="90%" 
            MaxLength="250" ></asp:TextBox>
    </td>
    <td><asp:Label ID="LabelIsPaidBack" runat="server" Text="Terugbetaald"></asp:Label> </td>
        <td>
            <asp:CheckBox ID="CheckBox_IsPaidBack_Checked" Text="" runat="server" 
                Checked="True" /></td>
</tr>

<tr>
<td>
    <asp:Label ID="LabelPaymentType" runat="server" Text="Soort voorschot"></asp:Label>
    </td>
<td>

    <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" 
        ID="DropDownList_PaymentType_SelectedValue" runat="server">
    </cc11:ClassComboBox>

    </td>
<td></td>
<td></td>
<td></td>
<td></td>
</tr>
<tr>
<td>
    <asp:Label ID="LabelPaymentDateTime" runat="server" Text="Betaaldatum"></asp:Label>
    </td>
<td>
    <uc1:CalendarControl ID="CalendarControl_PaymentDateTime_SelectedDate" 
        runat="server" />
    </td>
<td>
    <asp:Label ID="LabelAmount" runat="server" Text="Uitgeleend bedrag"></asp:Label>
</td>
<td>
    <asp:TextBox ID="TextBox_Amount" runat="server"></asp:TextBox>
</td>
<td>
    <asp:Label ID="LabelAmountPaidBack" runat="server" Text="Reeds terugbetaald bedrag"></asp:Label>
</td>
<td>
    <asp:Label ID="Label_AmountPaidBack" runat="server" Text="Reeds terugbetaald"></asp:Label>
    </td>
</tr>

<tr>
<td>
    <asp:Label ID="LabelPayedBy" runat="server" Text="Betaald per/aan"></asp:Label>
</td>
<td>

    <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" ID="DropDownList_Ledger" runat="server" 
        DataSourceID="EntityDataSourceLedgers" DataTextField="Description" 
        DataValueField="Id">
    </cc11:ClassComboBox>

</td>
<td>
    <asp:Label ID="LabelBookingCode" runat="server" Text="Boekingscode"></asp:Label>
    </td>
<td>

    <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" ID="DropDownList_LedgerBookingCode" runat="server" 
        DataSourceID="EntityDataSourceLedgerBookingCodes" DataTextField="Description" 
        DataValueField="Id">
    </cc11:ClassComboBox>

    </td>
<td colspan="2">
    <asp:Button ID="ButtonPayOut" runat="server" Text="Bedrag uitbetalen/ontvangen" 
        onclick="ButtonPayOut_Click" />
    <asp:Button ID="ButtonPayBack" runat="server" Text="Uitbetaling terugdraaien" 
        onclick="ButtonPayBack_Click" />
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
<table style="width:100%">
<tr><td>
<nobr>
<asp:Button ID="ButtonCancel" runat="server" Text="Wijzigingen annuleren" 
    onclick="ButtonCancel_Click" />
<asp:Button ID="ButtonSave" runat="server" Text="Wijzigingen opslaan" 
    onclick="ButtonSave_Click" />
<asp:Button ID="ButtonDelete" runat="server" onclick="ButtonDelete_Click" OnClientClick="return confirm('Weet u het zeker dat u dit wilt?');"
    Text="Voorschot verwijderen" />
</nobr>
</td>
<td style="width:90%; text-align:right;">
    <uc2:URLPopUpControl ID="URLPopUpControlContract" runat="server" Text="Toon contract" URLToPopup="WebFormPopup.aspx" />
    <uc2:URLPopUpControl ID="URLPopUpControlLink" runat="server" Text="Toon gekoppelde facturen" URLToPopup="WebFormPopup.aspx" />
</td>
</tr></table>
<cc11:ClassEntityDataSource ID="EntityDataSourceLedgers" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" 
    Where="" OrderBy="" CommandText="select  it.Id, it.IsActive, it.Description from 
(

(
select it.Id, it.IsActive, it.Description + @LocationText + ls.Description as Description
from LedgerSet as it inner join LocationSet as ls on (it.LimitToLocation.Id = ls.Id)
where (it.IsActive) 
)

union all

(
select it.Id, it.IsActive, it.Description as Description
from LedgerSet as it 
where (it.IsActive) and (it.LimitToLocation is null)
)

) as it
order by it.description" EntityTypeFilter="">
    <CommandParameters>
        <asp:ControlParameter ControlID="LabelLocation" Name="LocationText" 
            PropertyName="Text" Type="String" />
    </CommandParameters>
</cc11:ClassEntityDataSource>
<cc11:ClassEntityDataSource ID="EntityDataSourceLedgerBookingCodes" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EntitySetName="LedgerBookingCodeSet" 
    Select="it.[Id], it.[IsActive], it.[Description]" 
    Where="it.IsActive" OrderBy="it.Description">
</cc11:ClassEntityDataSource>

    




<asp:Label ID="LabelLocation" runat="server" Text=" / lokatie " Visible="False"></asp:Label>


    




