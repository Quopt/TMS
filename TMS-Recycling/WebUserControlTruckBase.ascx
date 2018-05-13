<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlTruckBase.ascx.cs" Inherits="TMS_Recycling.WebUserControlTruckBase" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>
<%@ Register src="URLPopUpControl.ascx" tagname="URLPopUpControl" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc1" %>

<table width="100%">
<tr>
 <td colspan="2">
     <asp:Label ID="LabelBaseData" runat="server" 
         Text="Basisgegevens vrachtwagen" CssClass="SubMenuHeader"></asp:Label></td>
 <td>    &nbsp;</td>
 <td>   &nbsp;</td>
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
    <asp:Label ID="LabelPlate" runat="server" Text="Kenteken"></asp:Label> 
</td>
<td>
    <asp:TextBox ID="TextBox_TruckPlate" runat="server" Width="90%" 
            MaxLength="250" ></asp:TextBox>
</td>
<td>
</td>
<td>
</td>
</tr>

<tr>
<td>
    <asp:Label ID="LabelHomeLocation" runat="server" Text="Thuislokatie truck"></asp:Label> 
</td>
<td>

    <cc1:ClassComboBoxLocation ID="ClassComboBoxLocation_HomeTruckLocation" 
        runat="server" AutoCompleteMode="SuggestAppend" 
        DataSourceID="EntityDataSourceLocations" DataTextField="Description" 
        DataValueField="Id" DropDownStyle="DropDownList" MaxLength="0" 
        style="display: inline;">
    </cc1:ClassComboBoxLocation>

</td>
<td>
    <asp:Label ID="LabelCurrentLocation" runat="server" Text="Huidige lokatie truck"></asp:Label> 
</td>
<td>
    <cc1:ClassComboBoxLocation ID="ClassComboBoxLocation_CurrentTruckLocation" 
        runat="server" AutoCompleteMode="SuggestAppend" 
        DataSourceID="EntityDataSourceLocations" DataTextField="Description" 
        DataValueField="Id" DropDownStyle="DropDownList" MaxLength="0" 
        style="display: inline;">
    </cc1:ClassComboBoxLocation>
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
<asp:Button ID="ButtonDelete" runat="server" onclick="ButtonDelete_Click" 
    Text="Vrachtwagen verwijderen" />
</nobr>
</td>
<td style="width:90%;text-align:right;">
    &nbsp;</td></tr></table>


    






<cc11:ClassEntityDataSource ID="EntityDataSourceLocations" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EntitySetName="LocationSet" 
    Select="it.[Id], it.[Description], it.[IsActive]" OrderBy="it.Description" 
    Where="it.IsActive">
</cc11:ClassEntityDataSource>




    






