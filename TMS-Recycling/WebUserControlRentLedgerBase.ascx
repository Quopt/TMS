<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlRentLedgerBase.ascx.cs" Inherits="TMS_Recycling.WebUserControlRentLedgerBase" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="CalendarWithTimeControl.ascx" tagname="CalendarControl" tagprefix="uc1" %>
<%@ Register src="URLPopUpControl.ascx" tagname="URLPopUpControl" tagprefix="uc2" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<table width="100%">
<tr>
 <td colspan="2"><asp:Label ID="LabelBasicData" runat="server" 
         Text="Basisgegevens verhuurboek" CssClass="SubMenuHeader"></asp:Label></td>
 <td>    <asp:Label ID="LabelAdvancePayment" runat="server" 
         Text="Behandelen als voorschot" ></asp:Label> </td>
 <td>    
            <asp:CheckBox ID="CheckBox_IsTreatedAsAdvancePayment_Checked" Text="" 
                runat="server" /> </td>
 <td>    &nbsp;</td>
 <td>   
            &nbsp;</td>
</tr>

<tr>
<td>
    <asp:Label ID="LabelName" runat="server" Text="Beschrijving"></asp:Label> </td>
    <td> <asp:TextBox ID="TextBox_Description_Text" runat="server" Width="200px" 
            MaxLength="250" ></asp:TextBox>
    </td>
    <td><asp:Label ID="LabelLedgerStatus" runat="server" Text="Status boeking"></asp:Label> </td>
        <td>
        <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" ID="DropDownList_InvoiceStatus_SelectedValue" runat="server" 
                Width="89px">
        </cc11:ClassComboBox> 
        </td>
    <td>&nbsp;</td>
        <td>
            &nbsp;</td>
</tr>

<tr>
    <td><asp:Label ID="LabelStartRentPeriod" runat="server" Text="Start verhuurperiode"></asp:Label> </td>
    <td>
        <uc1:CalendarControl ID="CalendarControl_RentStartDateTime_SelectedDateTime" runat="server" />
    </td>
    <td><asp:Label ID="LabelEndRentPeriod" runat="server" Text="Einde verhuurperiode"></asp:Label> </td>
    <td> 
        <uc1:CalendarControl ID="CalendarControl_RentEndStartDateTime_SelectedDateTime" runat="server" />
    </td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
</tr>

<tr>
    <td><asp:Label ID="LabelBaseRentPrice" runat="server" Text="Basishuurprijs"></asp:Label> </td>
    <td>
      <asp:TextBox ID="TextBox_CalculatedRentPrice" runat="server" ></asp:TextBox>     
     <nobr>
        <asp:Button ID="ButtonRecalc" runat="server" onclick="ButtonRecalc_Click" 
            Text="Herberekenen" /><br />
        <asp:CheckBox ID="CheckBoxChangeInvoiceLine" Text="Factuurregel aanpassen" runat="server" Checked="True" />
        <asp:CheckBox ID="CheckBoxBasedOnOfficialRent" Text="Gebaseerd op gepubliceerde verhuurprijzen" runat="server" Checked="True" />
     </nobr>
    </td>
    <td><asp:Label ID="LabelDiscountPercentage" runat="server" Text="Kortingspercentage"></asp:Label> </td>
    <td><asp:TextBox ID="TextBox_DiscountPercentage" runat="server" ></asp:TextBox>     </td>
    <td> </td>
    <td> </td>
</tr>

<tr>
<td><asp:Label ID="LabelCalculatedRentPrice" runat="server" Text="Huurprijs"></asp:Label></td>
<td><asp:Label ID="Label_BaseRentPrice" runat="server" Text=" "></asp:Label></td>
<td><asp:Label ID="LabelVAT" runat="server" Text="BTW"></asp:Label></td>
<td><asp:Label ID="Label_VATRentPrice" runat="server" Text=" "></asp:Label></td>
<td><asp:Label ID="LabelTotalRentPrice" runat="server" Text="Totaal huurprijs"></asp:Label></td>
<td><asp:Label ID="Label_TotalRentPrice" runat="server" Text=" "></asp:Label></td>
</tr>

<tr>
    <td><asp:Label ID="LabelComments" runat="server" Text="Opmerkingen"></asp:Label> </td>
    <td colspan=5> 
        <asp:TextBox ID="TextBox_Comments" runat="server" Width="95%" 
            MaxLength="2000" Rows="4" TextMode="MultiLine" ></asp:TextBox>
    </td>

</tr>
</table>

<table width="100%">
<tr>
<td>
<asp:Button ID="ButtonCancel" runat="server" Text="Wijzigingen annuleren" 
    onclick="ButtonCancel_Click" />
<asp:Button ID="ButtonSave" runat="server" Text="Wijzigingen opslaan" 
    onclick="ButtonSave_Click" />
<asp:Button ID="ButtonDelete" runat="server" Text="Boeking verwijderen" 
    onclick="ButtonDelete_Click" />
</td>
<td align="right">
    <uc2:URLPopUpControl ID="URLPopUpControl_RentInvoice" runat="server" 
        Text="Open huur verzamelfactuur" 
        URLToPopup="WebFormPopup.aspx?uc=InvoiceBase&Id=" />
</td>
</tr></table>


<cc11:ClassEntityDataSource ID="EntityDataSourceLocations" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EnableFlattening="False" 
    EntitySetName="LocationSet" Select="distinct it.[Description], it.[Id], it.IsActive" 
    Where="it.IsActive = true">
</cc11:ClassEntityDataSource>


