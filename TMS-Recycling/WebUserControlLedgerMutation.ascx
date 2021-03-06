﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlLedgerMutation.ascx.cs" Inherits="TMS_Recycling.WebUserControlLedgerMutation" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="URLPopUpControl.ascx" tagname="URLPopUpControl" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<table width="100%" >
<tr>
 <td colspan="2">
     <asp:Label ID="LabelBasicData" runat="server" 
         Text="Basisgegevens dagboekmutatie" CssClass="SubMenuHeader"></asp:Label></td>
 <td>    &nbsp;</td>
 <td>    
        &nbsp;</td>
 <td>    
     <br />
    </td>
 <td>   
            <asp:CheckBox ID="CheckBox_IsEditable_Checked" Text="Bewerkbaar" runat="server" 
                Checked="True" Enabled="False" /> 
            <br />
            <asp:CheckBox ID="CheckBox_IsCorrection_Checked" Text="Is correctie" runat="server" 
                Checked="True" Enabled="False" /> </td>
</tr>

<tr>
<td>
    <asp:Label ID="LabelName" runat="server" Text="Mutatiebeschrijving"></asp:Label> </td>
    <td> 
        <asp:TextBox ID="TextBox_Description_Text" runat="server" Width="200px" 
            MaxLength="250" ReadOnly="True" ></asp:TextBox>
    </td>
    <td><asp:Label ID="LabelLedgerBookingType" runat="server" Text="Boekingstype"></asp:Label> </td>
        <td>
        <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" ID="DropDownList_BookingType_SelectedValue" runat="server" 
                Width="89px" Enabled="False">
        </cc11:ClassComboBox> 
        </td>
    <td><asp:Label ID="LabelLedgerType0" runat="server" Text="Boekdatum/tijd"></asp:Label> </td>
        <td>
            <asp:TextBox ID="TextBox_BookingDateTime" runat="server" Width="150px" 
                ReadOnly="True"></asp:TextBox> </td>
</tr>

<tr>
    <td><asp:Label ID="LabelBank" runat="server" Text="Bedrag"></asp:Label> </td>
    <td><asp:TextBox ID="TextBox_AmountEXVat" runat="server" Width="90%" MaxLength="40" 
            ReadOnly="True" ></asp:TextBox></td>
    <td><asp:Label ID="LabelBankAccount" runat="server" Text="BTW"></asp:Label> </td>
    <td> 
        <asp:TextBox ID="TextBox_VATAmount" runat="server" Width="90%" 
            MaxLength="40" ReadOnly="True" ></asp:TextBox> </td>
    <td><asp:Label ID="LabelBankIbanBic" runat="server" Text="Totaal bedrag incl BTW"></asp:Label> </td>
    <td><asp:TextBox ID="TextBox_TotalAmount" runat="server" Width="150px" 
            ReadOnly="True"></asp:TextBox></td>
</tr>


<tr>
    <td><asp:Label ID="LabelComments" runat="server" Text="Opmerkingen"></asp:Label> </td>
    <td colspan=5> 
        <asp:TextBox ID="TextBox_Comments" runat="server" Width="95%" 
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
<asp:Button ID="ButtonDelete" runat="server" Text="Boeking verwijderen" OnClientClick="return confirm('Weet u het zeker dat u dit wilt?');"
    onclick="ButtonDelete_Click" Enabled="False" />
</nobr>
</td>
<td style="width:90%;text-align:right;">
    <uc1:URLPopUpControl ID="URLPopUpControlLink" runat="server" 
        Text="Toon gekoppeld object" Visible="False" />
</td></tr>
</table>

<cc11:ClassEntityDataSource ID="EntityDataSourceLocations" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EnableFlattening="False" 
    EntitySetName="LocationSet" Select="it.[Description], it.[Id], it.IsActive" 
    Where="it.IsActive = true">
</cc11:ClassEntityDataSource>


