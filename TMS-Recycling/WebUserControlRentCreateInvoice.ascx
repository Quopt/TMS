<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlRentCreateInvoice.ascx.cs" Inherits="TMS_Recycling.WebUserControlRentCreateInvoice" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="URLPopUpControl.ascx" tagname="URLPopUpControl" tagprefix="uc1" %>
<%@ Register src="WebUserControlEditAdvancePayments.ascx" tagname="WebUserControlEditAdvancePayments" tagprefix="uc2" %>
<%@ Register src="WebUserControlEditOrderMaterials.ascx" tagname="WebUserControlEditOrderMaterials" tagprefix="uc3" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc2" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Panel ID="PanelCustomerInformation" runat="server">
<asp:Label ID="LabelBasicData" runat="server" Text="Basisgegevens klant" CssClass="SubMenuHeader"></asp:Label>
    <table style="width: 100%;">
        <tr>
            <td  style="width:200px;">
                <asp:Label ID="LabelPurchaseLocation" runat="server" Text="Inkoop lokatie"></asp:Label>
            </td>
            <td>
                <cc2:ClassComboBoxLocation ID="DropDownListLocations" runat="server" 
                    AutoCompleteMode="SuggestAppend" DataSourceID="EntityDataSourceLocations" 
                    DataTextField="Description" DataValueField="Id" 
                    DropDownStyle="DropDownList" AppendDataBoundItems="True" 
                    AutoPostBack="True" 
                    onselectedindexchanged="DropDownListLocations_SelectedIndexChanged">
                    <asp:ListItem Selected="True" Value="00000000-0000-0000-0000-000000000000">Alle lokaties</asp:ListItem>
                </cc2:ClassComboBoxLocation>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LabelCustomer" runat="server" Text="Klant"></asp:Label>
            </td>
            <td>
                <cc11:ClassComboBox ID="DropDownListCustomers" runat="server" 
                    AutoCompleteMode="SuggestAppend" DataSourceID="EntityDataSourceCustomers" 
                    DataTextField="Description" DataValueField="Id" DropDownStyle="DropDownList" 
                    MaxLength="0" style="display: inline;">
                </cc11:ClassComboBox>
            </td>
        </tr>
    </table>
 </asp:Panel>

<asp:Panel ID="PanelRentalSelection" runat="server">
<asp:Label ID="LabelRentOutSelection" runat="server" Text="Selecteer de te factureren verhuringen" CssClass="SubMenuHeader"></asp:Label>
    <table style="width: 100%;">
        <tr>
            <td>

                <asp:GridView ID="GridViewSelectedRentOuts" runat="server" 
                    AutoGenerateColumns="False" DataSourceID="EntityDataSourceRentOuts" 
                    PageSize="9999" onrowdatabound="GridViewSelectedRentOuts_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Geselecteerd">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBoxSelected" runat="server" Checked='false' />
                            </ItemTemplate>                        
                        </asp:TemplateField> 
                        <asp:BoundField DataField="RentNumber" HeaderText="Nr" 
                            ReadOnly="True" SortExpression="RentNumber" />
                        <asp:BoundField DataField="Description" HeaderText="Toelichting" 
                            ReadOnly="True" SortExpression="Description" />
                        <asp:BoundField DataField="RentStartDateTime" HeaderText="Ingang huur" 
                            ReadOnly="True" SortExpression="RentStartDateTime" />
                        <asp:BoundField DataField="RentEndStartDateTime" HeaderText="Tot" 
                            ReadOnly="True" SortExpression="RentEndStartDateTime" />
                        <asp:CheckBoxField DataField="IsTreatedAsAdvancePayment" 
                            HeaderText="Als voorschot behandelen" ReadOnly="True" 
                            SortExpression="IsTreatedAsAdvancePayment" />
                        <asp:BoundField DataField="CalculatedRentPrice" HeaderText="Berekende prijs" 
                            ReadOnly="True" SortExpression="CalculatedRentPrice" />
                        <asp:BoundField DataField="DiscountPercentage" HeaderText="Korting" 
                            ReadOnly="True" SortExpression="DiscountPercentage" />
                        <asp:BoundField DataField="BaseRentPrice" HeaderText="Basis prijs" 
                            ReadOnly="True" SortExpression="BaseRentPrice" />
                        <asp:BoundField DataField="VATRentPrice" HeaderText="BTW" ReadOnly="True" 
                            SortExpression="VATRentPrice" />
                        <asp:BoundField DataField="TotalRentPrice" HeaderText="Totaal" ReadOnly="True" 
                            SortExpression="TotalRentPrice" />
                    </Columns>
                </asp:GridView>

            </td>
        </tr>
    </table>
</asp:Panel>

<asp:Panel ID="PanelInvoiceDetails" runat="server">
<asp:Label ID="LabelOrderSummary" runat="server" Text="Factuurdetails" CssClass="SubMenuHeader"></asp:Label>

<table style="width: 100%;">
<tr>
<td class="style1">
    <asp:Label ID="LabelTotalItems" runat="server" Text="# items"></asp:Label>
</td>
<td>
    <asp:Label ID="LabelTotalItemsValue" runat="server" Text="0"></asp:Label>
</td>
</tr><tr>
    <td class="style1">
        <asp:Label ID="LabelTotalPrice" runat="server" Text="Totaal verhuurprijs"></asp:Label>
    </td>
    <td>
        <asp:Label ID="LabelTotalPriceValue" runat="server" Text="0.00"></asp:Label>
    </td>
    </tr>
<tr>
        <td class="style1">
            <asp:Label ID="LabelInvoiceDescription" runat="server" Text="Totaal borg"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="TextBoxDescription" Width="80%" runat="server"></asp:TextBox>
        </td>
        </tr>            
    <tr>
            <td class="style1">
                <asp:Label ID="LabelTotalBail" runat="server" Text="Totaal borg"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBoxBail" Text="0" runat="server"></asp:TextBox>
            </td>
        </tr>
    <tr>
        <td class="style1">
            <asp:Label ID="LabelInvoiceDiscount" runat="server" Text="Factuurkorting"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="TextBoxInvoiceDiscount" Text="0" runat="server"></asp:TextBox>
            <asp:MaskedEditExtender ID="TextBoxInvoiceDiscount_MaskedEditExtender" 
                runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                Mask="999" TargetControlID="TextBoxInvoiceDiscount">
            </asp:MaskedEditExtender>
        </td>
    </tr>
    <tr>
        <td class="style1" valign="top">
            <asp:Label ID="LabelInvoiceNote" runat="server" Text="Factuur notitie"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="TextBoxInvoiceNote" runat="server" Rows="4" 
                TextMode="MultiLine" Width="90%"></asp:TextBox>
        </td>
    </tr>
</table>
</asp:Panel>


<asp:Panel ID="PanelPreviewInvoice" runat="server">
<asp:Label ID="LabelPreviewInvoice" runat="server" Text="Printvoorbeeld verhuring" CssClass="SubMenuHeader"></asp:Label>
    <br />
  <iframe id="FrameShowInvoice" scrolling="auto" runat="server" height="400px" width="100%">
  </iframe>
</asp:Panel>

<table width="100%">
<tr>
<td>
<asp:Button ID="ButtonPrevious" runat="server" Text="Vorige"  Visible="false" onclick="ButtonPrevious_Click"/>
<asp:Button ID="ButtonNext"  runat="server" Text="Volgende"  Visible="false" onclick="ButtonNext_Click"/>
<asp:Button ID="ButtonNext2"  runat="server" Text="Factuur aanmaken"  Visible="false" onclick="ButtonNext_Click"/>
<asp:Button ID="ButtonCorrect" runat="server" Text="Factuur corrigeren" Visible="false" onclick="ButtonCorrect_Click" />
<asp:Button ID="ButtonNew" runat="server" Text="Nieuwe verhuurfactuur maken"  Visible="false" onclick="ButtonNew_Click"/>
</td>
<td align=right>
    <uc1:URLPopUpControl ID="URLPopUpOpenInvoice" runat="server" 
        Text="Open factuur" />
</td></tr></table>

 <cc11:ClassEntityDataSource ID="EntityDataSourceLocations" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EnableFlattening="False" 
    EntitySetName="LocationSet" Select="it.[Description], it.[Id], it.[IsActive]" 
    Where="it.IsActive = true">
</cc11:ClassEntityDataSource>

<cc11:ClassEntityDataSource ID="EntityDataSourceCustomers" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EnableFlattening="False" 
    
    Where="" CommandText="select distinct it.Id, it.Description
from RentLedgerSet as rl inner join RentalItemActivitySet as os on os.RentLedger.Id = rl.Id
 inner join RelationSet as it on rl.Relation.Id = it.Id
where (it.IsActive = true) and (os.InvoiceLine is null)  and (rl.Location.Id = @IdLoc or cast(@IdLoc as System.String) = &quot;00000000-0000-0000-0000-000000000000&quot;)
" onselecting="EntityDataSourceCustomers_Selecting" EntityTypeFilter="">
    <CommandParameters>
        <asp:ControlParameter ControlID="DropDownListLocations" Name="IdLoc" 
            PropertyName="SelectedValue" 
            DefaultValue="00000000-0000-0000-0000-000000000000" DbType="Guid" />
    </CommandParameters>
</cc11:ClassEntityDataSource>
<cc11:ClassEntityDataSource ID="EntityDataSourceRentOuts" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" 
    OrderBy="" 
    Where="" 
    CommandText="select
it.[Description], it.[RentStartDateTime], it.[RentEndStartDateTime], it.[IsTreatedAsAdvancePayment], it.[CalculatedRentPrice], it.[DiscountPercentage], it.[BaseRentPrice], it.[VATRentPrice], it.[TotalRentPrice], it.[InvoiceStatus], it.Id, rl.RentNumber
from RentalItemActivitySet as it inner join RentLedgerSet as rl on it.RentLedger.Id = rl.Id
where (it.InvoiceLine is null) and (rl.Location.Id = @LocID or (cast(@LocID as System.String) = &quot;00000000-0000-0000-0000-000000000000&quot;))
and (rl.Relation.Id = @CustID or (cast(@CustID as System.String) = &quot;00000000-0000-0000-0000-000000000000&quot;))
order by it.RentStartDateTime" EntitySetName="" EntityTypeFilter="" Select="">
    <CommandParameters>
        <asp:ControlParameter ControlID="DropDownListLocations" DbType="Guid" 
            DefaultValue="00000000-0000-0000-0000-000000000000" Name="LocID" 
            PropertyName="SelectedValue" />
        <asp:ControlParameter ControlID="DropDownListCustomers" DbType="Guid" 
            DefaultValue="00000000-0000-0000-0000-000000000000" Name="CustID" 
            PropertyName="SelectedValue" />
    </CommandParameters>
</cc11:ClassEntityDataSource>

<asp:Label ID="LabelCurrentPageNr" runat="server" Text="1"  Visible="false"></asp:Label>
<asp:Label ID="LabelGeneratedInvoiceNr" runat="server" Text="1"  Visible="false"></asp:Label>
<asp:Label ID="LabelGeneratedInvoiceId" runat="server" Text="1"  Visible="false"></asp:Label>
<asp:Label ID="LabelGroupId" runat="server" Text=""  Visible="false"></asp:Label>


