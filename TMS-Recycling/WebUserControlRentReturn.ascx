<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlRentReturn.ascx.cs" Inherits="TMS_Recycling.WebUserControlRentReturn" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="URLPopUpControl.ascx" tagname="URLPopUpControl" tagprefix="uc1" %>
<%@ Register src="WebUserControlEditAdvancePayments.ascx" tagname="WebUserControlEditAdvancePayments" tagprefix="uc2" %>
<%@ Register src="WebUserControlEditOrderMaterials.ascx" tagname="WebUserControlEditOrderMaterials" tagprefix="uc3" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc2" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<%@ Register src="CalendarWithTimeControl.ascx" tagname="CalendarWithTimeControl" tagprefix="uc4" %>

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
                    AutoPostBack="True" >
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
        <tr>
            <td>
                <asp:Label ID="LabelRentNumber" runat="server" Text="Volgnummer verhuring (optioneel)"></asp:Label>
            </td>
            <td>
            
                <asp:TextBox ID="TextBoxRentNumber" runat="server">0</asp:TextBox>
                <asp:MaskedEditExtender ID="TextBoxRentNumber_MaskedEditExtender" runat="server" 
                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                    Mask="999999999" TargetControlID="TextBoxRentNumber">
                </asp:MaskedEditExtender>
            
            </td>
        </tr>
    </table>
 </asp:Panel>

<asp:Panel ID="PanelRentOverview" runat="server">
<asp:Label ID="LabelRentedOutMaterials" runat="server" Text="Uitstaande verhuurmaterialen" CssClass="SubMenuHeader"></asp:Label>
    <asp:GridView ID="GridViewRentedOutMaterials" runat="server" 
        AllowSorting="True" DataSourceID="EntityDataSourceRentOuts" 
        PageSize="9999" AutoGenerateColumns="False" 
        onrowdatabound="GridViewRentedOutMaterials_RowDataBound">
        <Columns>
            <asp:TemplateField>
               <ItemTemplate>
                <asp:CheckBox runat="server" Text="Ingenomen" ID="CheckBoxItemReturned">
                </asp:CheckBox><br />
                <nobr>
                <asp:RadioButtonList runat="server" ID="RBListItemReturned" RepeatDirection="Horizontal" RepeatLayout="Flow">
                <asp:ListItem Value="Returned" Text="In orde" Selected="True"></asp:ListItem>
                <asp:ListItem Value="Damaged" Text="Beschadigd"></asp:ListItem>
                <asp:ListItem Value="Lost" Text="Verloren"></asp:ListItem>
                </asp:RadioButtonList>
                </nobr>
               </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="RentNumber" HeaderText="Verhuring" 
                SortExpression="RentNumber" />
            <asp:BoundField DataField="Description" HeaderText="Toelichting" 
                ReadOnly="True" SortExpression="Description" />
            <asp:TemplateField  HeaderText="Startdatum" SortExpression="RentStartDateTime">
                    <ItemTemplate>
                        <asp:TextBox ID="TextBoxRentStartDateTime" runat="server" Text='<%# Bind("RentStartDateTime") %>' ReadOnly="true" ToolTip ='<%# Bind("RentEndStartDateTime") %>' ></asp:TextBox>
                    </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField  HeaderText="Einddatum" SortExpression="RentEndStartDateTime">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox11" runat="server" Text='<%# Bind("RentEndStartDateTime") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <nobr>
                        <uc4:CalendarWithTimeControl ID="TextBoxRentEndDateTime" runat="server" Text='<%# Bind("RentEndStartDateTime") %>' />

                        <asp:Button ID="ButtonUpdate" runat="server" Text="Update" ToolTip="Update beschikbaarheid van vervangende materialen." onclick="ButtonUpdate_Click"  />
                        </nobr>
                    </ItemTemplate>
            </asp:TemplateField>

            <asp:BoundField DataField="VATRentPrice" HeaderText="BTW bedrag" 
                ReadOnly="True" SortExpression="VATRentPrice" 
                DataFormatString="{0:c}" />
            <asp:BoundField DataField="TotalRentPrice" HeaderText="Totaalprijs" 
                ReadOnly="True" SortExpression="TotalRentPrice" DataFormatString="{0:c}" />
            <asp:BoundField DataField="InvoiceStatus" HeaderText="Status" 
                ReadOnly="True" SortExpression="InvoiceStatus" />
            <asp:TemplateField>
               <ItemTemplate>
                   <asp:Label ID="LabelReplacedBy" runat="server" Text="Vervangen door"></asp:Label><br />
                   <cc11:ClassComboBox ID="ComboBoxReplacedBy" runat="server">
                    <asp:ListItem Value="" Text="-Nvt-" Selected="True"></asp:ListItem>
                   </cc11:ClassComboBox>
                   <br />
                   <uc1:URLPopUpControl ID="URLPopUpControlOpenRowInvoice" runat="server" Text="Toon factuur" />
               </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

</asp:Panel>

<asp:Panel ID="PanelInvoice" runat="server">
<asp:Label ID="LabelInvoice" runat="server" Text="Aanmaken factuur/teruggave borg" CssClass="SubMenuHeader"></asp:Label>
<br />
    <asp:CheckBox ID="CheckBoxInvoice" runat="server" 
        Text="Aangegeven verhuringen factureren (zonder borg)" Checked="True" /><br />
    <asp:CheckBox ID="CheckBoxBailReturn" runat="server" 
        Text="Genereer factuur voor teruggave borg" Checked="True" />&nbsp;&nbsp;
    <asp:Label ID="LabelBailAmount" runat="server" Text="Terug te geven hoeveelheid borg"></asp:Label>&nbsp;&nbsp;
    <asp:TextBox ID="TextBoxBail" runat="server">0</asp:TextBox>
    <br />
    <asp:Label ID="LabelLedger" runat="server" Text="Kasboek"></asp:Label> &nbsp;&nbsp;
    <cc11:ClassComboBox ID="ComboBoxLedger" runat="server" 
        DataSourceID="EntityDataSourceLocationLedger" DataTextField="Description" 
        DataValueField="Id">
    </cc11:ClassComboBox>&nbsp;&nbsp;
    <asp:Label ID="LabelDiscount" runat="server" Text="Factuurkorting"></asp:Label> &nbsp;&nbsp;
    <asp:TextBox ID="TextBoxDiscount" runat="server" Text="0"></asp:TextBox>

    <asp:MaskedEditExtender ID="TextBoxDiscount_MaskedEditExtender" runat="server" 
        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
        Mask="999" TargetControlID="TextBoxDiscount">
    </asp:MaskedEditExtender>

    <table style="vertical-align:text-top; width:100%;">
        <tr>
            <td>
                <asp:Label ID="LabelInvoiceNote" runat="server" Text="Factuurnotitie"></asp:Label>
            </td>
            <td style="width:80%">
                <asp:TextBox ID="TextBoxInvoiceNote" runat="server" MaxLength="1000" Rows="4" 
                    TextMode="MultiLine" Width="100%"></asp:TextBox>
            </td>
        </tr>
    </table>
    <asp:CheckBox ID="CheckBoxDisableDamagedAndLost" runat="server" ToolTip="Let op : naar het contact e-mail adres van de lokatie wordt een e-mail verzonden dat dit materiaal niet langer in productie is. Eventuele verhuringen van dit materiaal moeten dan worden herpland."
        
        Text="Beschadigde of verloren items (tijdelijk) uit verhuur nemen. Let op : U dient uit de verhuur genomen materialen met de hand te activeren op het moment dat deze weer beschikbaar zijn. " 
        Checked="True" />&nbsp;&nbsp;
</asp:Panel>

<asp:Panel ID="PanelCreatedInvoice" runat="server">
<asp:Label ID="LabelCreatedInvoice" runat="server" Text="Aangemaakte factuur" CssClass="SubMenuHeader"></asp:Label>
  <iframe id="FrameShowInvoiceA" scrolling="auto" runat="server" height="400px" width="100%">
  </iframe>
</asp:Panel>

<table width=100%><tr><td>
<asp:Button ID="ButtonPrevious" runat="server" Text="Vorige" 
        onclick="ButtonPrevious_Click" />
<asp:Button ID="ButtonNext" runat="server" Text="Volgende" 
        onclick="ButtonNext_Click" />
<asp:Button ID="ButtonProcess" runat="server" Text="Verwerk wijzigingen" 
        onclick="ButtonProcess_Click" />
<asp:Button ID="ButtonUnprocess" runat="server" Text="Wijzigingen terugdraaien" 
        onclick="ButtonUnprocess_Click" />
<asp:Button ID="ButtonNewRentReturn" runat="server" 
        Text="Volgende verhuring innemen" onclick="ButtonNewRentReturn_Click" />
</td><td align=right>

        <uc1:URLPopUpControl ID="URLPopUpControlOpenInvoice" runat="server" 
            Text="Toon factuur" />

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
where (it.IsActive = true) and (rl.Location.Id = @IdLoc or cast(@IdLoc as System.String) = &quot;00000000-0000-0000-0000-000000000000&quot;) and ( (os.InvoiceStatus = &quot;Open&quot;) or (os.InvoiceStatus=&quot;Invoiced&quot;) )
" EntityTypeFilter="">
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
    CommandText="select it.Id, ls.Id, rt.Id, it.[RentStartDateTime], it.[RentEndStartDateTime], 
rl.RentNumber, it.[Description], it.[IsTreatedAsAdvancePayment], it.[CalculatedRentPrice], it.[DiscountPercentage], it.[BaseRentPrice], it.[VATRentPrice], it.[TotalRentPrice], it.[InvoiceStatus]
from RentalItemActivitySet as it inner join RentLedgerSet as rl on it.RentLedger.Id = rl.Id inner join RentalItemSet as ri on it.RentalItem.Id = ri.Id inner join RentalTypeSet as rt on ri.RentalType.id = rt.id inner join LocationSet as ls on rl.Location.Id = ls.Id
where  (rl.Location.Id = @LocID or (cast(@LocID as System.String) = &quot;00000000-0000-0000-0000-000000000000&quot;))
and (rl.Relation.Id = @CustID or (cast(@CustID as System.String) = &quot;00000000-0000-0000-0000-000000000000&quot;)) and (rl.RentNumber = @RentNr or (@RentNr &lt;= 0)) and ( (it.InvoiceStatus = &quot;Open&quot;) or (it.InvoiceStatus=&quot;Invoiced&quot;) )
order by it.RentStartDateTime">
    <CommandParameters>
        <asp:ControlParameter ControlID="DropDownListLocations" DbType="Guid" 
            DefaultValue="00000000-0000-0000-0000-000000000000" Name="LocID" 
            PropertyName="SelectedValue" />
        <asp:ControlParameter ControlID="DropDownListCustomers" DbType="Guid" 
            DefaultValue="00000000-0000-0000-0000-000000000000" Name="CustID" 
            PropertyName="SelectedValue" />
        <asp:ControlParameter ControlID="TextBoxRentNumber" DbType="Int64" 
            DefaultValue="0" Name="RentNr" PropertyName="Text" />
    </CommandParameters>
</cc11:ClassEntityDataSource>

<cc11:ClassEntityDataSource ID="EntityDataSourceLocationLedger" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EnableFlattening="False" 
    EntitySetName="LedgerSet" OrderBy="it.Description" 
    Select="it.[Id], it.[Description], it.[IsActive]" 
    
    Where="it.IsActive and  ( (it.LimitToLocation is null)  or (it.LimitToLocation.Id = @LocId) or (cast(@LocId as System.String) = &quot;00000000-0000-0000-0000-000000000000&quot; ) )" 
    EntityTypeFilter="">
    <WhereParameters>
        <asp:ControlParameter ControlID="DropDownListLocations" DbType="Guid" 
            DefaultValue="00000000-0000-0000-0000-000000000000" Name="LocId" 
            PropertyName="SelectedValue" />
    </WhereParameters>
</cc11:ClassEntityDataSource>

<asp:Label ID="LabelCurrentPageNr" runat="server" Text="1" Visible="false"></asp:Label>
<asp:Label ID="LabelInvoiceId" runat="server" Text="" Visible="false"></asp:Label>
<asp:Label ID="LabelGroupId" runat="server" Text="" Visible="false"></asp:Label>
<asp:Label ID="LabelNewRIAId" runat="server" Text="" Visible="false"></asp:Label>
<asp:Label ID="LabelDisabledItems" runat="server" Text="" Visible="false"></asp:Label>









