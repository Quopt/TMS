﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlFreightBase.ascx.cs" Inherits="TMS_Recycling.WebUserControlFreightBase" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit.HTMLEditor" tagprefix="cc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="CalendarWithTimeControl.ascx" tagname="CalendarWithTimeControl" tagprefix="uc1" %>
<%@ Register src="URLPopUpControl.ascx" tagname="URLPopUpControl" tagprefix="uc2" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<table width="100%">
<tr>
 <td colspan="4"><asp:Label ID="LabelBasisgegevens" runat="server" Text="Basisgegevens vracht" CssClass="SubMenuHeader"></asp:Label></td>
 <td>    <asp:Label ID="LabelFreightNumber" runat="server" Text="Vrachtnummer"></asp:Label> <br /> <asp:Label ID="LabelTotalWeight" runat="server" Text="Totaal gewicht vracht"></asp:Label> </td>
 <td>   <asp:Label ID="Label_OurReference_Text" runat="server"></asp:Label> <br /> <asp:Label ID="Label_TotalNetWeight" runat="server"></asp:Label> </td>
</tr>
<tr>
<td>
    <asp:Label ID="LabelDescription" runat="server" Text="Omschrijving"></asp:Label> </td>
    <td colspan="3"> <asp:TextBox ID="TextBox_Description_Text" runat="server" Width="90%" 
            MaxLength="250" ></asp:TextBox>
    </td>
    <td><asp:Label ID="LabelIsActive" runat="server" Text="Actief"></asp:Label> </td>
        <td>
            <asp:CheckBox ID="CheckBox_IsActive_Checked" Text="" runat="server" Checked="True" /></td>
</tr>
<tr>
<td>
    <asp:Label ID="LabelRelation" runat="server" Text="Relatie"></asp:Label> 
</td>
<td colspan="2">
        <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" 
        ID="DropDownList_FromRelation" runat="server" DataSourceID="EntityDataSourceRelations" 
            DataTextField="Description" DataValueField="Id" Width="300px">
        </cc11:ClassComboBox>
</td>
<td></td>
<td></td>
<td></td>
</tr>
<tr>
    <td><asp:Label ID="LabelFreightDateTime" runat="server" Text="Vrachtdatum/-tijd"></asp:Label> </td>
    <td> 
        <uc1:CalendarWithTimeControl ID="CalendarWithTimeControl1_FreightDateTime_SelectedDateTime" runat="server" />
    </td>
    <td><asp:Label ID="LabelFreightType" runat="server" Text="Vrachttype"></asp:Label> </td>
    <td> 
        <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" 
            ID="DropDownList_FreightType_SelectedValue" runat="server">
        </cc11:ClassComboBox>
    </td>
    <td><asp:Label ID="LabelYourReference" runat="server" Text="Klantreferentie"></asp:Label> </td>
    <td> 
        <asp:TextBox ID="TextBox_YourReference" runat="server" Width="200px" 
            MaxLength="40" ></asp:TextBox>
    </td>

</tr>
<tr>
    <td><asp:Label ID="LabelFreightStatus" runat="server" Text="Status van de vracht"></asp:Label> </td>
    <td> 
        <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" ID="DropDownList_FreightStatus_SelectedValue" runat="server">
        </cc11:ClassComboBox>
    </td>
    <td>
      <asp:Label ID="LabelFreightStartDateTime" runat="server" Text="Start datum/tijd vracht"></asp:Label> </td>
    <td> 
        <uc1:CalendarWithTimeControl ID="CalendarWithTimeControl1_RequestedFreightStartDateTime_SelectedDateTime0" 
            runat="server" />
    </td>
    <td>
      <asp:Label ID="LabelFreightEndDateTime" runat="server" 
            Text="Eind datum/tijd vracht"></asp:Label> </td>
    <td> 
        <uc1:CalendarWithTimeControl ID="CalendarWithTimeControl1_RequestedFreightEndDateTime_SelectedDateTime1" 
            runat="server" />
    </td></tr>

<tr>
<td>
    <asp:Label ID="LabelFreightDirection" runat="server" Text="Vracht richting"></asp:Label> 
</td>
<td>
        <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" 
        ID="DropDownList_FreightDirection_SelectedValue" runat="server" 
            Enabled="False">
        </cc11:ClassComboBox>
</td>
<td>
    <asp:Label ID="LabelFreighID" runat="server" Text="Vracht id"></asp:Label> 
</td>
<td>
        <asp:TextBox ID="TextBox_YourDriverName" runat="server" Width="200px" 
            MaxLength="40" ></asp:TextBox>
</td>
<td>
    <asp:Label ID="LabelFreightPlate" runat="server" Text="Vracht kenteken"></asp:Label> 
</td>
<td>
        <asp:TextBox ID="TextBox_YourTruckPlate" runat="server" Width="200px" 
            MaxLength="40" ></asp:TextBox>
</td>
</tr>

<tr>
<td>
    <asp:Label ID="LabelFreightOurTruck" runat="server" Text="Onze vrachtwagen"></asp:Label> 
</td>
<td>
        <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" 
        ID="DropDownList_OurTruck" runat="server" 
            AppendDataBoundItems="True" DataSourceID="EntityDataSourceTrucks" 
            DataTextField="Description" DataValueField="Id">
            <asp:ListItem Selected="True" Value="">-Nvt-</asp:ListItem>
        </cc11:ClassComboBox>
</td>
<td>
    <asp:Label ID="LabelFreighOurDriver" runat="server" Text="Onze chauffeur"></asp:Label> 
</td>
<td>
        <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" 
        ID="DropDownList_OurDriverID" runat="server" 
            AppendDataBoundItems="True" DataSourceID="EntityDataSourceDrivers" 
            DataTextField="Description" DataValueField="Id">
        <asp:ListItem Selected="True" Value="">-Nvt-</asp:ListItem>
        </cc11:ClassComboBox>
</td>
<td>
</td>
<td>
</td>
</tr>

<tr>
    <td><asp:Label ID="Label15" runat="server" Text="Opmerkingen"></asp:Label> </td>
    <td colspan="4"> 
        <asp:TextBox ID="TextBox_Comments" runat="server" Width="90%" 
            MaxLength="2000" Rows="4" TextMode="MultiLine" ></asp:TextBox>
    </td>
    <td>

        <uc2:URLPopUpControl ID="URLPopUpControlWeighing" runat="server" 
            Text="Toon weegbon" OnBeforePopUpOpened="ButtonSave_Click" /><br />
        <uc2:URLPopUpControl ID="URLPopUpControlSorting" runat="server" 
            Text="Toon sorteerbon" OnBeforePopUpOpened="ButtonSave_Click" /><br />
        <uc2:URLPopUpControl ID="URLPopUpControlLegalDocuments" runat="server" 
            Text="Transportdocumenten" OnBeforePopUpOpened="ButtonSave_Click" />

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
<asp:Button ID="ButtonDelete" runat="server" onclick="ButtonDelete_Click" OnClientClick="return confirm('Weet u het zeker dat u dit wilt?');"
    Text="Weging verwijderen" />
</td>
<td style="text-align:right;">
    <uc2:URLPopUpControl ID="URLPopUpControlSort" runat="server" 
        Text="Sorteerbon (nogmaals) invullen" OnBeforePopUpOpened="ButtonSave_Click" OnPopUpClosed="GeneralOnPopupClosed" />
    <uc2:URLPopUpControl ID="URLPopUpControlInvoice" runat="server" 
        Text="Vracht (nogmaals) factureren" OnBeforePopUpOpened="ButtonSave_Click" OnPopUpClosed="GeneralOnPopupClosed" />
</td>
</tr></table>

<cc11:ClassEntityDataSource ID="EntityDataSourceTrucks" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EntitySetName="TruckSet" 
    Select="it.[Id], it.[Description], it.[IsActive]" OrderBy="it.Description" 
    Where="it.IsActive">
</cc11:ClassEntityDataSource>
<cc11:ClassEntityDataSource ID="EntityDataSourceDrivers" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EntitySetName="StaffMemberSet" 
    OrderBy="it.Description" 
    Select="it.[Id], it.[Description], it.[IsDriver], it.[IsActive]" 
    Where="it.IsActive and it.IsDriver">
</cc11:ClassEntityDataSource>

<cc11:ClassEntityDataSource ID="EntityDataSourceRelations" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EnableFlattening="False" 
    EntitySetName="RelationSet" OrderBy="it.Description" 
    Select="it.[Id], it.[Description], it.[IsActive]" Where="it.IsActive">
</cc11:ClassEntityDataSource>




    




