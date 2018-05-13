<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlRentVATOverview.ascx.cs" Inherits="TMS_Recycling.WebUserControlRentVATOverview" %>
<%@ Register src="WebUserControlRentVATBase.ascx" tagname="WebUserControlRentVATBase" tagprefix="uc1" %>
<%@ Register src="ComboBoxLocation.ascx" tagname="ComboBoxLocation" tagprefix="uc2" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>
    <asp:Label ID="LabelObjectName" runat="server" Text="..." 
        CssClass="SubMenuHeader"></asp:Label>
<table width="100%" class="filterpanel">
    <tr><td><asp:Label ID="LabelFilter" runat="server" Text="Filter" 
            CssClass="filterheader"></asp:Label></td></tr>
    <tr><td>
        <asp:Label ID="Label1" runat="server" Text="Naam"></asp:Label> &nbsp; <asp:TextBox ID="TextBoxFilterName"
            runat="server"></asp:TextBox></td>
<td>
<asp:Label ID="LabelLocation" runat="server" Text="Lokatie"></asp:Label>
</td>            
<td>
    <uc2:ComboBoxLocation ID="ComboBoxLocationDescription" runat="server" />
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

    <table width="100%" class="resultspanel">
    <tr><td>
        <cc11:ClassGridView ID="GridViewResults" runat="server" AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" 
            DataSourceID="EntityDataSourceGridBase" 
            DataMember="DefaultView" EmptyDataText="Er zijn geen gegevens beschikbaar" 
            onselectedindexchanged="GridViewResults_SelectedIndexChanged" 
    DataKeyNames="Id">
            <Columns>
                <asp:CommandField SelectText="Bewerk" ShowSelectButton="True" />
                <asp:BoundField DataField="Id" HeaderText="Id" 
                    SortExpression="Id" ReadOnly="True" Visible="False" />
                <asp:BoundField DataField="Description" HeaderText="Naam" 
                    ReadOnly="True" SortExpression="Description" />
                <asp:BoundField DataField="Location" HeaderText="Lokatie" 
                    SortExpression="Location" />
                <asp:BoundField DataField="VATPercentage" HeaderText="BTW%" 
                    SortExpression="VATPercentage" />
                <asp:CheckBoxField DataField="IsActive" HeaderText="IsActive" ReadOnly="True" 
                    SortExpression="IsActive" />
            </Columns>
        </cc11:ClassGridView>
        <br />
        <asp:Button ID="ButtonNew" runat="server" Text="Nieuw BTW niveau toevoegen" 
            onclick="ButtonNew_Click" />
    </td></tr></table>

    <table width="100%" class="detailpanel">
    <tr><td>
    <uc1:WebUserControlRentVATBase ID="WebUserControlRentVATBase1" 
    runat="server" Visible="False" />
    </td></tr></table>

    <cc11:ClassEntityDataSource ID="EntityDataSourceGridBase" runat="server" 
        ConnectionString="name=ModelTMSContainer" 
        DefaultContainerName="ModelTMSContainer" 
        
    Where="" 
        OrderBy="it.[Description]" CommandText="select it.[Id], it.[Description], it.[IsActive], lo.Description as Location, it.VATPercentage
from RentalTypeVATSet as it inner join LocationSet as lo on it.Location.Id = lo.Id
where (it.[Description] like @Description) and (lo.Description like @LocationDescription) and  (it.[IsActive] = @IsActive) and (it.RentalType.Id = @Id)
" EntitySetName="" Select="">
        <CommandParameters>
            <asp:ControlParameter ControlID="TextBoxFilterName" Name="Description" 
                PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="CheckBoxFilterIsActive" Name="IsActive" 
                PropertyName="Checked" Type="Boolean" />
            <asp:Parameter Name="LocationDescription"  Type="String" />
            <asp:Parameter DbType="Guid" Name="Id" />
        </CommandParameters>
    </cc11:ClassEntityDataSource>

