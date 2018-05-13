﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlStockUnitsOverview.ascx.cs" Inherits="TMS_Recycling.WebUserControlStockUnitsOverview" %>
<%@ Register src="WebUserControlStockUnitsBase.ascx" tagname="WebUserControlStockUnitsBase" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<table class="filterpanel" width="100%">
    <tr>
        <td>
            <asp:Label ID="LabelFilter" runat="server" CssClass="filterheader" 
                Text="Filter"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label1" runat="server" Text="Naam"></asp:Label>
            &nbsp;
            <asp:TextBox ID="TextBoxFilterName" runat="server"></asp:TextBox>
        </td>
        <td>
            <asp:CheckBox ID="CheckBoxFilterIsActive" runat="server" Checked="True" 
                Text="Is actief" />
        </td>
        <td>
            <asp:Button ID="ButtonSearch" runat="server" CssClass="sleekbutton" 
                onclick="ButtonSearch_Click" Text="Zoeken/verversen" />
        </td>
    </tr>
</table>


<table class="resultspanel"><tr><td>
<cc11:ClassGridView ID="GridViewResults" runat="server" AllowPaging="True" 
    AllowSorting="True" AutoGenerateColumns="False" 
    DataMember="DefaultView" DataSourceID="EntityDataSourceGridBase" 
    EmptyDataText="Er zijn geen gegevens beschikbaar" 
    onselectedindexchanged="GridViewResults_SelectedIndexChanged" 
        DataKeyNames="Id">
    <Columns>
        <asp:CommandField SelectText="Selecteer" ShowSelectButton="True" />
        <asp:BoundField DataField="Description" HeaderText="Omschrijving" ReadOnly="True" 
            SortExpression="Description" />
        <asp:BoundField DataField="StockUnit" HeaderText="Eenheid" ReadOnly="True" 
            SortExpression="StockUnit" />
        <asp:BoundField DataField="StockKgMultiplier" HeaderText="Kg factor" 
            SortExpression="StockKgMultiplier" ReadOnly="True" />
        <asp:CheckBoxField DataField="IsActive" HeaderText="Actief" ReadOnly="True" 
            SortExpression="IsActive" />
    </Columns>
</cc11:ClassGridView>
<br />
<asp:Button ID="ButtonNew" runat="server" onclick="ButtonNew_Click" 
    Text="Nieuwe materiaaleenheid toevoegen" />
</td></tr></table>

<table class="detailpanel"><tr><td>
<uc1:WebUserControlStockUnitsBase ID="WebUserControlStockUnitsBase1" 
    runat="server" Visible="False" />
</td></tr></table>

<cc11:ClassEntityDataSource ID="EntityDataSourceGridBase" runat="server" 
    CommandText="" ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EntitySetName="MaterialUnitSet" 
    OrderBy="it.[Description]" 
    Select="it.Id, it.Description, it.StockUnit, it.StockKgMultiplier, it.IsActive" 
    
    Where="(it.Description like @Description) and (it.IsActive = @IsActive)">
    <WhereParameters>
        <asp:Parameter  Name="Description" 
            Type="String" />
        <asp:Parameter Name="IsActive" 
             Type="Boolean" />
    </WhereParameters>
</cc11:ClassEntityDataSource>
