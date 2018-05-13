<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlFreightOverview.ascx.cs" Inherits="TMS_Recycling.WebUserControlFreightOverview" %>
<%@ Register src="~/CalendarControl.ascx" tagname="CalendarControl" tagprefix="uc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="WebUserControlFreightBase.ascx" tagname="WebUserControlFreightBase" tagprefix="uc2" %>
<%@ Register src="ComboBoxLocation.ascx" tagname="ComboBoxLocation" tagprefix="uc3" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<style type="text/css">


.Header2
{
    color: white;
    border: none;
}

</style>
<table class="filterpanel" width="100%">
    <tr>
        <td>
            <asp:Label ID="LabelFilter" runat="server" CssClass="filterheader" 
                Text="Filter"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="LabelOrderNo" runat="server" Text="Volgnummer"></asp:Label>
            &nbsp;
        </td>
        <td>
            <asp:TextBox ID="TextBoxFreightNo" runat="server" MaxLength="15" Width="50%"></asp:TextBox>
        </td>
        <td>
            <asp:Label ID="LabelStartDate" runat="server" Text="Startdatum selectie"></asp:Label>
            &nbsp;
        </td>
        <td>
            <uc1:calendarcontrol id="CalendarControlStartDate" runat="server" />
        </td>
        <td>
            <asp:Label ID="LabelEndDate" runat="server" Text="Einddatum selectie"></asp:Label>
            &nbsp;
        </td>
        <td>
            <uc1:calendarcontrol id="CalendarControlEndDate" runat="server" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="LabelFreightStatus" runat="server" Text="Vracht status"></asp:Label>
        </td>
        <td>
            <cc11:ClassComboBox ID="DropDownListFreightStatus" runat="server" 
                AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList">
 <asp:ListItem Selected="True" Value="%">-nvt-</asp:ListItem>
 </cc11:ClassComboBox>
        </td>
        <td>
            <asp:Label ID="LabelLocation" runat="server" Text="Lokatie"></asp:Label>
        </td>
        <td>
            <uc3:ComboBoxLocation ID="TextBoxLocation" runat="server" />
        </td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="LabelFreightType" runat="server" Text="Vracht type"></asp:Label>
        </td>
        <td>
            <cc11:ClassComboBox ID="DropDownListFreightType" runat="server" 
                AppendDataBoundItems="True" AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" 
                EnableViewState="False">
                
 <asp:ListItem Selected="True" Value="%">-nvt-</asp:ListItem>
                
 </cc11:ClassComboBox>
        </td>
        <td>
            <asp:Label ID="LabelCustomer" runat="server" Text="Leverancier"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="TextBoxCustomer" runat="server" MaxLength="40" Width="90%"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="LabelName" runat="server" Text="Toelichting"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="TextBoxFilterName" runat="server" MaxLength="255" Width="90%"></asp:TextBox>
        </td>
        <td>
            <asp:Label ID="LabelIsActive" runat="server" Text="Actief"></asp:Label>
        </td>
        <td>
            <cc11:ClassComboBox ID="DropDownListCorrected" runat="server" 
                AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList">
<asp:ListItem Value="%">-nvt-</asp:ListItem>
<asp:ListItem Value="1">Ja</asp:ListItem>
 <asp:ListItem Value="0">Nee</asp:ListItem>
                
</cc11:ClassComboBox>
        </td>
        <td>
            &nbsp;</td>
        <td>
            <asp:Button ID="ButtonSearch" runat="server" CssClass="sleekbutton" 
                onclick="ButtonSearch_Click" Text="Zoeken/verversen" />
        </td>
    </tr>
</table>
<table class="resultspanel">
    <tr>
        <td>
            <cc11:ClassGridView ID="GridViewSelectedFreights" runat="server" AllowPaging="True" 
                AllowSorting="True" AutoGenerateColumns="False" 
                DataMember="DefaultView" DataSourceID="EntityDataSourceFreights" 
                onselectedindexchanged="GridViewSelectedFreights_SelectedIndexChanged" 
                ShowHeaderWhenEmpty="True" DataKeyNames="Id">
                <Columns>
                    <asp:CommandField SelectText="Bewerk" ShowSelectButton="True" />
                    <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" 
                        SortExpression="Id" Visible="False" />
                    <asp:BoundField DataField="Description" HeaderText="Omschrijving" 
                        ReadOnly="True" SortExpression="Description" />
                    <asp:BoundField DataField="OurReference" HeaderText="Vracht nr" ReadOnly="True" 
                        SortExpression="OurReference" />
                    <asp:BoundField DataField="FreightStatus" HeaderText="Status" ReadOnly="True" 
                        SortExpression="FreightStatus" />
                    <asp:BoundField DataField="FreightDateTime" HeaderText="Vrachtdatum" 
                        ReadOnly="True" SortExpression="FreightDateTime" />
                    <asp:BoundField DataField="FreightType" HeaderText="Soort vracht" 
                        ReadOnly="True" SortExpression="FreightType" />
                    <asp:BoundField DataField="TotalNetWeight" HeaderText="Totaal gewicht" 
                        ReadOnly="True" SortExpression="TotalNetWeight" />
                    <asp:BoundField DataField="RelationName" HeaderText="Relatie" ReadOnly="True" 
                        SortExpression="RelationName" />
                    <asp:BoundField DataField="LocationName" HeaderText="Locatie" ReadOnly="True" 
                        SortExpression="LocationName" />
                    <asp:CheckBoxField DataField="IsActive" HeaderText="Actief" ReadOnly="True" 
                        SortExpression="IsActive" />
                </Columns>
            </cc11:ClassGridView>
        </td>
    </tr>
</table>
<table class="detailpanel">
    <tr>
        <td>
            <uc2:WebUserControlFreightBase ID="WebUserControlFreightBase1" runat="server" />
        </td>
    </tr>
</table>


    <cc11:ClassEntityDataSource ID="EntityDataSourceFreights" runat="server" 
        ConnectionString="name=ModelTMSContainer" 
        DefaultContainerName="ModelTMSContainer" Where="" CommandText="select distinct it.Id, it.Description, it.OurReference, it.FreightStatus, it.FreightDateTime, it.FreightType, 
	it.YourReference, it.TotalNetWeight, it.IsActive, rs.Description as RelationName, 
	ls.Description as LocationName
from FreightSet as it left join RelationSet as rs on it.FromRelation.Id = rs.Id 
	left join LocationSet as ls on it.SourceOrDestinationLocation.Id = ls.Id 
where (it.FreightType LIKE @FreightType) and
	(it.Description LIKE  @Description  ) and (cast(it.OurReference as System.String) LIKE @FreightNumber ) and 
	(it.FreightDateTime BETWEEN @StartDate and @EndDate) and (it.FreightStatus LIKE @FreightStatus)  and
	(rs.Description LIKE @RelationDescription ) and 
        (ls.Description LIKE @LocationDescription) and
	((cast(it.IsActive as System.String) LIKE @ActiveStatus) or (@ActiveStatus=&quot;&quot;))" 
        OrderBy="it.FreightDateTime desc">
        <CommandParameters>
            <asp:FormParameter Name="Description" FormField="TextBoxFilterName.Text" Type="String" />
            <asp:Parameter Name="FreightNumber" Type="String" />
            <asp:Parameter Name="StartDate" Type="DateTime" />
            <asp:Parameter Name="EndDate"  Type="DateTime" />
            <asp:Parameter Name="FreightStatus" Type="String" />
            <asp:Parameter Name="RelationDescription" Type="String" />
            <asp:Parameter Name="ActiveStatus" Type="String" />
            <asp:Parameter Name="LocationDescription" Type="String" />
            <asp:Parameter Name="FreightType" Type="String" />
        </CommandParameters>
    </cc11:ClassEntityDataSource>
    

