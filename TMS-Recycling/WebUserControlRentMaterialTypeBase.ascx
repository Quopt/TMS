﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlRentMaterialTypeBase.ascx.cs" Inherits="TMS_Recycling.WebUserControlRentMaterialTypeBase" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="URLPopUpControl.ascx" tagname="URLPopUpControl" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<table width="100%">
<tr>
 <td colspan="4"><asp:Label ID="LabelBasisgegevens" runat="server" 
         Text="Basisgegevens materiaalsoort" CssClass="SubMenuHeader"></asp:Label></td>
 <td>    &nbsp;</td>
 <td>   &nbsp;</td>
</tr>
<tr>
<td>
    <asp:Label ID="LabelName" runat="server" Text="Naam"></asp:Label> </td>
    <td colspan="3" width="300px"> <asp:TextBox ID="TextBox_Description_Text" runat="server" Width="90%" 
            MaxLength="250" ></asp:TextBox>
    </td>
    <td><asp:Label ID="LabelIsActive" runat="server" Text="Actief"></asp:Label> </td>
        <td>
            <asp:CheckBox ID="CheckBox_IsActive_Checked" Text="" runat="server" Checked="True" /></td>
</tr>

<tr>
<td>
    <asp:Label ID="LabelLedgerBookingCode" runat="server" Text="Boekingscode materiaal"></asp:Label> 
</td>
<td>
    <cc11:ClassComboBox ID="ComboBox_LedgerBookingCode" runat="server" AutoCompleteMode="SuggestAppend" 
        DropDownStyle="DropDownList" 
        DataSourceID="EntityDataSourceLedgerBookingCodes" DataTextField="Description" 
        DataValueField="Id">
    </cc11:ClassComboBox>
</td>
<td>
</td>
<td>
</td>
<td>
</td>
<td>
</td>
</tr>


<tr>
    <td><asp:Label ID="Label15" runat="server" Text="Contractvoorwaarden"></asp:Label> </td>
    <td colspan="5"> 

        <asp:HtmlEditorExtender ID="Editor_RentalConditions_Content" runat="server" />

    </td>
</tr>

<tr>
<td>
  <asp:Label ID="LabelAlternativeMaterialTypes" runat="server" Text="Alternatieve materiaaltypes"></asp:Label> 
</td>
<td colspan="5">
  <div style="height:100px; overflow:scroll;">
    <asp:CheckBoxList ID="CheckBoxListAlternativeMaterialTypes" runat="server" 
        RepeatColumns="4" RepeatDirection="Horizontal" Width="90%">
    </asp:CheckBoxList>
   </div>
</td>
</tr>

<tr>
    <td><asp:Label ID="Label1" runat="server" Text="Opmerkingen"></asp:Label> </td>
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
    Text="Materiaalsoort verwijderen" />
</nobr>
</td>
<td style="width:90%;text-align:right;">
    &nbsp;</td></tr>
</table>


    <cc11:ClassEntityDataSource ID="EntityDataSourceLedgerBookingCodes" 
    runat="server" ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EnableFlattening="False" 
    EntitySetName="LedgerBookingCodeSet" OrderBy="it.Description" 
    Select="it.[Id], it.[Description], it.[IsActive]" Where="it.IsActive">
</cc11:ClassEntityDataSource>



    




    
