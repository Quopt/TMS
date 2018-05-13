<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlBookKeepingOverview.ascx.cs" Inherits="TMS_Recycling.WebUserControlBookKeepingOverview" %>
<%@ Register src="WebUserControlLedgerBase.ascx" tagname="WebUserControlLedgerBase" tagprefix="uc1" %>
<%@ Register src="ComboBoxLocation.ascx" tagname="ComboBoxLocation" tagprefix="uc2" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<table width="100%" class="filterpanel">
    <tr><td><asp:Label ID="LabelFilter" runat="server" Text="Filter" 
            CssClass="filterheader"></asp:Label></td></tr>
    <tr><td>
        <asp:Label ID="Label1" runat="server" Text="Naam"></asp:Label> &nbsp; <asp:TextBox ID="TextBoxFilterName"
            runat="server"></asp:TextBox></td>
<td>
<asp:Label ID="Label2" runat="server" Text="Lokatie"></asp:Label>
</td>
<td>
    <uc2:ComboBoxLocation ID="ComboBoxLocationDescription" runat="server" />
</td>
            <td>
                <asp:CheckBox
                    ID="CheckBoxFilterIsActive" runat="server" Text="Is actief" 
                    Checked="True" /></td>
                    <td>
                        <asp:CheckBox ID="CheckBoxDebug" runat="server" Text="Debug code" 
                            Visible="False" />
                    </td>
                        <td> 
                            <asp:Button ID="ButtonSearch" runat="server" Text="Zoeken/verversen" 
                                CssClass="sleekbutton" onclick="ButtonSearch_Click" /></td>
                        </tr>
    </table>
    <table class="resultspanel"><tr><td>
        <cc11:ClassGridView ID="GridViewResults" runat="server" AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" 
            DataSourceID="EntityDataSourceGridBase" 
            DataMember="DefaultView" EmptyDataText="Er zijn geen gegevens beschikbaar" 
            onselectedindexchanged="GridViewResults_SelectedIndexChanged" 
            DataKeyNames="Id">
            <Columns>
                <asp:CommandField SelectText="Bewerk" ShowSelectButton="True" />
                <asp:CheckBoxField DataField="IsActive" HeaderText="IsActive" ReadOnly="True" 
                    SortExpression="IsActive" />
                <asp:BoundField DataField="Id" HeaderText="Id" 
                    SortExpression="Id" ReadOnly="True" Visible="False" />
                <asp:BoundField DataField="Description" HeaderText="Boeknaam" 
                    ReadOnly="True" SortExpression="Description" />
                <asp:BoundField DataField="LedgerCurrency" HeaderText="Valuta" 
                    ReadOnly="True" SortExpression="LedgerCurrency" />
                <asp:BoundField DataField="LedgerLevel" HeaderText="Huidig niveau" ReadOnly="True" 
                    SortExpression="LedgerLevel" />
                <asp:BoundField DataField="LedgerType" HeaderText="Soort dagboek" ReadOnly="True" 
                    SortExpression="LedgerType" />
                <asp:BoundField DataField="Bank" HeaderText="Bank" 
                    ReadOnly="True" SortExpression="Bank" />
                <asp:BoundField DataField="BankAccount" HeaderText="Bank rekening" 
                    ReadOnly="True" SortExpression="BankAccount" />
                <asp:BoundField DataField="LocationDescription" HeaderText="Tbv lokatie" ReadOnly="True" 
                    SortExpression="LocationDescription" />
            </Columns>
        </cc11:ClassGridView>
        <asp:Button ID="ButtonNew" runat="server" Text="Nieuw dagboek toevoegen" 
            onclick="ButtonNew_Click" />
    </td></tr></table>
    <table class="detailpanel"><tr><td>

        <uc1:WebUserControlLedgerBase ID="WebUserControlLedgerBase1" runat="server" 
            Visible="False" />

    </td></tr></table>
    <cc11:ClassEntityDataSource ID="EntityDataSourceGridBase" runat="server" 
        ConnectionString="name=ModelTMSContainer" 
        DefaultContainerName="ModelTMSContainer" 
        Where="" 
        CommandText="SELECT it.[Id], it.[IsActive], it.[Description], it.[LedgerCurrency], it.[LedgerLevel], it.[LedgerType], it.[Bank], it.[BankAccount], it.LocationDescription
FROM 
(
SELECT it.[Id], it.[IsActive], it.[Description], it.[LedgerCurrency], it.[LedgerLevel], it.[LedgerType], it.[Bank], it.[BankAccount], ls.Description as LocationDescription
FROM LedgerSet as it left join LocationSet as ls on it.LimitToLocation.Id = ls.Id 
WHERE (it.Description LIKE @Description)  and (it.IsActive = @IsActive) and (it.IsDebugLedger = @IsDebug)
) as it
where ( (it.LocationDescription is null) and (@LocationDescription = &quot;%&quot;) ) or 
 ( (it.LocationDescription is not null) and (it.LocationDescription like @LocationDescription) )
Order by it.Description, it.LocationDescription
" 
        Include="" OrderBy="it.Description" EntitySetName="" 
    EntityTypeFilter="" Select="">
        <CommandParameters>
            <asp:FormParameter DefaultValue=" " FormField="TextBoxFilterName.text" 
                Name="Description" Type="String" />
            <asp:FormParameter DefaultValue="" FormField="CheckBoxFilterIsActive.checked" 
                Name="IsActive" Type="Boolean" />
            <asp:Parameter Name="LocationDescription" Type="String" />
            <asp:Parameter Name="IsDebug" Type="Boolean" />
        </CommandParameters>

    </cc11:ClassEntityDataSource>