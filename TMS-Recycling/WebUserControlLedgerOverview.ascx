<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlLedgerOverview.ascx.cs" Inherits="TMS_Recycling.WebUserControlLedgerOverview" %>
<%@ Register src="~/CalendarControl.ascx" tagname="CalendarControl" tagprefix="uc1" %><%@ Register src="~/WebUserControlOrderBase.ascx" tagname="WebUserControlOrderBase" tagprefix="uc2" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="ComboBoxLocation.ascx" tagname="ComboBoxLocation" tagprefix="uc3" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

    <table width="100%" class="filterpanel">
    <tr><td><asp:Label ID="LabelFilter" runat="server" Text="Filter" 
            CssClass="filterheader"></asp:Label></td></tr>
            <tr>
            <td>
                <asp:Label ID="LabelOrderNo" runat="server" Text="Volgnummer"></asp:Label>&nbsp;
            </td><td>
                <asp:TextBox ID="TextBoxOrderNo" runat="server" MaxLength="15" Width="50%"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="LabelStartDate" runat="server" Text="Startdatum selectie"></asp:Label>&nbsp;
            </td><td>
                    <uc1:CalendarControl ID="CalendarControlStartDate" runat="server" />
                </td>
            <td>
                <asp:Label ID="LabelEndDate" runat="server" Text="Einddatum selectie"></asp:Label>&nbsp;
            </td>
            <td>
                <uc1:CalendarControl ID="CalendarControlEndDate" runat="server" />
                </td>
            </tr>

<tr>
<td>
    <asp:Label ID="LabelBookingStatus" runat="server" Text="Boekstatus"></asp:Label>
</td>
<td>
    <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" ID="DropDownListBookingType" runat="server">
        <asp:ListItem Selected="True" Value="%">-nvt-</asp:ListItem>
        <asp:ListItem Value="Open">Nog te leveren</asp:ListItem>
        <asp:ListItem Value="Processed">Geleverd</asp:ListItem>
    </cc11:ClassComboBox>
</td>
<td>
    <asp:Label ID="LabelInvoiced" runat="server" Text="Gefactureerd"></asp:Label>
</td>
<td>
    <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" ID="DropDownListInvoiced" runat="server">
        <asp:ListItem Value="%" Selected="True" >-nvt-</asp:ListItem>
        <asp:ListItem Value="Yes">Ja</asp:ListItem>
        <asp:ListItem Value="No">Nee</asp:ListItem>
    </cc11:ClassComboBox>
</td>
<td>
    <asp:Label ID="LabelPerson" runat="server" Text="Aangemaakt door"></asp:Label>
</td>
<td>
        <asp:TextBox ID="TextBoxUserName" runat="server" MaxLength="40" Width="90%"></asp:TextBox>
</td>
</tr>

<tr>
<td>
    <asp:Label ID="LabelContainsMaterial" runat="server" Text="Bevat materiaal"></asp:Label>
</td>
<td>
    <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" ID="DropDownListMaterial" runat="server" 
        AppendDataBoundItems="True" EnableViewState="False" 
        DataSourceID="EntityDataSourceMaterials" DataTextField="Description" 
        DataValueField="Description">
        <asp:ListItem Selected="True" Value="%">-nvt-</asp:ListItem>
    </cc11:ClassComboBox>
</td>
<td>
    <asp:Label ID="LabelCustomer" runat="server" Text="Leverancier/Afnemer"></asp:Label>
</td>
<td>
        <asp:TextBox ID="TextBoxCustomer" runat="server" MaxLength="40" Width="90%"></asp:TextBox>
</td>
<td>
    <asp:Label ID="LabelDriverOrPlate" runat="server" 
        Text="Identificatie brenger/haler"></asp:Label>
</td>
<td>
        <asp:TextBox ID="TextBoxDriverOrPlate" runat="server" MaxLength="40" Width="90%"></asp:TextBox>
</td>
</tr>
    <tr><td>
        <asp:Label ID="LabelName" runat="server" Text="Toelichting"></asp:Label> 
        </td><td> 
        <asp:TextBox ID="TextBoxFilterName" runat="server" MaxLength="255" Width="90%"></asp:TextBox></td><td>
    <asp:Label ID="LabelCorrected" runat="server" Text="Gecorrigeerd"></asp:Label>
        </td>
    <td>
    <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" ID="DropDownListCorrected" runat="server">
        <asp:ListItem Value="%" >-nvt-</asp:ListItem>
        <asp:ListItem Value="1">Ja</asp:ListItem>
        <asp:ListItem Value="0">Nee</asp:ListItem>
    </cc11:ClassComboBox>
        </td>
        <td>
        <asp:Label ID="LabelLocation" runat="server" Text="In-/Verkooplokatie"></asp:Label>    
        </td>
                        <td> 
                            <uc3:ComboBoxLocation ID="ComboBoxLocationSelection" runat="server" />
        </td>
                        </tr>
<tr>
<td>
</td>
<td>
</td>
<td>
</td>
<td>
</td>
<td>
            <asp:Label ID="LabelInvoiceType" runat="server" Text="Buy" Visible="False"></asp:Label>
</td>
<td>
                            <asp:Button ID="ButtonSearch" runat="server" Text="Zoeken/verversen" 
                                CssClass="sleekbutton" onclick="ButtonSearch_Click" />
</td>
</tr>
    </table>

    <table class="resultspanel"><tr><td>
    <cc11:ClassGridView ID="GridViewSelectedPurchases" runat="server" AllowPaging="True" 
        AllowSorting="True" DataSourceID="EntityDataSourcePurchaseLedger" 
        AutoGenerateColumns="False" DataKeyNames="Id" DataMember="DefaultView" 
        ShowHeaderWhenEmpty="False" 
            onselectedindexchanged="GridViewSelectedPurchases_SelectedIndexChanged">
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
            <asp:BoundField DataField="OrderNumber" HeaderText="Volgnummer" 
                SortExpression="OrderNumber" />
            <asp:BoundField DataField="BookingDateTime" HeaderText="Boekdatum" 
                SortExpression="BookingDateTime" />
            <asp:BoundField DataField="RelationName" HeaderText="Leverancier" 
                SortExpression="RelationName" />
            <asp:BoundField DataField="Description" HeaderText="Omschrijving" 
                SortExpression="Description" />
            <asp:BoundField DataField="OrderStatus" HeaderText="Status" 
                SortExpression="OrderStatus" />
            <asp:CheckBoxField DataField="IsInvoiced" HeaderText="Gefactureerd" 
                SortExpression="IsInvoiced" />
            <asp:CheckBoxField DataField="IsCorrected" HeaderText="Gecorrigeerd" 
                SortExpression="IsCorrected" />
            <asp:BoundField DataField="YourTruckPlate" HeaderText="Kenteken" 
                SortExpression="YourTruckPlate" />
            <asp:BoundField DataField="YourDriverName" HeaderText="Bestuurder" 
                SortExpression="YourDriverName" />
            <asp:BoundField DataField="EmployeeName" 
                HeaderText="Inkoper" SortExpression="EmployeeName" />
            <asp:BoundField DataField="LocationName" HeaderText="Inkooplokatie" 
                SortExpression="LocationName" />
            <asp:BoundField DataField="TotalPrice" DataFormatString="{0:c}" 
                HeaderText="Bedrag" SortExpression="TotalPrice" />
        </Columns>
    </cc11:ClassGridView>
    </td></tr></table>

    <table class="detailpanel"><tr><td>


        <uc2:WebUserControlOrderBase ID="WebUserControlOrderBase1" runat="server" visible="false" />


    </td></tr></table>


    <cc11:ClassEntityDataSource ID="EntityDataSourcePurchaseLedger" runat="server" 
        ConnectionString="name=ModelTMSContainer" 
        DefaultContainerName="ModelTMSContainer" Where="" CommandText="select distinct it.Id, it.YourTruckPlate, it.YourDriverName, it.OrderNumber, it.BookingDateTime, it.Description, it.OrderStatus, 
	(it.Invoice IS NOT NULL) as IsInvoiced, it.IsCorrected, it.TotalPrice, rs.Description as RelationName, 
	es.Description as EmployeeName, ls.Description as LocationName
from OrderSet as it left join RelationSet as rs on it.Relation.Id = rs.Id left join StaffMemberSet as es 
	on it.StaffMemberPurchaser.Id = es.Id left join LocationSet as ls on it.Location.Id = ls.Id inner join OrderLineSet as ol 
	on it.Id = ol.Order.Id left join MaterialSet as ms on ol.Material.Id = ms.Id
where (it.OrderType = @InvoiceType) and
	(it.Description LIKE  @Description  ) and (cast(it.OrderNumber as System.String) LIKE  @OrderNumber ) and 
	(it.BookingDateTime BETWEEN @StartDate and @EndDate) and (it.OrderStatus LIKE @OrderStatus)  and 
 	((es.Description LIKE  @EmployeeName) or (es.Description IS NULL)) and (ms.Description LIKE  @MaterialDescription) and 
	(rs.Description LIKE @RelationDescription ) and (ls.Description like @LocationDescription) and
	( ( it.YourTruckPlate LIKE  @IDDelivery ) or  ( it.YourDriverName LIKE  @IDDelivery) ) and 
	(cast(it.IsCorrected as System.String) LIKE @CorrectedStatus) and 
	(CAST((CASE WHEN (it.Invoice IS NOT NULL) THEN &quot;Yes&quot; ELSE &quot;No&quot; END) as System.String) LIKE @InvoiceStatus) " 
        OrderBy="it.BookingDateTime desc" EntitySetName="" EntityTypeFilter="" 
    Select="">
        <CommandParameters>
            <asp:FormParameter Name="Description" FormField="TextBoxFilterName.Text" Type="String" />
            <asp:Parameter Name="OrderNumber" Type="String" />
            <asp:Parameter Name="StartDate" Type="DateTime" />
            <asp:Parameter Name="EndDate"  Type="DateTime" />
            <asp:Parameter Name="OrderStatus" Type="String" />
            <asp:Parameter Name="InvoiceStatus" Type="String" />
            <asp:Parameter Name="EmployeeName" Type="String" />
            <asp:Parameter Name="MaterialDescription" Type="String" />
            <asp:Parameter Name="RelationDescription" Type="String" />
            <asp:Parameter Name="IDDelivery" Type="String" />
            <asp:Parameter Name="CorrectedStatus" Type="String" />
            <asp:ControlParameter ControlID="LabelInvoiceType" Name="InvoiceType" 
                PropertyName="Text" Type="String" />
            <asp:Parameter Name="LocationDescription" Type="String" />
        </CommandParameters>
    </cc11:ClassEntityDataSource>
    <cc11:ClassEntityDataSource ID="EntityDataSourceMaterials" runat="server" 
        ConnectionString="name=ModelTMSContainer" 
        DefaultContainerName="ModelTMSContainer" EntitySetName="MaterialSet" 
        Select="it.[Id], it.[IsActive], it.[Description]" 
    Where="it.IsActive and ( (it.InvoiceType = @InvoiceType) or (it.InvoiceType = &quot;Both&quot;))" 
    EntityTypeFilter="">
        <WhereParameters>
            <asp:ControlParameter ControlID="LabelInvoiceType" Name="InvoiceType" 
                PropertyName="Text" Type="String" />
        </WhereParameters>
    </cc11:ClassEntityDataSource>
