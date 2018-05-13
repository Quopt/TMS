<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlCustomerRelationContactLog.ascx.cs" Inherits="TMS_Recycling.WebUserControlCustomerRelationContactLog" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit.HTMLEditor" tagprefix="cc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="CalendarControl.ascx" tagname="CalendarControl" tagprefix="uc1" %>
<%@ Register src="CalendarWithTimeControl.ascx" tagname="CalendarWithTimeControl" tagprefix="uc2" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<style type="text/css">
    .style1
    {
        height: 30px;
    }
</style>
<table width="100%">
<tr>
 <td colspan="4">
     <asp:Label ID="LabelBasisgegevens" runat="server" 
         Text="Basisgegevens contactmoment" CssClass="SubMenuHeader"></asp:Label></td>
 <td>    &nbsp;</td>
 <td>   &nbsp;</td>
</tr>

<tr>
<td>
    <asp:Label ID="LabelName" runat="server" Text="Naam"></asp:Label> </td>
    <td colspan="3" width="300px"> <asp:TextBox ID="TextBox_Description_Text" runat="server" Width="90%" 
            MaxLength="250" ></asp:TextBox>
    </td>
    <td><asp:Label ID="LabelContactType" runat="server" Text="Soort contact"></asp:Label> </td>
        <td>
    <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" ID="DropDownList_ContactType_SelectedValue" runat="server">
    </cc11:ClassComboBox>
    </td>
</tr>


<tr>
<td class="style1">
    <asp:Label ID="LabelContactDate" runat="server" Text="Contact datum"></asp:Label>
    </td>
<td class="style1">
    <uc2:CalendarWithTimeControl ID="CalendarWithTimeControl_ContactDateTime_SelectedDateTime" runat="server" />
    </td>
<td class="style1">
    <asp:Label ID="LabelFollowUpDate" runat="server" Text="Follow-up datum"></asp:Label>
</td>
<td class="style1">
    <uc2:CalendarWithTimeControl ID="CalendarControl_FollowUpDateTime_SelectedDateTime" runat="server" />
</td>
<td class="style1">
    <asp:Label ID="LabelFollowUpState" runat="server" Text="Status"></asp:Label>
</td>
<td class="style1">
    <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" ID="DropDownList_FollowUpState_SelectedValue" runat="server">
    </cc11:ClassComboBox>
    </td>
</tr>

<tr>
<td>
    <asp:Label ID="LabelPausedUntil" runat="server" Text="Gepauzeerd tot datum"></asp:Label>
</td>
<td>

    <uc2:CalendarWithTimeControl ID="CalendarControl_PausedUntilDateTime_SelectedDateTime" 
        runat="server" />

</td>

<td>
    <asp:Label ID="LabelHandledBy" runat="server" Text="Behandeld door"></asp:Label>
</td>
<td>

    <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" ID="DropDownList_HandledBy" runat="server" 
        DataSourceID="EntityDataSourceStaffMembers" DataTextField="Description" 
        DataValueField="Id">
    </cc11:ClassComboBox>

</td>
<td>
    <asp:Label ID="LabelHandler" runat="server" Text="Behandelaar"></asp:Label>
</td>
<td>

    <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" ID="DropDownList_Handler" runat="server" 
        DataSourceID="EntityDataSourceStaffMembers" DataTextField="Description" 
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
    Text="Contactmoment verwijderen" />

<cc11:ClassEntityDataSource ID="EntityDataSourceStaffMembers" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EntitySetName="StaffMemberSet" 
    OrderBy="it.Description" Select="it.[Id], it.[Description], it.[IsActive]" 
    Where="it.IsActive=true">
</cc11:ClassEntityDataSource>




    




