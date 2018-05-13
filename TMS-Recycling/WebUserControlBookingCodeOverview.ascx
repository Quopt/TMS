﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlBookingCodeOverview.ascx.cs" Inherits="TMS_Recycling.WebUserControlBookingCodeOverview" %>
<%@ Register src="WebUserControlLedgerBase.ascx" tagname="WebUserControlLedgerBase" tagprefix="uc1" %>
<%@ Register src="ComboBoxLocation.ascx" tagname="ComboBoxLocation" tagprefix="uc2" %>
<%@ Register src="WebUserControlBookingCodeBase.ascx" tagname="WebUserControlBookingCodeBase" tagprefix="uc3" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>
<table width="100%" class="filterpanel">
    <tr><td><asp:Label ID="LabelFilter" runat="server" Text="Filter" 
            CssClass="filterheader"></asp:Label></td></tr>
    <tr><td>
        <asp:Label ID="Label1" runat="server" Text="Naam"></asp:Label> &nbsp; <asp:TextBox ID="TextBoxFilterName"
            runat="server"></asp:TextBox></td>
<td>
                <asp:CheckBox
                    ID="CheckBoxFilterIsActive" runat="server" Text="Is actief" 
                    Checked="True" /></td>
<td>
    &nbsp;</td>
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
                <asp:CommandField SelectText="Selecteer" ShowSelectButton="True" />
                <asp:BoundField DataField="Id" HeaderText="Id" 
                    SortExpression="Id" ReadOnly="True" Visible="False" />
                <asp:BoundField DataField="Description" HeaderText="Omscchrijving" ReadOnly="True" 
                    SortExpression="Description" />
                <asp:BoundField DataField="LedgerCurrency" HeaderText="Munteenheid" 
                    ReadOnly="True" SortExpression="LedgerCurrency" />
                <asp:BoundField DataField="LedgerLevel" HeaderText="Niveau" 
                    ReadOnly="True" SortExpression="LedgerLevel" />
                <asp:CheckBoxField DataField="IsActive" HeaderText="IsActive" ReadOnly="True" 
                    SortExpression="IsActive" />
            </Columns>
        </cc11:ClassGridView>
        <asp:Button ID="ButtonNew" runat="server" Text="Nieuwe boekingscode toevoegen" 
            onclick="ButtonNew_Click" />
    </td></tr></table>
    <table class="detailpanel"><tr><td>

        <uc3:WebUserControlBookingCodeBase ID="WebUserControlBookingCodeBase1" 
            runat="server" Visible="false" />

    </td></tr></table>
    <cc11:ClassEntityDataSource ID="EntityDataSourceGridBase" runat="server" 
        ConnectionString="name=ModelTMSContainer" 
        DefaultContainerName="ModelTMSContainer" 
        Where="" 
        CommandText="SELECT it.[Id], it.[IsActive], it.[Description], it.[LedgerCurrency], it.[LedgerLevel]
FROM LedgerBookingCodeSet as it 
WHERE (it.Description LIKE @Description) and (it.IsActive = @IsActive) and (it.IsDebugLedgerCode = @IsDebug)
Order by it.Description
" 
        Include="" OrderBy="it.Description">
        <CommandParameters>
            <asp:FormParameter DefaultValue=" " FormField="TextBoxFilterName.text" 
                Name="Description" Type="String" />
            <asp:FormParameter DefaultValue="" FormField="CheckBoxFilterIsActive.checked" 
                Name="IsActive" Type="Boolean" />
            <asp:Parameter Name="IsDebug" Type="Boolean" />
        </CommandParameters>

    </cc11:ClassEntityDataSource>
