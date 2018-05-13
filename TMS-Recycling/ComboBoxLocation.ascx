<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ComboBoxLocation.ascx.cs" Inherits="TMS_Recycling.ComboBoxLocation" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>


<cc1:ClassComboBoxLocation ID="ClassComboBoxLocation1" runat="server" 
    AppendDataBoundItems="True" AutoCompleteMode="SuggestAppend" 
    DataSourceID="EntityDataSourceLocations" DataTextField="Description" 
    DataValueField="Id" DropDownStyle="DropDownList">
    <asp:ListItem Value="00000000-0000-0000-0000-000000000000" Selected="True">- Alle lokaties -</asp:ListItem>
</cc1:ClassComboBoxLocation>
<cc11:ClassEntityDataSource ID="EntityDataSourceLocations" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EntitySetName="LocationSet" 
    Select="it.[Id], it.[Description], it.[IsActive]" OrderBy="it.Description" 
    Where="it.IsActive">
</cc11:ClassEntityDataSource>

<asp:Label ID="LabelDataBound" runat="server" Text="" Visible="False"></asp:Label>


