<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlInvoicePartialPayment.ascx.cs" Inherits="TMS_Recycling.WebUserControlInvoicePartialPayment" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<table>
<tr>
<td>
    <asp:Label ID="Label1" runat="server" Text="Registratie deelbetaling"></asp:Label>
</td>
<td>
    <asp:Label ID="Label_Description" runat="server" Text="..."></asp:Label>
    
</td>
</tr>
<tr>
<td>
    <asp:Label ID="Label4" runat="server" Text="Factuurnummer"></asp:Label>
</td>
<td>
    <asp:Label ID="Label_InvoiceNumber" runat="server" Text="..."></asp:Label>
</td>
</tr>
<tr>
<td>
    <asp:Label ID="Label8" runat="server" Text="Factuurbedrag"></asp:Label>
</td>
<td>
    <asp:Label ID="Label_TotalPrice" runat="server" Text="..."></asp:Label>
</td>
</tr>
<tr>
<td>
    <asp:Label ID="Label10" runat="server" Text="Reeds betaald"></asp:Label>
</td>
<td>
    <asp:Label ID="Label_AlreadyPaid" runat="server" Text="..."></asp:Label>
</td>
</tr>
<tr>
<td>
    <asp:Label ID="Label5" runat="server" Text="Lokatie kasboek"></asp:Label>
</td>
<td>
    <cc1:ClassComboBoxLocation ID="DropDownListLocation" runat="server" 
        AutoCompleteMode="SuggestAppend" AutoPostBack="True" 
        DataSourceID="EntityDataSourceLocations" DataTextField="Description" 
        DataValueField="Id" DropDownStyle="DropDownList" MaxLength="0" 
        style="display: inline;">
    </cc1:ClassComboBoxLocation>
</td>
</tr>
<tr>
<td>
    <asp:Label ID="Label6" runat="server" Text="Kasboek"></asp:Label>
</td>
<td>
    <cc11:ClassComboBox ID="DropDownListLedger" runat="server" AutoCompleteMode="SuggestAppend" 
        DataSourceID="EntityDataSourceLedgers" DataTextField="Description" 
        DataValueField="Id" DropDownStyle="DropDownList" MaxLength="0" 
        style="display: inline;">
    </cc11:ClassComboBox>
</td>
</tr>
<tr>
<td>
    <asp:Label ID="Label7" runat="server" Text="Vooruitbetaald bedrag"></asp:Label>
</td>
<td>
    <asp:TextBox ID="TextBoxPaidAmount" runat="server"></asp:TextBox>
</td>
</tr>
<tr>
<td>
</td>
<td>
</td>
</tr>
<tr>
<td>
</td>
<td>
    <asp:Button ID="ButtonProcess" runat="server" Text="Deelbetaling registreren" 
        onclick="ButtonProcess_Click" />
</td>
</tr>
</table>

<cc11:ClassEntityDataSource ID="EntityDataSourceLocations" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EntitySetName="LocationSet" 
    Select="it.[Id], it.[Description], it.[IsActive]" OrderBy="it.Description" 
    Where="it.IsActive">
</cc11:ClassEntityDataSource>

<cc11:ClassEntityDataSource ID="EntityDataSourceLedgers" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EnableFlattening="False" 
    EntitySetName="LedgerSet" OrderBy="it.Description" 
    Select="it.[Id], it.[IsActive], it.[Description]" 
    Where="(it.LimitToLocation is NULL) or (it.LimitToLocation.Id == @LocationID) and (it.IsActive)">
    <WhereParameters>
        <asp:ControlParameter ControlID="DropDownListLocation" DbType="Guid" 
            DefaultValue="{00000000-0000-0000-0000-000000000000}" Name="LocationID" 
            PropertyName="SelectedValue" />
    </WhereParameters>
</cc11:ClassEntityDataSource>



