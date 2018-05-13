<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlRentLedgerOverview.ascx.cs" Inherits="TMS_Recycling.WebUserControlRentLedgerOverview" %>
<%@ Register src="~/CalendarControl.ascx" tagname="CalendarControl" tagprefix="uc1" %><%@ Register src="~/WebUserControlOrderBase.ascx" tagname="WebUserControlOrderBase" tagprefix="uc2" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit.HTMLEditor" tagprefix="cc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="WebUserControlRentLedgerBase.ascx" tagname="WebUserControlRentLedgerBase" tagprefix="uc3" %>
<%@ Register src="ComboBoxLocation.ascx" tagname="ComboBoxLocation" tagprefix="uc4" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

  <table width="100%" class="filterpanel">
    <tr><td><asp:Label ID="LabelFilter" runat="server" Text="Filter" 
            CssClass="filterheader"></asp:Label></td></tr>
            <tr>
            <td>
                <asp:Label ID="LabelOrderNo" runat="server" Text="Volgnummer verhuur"></asp:Label>
            </td><td width="200px">
                <asp:TextBox ID="TextBoxOrderNo" runat="server" MaxLength="15" Width="50%"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="LabelStartDate" runat="server" Text="Startdatum selectie"></asp:Label>
            </td><td>
                    <uc1:CalendarControl ID="CalendarControlStartDate" runat="server" />
                </td>
            <td>
                <asp:Label ID="LabelEndDate" runat="server" Text="Einddatum selectie"></asp:Label>
            </td>
            <td>
                <uc1:CalendarControl ID="CalendarControlEndDate" runat="server" />
                </td>
            </tr>

<tr>
<td>
    <asp:Label ID="LabelMaterialType" runat="server" Text="Materiaaltype"></asp:Label>
</td>
<td>
        <asp:TextBox ID="TextBoxFilterMaterialType" runat="server" MaxLength="255" 
        Width="90%"></asp:TextBox>
</td>
<td>
    <asp:Label ID="LabelCustomer" runat="server" Text="Huurder"></asp:Label>
</td>
<td>
        <asp:TextBox ID="TextBoxCustomer" runat="server" MaxLength="40" Width="90%"></asp:TextBox>
</td>
<td>
                <asp:Label ID="LabelEndDate0" runat="server" 
        Text="Lokatie"></asp:Label>
            </td>
<td>
        <uc4:ComboBoxLocation ID="ComboBoxLocationDescription" runat="server" />
    </td>
</tr>

<tr>
<td>
    <asp:Label ID="LabelContainsMaterial" runat="server" Text="Bevat materiaal"></asp:Label>
</td>
<td>
        <asp:TextBox ID="TextBoxFilterMaterial" runat="server" MaxLength="255" 
        Width="90%"></asp:TextBox>
</td>
<td>
        <asp:Label ID="LabelName" runat="server" Text="Toelichting"></asp:Label> 
</td>
<td>
        <asp:TextBox ID="TextBoxFilterName" runat="server" MaxLength="255" Width="90%"></asp:TextBox>
</td>
<td>
        <asp:Label ID="LabelMaterialStatus" runat="server" Text="Status"></asp:Label> 
    </td>
<td>
        <cc11:ClassComboBox ID="ComboBoxInvoiceStatus" runat="server" 
            AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList">
            <asp:ListItem Value="">-nvt-</asp:ListItem>
        </cc11:ClassComboBox>
    </td>
</tr>
    <tr><td>
        &nbsp;</td><td> 
            &nbsp;</td><td>
            &nbsp;</td>
    <td>
        &nbsp;</td><td>
            &nbsp;</td>
                        <td> 
                            <asp:Button ID="ButtonSearch" runat="server" Text="Zoeken/verversen" 
                                CssClass="sleekbutton" onclick="ButtonSearch_Click" /></td>
                        </tr>
    </table>

    <table class="resultspanel"><tr><td>

        <cc11:ClassGridView ID="GridViewRentLedger" runat="server" AutoGenerateColumns="False" 
            DataSourceID="EntityDataSourceLedgerBase" 
            onselectedindexchanged="GridViewRentLedger_SelectedIndexChanged" 
            DataKeyNames="Id" AllowPaging="True" AllowSorting="True">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="RentNumber" HeaderText="Volgnummer" ReadOnly="True" 
                    SortExpression="RentNumber" />
                <asp:BoundField DataField="Description" HeaderText="Omschrijving verhuur" 
                    ReadOnly="True" SortExpression="Description" />
                <asp:BoundField DataField="MaterialType" HeaderText="Soort materiaal" 
                    ReadOnly="True" SortExpression="MaterialType" />
                <asp:BoundField DataField="Material" HeaderText="Materiaal" 
                    ReadOnly="True" SortExpression="Material" />
                <asp:BoundField DataField="RentStartDateTime" HeaderText="Aanvang huur" 
                    ReadOnly="True" SortExpression="RentStartDateTime" />
                <asp:BoundField DataField="RentEndStartDateTime" 
                    HeaderText="Einde huur" ReadOnly="True" 
                    SortExpression="RentEndStartDateTime" />
                <asp:BoundField DataField="BaseRentPrice" HeaderText="Prijs" 
                    ReadOnly="True" SortExpression="BaseRentPrice" />
                <asp:BoundField DataField="VATRentPrice" HeaderText="BTW" 
                    ReadOnly="True" SortExpression="VATRentPrice" />
                <asp:BoundField DataField="TotalRentPrice" HeaderText="Totaal" 
                    ReadOnly="True" SortExpression="TotalRentPrice" />
                <asp:BoundField DataField="InvoiceStatus" HeaderText="Materiaalstatus" ReadOnly="True" 
                    SortExpression="InvoiceStatus" />
                <asp:BoundField DataField="Relation" HeaderText="Huurder" ReadOnly="True" 
                    SortExpression="Relation" />
            </Columns>
        </cc11:ClassGridView>

    </td></tr></table>

    <table class="detailpanel"><tr><td>
<uc3:WebUserControlRentLedgerBase ID="WebUserControlRentLedgerBase1" 
    runat="server" Visible="false" />
    </td></tr></table>

<cc11:ClassEntityDataSource ID="EntityDataSourceLedgerBase" runat="server" CommandText="select it.RentNumber, ria.Id, itx.Description as MaterialType,  ria.RentStartDateTime, ria.RentEndStartDateTime, ria.BaseRentPrice, 
ria.VATRentPrice, ria.TotalRentPrice, ria.InvoiceStatus, ri.Description as Material, rel.Description as Relation, it.Description as Description
from RentLedgerSet as it 
    inner join RentalItemActivitySet as ria on ria.RentLedger.Id = it.Id
    inner join RentalItemSet as ri on ri.Id = ria.RentalItem.Id
	inner join RentalTypeSet as itx on itx.Id = ri.RentalType.Id
    inner join LocationSet as ls on ri.Location.Id = ls.Id
	inner join RelationSet as rel on it.Relation.Id = rel.Id
where (itx.Description like @MaterialType) and (ls.Description like @LocationDescription) and
      (cast(it.RentNumber as System.String) like @RentNumber) and
      ( (ria.RentStartDateTime between @StartDate and @EndDate) or (ria.RentEndStartDateTime between @StartDate and @EndDate) ) and
      (rel.Description like @Relation) and 
      (ri.Description like @Material) and 
      (it.Description like @Description) and 
      (ria.InvoiceStatus like @InvoiceStatus)
      order by it.RentNumber desc 
" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" OrderBy="it.RentNumber desc">
    <CommandParameters>
        <asp:Parameter Name="Material" Type="String" />
        <asp:Parameter Name="Description" Type="String" />
        <asp:Parameter Name="LocationDescription" Type="String" />
        <asp:Parameter Name="Relation" Type="String" />
        <asp:Parameter Name="StartDate" Type="DateTime" />
        <asp:Parameter Name="EndDate"  Type="DateTime"/>
        <asp:Parameter Name="RentNumber" Type="String"/>
        <asp:Parameter Name="MaterialType" Type="String" />
        <asp:Parameter Name="InvoiceStatus" Type="String" />
    </CommandParameters>
</cc11:ClassEntityDataSource>


<cc11:ClassEntityDataSource ID="EntityDataSourceMaterialType" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EntitySetName="RentalTypeSet" 
    OrderBy="it.Description" Select="it.[Id], it.[Description], it.[IsActive]" 
    Where="it.IsActive">
</cc11:ClassEntityDataSource>



