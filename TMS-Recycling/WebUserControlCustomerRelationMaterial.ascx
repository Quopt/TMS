﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlCustomerRelationMaterial.ascx.cs" Inherits="TMS_Recycling.WebUserControlCustomerRelationMaterial" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<table width="100%">
<tr>
 <td colspan="4">
     <asp:Label ID="LabelBasisgegevens" runat="server" 
         Text="Basisgegevens lokatie" CssClass="SubMenuHeader"></asp:Label></td>
 <td>    &nbsp;</td>
 <td>   &nbsp;</td>
</tr>
<tr>
<td>
    <asp:Label ID="LabelName" runat="server" Text="Naam"></asp:Label> </td>
    <td width="300px"> <asp:TextBox ID="TextBox_Description_Text" runat="server" Width="90%" 
            MaxLength="250" ></asp:TextBox>
    </td>
    <td><asp:Label ID="LabelLMECode" runat="server" Text="Afvalstroomnummer"></asp:Label> </td>
        <td>
            <asp:TextBox ID="TextBox_LMECode" runat="server" Width="150px" 
            MaxLength="40" ></asp:TextBox>
    </td>
    <td><asp:Label ID="LabelMaterial" runat="server" Text="Linked to material"></asp:Label> </td>
        <td>
            <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" ID="DropDownList_Material" runat="server" 
                DataSourceID="EntityDataSourceMaterials" DataTextField="Description" 
                DataValueField="Id">
            </cc11:ClassComboBox>
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
<asp:Button ID="ButtonCancel" runat="server" Text="Wijzigingen annuleren" 
    onclick="ButtonCancel_Click" />
<asp:Button ID="ButtonSave" runat="server" Text="Wijzigingen opslaan" 
    onclick="ButtonSave_Click" />
<asp:Button ID="ButtonDelete" runat="server" onclick="ButtonDelete_Click" OnClientClick="return confirm('Weet u het zeker dat u dit wilt?');"
    Text="Materiaal verwijderen" />


    




<cc11:ClassEntityDataSource ID="EntityDataSourceMaterials" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EntitySetName="MaterialSet" 
    Select="it.[Id], it.[Description], it.[IsActive]" Where="it.IsActive = true">
</cc11:ClassEntityDataSource>



    




