<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlCustomerRelationContract.ascx.cs" Inherits="TMS_Recycling.WebUserControlCustomerRelationContract" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit.HTMLEditor" tagprefix="cc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="CalendarControl.ascx" tagname="CalendarControl" tagprefix="uc1" %>
<%@ Register src="URLPopUpControl.ascx" tagname="URLPopUpControl" tagprefix="uc2" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<table width="100%">
<tr>
 <td colspan="4">
     <asp:Label ID="LabelBasisgegevens" runat="server" 
         Text="Basisgegevens contractovereenkomst" CssClass="SubMenuHeader"></asp:Label></td>
 <td>    &nbsp;</td>
 <td>   &nbsp;</td>
</tr>

<tr>
<td>
    <asp:Label ID="LabelName" runat="server" Text="Naam"></asp:Label> </td>
    <td colspan="3" width="300px"> <asp:TextBox ID="TextBox_Description_Text" runat="server" Width="90%" 
            MaxLength="250" ></asp:TextBox>
    </td>
    <td><asp:Label ID="LabelHasContractGuidance" runat="server" Text="Contractbegeleiding actief"></asp:Label> </td>
        <td valign="top">
            <asp:CheckBox ID="CheckBox_HasContractGuidance_Checked" Text="" runat="server" Checked="True" Width="50px" /><br />
            <asp:Label ID="LabelContractGuidanceFeedback" runat="server" Text="..." 
                Visible="False"></asp:Label>
    </td>
</tr>


<tr>
<td>
    <asp:Label ID="LabelStartDate" runat="server" Text="Start datum contract"></asp:Label>
    </td>
<td>
    <uc1:CalendarControl ID="CalendarControl_ContractStartDate_SelectedDate" runat="server" />
    </td>
<td>
    <asp:Label ID="LabelEndDate" runat="server" Text="Eind datum contract"></asp:Label>
</td>
<td>
    <uc1:CalendarControl ID="CalendarControl_ContractEndDate_SelectedDate" runat="server" />
</td>
<td>
    <asp:Label ID="LabelContractDate" runat="server" Text="Contract datum"></asp:Label>
<td>
    <uc1:CalendarControl ID="CalendarControl_ContractDate_SelectedDate" runat="server" />
</td>
</tr>

<tr>
<td>
    <asp:Label ID="LabelContractType" runat="server" Text="Contract type"></asp:Label>
</td>
<td>
    <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" ID="DropDownList_ContractType_SelectedValue" runat="server">
    </cc11:ClassComboBox>
</td>
<td>
    <asp:Label ID="LabelPrio" runat="server" Text="Contractprioriteit"></asp:Label>
</td>
<td>
    <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" ID="DropDownList_ContractPriority_SelectedValue" runat="server">
        <asp:ListItem Value="1">1 (zeer laag)</asp:ListItem>
        <asp:ListItem Value="2"></asp:ListItem>
        <asp:ListItem Value="3"></asp:ListItem>
        <asp:ListItem Value="4"></asp:ListItem>
        <asp:ListItem Value="5"></asp:ListItem>
        <asp:ListItem Value="6"></asp:ListItem>
        <asp:ListItem Value="7"></asp:ListItem>
        <asp:ListItem Value="8"></asp:ListItem>
        <asp:ListItem Value="9">9 (zeer hoog)</asp:ListItem>
    </cc11:ClassComboBox>
</td>
<td>
    <asp:Label ID="LabelContractStatus" runat="server" Text="Contract status"></asp:Label>
</td>
<td>
    
    <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" ID="DropDownList_ContractStatus_SelectedValue" 
        runat="server">
    </cc11:ClassComboBox>
    
</td>
</tr>

<tr>
<td>
    <asp:Label ID="LabelYourReference" runat="server" Text="Uw referentie"></asp:Label>
</td>
<td>
<asp:TextBox ID="TextBox_YourReference" runat="server"></asp:TextBox>
</td>
<td>
    <asp:Label ID="LabelOurReference" runat="server" Text="Onze referentie"></asp:Label>
</td>
<td>
<asp:TextBox ID="TextBox_OurReference" runat="server" ReadOnly="True"></asp:TextBox>
</td>
<td>
</td>
<td>
</td>
</tr>

<tr>
<td>

    <asp:Label ID="LabelPaymentConditions" runat="server" Text="Betalingscondities"></asp:Label>

</td>
<td colspan="2">
<asp:TextBox ID="TextBox_PaymentConditions" runat="server" Rows="3" 
        TextMode="MultiLine" Width="90%"></asp:TextBox>
</td>
<td>
    <asp:Label ID="LabelDeliveryConditions" runat="server" Text="Levercondities"></asp:Label>
</td>
<td colspan="2">
<asp:TextBox ID="TextBox_DeliveryConditions" runat="server" Rows="3" 
        TextMode="MultiLine" Width="90%"></asp:TextBox>
</td>

</tr>

<tr>
    <td><asp:Label ID="Label15" runat="server" Text="Opmerkingen"></asp:Label> </td>
    <td colspan="6"> 
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
    Text="Overeenkomst verwijderen" />

</nobr>
</td>
<td style="width:90%;text-align:right;">
    <uc2:URLPopUpControl ID="URLPopUpControlShowContract" runat="server" 
        Text="Toon contract" />
    <uc2:URLPopUpControl ID="URLPopUpControlContractGuidance" runat="server" 
        Text="Toon contractbegeleidingsorders" Visible="False" />
    <uc2:URLPopUpControl ID="URLPopUpControlLink" runat="server" 
        Text="Toon gekoppelde orders" />
</td></tr>
</table>




    




