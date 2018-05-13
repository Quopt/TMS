<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlInvoiceOverview.ascx.cs" Inherits="TMS_Recycling.WebUserControlInvoiceOverview" %>
<%@ Register src="~/CalendarControl.ascx" tagname="CalendarControl" tagprefix="uc1" %><%@ Register src="~/WebUserControlInvoiceBase.ascx" tagname="WebUserControlInvoiceBase" tagprefix="uc3" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit.HTMLEditor" tagprefix="cc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
    <%@ Register src="ComboBoxLocation.ascx" tagname="ComboBoxLocation" tagprefix="uc2" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

    <table width="100%" class="filterpanel">
    <tr><td><asp:Label ID="LabelFilter" runat="server" Text="Filter" 
            CssClass="filterheader"></asp:Label></td></tr>
            <tr>
            <td>
                <asp:Label ID="LabelOrderNo" runat="server" Text="Volgnummer"></asp:Label>&nbsp;
            </td><td>
                <asp:TextBox ID="TextBoxInvoiceNo" runat="server" MaxLength="15" Width="50%"></asp:TextBox>
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
    </cc11:ClassComboBox>
</td>
<td>
    <asp:Label ID="LabelLocation" runat="server" Text="Lokatie"></asp:Label>
</td>
<td>
        <uc2:ComboBoxLocation ID="TextBoxLocation" runat="server" />
</td>
<td>
    &nbsp;</td>
<td>
        &nbsp;</td>
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
        </td><td>
            <asp:Label ID="LabelInvoiceType" runat="server" Text="Buy" Visible="False"></asp:Label>
            <asp:Label ID="LabelSubInvoiceType" runat="server" Text="Purchase" Visible="False"></asp:Label>
        </td>
                        <td> 
                            <asp:Button ID="ButtonSearch" runat="server" Text="Zoeken/verversen" 
                                CssClass="sleekbutton" onclick="ButtonSearch_Click" /></td>
                        </tr>
    </table>

    <table class="resultspanel"><tr><td>
    <cc11:ClassGridView ID="GridViewSelectedInvoices" runat="server" AllowPaging="True" 
        AllowSorting="True" DataSourceID="EntityDataSourcePurchaseInvoices" 
        AutoGenerateColumns="False" DataKeyNames="Id" DataMember="DefaultView" 
            onselectedindexchanged="GridViewSelectedInvoices_SelectedIndexChanged" 
            onrowdatabound="GridViewSelectedInvoices_RowDataBound" 
            EmptyDataText="Er zijn geen gegevens gevonden die voldoen aan uw zoekcriteria." 
            ShowHeaderWhenEmpty="True">
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
            <asp:BoundField DataField="InvoiceNumber" HeaderText="Volgnummer" 
                SortExpression="InvoiceNumber" />
            <asp:BoundField DataField="BookingDateTime" HeaderText="Boekdatum" 
                SortExpression="BookingDateTime" />
            <asp:BoundField DataField="RelationName" HeaderText="Leverancier" 
                SortExpression="RelationName" />
            <asp:BoundField DataField="Description" HeaderText="Omschrijving" 
                SortExpression="Description" />
            <asp:BoundField DataField="InvoiceStatus" HeaderText="Status" 
                SortExpression="InvoiceStatus" />
            <asp:CheckBoxField DataField="IsCorrected" HeaderText="Gecorrigeerd" 
                SortExpression="IsCorrected" />
            <asp:BoundField DataField="LocationName" HeaderText="Inkooplokatie" 
                SortExpression="LocationName" />
            <asp:BoundField DataField="VATPrice" DataFormatString="{0:c}" HeaderText="BTW" 
                SortExpression="VATPrice" />
            <asp:BoundField DataField="TotalPrice" DataFormatString="{0:c}" 
                HeaderText="Bedrag" SortExpression="TotalPrice" />
        </Columns>
    </cc11:ClassGridView>
        <br />
        <asp:Button ID="ButtonNew" runat="server" onclick="ButtonNew_Click" 
            Text="Nieuwe factuur aanmaken of inboeken" Visible="False" />
    </td></tr></table>

    <table class="detailpanel"><tr><td>


        <uc3:WebUserControlInvoiceBase ID="WebUserControlInvoiceBase1" runat="server" />


    </td></tr></table>


    <cc11:ClassEntityDataSource ID="EntityDataSourcePurchaseInvoices" runat="server" 
        ConnectionString="name=ModelTMSContainer" 
        DefaultContainerName="ModelTMSContainer" Where="" CommandText="select distinct it.Id, it.Description, it.InvoiceNumber, it.InvoiceStatus, it.BookingDateTime, it.InvoiceStatus, 
	it.YourReference, it.Price, it.VATPrice, it.TotalPrice, it.IsCorrected, rs.Description as RelationName, 
	ls.Description as LocationName
from InvoiceSet as it 
    left join RelationSet as rs on it.Relation.Id = rs.Id and (rs.Description LIKE @RelationDescription)
	left join LocationSet as ls on it.Location.Id = ls.Id 
    left join InvoiceLineSet as ol on it.Id = ol.Invoice.Id 
    left join MaterialSet as ms on ol.Material.Id = ms.Id 
where (it.InvoiceType = @InvoiceType) and
	(it.Description LIKE  @Description  ) and (cast(it.InvoiceNumber as System.String) LIKE @InvoiceNumber ) and 
	(it.BookingDateTime BETWEEN @StartDate and @EndDate) and (it.InvoiceStatus LIKE @InvoiceStatus)  and 
	(cast(it.IsCorrected as System.String) LIKE @CorrectedStatus) and 
        (it.InvoiceSubType = @InvoiceSubType) and 
       ( (ms.Description LIKE  @MaterialDescription) or (@MaterialDescription = &quot;%&quot;)) and 
        ( (ls.Description LIKE @LocationDescription) or (@LocationDescription = &quot;%&quot;)) and
       ((rs.Description LIKE @RelationDescription) or (@RelationDescription = &quot;%&quot;))  " 
        OrderBy="it.BookingDateTime desc" EntitySetName="" EntityTypeFilter="" 
    Select="">
        <CommandParameters>
            <asp:FormParameter Name="Description" FormField="TextBoxFilterName.Text" Type="String" />
            <asp:Parameter Name="InvoiceNumber" Type="String" />
            <asp:Parameter Name="StartDate" Type="DateTime" />
            <asp:Parameter Name="EndDate"  Type="DateTime" />
            <asp:Parameter Name="InvoiceStatus" Type="String" />
            <asp:Parameter Name="MaterialDescription" Type="String" />
            <asp:Parameter Name="RelationDescription" Type="String" />
            <asp:Parameter Name="CorrectedStatus" Type="String" />
            <asp:Parameter Name="LocationDescription" Type="String" />
            <asp:ControlParameter ControlID="LabelInvoiceType" Name="InvoiceType" 
                PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="LabelSubInvoiceType" Name="InvoiceSubType" 
                PropertyName="Text" Type="String" />
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
