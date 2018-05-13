<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlStaffMember.ascx.cs" Inherits="TMS_Recycling.WebUserControlStaffMember" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit.HTMLEditor" tagprefix="cc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc2" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<table width="100%">
<tr>
 <td colspan="4"><asp:Label ID="LabelBasisgegevens" runat="server" Text="Basisgegevens" CssClass="SubMenuHeader"></asp:Label></td>
 <td>    <asp:Label ID="LabelStaffMemberNumber" runat="server" Text="Personeelsnummer"></asp:Label> </td>
 <td>   <asp:Label ID="Label_StaffMemberNumber_Text" runat="server" Text=""></asp:Label> </td>
</tr>
<tr>
<td>
    <asp:Label ID="LabelName" runat="server" Text="Naam"></asp:Label> </td>
    <td> <asp:TextBox ID="TextBox_Description_Text" runat="server" Width="200px" 
            MaxLength="250" ></asp:TextBox>
    </td>
    <td><asp:Label ID="LabelIsDriver" runat="server" Text="Is een truckchauffeur"></asp:Label> </td>
        <td>
            <asp:CheckBox ID="CheckBox_IsDriver_Checked" Text="" runat="server" Checked="True" /></td>
    <td><asp:Label ID="LabelIsActive" runat="server" Text="Actief"></asp:Label> </td>
        <td>
            <asp:CheckBox ID="CheckBox_IsActive_Checked" Text="" runat="server" Checked="True" /></td>
</tr>
<tr>
    <td><asp:Label ID="LabelContractHoursPerWeek" runat="server" Text="Contract uren per week"></asp:Label> </td>
    <td> 
        <asp:TextBox ID="TextBoxContractHours_ContractHoursPerWeek_Text" runat="server" 
            Width="200px" MaxLength="40" ></asp:TextBox>
    </td>
    <td><asp:Label ID="LabelInServiceDate" runat="server" Text="In dienst datum"></asp:Label> </td>
    <td> 
        <asp:TextBox ID="TextBox_InServiceDate_Text" runat="server" Width="200px" 
            MaxLength="40" ></asp:TextBox>
    </td>
    <td><asp:Label ID="LabelOutOfServiceDate" runat="server" Text="Uit dienst datum"></asp:Label> </td>
    <td> 
        <asp:TextBox ID="TextBox_OutOfServiceDate_Text" runat="server" Width="200px" 
            MaxLength="40" ></asp:TextBox>
    </td>

</tr>
<tr>
    <td><asp:Label ID="Label2" runat="server" Text="Netto uurtarief"></asp:Label> </td>
    <td> 
        <asp:TextBox ID="TextBox_NetHourlyRate_Text" runat="server" Width="200px" 
            MaxLength="40" ></asp:TextBox>
    </td>
    <td><asp:Label ID="Label3" runat="server" Text="SOFI nummer"></asp:Label> </td>
    <td> 
        <asp:TextBox ID="TextBox_SocialSecurityNumber_Text" runat="server" 
            Width="200px" MaxLength="40" ></asp:TextBox>
    </td>
    <td> </td>
    <td> </td>
</tr>
<tr>
    <td><asp:Label ID="Label4" runat="server" Text="Identificatie type"></asp:Label> </td>
    <td> 
        <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" ID="DropDownList_IDType_SelectedValue" runat="server">
        </cc11:ClassComboBox>
    </td>
    <td><asp:Label ID="Label5" runat="server" Text="Verloopdatum identificatie"></asp:Label> </td>
    <td> 
        <asp:TextBox ID="TextBox_IDExpirationDate" runat="server" Width="200px" 
            MaxLength="40" ></asp:TextBox>
    </td>
    <td><asp:Label ID="Label6" runat="server" Text="Nationaliteit identificatie"></asp:Label> </td>
    <td> 
        <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" ID="DropDownList_IDNationality_SelectedValue" runat="server">
        </cc11:ClassComboBox>
    </td></tr>
<tr>
    <td><asp:Label ID="Label7" runat="server" Text="Nummer identificatie"></asp:Label> </td>
    <td> 
        <asp:TextBox ID="TextBox_IDNumber" runat="server" Width="200px" MaxLength="40" ></asp:TextBox>
    </td>
    <td><asp:Label ID="Label8" runat="server" Text="Werkzaam op lokatie"></asp:Label> </td>
    <td> 
        <cc2:ClassComboBoxLocation AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" ID="DropDownListHomeLocation_HomeLocation" runat="server" 
            DataSourceID="EntityDataSourceLocations" DataTextField="Description" 
            DataValueField="Id">
        </cc2:ClassComboBoxLocation>
    </td>

</tr>
<tr>
    <td><asp:Label ID="Label13" runat="server" Text="Thuisadres"></asp:Label> </td>
    <td> 
        <asp:TextBox ID="TextBox_HomeAddress" runat="server" Width="200px" 
            MaxLength="2000" Rows="4" TextMode="MultiLine" ></asp:TextBox>
    </td>
    <td><asp:Label ID="Label14" runat="server" Text="Verblijfsadres (indien niet thuisadres)"></asp:Label> </td>
    <td> 
        <asp:TextBox ID="TextBox_LivingAddress" runat="server" Width="200px" 
            MaxLength="2000" Rows="4" TextMode="MultiLine" ></asp:TextBox>
    </td>
    <td><asp:Label ID="Label15" runat="server" Text="Opmerkingen"></asp:Label> </td>
    <td> 
        <asp:TextBox ID="TextBox_Comments" runat="server" Width="200px" 
            MaxLength="2000" Rows="4" TextMode="MultiLine" ></asp:TextBox>
    </td>

</tr>
<tr><td colspan="6"><asp:Label ID="Label1" runat="server" Text="Accountgegevens" CssClass="SubMenuHeader"></asp:Label></td></tr>
<tr>
    <td><asp:Label ID="Label10" runat="server" Text="Heeft een TMS login"></asp:Label> </td>
    <td> <asp:CheckBox ID="CheckBox_HasVMSAccount_Checked" Text="" runat="server" Checked="True" /></td>
    <td><asp:Label ID="Label9" runat="server" Text="GebruikersNaam"></asp:Label> </td>
    <td> <asp:TextBox ID="TextBox_AccountName" runat="server" Width="200px" 
            MaxLength="50" ></asp:TextBox> </td>
    <td><asp:Label ID="Label11" runat="server" Text="Wachtwoord"></asp:Label> </td>
    <td> <asp:TextBox ID="TextBox_Password" runat="server" Width="200px" 
            MaxLength="50" ></asp:TextBox> </td>
</tr>
<tr>
    <td><asp:Label ID="Label12" runat="server" Text="Alleen systeemtoegang voor lokatie"></asp:Label> </td>
    <td> 
        <cc2:ClassComboBoxLocation AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" ID="DropDownListLimitLocation_LimitAccessToThisLocation" runat="server" 
            DataSourceID="EntityDataSourceLocations" DataTextField="Description" 
            DataValueField="Id" AppendDataBoundItems="True" EnableViewState="False" style="margin-bottom: 0px">
            <asp:ListItem Value="">-geen beperking-</asp:ListItem>
        </cc2:ClassComboBoxLocation>
    </td>
    <td>
        <asp:Label ID="LabelRoleMemberships" runat="server" Text="Rol lidmaatschappen (beveiliging)"></asp:Label>
    </td>
    <td colspan="3">

        <asp:CheckBoxList ID="CheckBoxListRoleMemberships" runat="server" 
            DataSourceID="EntityDataSourceSecurityRoles" DataTextField="Description" 
            DataValueField="Id" RepeatColumns="3" RepeatDirection="Horizontal">
        </asp:CheckBoxList>

    </td>
</tr>
</table>
<asp:Button ID="ButtonCancel" runat="server" Text="Wijzigingen annuleren" 
    onclick="ButtonCancel_Click" />
<asp:Button ID="ButtonSave" runat="server" Text="Wijzigingen opslaan" 
    onclick="ButtonSave_Click" />
<asp:Button ID="ButtonDelete" runat="server" onclick="ButtonDelete_Click" 
    Text="Medewerker verwijderen" />


    <cc11:ClassEntityDataSource ID="EntityDataSourceLocations" runat="server" 
        ConnectionString="name=ModelTMSContainer" 
        DefaultContainerName="ModelTMSContainer" EntitySetName="LocationSet" 
        Where="(it.IsActive = True)" EnableFlattening="False" 
    Select="it.[Id], it.[Description], it.IsActive">
    </cc11:ClassEntityDataSource>




<cc11:ClassEntityDataSource ID="EntityDataSourceSecurityRoles" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EnableFlattening="False" 
    EntitySetName="SecurityRoleSet" 
    Select="it.[IsRoleTemplate], it.IsActive, it.[Id], it.[Description]" Where="it.IsActive and (!it.IsRoleTemplate)">
</cc11:ClassEntityDataSource>





<asp:CheckBox ID="CheckBoxOldVMSAccount" runat="server" Visible="False" />






