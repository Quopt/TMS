﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlInvoiceBase.ascx.cs" Inherits="TMS_Recycling.WebUserControlInvoiceBase" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit.HTMLEditor" tagprefix="cc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="CalendarWithTimeControl.ascx" tagname="CalendarWithTimeControl" tagprefix="uc1" %>
<%@ Register src="URLPopUpControl.ascx" tagname="URLPopUpControl" tagprefix="uc2" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<table width="100%">
<tr>
 <td colspan="2">
     <asp:Label ID="LabelBasicData" runat="server" 
         Text="Basisgegevens factuur" CssClass="SubMenuHeader"></asp:Label></td>
 <td>    &nbsp;</td>
 <td>    &nbsp;</td>
 <td>  &nbsp;   </td>
 <td>   
            <nobr>
            <asp:CheckBox ID="CheckBox_IsCorrected_Checked" Text="Is gecorrigeerd" runat="server" 
                Checked="True" /> 
            </nobr>
    </td>
</tr>

<tr>
<td>
    <asp:Label ID="LabelName" runat="server" Text="Factuuromschrijving"></asp:Label> </td>
    <td colspan="3"> <asp:TextBox ID="TextBox_Description_Text" runat="server" Width="90%" 
            MaxLength="250" ></asp:TextBox>
    </td>
    <td>
    <nobr>
    <asp:Label ID="LabelInvoiceNumber" runat="server" Text="Factuurnummer"></asp:Label> 
    </nobr>
    </td>
        <td>
    <asp:Label ID="Label_InvoiceNumber" runat="server" Text="..."></asp:Label> 
    </td>
</tr>

<tr>
<td>
    <asp:Label ID="LabelInvoiceStatus" runat="server" Text="Factuurstatus"></asp:Label> 
</td>
<td style="vertical-align:top;">
        <nobr>
        <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" ID="DropDownList_InvoiceStatus_SelectedValue" runat="server" 
                Enabled="False"></cc11:ClassComboBox> 
        <asp:Button ID="ButtonToProcessed" runat="server" Text="Status naar betaald" 
            onclick="ButtonToProcessed_Click"></asp:Button>
        </nobr>
        <br /><br />
            <asp:Label ID="LabelProcessingInfo" runat="server" Text="LET OP : Zet de status van de factuur op betaald en u kunt niets meer aan deze factuur wijzigen."></asp:Label>
</td>
<td>
    <asp:Label ID="LabelBookDate" runat="server" Text="Boekdatum"></asp:Label> 
</td>
<td>
    <uc1:CalendarWithTimeControl ID="CalendarWithTimeControl_BookingDateTime_SelectedDateTime" runat="server" />
</td>
<td>
    <asp:Label ID="LabelLedger" runat="server" Text="Kasboek"></asp:Label> 
</td>
<td>
    <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" ID="DropDownList_Ledger" runat="server" 
        DataSourceID="EntityDataSourceLedgers" DataTextField="Description" 
        DataValueField="Id"></cc11:ClassComboBox>
</td>
</tr>

<tr>
    <td>    <asp:Label ID="LabelRelation" runat="server" Text="Relatie"></asp:Label> </td>
    <td>
        <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" ID="DropDownList_Relation" runat="server" 
            DataSourceID="EntityDataSourceRelations" DataTextField="Description" 
            DataValueField="Id" AutoPostBack="True"><asp:ListItem Value="00000000-0000-0000-0000-000000000000">- nog niet bekend -</asp:ListItem></cc11:ClassComboBox>  
        </td>
    <td><asp:Label ID="YourReference" runat="server" Text="Uw referentie"></asp:Label> </td>
    <td> 
        <asp:TextBox ID="TextBox_YourReference" runat="server" Width="100px" 
            MaxLength="250" ></asp:TextBox>
    </td>
    <td><asp:Label ID="LabelLedgerType" runat="server" Text="Onze lokatie"></asp:Label> </td>
    <td>
        <cc11:ClassComboBox  AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" 
            ID="DropDownList_Location" runat="server" DataSourceID="EntityDataSourceLocations" 
                DataTextField="Description" DataValueField="Id" AutoPostBack="True" 
            onselectedindexchanged="DropDownList_Location_SelectedIndexChanged"></cc11:ClassComboBox> 
        </td>
</tr>

<tr>
<td>
        <asp:Label ID="LabelTotalAmountExVAT" runat="server" Text="Totaal bedrag ex BTW"></asp:Label> 
</td>
<td>
        <asp:Label ID="Label_Price" runat="server" Text="..."></asp:Label> 
</td>
<td>
        <asp:Label ID="LabelVAT" runat="server" Text="BTW"></asp:Label> 
</td>
<td>
        <asp:Label ID="Label_VATPrice" runat="server" Text="..."></asp:Label> 
</td>
<td>
        <asp:Label ID="LabelTotalPrice" runat="server" Text="Totaal bedrag"></asp:Label> 
</td>
<td>
        <asp:Label ID="Label_TotalPrice" runat="server" Text="..."></asp:Label> 
</td>
</tr>

<tr>
<td>
        <asp:Label ID="LabelAlreadyPaid" runat="server" Text="Reeds betaald bedrag"></asp:Label> 
</td>
<td>
        <asp:TextBox ID="TextBox_AlreadyPaid" runat="server" Width="100px" 
            MaxLength="250" Enabled="False" ></asp:TextBox>
        <uc2:URLPopUpControl ID="URLPopUpControlPartialPayment" runat="server" 
            OnPopUpClosed="URLPopUpControlPartialPayment_OnPopupClosed" 
            OnBeforePopUpOpened="URLPopUpControlPartialPayment_OnBeforePopUpOpened" 
            Text="Registreer deelbetaling"/>
</td>
<td>
        <asp:Label ID="LabelInvoiceDiscount" runat="server" 
        Text="Factuurkortingspercentage"></asp:Label> 
</td>
<td>
        <asp:TextBox ID="TextBox_DiscountPercentage" runat="server" Width="100px" 
            MaxLength="250" ></asp:TextBox>
</td>
<td>
</td>
<td>
</td>
</tr>

<tr>
    <td><asp:Label ID="LabelInvoiceNote" runat="server" Text="Factuurnotitie"></asp:Label> </td>
    <td colspan="5">
        <asp:TextBox ID="TextBox_InvoiceNote" runat="server" Width="90%" 
            MaxLength="1000" Rows="2" TextMode="MultiLine" ></asp:TextBox>
    </td>
</tr>



</table>
<br />
<asp:Label ID="LabelLinesOverview" runat="server" Text="Overzicht factuurregels"  CssClass="SubMenuHeader"></asp:Label>
<hr />
<table width="100%">
<tr>
<td colspan="9">
    <cc11:ClassGridView ID="GridViewOrderLines" runat="server" AllowPaging="True" 
        AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="Id" 
        DataSourceID="EntityDataSourceInvoiceLines"  
        onrowdeleting="GridViewOrderLines_RowDeleting" 
        onrowupdating="GridViewOrderLines_RowUpdating" >
        <Columns>
            <asp:CommandField DeleteText="Verwijderen" ShowDeleteButton="True" />
            <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" 
                SortExpression="Id" Visible="False" />
            <asp:BoundField DataField="LineNumber" HeaderText="Regel" 
                SortExpression="LineNumber" ReadOnly="True" />
            <asp:BoundField DataField="Description" HeaderText="Omschrijving" 
                SortExpression="Description" />
            <asp:BoundField DataField="Amount" HeaderText="Hoeveelheid" 
                SortExpression="Amount" />
            <asp:BoundField DataField="PricePerUnit" HeaderText="Prijs per eenheid" 
                SortExpression="PricePerUnit" />
            <asp:BoundField DataField="DiscountPercentage" 
                HeaderText="Regelkorting% " SortExpression="DiscountPercentage" />
            <asp:BoundField DataField="VATPercentage" HeaderText="BTW%" 
                SortExpression="VATPercentage" />
            <asp:BoundField DataField="OriginalPrice" HeaderText="Prijs" ReadOnly="True" 
                SortExpression="OriginalPrice" />
            <asp:BoundField DataField="PriceWithDiscount" HeaderText="Prijs na korting" ReadOnly="True" 
                SortExpression="PriceWithDiscount"  />
            <asp:BoundField DataField="VATPrice" HeaderText="BTW" 
                SortExpression="VATPrice" ReadOnly="True" />
            <asp:BoundField DataField="TotalPrice" HeaderText="Totaalprijs" 
                SortExpression="TotalPrice" ReadOnly="True" />
            <asp:BoundField DataField="LedgerDescription" HeaderText="Kasboek" 
                SortExpression="LedgerDescription" ReadOnly="True" />
            <asp:BoundField DataField="LedgerBookingCodeDescription" HeaderText="Boekcode" 
                SortExpression="LedgerBookingCodeDescription" ReadOnly="True" />
            <asp:CommandField CancelText="Annuleren " EditText="Bewerken" 
                ShowEditButton="True" UpdateText="Bijwerken" />
        </Columns>
    </cc11:ClassGridView>
</td>
</tr>
</table>
<asp:Label ID="LabelAdditionalInvoiceLines" runat="server" Text="Extra factuurregels"  CssClass="SubMenuHeader"></asp:Label>
<hr />
<table>
<tr>
<td>
        <asp:Label ID="LabelDescription" runat="server" 
            Text="Vrije factuurregel toelichting"></asp:Label>
</td>
<td>
            <asp:TextBox ID="TextBoxFreeLine" runat="server"></asp:TextBox>
</td>
<td>
                <asp:Label ID="LabelPrice" runat="server" Text="Prijs"></asp:Label>
</td>
<td>
                    <asp:TextBox ID="TextBoxFreeLinePrice" runat="server" Width="100px"></asp:TextBox>
</td>
<td>
                <asp:Label ID="LabelVATPercentage" runat="server" Text="BTW%"></asp:Label>
</td>
<td>
                    <asp:TextBox ID="TextBoxFreeLineVATPercentage" runat="server" Text="0" 
                        Width="50px"></asp:TextBox>
</td>
<td>
                <asp:Label ID="LabelLedgerBookingCode" runat="server" Text="Boekcode"></asp:Label>
</td>
<td>
    <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" ID="DropDownListFreeLineLedgerBookingCode" runat="server" 
        DataSourceID="EntityDataSourceLedgerBookingCodes" DataTextField="Description" 
        DataValueField="Id"></cc11:ClassComboBox>
</td>
<td>
    <asp:Button ID="ButtonAddInvoiceLine" runat="server" 
        Text="Vrije regel toevoegen aan factuur" 
        onclick="ButtonAddInvoiceLine_Click" />
</td>
</tr>

<tr>
<td>
        <asp:Label ID="LabelAdvancePayment" runat="server" Text="Voorschot"></asp:Label>
</td>
<td colspan="5">
            
    <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" ID="DropDownListAPCorrection" runat="server" 
        DataSourceID="EntityDataSourceAdvancePayments" DataTextField="Description" 
        DataValueField="Id"></cc11:ClassComboBox>
            
</td>
<td>
                <asp:Label ID="LabelCorrectionAP" runat="server" Text="Correctiebedrag"></asp:Label>
</td>
<td>
                    <asp:TextBox ID="TextBoxCorrectionAP" runat="server"></asp:TextBox>
</td>
<td>
    <asp:Button ID="ButtonAddAPCorrection" runat="server" 
        Text="Voorschotcorrectie toevoegen" 
        onclick="ButtonAddAPCorrection_Click" />
</td>
</tr>


<tr>
<td>
        <asp:Label ID="LabelWorkCorrection" runat="server" Text="Werk derden"></asp:Label>
</td>
<td colspan="5">
            
    <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" ID="DropDownListWorkCorrection" runat="server" 
        DataSourceID="EntityDataSourceWorkPayments" DataTextField="Description" 
        DataValueField="Id"></cc11:ClassComboBox>
            
</td>
<td>
                <asp:Label ID="LabelWorkCorrectionAmount" runat="server" Text="Correctiebedrag"></asp:Label>
</td>
<td>
                    <asp:TextBox ID="TextBoxWorkCorrectionAmount" runat="server"></asp:TextBox>
</td>
<td>
    <asp:Button ID="ButtonAddWorkCorrection" runat="server" 
        Text="Werk derden correctie toevoegen" 
        onclick="ButtonAddWorkCorrection_Click" />
</td>
</tr>

</table>
<asp:Panel ID="PanelLinkedOrders" runat="server">

<asp:Label ID="LabelLinkedOrders" runat="server" Text="Aangekoppelde orders"  CssClass="SubMenuHeader"></asp:Label>
<hr />
<cc11:ClassGridView ID="GridViewOrders" runat="server" AutoGenerateColumns="False" 
    DataKeyNames="Id" DataSourceID="EntityDataSourceOrders" 
    EnableViewState="False" onrowdatabound="GridViewOrders_RowDataBound" 
    onrowcommand="GridViewOrders_RowCommand" style="margin-top: 0px" 
        EnableTheming="True">
    <Columns>
        <asp:ButtonField CommandName="UnlinkOrder" HeaderText="Koppeling naar order" 
            InsertVisible="False" ShowHeader="True" Text="Orderkoppeling verbreken" />
        <asp:BoundField DataField="OrderNumber" HeaderText="Order nummer" 
            SortExpression="OrderNumber" />
        <asp:BoundField DataField="Description" HeaderText="Beschrijving" 
            SortExpression="Description" />
        <asp:BoundField DataField="BookingDateTime" 
            HeaderText="Boekingsdatum &amp; -tijd" SortExpression="BookingDateTime" />
        <asp:BoundField DataField="OrderStatus" HeaderText="Status" 
            SortExpression="OrderStatus" />
        <asp:BoundField DataField="TotalPrice" HeaderText="Prijs" 
            SortExpression="TotalPrice" />
        <asp:BoundField DataField="TotalAmount" HeaderText="Hoeveelheid" 
            SortExpression="TotalAmount" />
        <asp:CheckBoxField DataField="DeterminePriceDuringInvoicing" 
            HeaderText="Bepaal prijs/gewichten tijdens factureren" 
            SortExpression="DeterminePriceDuringInvoicing" />
        <asp:TemplateField HeaderText="Order bekijken/bewerken"></asp:TemplateField>
    </Columns>
</cc11:ClassGridView>
</asp:Panel>
<hr />
<br />

<table  width="100%">
<tr>
    <td><asp:Label ID="LabelComments" runat="server" Text="Opmerkingen"></asp:Label> </td>
    <td width="80%"> 
        <asp:TextBox ID="TextBox_Comments" runat="server" Width="95%" 
            MaxLength="2000" Rows="4" TextMode="MultiLine" ></asp:TextBox>
        <br />
    </td>
</tr>
</table>



<asp:Button ID="ButtonCancel" runat="server" Text="Wijzigingen annuleren" 
    onclick="ButtonCancel_Click" />
<asp:Button ID="ButtonSave" runat="server" Text="Wijzigingen opslaan" 
    onclick="ButtonSave_Click" />
<asp:Button ID="ButtonDelete" runat="server" Text="Factuur verwijderen" OnClientClick="return confirm('Weet u het zeker dat u dit wilt?');"
    onclick="ButtonDelete_Click" />
<asp:Button ID="ButtonCorrectInvoice" runat="server" Text="Factuur corrigeren" 
    OnClientClick="return confirm('De factuur wordt tegengeboekt. Op basis van de gegevens in deze factuur kunt u een nieuwe factuur maken waarin u uw correcties kunt aanbrengen. Weet u het zeker dat u dit wilt?');" onclick="ButtonCorrectInvoice_Click"
     />
<asp:Button ID="ButtonCreateClonedInvoiceAndOrders" runat="server" Text="Factuur en orders klonen naar onverwerkt" 
    
    
    OnClientClick="return confirm('De factuur en eventuele bijbehorende orders worden gekloond naar nieuwe orders en een nieuwe factuur. Deze zijn vervolgens onverwerkt en niet betaald. Weet u het zeker dat u dit wilt?');" 
    onclick="ButtonCreateClonedInvoiceAndOrders_Click" 
     />

<cc11:ClassEntityDataSource ID="EntityDataSourceRelations" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EnableFlattening="False" 
    EntitySetName="RelationSet" Select="it.[Description], it.[Id], it.IsActive" 
    
    
    Where="(it.IsActive = true) and ( (it.CustomerType=&quot;Both&quot;) or (it.CustomerType=@RelationType) )" 
    EntityTypeFilter="">
    <WhereParameters>
        <asp:ControlParameter ControlID="LabelRelationType" DbType="String" 
            DefaultValue="Creditor" Name="RelationType" PropertyName="Text" />
    </WhereParameters>
</cc11:ClassEntityDataSource>


<uc2:URLPopUpControl ID="URLPopUpControlShowInvoice" runat="server" 
    Text="Factuur tonen" />


<cc11:ClassEntityDataSource ID="EntityDataSourceLocations" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EnableFlattening="False" 
    EntitySetName="LocationSet" Select="it.[Description], it.[Id], it.IsActive" 
    
    Where="(it.IsActive = true) ">

</cc11:ClassEntityDataSource>


<cc11:ClassEntityDataSource ID="EntityDataSourceStaffMembers" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EnableFlattening="False" 
    EntitySetName="StaffMemberSet" Select="it.[Description], it.[Id], it.IsActive" 
    
    Where="(it.IsActive = true) ">

</cc11:ClassEntityDataSource>


<cc11:ClassEntityDataSource ID="EntityDataSourceInvoiceLines" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" 
    Where="" EntityTypeFilter="" Select="" CommandText="select distinct it.Id, it.Description, it.LineNumber, it.PriceWithDiscount, it.VATPrice, it.TotalPrice, it.Amount, it.PricePerUnit, it.VATPercentage,
    lg.Description as LedgerDescription, lbc.Description as LedgerBookingCodeDescription, it.DiscountPercentage, it.OriginalPrice
from InvoiceLineSet as it inner join LedgerSet as lg on it.Ledger.Id = lg.Id inner join LedgerBookingCodeSet as lbc on it.LedgerBookingCode.Id = lbc.Id
where it.Invoice.Id = @ID" OrderBy="it.LineNumber">
    <CommandParameters>
        <asp:ControlParameter ControlID="LabelID" DbType="Guid" Name="ID" 
            PropertyName="Text" />
    </CommandParameters>
</cc11:ClassEntityDataSource>

<cc11:ClassEntityDataSource ID="EntityDataSourceOrders" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EntitySetName="OrderSet" 
    Where="it.Invoice.Id = @ID" 
    Select="it.[Id], it.[Description], it.[OrderNumber], it.[OrderType], it.[BookingDateTime], it.[OrderStatus], it.[TotalPrice], it.[TotalAmount], it.[DeterminePriceDuringInvoicing]">
    <WhereParameters>
        <asp:ControlParameter ControlID="LabelID" DbType="Guid" Name="ID" 
            PropertyName="Text" />
    </WhereParameters>
</cc11:ClassEntityDataSource>

<cc11:ClassEntityDataSource ID="EntityDataSourceLedgers" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EnableFlattening="False" 
    EntitySetName="LedgerSet" OrderBy="it.Description" 
    Select="it.[Id], it.[IsActive], it.[Description]" 
    Where="(it.LimitToLocation is NULL) or (it.LimitToLocation.Id == @LocationID) and (it.IsActive)">
    <WhereParameters>
        <asp:ControlParameter ControlID="DropDownList_Location" DbType="Guid" 
            DefaultValue="{00000000-0000-0000-0000-000000000000}" Name="LocationID" 
            PropertyName="SelectedValue" />
    </WhereParameters>
</cc11:ClassEntityDataSource>

<cc11:ClassEntityDataSource ID="EntityDataSourceLedgerBookingCodes" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EnableFlattening="False" 
    EntitySetName="LedgerBookingCodeSet" OrderBy="it.IsDebugLedgerCode, it.Description" 
    Select="it.[Id], it.[IsActive], it.[Description], it.IsDebugLedgerCode" 
    
    Where="(it.IsActive)">
</cc11:ClassEntityDataSource>

<cc11:ClassEntityDataSource ID="EntityDataSourceAdvancePayments" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" 
    EntitySetName="RelationAdvancePaymentSet" Select="it.[Id], it.[Description] + &quot; (&quot; + @OpenAmountText + cast((it.Amount - it.AmountPaidBack) as System.String) + &quot;)&quot; as Description" 
    
    Where="(it.Relation.Id = @CustID) and (not it.IsPaidBack) and (it.PaymentType = @APType)" 
    EntityTypeFilter="">
    <SelectParameters>
        <asp:ControlParameter ControlID="LabelOpenAmount" DbType="String" 
            Name="OpenAmountText" PropertyName="Text" />
    </SelectParameters>
    <WhereParameters>
        <asp:ControlParameter ControlID="DropDownList_Relation" DbType="Guid" 
            Name="CustID" PropertyName="SelectedValue" />
        <asp:ControlParameter ControlID="LabelAPType" Name="APType" PropertyName="Text" 
            Type="String" />
    </WhereParameters>
</cc11:ClassEntityDataSource>

<cc11:ClassEntityDataSource ID="EntityDataSourceWorkPayments" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" 
    EntitySetName="RelationWorkSet" Select="it.[Id], it.[Description] + &quot; (&quot; + @OpenAmountText + cast((it.TotalAmount - it.AmountPaidBack) as System.String) + &quot;)&quot; as Description" 
    
    Where="(it.Relation.Id = @CustID) and (it.IsActive) and (it.IsTreatedAsAdvancePayment) and (it.WorkType = @WorkType)" 
    EntityTypeFilter="">
    <SelectParameters>
        <asp:ControlParameter ControlID="LabelOpenAmount" DbType="String" 
            Name="OpenAmountText" PropertyName="Text" />
    </SelectParameters>
    <WhereParameters>
        <asp:ControlParameter ControlID="DropDownList_Relation" DbType="Guid" 
            Name="CustID" PropertyName="SelectedValue" />
        <asp:ControlParameter ControlID="LabelWorkType" Name="WorkType" 
            PropertyName="Text" Type="String" />
    </WhereParameters>
</cc11:ClassEntityDataSource>


<asp:CheckBox ID="CheckBoxRefreshRequired" Visible="false" runat="server" />
<asp:Label ID="LabelID" runat="server" Visible="false" Text="{00000000-0000-0000-0000-000000000000}"></asp:Label>
<asp:Label ID="LabelCustID" runat="server" 
    Text="{00000000-0000-0000-0000-000000000000}" Visible="False"></asp:Label>
<asp:Label ID="LabelDeliveryPeriode" runat="server" Text="Leverperiode : " Visible="False"></asp:Label>
<asp:Label ID="LabelInStock" runat="server" Text="Op voorraad : " Visible="False"></asp:Label>
<asp:Label ID="LabelDeliveredAmount" runat="server" Text="Al geleverd : " Visible="False"></asp:Label>
<asp:Label ID="LabelContractAmount" runat="server" Text="Te leveren hoeveelheid : " Visible="False"></asp:Label>
<asp:Label ID="LabelMaxContractAmount" runat="server" Text="Max nog te leveren : " Visible="False"></asp:Label>
<asp:Label ID="LabelOpenAmount" runat="server" Text="Openstaand bedrag : " Visible="False"></asp:Label>

<asp:Label ID="LabelInvoiceType" runat="server" Visible="false" Text="Unknown"></asp:Label>
<asp:Label ID="LabelRelationType" runat="server" Text="Creditor" Visible="False"></asp:Label>
<asp:Label ID="LabelWorkType" runat="server" Visible="false" Text="ByUs"></asp:Label>
<asp:Label ID="LabelAPType" runat="server" Visible="false" Text="Paid"></asp:Label>
<asp:Label ID="LabelReportType" runat="server" Text="ReportInvoiceA4" Visible="false" ></asp:Label>
