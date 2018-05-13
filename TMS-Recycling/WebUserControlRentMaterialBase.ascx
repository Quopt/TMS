﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlRentMaterialBase.ascx.cs" Inherits="TMS_Recycling.WebUserControlRentMaterialBase" %>
<%@ Register src="URLPopUpControl.ascx" tagname="URLPopUpControl" tagprefix="uc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="ComboBoxLocation.ascx" tagname="ComboBoxLocation" tagprefix="uc2" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<table width="100%">
<tr>
 <td colspan="2"><asp:Label ID="LabelBasisgegevens" runat="server" 
         Text="Basisgegevens materiaal" CssClass="SubMenuHeader"></asp:Label></td>
 <td>    <asp:Label ID="LabelRelationNumber" runat="server" Text="Materiaalnummer"></asp:Label> </td>
 <td>   <asp:Label ID="Label_ItemNumber_Text" runat="server"></asp:Label> </td>
</tr>
<tr>
<td>
    <asp:Label ID="LabelName" runat="server" Text="Naam"></asp:Label> </td>
    <td width="300px"> <asp:TextBox ID="TextBox_Description_Text" runat="server" Width="90%" 
            MaxLength="250" ></asp:TextBox>
    </td>
    <td><asp:Label ID="LabelIsActive" runat="server" Text="Actief"></asp:Label> </td>
        <td>
            <asp:CheckBox ID="CheckBox_IsActive_Checked" Text="" runat="server" Checked="True" /></td>
</tr>

<tr>
<td>
    <asp:Label ID="LabelName0" runat="server" Text="Materiaalstatus"></asp:Label> 
</td>
<td>
    <cc11:ClassComboBox ID="ComboBox_ItemState_SelectedValue" runat="server">
    </cc11:ClassComboBox>
</td>
<td>
    <asp:Label ID="LabelLocation" runat="server" Text="Lokatie"></asp:Label> 
</td>
<td>
    <cc1:ClassComboBoxLocation ID="ComboBoxLocation_Location" runat="server" 
        DataSourceID="EntityDataSourceLocations" DataTextField="Description" 
        DataValueField="Id">
    </cc1:ClassComboBoxLocation>
</td>
</tr>

<tr>
<td>
    <asp:Label ID="Label1" runat="server" Text="Basis verhuurprijs"></asp:Label> 
</td>
<td>
    <asp:TextBox ID="TextBox_BaseRentalPrice" runat="server"></asp:TextBox>

</td>
<td>
    <asp:Label ID="Label16" runat="server" Text="Borg"></asp:Label> 
</td>
<td>
    <asp:TextBox ID="TextBox_BailPrice" runat="server"></asp:TextBox>

</td>
</tr>

<tr>
<td>
    <asp:Label ID="Label2" runat="server" Text="Huur per dag"></asp:Label> 
</td>
<td>
    <asp:TextBox ID="TextBox_RentPerDay" runat="server"></asp:TextBox>

</td>
<td>
</td>
<td>
</td>
</tr>

<tr>
<td>
    <asp:Label ID="Label3" runat="server" Text="Huur per week"></asp:Label> 
</td>
<td>
    <asp:TextBox ID="TextBox_RentPerWeek" runat="server"></asp:TextBox>

</td>
<td>
</td>
<td>
</td>
</tr>

<tr>
<td>
    <asp:Label ID="Label4" runat="server" Text="Huur per maand"></asp:Label> 
</td>
<td>
    <asp:TextBox ID="TextBox_RentPerMonth" runat="server"></asp:TextBox>

</td>
<td>
</td>
<td>
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
<asp:Button ID="ButtonDelete" runat="server" onclick="ButtonDelete_Click" OnClientClick="return confirm('Weet u het zeker dat u dit wilt?');"
    Text="Materiaal verwijderen" />
</nobr>
</td>
<td style="width:90%;text-align:right;">
    <uc1:URLPopUpControl ID="URLPopUpControlLink" runat="server" 
        Text="Toon gekoppelde verhuringen" />
</td></tr>
</table>


    





<cc11:ClassEntityDataSource ID="EntityDataSourceLocations" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EntitySetName="LocationSet" 
    Select="it.[Id], it.[Description], it.[IsActive]" OrderBy="it.Description" 
    Where="it.IsActive">
</cc11:ClassEntityDataSource>




    





