<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlRentMaterialOverview.ascx.cs" Inherits="TMS_Recycling.WebUserControlRentMaterialOverview" %>
    <%@ Register src="WebUserControlRentMaterialBase.ascx" tagname="WebUserControlRentMaterialBase" tagprefix="uc1" %>
    <%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
    <%@ Register src="ComboBoxLocation.ascx" tagname="ComboBoxLocation" tagprefix="uc2" %>
    <%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

    <asp:Label ID="LabelObjectName" runat="server" Text="..." 
        CssClass="SubMenuHeader"></asp:Label>

    <table width="100%" class="filterpanel">
    <tr><td><asp:Label ID="LabelFilter" runat="server" Text="Filter" 
            CssClass="filterheader"></asp:Label></td>
    <td width="30%">
    </td>
    <td>
    </td>
    <td>
    </td>
    </tr>

    <tr><td>
        <asp:Label ID="Label1" runat="server" Text="Naam"></asp:Label> </td>
    <td>
                <asp:TextBox ID="TextBoxFilterName"
            runat="server" Width="90%"></asp:TextBox>
    </td>
    <td>
        <asp:Label ID="LabelLocation" runat="server" Text="Lokatie"></asp:Label> 
    </td>
    <td>
        <uc2:ComboBoxLocation ID="ComboBoxLocationDescription" runat="server" />
    </td>
    </tr>

    <tr><td>
        <asp:Label ID="Label2" runat="server" Text="Materiaal status" 
           ></asp:Label> </td><td>
                <cc11:ClassComboBox ID="ComboBoxItemState" runat="server" AppendDataBoundItems="True" 
                    AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList">
                    <asp:ListItem Value="">-nvt-</asp:ListItem>
                </cc11:ClassComboBox>
        </td>

    <td>
                <asp:CheckBox
                    ID="CheckBoxFilterIsActive" runat="server" Text="Is actief" 
                    Checked="True" />
    </td>
                        <td> 
                            <asp:Button ID="ButtonSearch" runat="server" Text="Zoeken/verversen" 
                                CssClass="sleekbutton" onclick="ButtonSearch_Click" Height="26px" /></td>
                        </tr>
    </table>
    <table class="resultspanel"><tr><td>
        <cc11:ClassGridView ID="GridViewResults" runat="server" AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" 
            DataSourceID="EntityDataSourceRelation" 
            DataMember="DefaultView" EmptyDataText="Er zijn geen gegevens beschikbaar" 
            onselectedindexchanged="GridViewResults_SelectedIndexChanged" 
            DataKeyNames="Id" onrowdatabound="GridViewResults_RowDataBound">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Id" 
                    SortExpression="Id" ReadOnly="True" Visible="False" />
                <asp:CommandField SelectText="Bewerk" ShowSelectButton="True" />
                <asp:BoundField DataField="Description" HeaderText="Toelichting" ReadOnly="True" 
                    SortExpression="Description" />
                <asp:BoundField DataField="ItemState" HeaderText="Materiaal status" 
                    SortExpression="ItemState" />
                <asp:BoundField DataField="Location" HeaderText="Lokatie" 
                    SortExpression="Location" />
                <asp:CheckBoxField DataField="IsActive" HeaderText="Actief" ReadOnly="True" 
                    SortExpression="IsActive" />
            </Columns>
        </cc11:ClassGridView>
        <br />
        <asp:Button ID="ButtonNew" runat="server" onclick="ButtonNew_Click" 
            Text="Nieuw materiaal toevoegen" />
    </td></tr></table>
    <table class="detailpanel"><tr><td>
        <uc1:WebUserControlRentMaterialBase ID="WebUserControlRentMaterialBase1" 
            runat="server" Visible="False" />
        </td></tr></table>
    <cc11:ClassEntityDataSource ID="EntityDataSourceRelation" runat="server" 
        ConnectionString="name=ModelTMSContainer" 
        DefaultContainerName="ModelTMSContainer" 
        
    Where="" 
        OrderBy="it.[Description]" CommandText="select it.[Id], it.[Description], it.ItemState, it.[IsActive], ls.Description as Location
from RentalItemSet as it inner join LocationSet as ls on it.Location.Id = ls.Id
where (it.[Description] like  @Description) and 
(ls.[Description] like  @LocationDescription ) and 
(it.[ItemState] like  @ItemState) and 
(it.[IsActive] = @IsActive) and (it.RentalType.Id = @Id)" 
    EntityTypeFilter="">
        <CommandParameters>
            <asp:Parameter Name="IsActive" Type="Boolean" />
            <asp:Parameter Name="Description" Type="String" />
            <asp:Parameter Name="LocationDescription" Type="String" />
            <asp:Parameter Name="ItemState" Type="String" />
            <asp:Parameter DbType="Guid" Name="Id" />
        </CommandParameters>
    </cc11:ClassEntityDataSource>

