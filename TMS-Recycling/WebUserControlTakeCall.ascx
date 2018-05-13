<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlTakeCall.ascx.cs" Inherits="TMS_Recycling.WebUserControlTakeCall" %>

<%@ Register src="CalendarWithTimeControl.ascx" tagname="CalendarWithTimeControl" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc1" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<table style="width:350px; vertical-align:top;">
    <tr>
        <td>
            <asp:Label ID="LabelRelation" runat="server" Text="Bedrijf"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="TextBoxClient" runat="server" Width="100%" MaxLength="200"></asp:TextBox>
            <br />
            <asp:TextBoxWatermarkExtender ID="TextBoxClient_TextBoxWatermarkExtender" 
                runat="server" TargetControlID="TextBoxClient" WatermarkCssClass="Watermark" 
                WatermarkText="Naam van de (nieuwe) relatie">
            </asp:TextBoxWatermarkExtender>
            <asp:AutoCompleteExtender ID="TextBoxClient_AutoCompleteExtender" 
                runat="server" CompletionInterval="200" DelimiterCharacters="" Enabled="True" 
                FirstRowSelected="True" MinimumPrefixLength="1" ServiceMethod="GetRelationList" 
                ServicePath="WebServiceTMS.asmx" TargetControlID="TextBoxClient">
            </asp:AutoCompleteExtender>
            <asp:CheckBox ID="CheckBoxNewRelation" runat="server" Text="Nieuw bedrijf" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="LabelContactPerson" runat="server" Text="Contact persoon"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="TextBoxPerson" runat="server" Width="100%" MaxLength="200"></asp:TextBox>
            <br />
            <asp:TextBoxWatermarkExtender ID="TextBoxPerson_TextBoxWatermarkExtender" 
                runat="server" TargetControlID="TextBoxPerson" WatermarkCssClass="Watermark" 
                WatermarkText="Naam van de (nieuwe) contactpersoon">
            </asp:TextBoxWatermarkExtender>
            <asp:AutoCompleteExtender ID="TextBoxPerson_AutoCompleteExtender" 
                runat="server" CompletionInterval="200" DelimiterCharacters="" Enabled="True" 
                FirstRowSelected="True" MinimumPrefixLength="2" ServiceMethod="GetContactList" 
                ServicePath="WebServiceTMS.asmx" TargetControlID="TextBoxPerson">
            </asp:AutoCompleteExtender>
            <asp:CheckBox ID="CheckBoxNewContact" runat="server" 
                Text="Nieuw contactpersoon" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="LabelMessage" runat="server" Text="Bericht"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="TextBoxMessage" runat="server" Rows="3" TextMode="MultiLine" 
                Width="100%" MaxLength="4000"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="LabelReminderDateTime" runat="server" 
                Text="Herinneringsdatum/-tijd"></asp:Label>
        </td>
        <td>
            <uc1:CalendarWithTimeControl ID="CalendarWithTimeControlReminder" 
                runat="server" />
            <br />
            <asp:CheckBox ID="CheckBoxNoReminder" runat="server" 
                Text="Geen herinnering instellen" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="LabelHandler" runat="server" Text="Behandelaar"></asp:Label>
        </td>
        <td>
            <cc1:ClassComboBox ID="ClassComboBoxHandler" runat="server" 
                AutoCompleteMode="SuggestAppend" DataSourceID="ClassEntityDataSourceStaff" 
                DataTextField="Description" DataValueField="Id" DropDownStyle="DropDownList" 
                MaxLength="0" style="display: inline;">
            </cc1:ClassComboBox>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            <asp:Label ID="LabelErrorMessage" runat="server" Text=""></asp:Label>
            <br />
            <asp:Button ID="ButtonSave" runat="server" Height="26px" Text="Opslaan" 
                onclick="ButtonSave_Click" />
        </td>
    </tr>
</table>


<cc1:ClassEntityDataSource ID="ClassEntityDataSourceStaff" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EntitySetName="StaffMemberSet" 
    OrderBy="it.Description" Select="it.[Description], it.[Id], it.[IsActive]" 
    Where="it.IsActive">
</cc1:ClassEntityDataSource>


