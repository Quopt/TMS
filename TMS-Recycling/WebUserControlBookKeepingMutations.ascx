<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlBookKeepingMutations.ascx.cs" Inherits="TMS_Recycling.WebUserControlBookKeepingMutations" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>
<%@ Register src="WebUserControlLedgerMutation.ascx" tagname="WebUserControlLedgerMutation" tagprefix="uc1" %>
<%@ Register src="CalendarControl.ascx" tagname="CalendarControl" tagprefix="uc2" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="ComboBoxLocation.ascx" tagname="ComboBoxLocation" tagprefix="uc3" %>

    <asp:Label ID="LabelLedgerName" runat="server" Text="..." 
        CssClass="SubMenuHeader"></asp:Label>
    <table width="100%" class="filterpanel">
    <tr><td><asp:Label ID="LabelFilter" runat="server" Text="Filter" 
            CssClass="filterheader"></asp:Label></td></tr>
            <tr>
            <td>
                <asp:Label ID="LabelStartDate" runat="server" Text="Startdatum selectie"></asp:Label>&nbsp;
            </td><td>
                    <uc2:CalendarControl ID="CalendarControlStartDate" runat="server" />
                </td>
            <td>
                <asp:Label ID="LabelEndDate" runat="server" Text="Einddatum selectie"></asp:Label>&nbsp;
            </td>
            <td>
                <uc2:CalendarControl ID="CalendarControlEndDate" runat="server" />
                </td>
            </tr>

<tr>
<td>
    <asp:Label ID="LabelBookingType" runat="server" Text="Boekingstype"></asp:Label>
</td>
<td>
    <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" ID="DropDownListBookingType" runat="server">
    </cc11:ClassComboBox>
</td>
<td>
    <asp:Label ID="LabelPerson" runat="server" Text="Aangemaakt door"></asp:Label>
</td>
<td>
        <asp:TextBox ID="TextBoxUserName" runat="server" MaxLength="40" Width="90%"></asp:TextBox>
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
        <asp:ListItem Value="0">-nvt-</asp:ListItem>
        <asp:ListItem Value="1">Ja</asp:ListItem>
        <asp:ListItem Value="2">Nee</asp:ListItem>
    </cc11:ClassComboBox>
        &nbsp;&nbsp; 
                            </td>
                        </tr>

<tr>
<td>
        <asp:Label ID="LabelName0" runat="server" Text="Lokatie"></asp:Label> 
</td>
<td>
    <uc3:ComboBoxLocation ID="ComboBoxLocationDescription" runat="server" />
</td>
<td>
</td>
<td>
                            <asp:Button ID="ButtonSearch" runat="server" Text="Zoeken/verversen" 
                                CssClass="sleekbutton" onclick="ButtonSearch_Click" />
</td>
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
                <asp:BoundField DataField="Id" HeaderText="Id" 
                    SortExpression="Id" ReadOnly="True" Visible="False" />
                <asp:BoundField DataField="BookingDateTime" HeaderText="Boekingsdatum en -tijd" 
                    ReadOnly="True" SortExpression="BookingDateTime" />
                <asp:BoundField DataField="BookingType" HeaderText="Soort boeking" ReadOnly="True" 
                    SortExpression="BookingType" />
                <asp:BoundField DataField="Description" HeaderText="Toelichting" 
                    ReadOnly="True" SortExpression="Description" />
                <asp:BoundField DataField="AmountEXVat" HeaderText="Bedrag" 
                    ReadOnly="True" SortExpression="AmountEXVat" />
                <asp:BoundField DataField="VATAmount" HeaderText="BTW" ReadOnly="True" 
                    SortExpression="VATAmount" />
                <asp:BoundField DataField="TotalAmount" HeaderText="Totaal bedrag" 
                    ReadOnly="True" SortExpression="TotalAmount" />
                <asp:CheckBoxField DataField="IsCorrection" HeaderText="Is correctie" 
                    SortExpression="IsCorrection" />
                <asp:BoundField DataField="LocationDescription" HeaderText="Lokatie" 
                    ReadOnly="True" SortExpression="LocationDescription" />
                <asp:BoundField DataField="RelationDescription" HeaderText="Relatie" 
                    ReadOnly="True" SortExpression="RelationDescription" />
                <asp:BoundField DataField="MutationUser" HeaderText="Door gebruiker" 
                    ReadOnly="True" SortExpression="MutationUser" />
            </Columns>
        </cc11:ClassGridView>
    </td></tr></table>
    <table class="detailpanel"><tr><td>

        <uc1:WebUserControlLedgerMutation ID="WebUserControlLedgerMutation1" 
            runat="server" Visible="False" />

    </td></tr></table>
    <cc11:ClassEntityDataSource ID="EntityDataSourceGridBase" runat="server" 
        ConnectionString="name=ModelTMSContainer" 
        DefaultContainerName="ModelTMSContainer" 
        Where="" 
        CommandText="
SELECT it.[Id],  it.[Description], it.[AmountEXVat], it.[VATAmount], it.[TotalAmount], it.BookingDateTime, it.BookingType, ls.Description as LocationDescription, Rs.Description as RelationDescription, Sm.Description as MutationUser, it.IsCorrection
FROM LedgerMutationSet as it 
     left join LocationSet as ls on it.Location.Id = ls.Id 
     left join RelationSet as Rs on it.Relation.Id = Rs.Id 
     left join StaffMemberSet as Sm on it.ModifyUser = Sm.Id
WHERE (it.Description LIKE @Description)  and ((it.Ledger.Id = @Id) or (it.LedgerBookingCode.Id = @Id)) and (it.BookingType LIKE @BookingType) and (it.BookingDateTime BETWEEN @StartDate and @EndDate) 
     and ( ( (it.ModifyUser == @EmptyGUID) and (@UserName == &quot;%&quot;) ) or (sm.Description like @UserName) ) and (( it.IsCorrection == @CorrectedStatus ) or  ( it.IsCorrection == @CorrectedStatus2 ))
     and ( (ls.Description LIKE @LocationDescription) or (@LocationDescription = &quot;%&quot;))
order by it.BookingDateTime DESC" 
        Include="" OrderBy="it.BookingDateTime DESC">
        <CommandParameters>
            <asp:FormParameter DefaultValue=" " FormField="TextBoxFilterName.text" 
                Name="Description" Type="String" />
            <asp:QueryStringParameter DbType="Guid" 
                DefaultValue="00000000-0000-0000-0000-000000000000" Name="Id" 
                QueryStringField="Id" />
            <asp:FormParameter DefaultValue="" 
                FormField="CalendarControlStartDate.SelectedDate" Name="StartDate" 
                Type="DateTime" />
            <asp:FormParameter 
                FormField="CalendarControlEndDate.SelectedDate" Name="EndDate" 
                DefaultValue="" Type="DateTime" />
            <asp:FormParameter DbType="String" 
                FormField="DropDownListBookingType.SelectedValue" Name="BookingType" />
            <asp:FormParameter DbType="String" FormField="TextBoxUserName.Text" 
                Name="UserName" DefaultValue="" />
            <asp:Parameter DbType="Guid" 
                Name="EmptyGUID" DefaultValue="" />
            <asp:Parameter DefaultValue="True" Name="CorrectedStatus" Type="Boolean" />
            <asp:Parameter DefaultValue="False" Name="CorrectedStatus2" Type="Boolean" />
            <asp:Parameter Name="LocationDescription" Type="String" />
        </CommandParameters>

    </cc11:ClassEntityDataSource>

