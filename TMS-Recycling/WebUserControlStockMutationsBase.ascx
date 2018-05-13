<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlStockMutationsBase.ascx.cs" Inherits="TMS_Recycling.WebUserControlStockMutationsBase" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc1" %>
<%@ Register src="URLPopUpControl.ascx" tagname="URLPopUpControl" tagprefix="uc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="ComboBoxLocation.ascx" tagname="ComboBoxLocation" tagprefix="uc2" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<table width="100%">
<tr>
 <td colspan="2"><asp:Label ID="LabelBasisgegevens" runat="server" 
         Text="Basisgegevens mutatie" CssClass="SubMenuHeader"></asp:Label>&nbsp;<asp:Label 
         ID="Label17" runat="server" Text="("></asp:Label>
     <asp:Label ID="LabelMutationType_MutationType" runat="server" Text="Label"></asp:Label>
     <asp:Label ID="Label19" runat="server" Text=")"></asp:Label>
    </td>
 <td>    <asp:Label ID="LabelRelationNumber" runat="server" Text="Mutatienummer"></asp:Label> </td>
 <td>   <asp:Label ID="Label_MutationNumber_Text" runat="server"></asp:Label> </td>
</tr>
<tr>
<td>
    <asp:Label ID="LabelDescription" runat="server" Text="Omschrijving"></asp:Label> </td>
    <td width="300px"> 
        <asp:TextBox ID="TextBox_Description_Text" runat="server" Width="90%" 
            MaxLength="250" Enabled="False" ></asp:TextBox>
    </td>
    <td><asp:Label ID="LabelIsActive" runat="server" Text="Gecorrigeerd"></asp:Label> </td>
        <td>
            <asp:CheckBox ID="CheckBox_IsCorrection_Checked" Text="" runat="server" 
                Checked="True" Enabled="False" /></td>
</tr>

<tr>
<td>
    <asp:Label ID="LabelMutation" runat="server" Text="Mutatie datum/tijd"></asp:Label> 
</td>
<td>
    <asp:TextBox ID="TextBox_MutationDateTime" runat="server" Enabled="False"></asp:TextBox>

</td>
<td>
    <asp:Label ID="LabelAmountKg" runat="server" Text="Hoeveelheid (kg)"></asp:Label> 
</td>
<td>
    <asp:TextBox ID="TextBox_AmountInKg" runat="server" Enabled="False"></asp:TextBox>

</td>
</tr>

<tr>
<td>
    <asp:Label ID="LabelAmount" runat="server" Text="Bedrag"></asp:Label> 
</td>
<td>
    <asp:TextBox ID="TextBox_TotalPrice" runat="server" Enabled="False"></asp:TextBox>

</td>
<td>
    <asp:Label ID="Label16" runat="server" Text="Hoeveelheid"></asp:Label> 
</td>
<td>
    <asp:TextBox ID="TextBox_Amount" runat="server" Enabled="False"></asp:TextBox>

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
    Text="Mutatie verwijderen" Visible="False" />
</nobr>
</td>
<td style="width:90%;text-align:right;">
    <uc1:URLPopUpControl ID="URLPopUpControlLink" runat="server" 
        Text="Toon gekoppelde order" />
</td></tr>
</table>


    





<cc11:ClassEntityDataSource ID="EntityDataSourceLocations" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EntitySetName="LocationSet" 
    Select="it.[Id], it.[Description], it.[IsActive]" OrderBy="it.Description" 
    Where="it.IsActive">
</cc11:ClassEntityDataSource>




    






