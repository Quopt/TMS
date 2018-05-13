<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlRentVATBase.ascx.cs" Inherits="TMS_Recycling.WebUserControlRentVATBase" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit.HTMLEditor" tagprefix="cc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="URLPopUpControl.ascx" tagname="URLPopUpControl" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc2" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<table width="100%">
<tr>
 <td colspan="2"><asp:Label ID="LabelBasisgegevens" runat="server" 
         Text="Basisgegevens materiaalsoort" CssClass="SubMenuHeader"></asp:Label></td>
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
    <td><asp:Label ID="Label15" runat="server" Text="BTW Tarief"></asp:Label> </td>
    <td> 

        <asp:TextBox ID="TextBox_VATPercentage" runat="server" 
            MaxLength="250" ></asp:TextBox>

    </td>
    <td>
        <asp:Label ID="LabelLocation" runat="server" Text="Lokatie voor dit BTW tarief"></asp:Label>
    </td>
    <td>
        <cc2:ClassComboBoxLocation ID="ComboBox_Location" runat="server" 
            DataSourceID="EntityDataSourceLocations" DataTextField="Description" 
            DataValueField="Id">
        </cc2:ClassComboBoxLocation>
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
<asp:Button ID="ButtonDelete" runat="server" onclick="ButtonDelete_Click" 
    Text="BTW niveau verwijderen" />
</nobr>
</td>
<td style="width:90%;text-align:right;">
    &nbsp;</td></tr>
</table>


    <cc11:ClassEntityDataSource ID="EntityDataSourceLocations" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EntitySetName="LocationSet" 
    OrderBy="it.Description" Select="it.[Id], it.[Description], it.[IsActive]" 
    Where="it.IsActive">
</cc11:ClassEntityDataSource>



    
