<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlTruckOverview.ascx.cs" Inherits="TMS_Recycling.WebUserControlTruckOverview" %>
<%@ Register src="WebUserControlTruckBase.ascx" tagname="WebUserControlTruckBase" tagprefix="uc1" %>
<%@ Register src="ComboBoxLocation.ascx" tagname="ComboBoxLocation" tagprefix="uc2" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>


    <table width="100%" class="filterpanel">
    <tr><td><asp:Label ID="LabelFilter" runat="server" Text="Filter" 
            CssClass="filterheader"></asp:Label></td></tr>
    <tr><td>
        <asp:Label ID="Label1" runat="server" Text="Naam"></asp:Label> &nbsp; <asp:TextBox ID="TextBoxFilterName"
            runat="server"></asp:TextBox></td><td>
                <asp:CheckBox
                    ID="CheckBoxFilterIsActive" runat="server" Text="Is actief" 
                    Checked="True" /></td>
                    <td>
                        <nobr>
                        <asp:Label ID="LabelLocation" runat="server" Text="Thuislocatie truck"></asp:Label> &nbsp; 
                        <uc2:ComboBoxLocation ID="ComboBoxHomeLocation" runat="server" />
                        </nobr>
                    </td>
                        <td> 
                            <asp:Button ID="ButtonSearch" runat="server" Text="Zoeken/verversen" 
                                CssClass="sleekbutton" onclick="ButtonSearch_Click" /></td>
                        </tr>
    </table>
    <table class="resultspanel"><tr><td>
        <cc11:ClassGridView ID="GridViewResults" runat="server" AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" 
            DataSourceID="EntityDataSourceTrucks" 
            DataMember="DefaultView" EmptyDataText="Er zijn geen gegevens beschikbaar" 
            onselectedindexchanged="GridViewResults_SelectedIndexChanged" 
            DataKeyNames="Id">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Id" 
                    SortExpression="Id" ReadOnly="True" Visible="False" />
                <asp:CommandField SelectText="Bewerk" ShowSelectButton="True" />
                <asp:BoundField DataField="Description" HeaderText="Toelichting" ReadOnly="True" 
                    SortExpression="Description" />
                <asp:BoundField DataField="TruckPlate" HeaderText="Kenteken" 
                    SortExpression="TruckPlate" />
                <asp:BoundField DataField="HomeTruckLocation" 
                    HeaderText="Thuislokatie truck" SortExpression="HomeTruckLocation" />
                <asp:BoundField DataField="CurrentTruckLocation" 
                    HeaderText="Huidige lokatie truck" SortExpression="CurrentTruckLocation" />
                <asp:CheckBoxField DataField="IsActive" HeaderText="Actief" ReadOnly="True" 
                    SortExpression="IsActive" />
            </Columns>
        </cc11:ClassGridView>
        <br />
        <asp:Button ID="ButtonNew" runat="server" onclick="ButtonNew_Click" 
            Text="Nieuwe wagen toevoegen" />
    </td></tr></table>
    <table class="detailpanel"><tr><td>
       
        <uc1:WebUserControlTruckBase ID="WebUserControlTruckBase1" runat="server" Visible="false" />
       
        </td></tr></table>
    <cc11:ClassEntityDataSource ID="EntityDataSourceTrucks" runat="server" 
        ConnectionString="name=ModelTMSContainer" 
        DefaultContainerName="ModelTMSContainer" 
        
    Where="" 
        OrderBy="it.[Description]" 
    
    CommandText="select it.Description, it.IsActive, it.Id, it.TruckPlate, it.HomeTruckLocation.Description as HomeTruckLocation, it.CurrentTruckLocation.Description as CurrentTruckLocation
from TruckSet as it
where (it.[Description] like &quot;%&quot; + @Description + &quot;%&quot;) and (it.[IsActive] = @IsActive)  and (it.HomeTruckLocation.Description like @HomeTruckLocation)">
        <CommandParameters>
            <asp:ControlParameter ControlID="TextBoxFilterName" DefaultValue="%" 
                Name="Description" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="CheckBoxFilterIsActive" DefaultValue="True" 
                Name="IsActive" PropertyName="Checked" Type="Boolean" />
            <asp:QueryStringParameter Name="Id" QueryStringField="Id" DbType="Guid" />
            <asp:Parameter Name="HomeTruckLocation" Type="String" />        
        </CommandParameters>
    </cc11:ClassEntityDataSource>

