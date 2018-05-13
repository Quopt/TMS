<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlStockOverview.ascx.cs" Inherits="TMS_Recycling.WebUserControlStockOverview" %>
<%@ Register src="WebUserControlStockMaterial.ascx" tagname="WebUserControlStockMaterial" tagprefix="uc1" %>
<%@ Register src="ComboBoxLocation.ascx" tagname="ComboBoxLocation" tagprefix="uc2" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

    <table width="100%" class="filterpanel">
    <tr><td><asp:Label ID="LabelFilter" runat="server" Text="Filter" 
            CssClass="filterheader"></asp:Label></td></tr>
    <tr><td>
        <asp:Label ID="Label1" runat="server" Text="Naam"></asp:Label> &nbsp; <asp:TextBox ID="TextBoxFilterName"
            runat="server"></asp:TextBox></td>
<td><asp:Label ID="LabelLocation" runat="server" Text="Lokatie"></asp:Label>
</td>
<td>
    <uc2:ComboBoxLocation ID="ComboBoxLocation1" runat="server" />
</td>

            <td>
                <asp:CheckBox
                    ID="CheckBoxFilterIsActive" runat="server" Text="Is actief" 
                    Checked="True" /></td>

                        <td> 
                            <asp:Button ID="ButtonSearch" runat="server" Text="Zoeken/verversen" 
                                CssClass="sleekbutton" onclick="ButtonSearch_Click" /></td>
                        </tr>
    </table>
    <table class="resultspanel"><tr><td>
        <cc11:ClassGridView ID="GridViewResults" runat="server" AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" 
            DataSourceID="EntityDataSourceGridBase" 
            DataMember="DefaultView" EmptyDataText="Er zijn geen gegevens gevonden die voldoen aan uw zoekcriteria." 
            onselectedindexchanged="GridViewResults_SelectedIndexChanged" 
            DataKeyNames="Id" ShowHeaderWhenEmpty="True">
            <Columns>
                <asp:CommandField ShowSelectButton="True" SelectText="Bewerk" />
                <asp:BoundField DataField="Id" HeaderText="Id" 
                    SortExpression="Id" ReadOnly="True" Visible="false" />
                <asp:BoundField DataField="MaterialNumber" HeaderText="Materiaal nummer" 
                    ReadOnly="True" SortExpression="MaterialNumber" />
                <asp:BoundField DataField="Description" HeaderText="Omschrijving" 
                    ReadOnly="True" SortExpression="Description" />
                <asp:BoundField DataField="Category" HeaderText="Categorie" ReadOnly="True" 
                    SortExpression="Category" />
                <asp:BoundField DataField="Group" HeaderText="Groep" ReadOnly="True" 
                    SortExpression="Group" />
                <asp:BoundField DataField="CurrentStockLevel" HeaderText="Voorraad niveau" 
                    ReadOnly="True" SortExpression="CurrentStockLevel" />
                <asp:BoundField DataField="PurchasePrice" HeaderText="Koopprijs" 
                    ReadOnly="True" SortExpression="PurchasePrice" />
                <asp:BoundField DataField="SalesPrice" HeaderText="Verkoopprijs" ReadOnly="True" 
                    SortExpression="SalesPrice" />
                <asp:BoundField DataField="VATPercentage" HeaderText="BTW%" 
                    SortExpression="VATPercentage" />
                <asp:CheckBoxField DataField="IsActive" HeaderText="Actief" ReadOnly="True" 
                    SortExpression="IsActive" />
                <asp:BoundField DataField="LocationDescription" HeaderText="Lokatie" 
                    ReadOnly="True" SortExpression="LocationDescription" />
                <asp:BoundField DataField="StorageCode" HeaderText="Opslagcode" 
                    SortExpression="StorageCode" />
            </Columns>
        </cc11:ClassGridView>
        <br />
        <asp:Button ID="ButtonNew" runat="server" Text="Nieuw materiaal toevoegen" 
            onclick="ButtonNew_Click" />
    </td></tr></table>
    <table class="detailpanel"><tr><td>

<uc1:WebUserControlStockMaterial ID="WebUserControlStockMaterialDetail" runat="server" Visible="false"/>

    </td></tr></table>
    <cc11:ClassEntityDataSource ID="EntityDataSourceGridBase" runat="server" 
        ConnectionString="name=ModelTMSContainer" 
        DefaultContainerName="ModelTMSContainer" 
        Where="" 
        CommandText="SELECT it.[Id], it.[IsActive], it.[Description], it.[MaterialNumber], it.[Category], it.[Group], it.[CurrentStockLevel], it.[PurchasePrice], it.[SalesPrice], ls.Description as LocationDescription, it.StorageCode, it.VATPercentage
FROM MaterialSet as it join LocationSet as ls on it.Location.Id = ls.Id
WHERE (it.Description LIKE @Description) and (it.IsActive = @IsActive) and (ls.Description LIKE @LocationDescription)
order by it.Description, LocationDescription
" 
        Include="" OrderBy="it.Description" EntitySetName="" EntityTypeFilter="" 
        Select="">
        <CommandParameters>
            <asp:FormParameter DefaultValue=" " FormField="TextBoxFilterName.text" 
                Name="Description" Type="String" />
            <asp:FormParameter DefaultValue="" FormField="CheckBoxFilterIsActive.checked" 
                Name="IsActive" Type="Boolean" />
            <asp:Parameter Name="LocationDescription" Type="String" />
        </CommandParameters>

    </cc11:ClassEntityDataSource>
