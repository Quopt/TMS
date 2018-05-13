<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlConstructInvoiceFromOrder.ascx.cs" Inherits="TMS_Recycling.WebUserControlConstructInvoiceFromOrder" %>
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
                    <asp:ListItem Selected="True" Value="">Alle lokaties</asp:ListItem>
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

<asp:Panel ID="PanelOpenOrders" runat="server">
<asp:Label ID="LabelOpenOrders" runat="server" Text="Openstaande orders" CssClass="SubMenuHeader"></asp:Label>

                <cc11:ClassGridView ID="GridViewOpenOrders" runat="server" 
                    AllowSorting="True" AutoGenerateColumns="False" 
                    DataSourceID="EntityDataSourceOpenOrders" Width="100%" 
        ViewStateMode="Enabled" PageSize="99999" 
        onrowcommand="GridViewOpenOrders_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="Geselecteerd">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBoxSelected" runat="server" Checked='false' />
                            </ItemTemplate>                        
                        </asp:TemplateField>                       
                        <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" 
                            Visible="False" />
                        <asp:BoundField DataField="OrderNumber" HeaderText="Ordernummer" 
                            ReadOnly="True" SortExpression="OrderNumber" />
                        <asp:BoundField DataField="Description" HeaderText="Omschrijving" 
                            ReadOnly="True" SortExpression="Description" />
                        <asp:BoundField DataField="BookingDateTime" HeaderText="Datum &amp; tijd" 
                            ReadOnly="True" SortExpression="BookingDateTime" />
                        <asp:BoundField DataField="OrderStatus" HeaderText="Orderstatus" 
                            SortExpression="OrderStatus" />
                        <asp:BoundField DataField="TotalPrice" HeaderText="Totaal prijs (ex BTW)" 
                            ReadOnly="True" SortExpression="TotalPrice" />
                        <asp:BoundField DataField="TotalAmount" HeaderText="Totaal gewicht" 
                            ReadOnly="True" SortExpression="TotalAmount" />
                        <asp:BoundField DataField="YourTruckPlate" HeaderText="Kenteken" 
                            ReadOnly="True" SortExpression="YourTruckPlate" />
                        <asp:BoundField DataField="YourDriverName" HeaderText="Chauffeur" 
                            ReadOnly="True" SortExpression="YourDriverName" />
                        <asp:CheckBoxField DataField="DeterminePriceDuringInvoicing" 
                            HeaderText="Bepaal prijs tijdens factureren" ReadOnly="True" 
                            SortExpression="DeterminePriceDuringInvoicing" />
                        <asp:CommandField SelectText="Toon details" ShowSelectButton="True" />
                    </Columns>
                </cc11:ClassGridView>
    <asp:Panel ID="PanelOpenOrdersDetails" runat="server">
    <uc3:WebUserControlEditOrderMaterials ID="WebUserControlEditOrderMaterials1" 
        runat="server" ShowAlreadyDeliveredAmount="True" ShowSaveButton="True" 
        Visible="False" />
    </asp:Panel>
 </asp:Panel>
<asp:Panel ID="PanelTotals" runat="server">
<asp:Label ID="LabelTotals" runat="server" Text="Te factureren totalen" CssClass="SubMenuHeader"></asp:Label>
<table width="100%">
<tr>
<td> <asp:Label ID="LabelNrOfPurchases" runat="server" Text="# orders"></asp:Label> &nbsp; <asp:Label ID="LabelNrOfPurchasesNr" runat="server" Text="..."></asp:Label> </td>
<td> <asp:Label ID="LabelTotalPriceExVat" runat="server" Text="Totaal prijs ex BTW"></asp:Label> &nbsp; <asp:Label ID="LabelTotalPriceExVatNr" runat="server" Text="..."></asp:Label> </td>
<td> <asp:Label ID="LabelTotalWeight" runat="server" Text="Totaal gewicht"></asp:Label> &nbsp; <asp:Label ID="LabelTotalWeightNr" runat="server" Text="..."></asp:Label> </td>
</tr>
<tr>
    <td>
    <asp:Label ID="LabelInvoiceDescription" runat="server" Text="Factuur beschrijving"></asp:Label>
    </td>
    <td colspan="2">
        <asp:TextBox ID="TextBoxInvoiceDescription" runat="server"  
            MaxLength="200" Width="100%"></asp:TextBox>
    </td>
</tr><tr>
    <td>
    <asp:Label ID="LabelInvoiceNote" runat="server" Text="Factuur notitie"></asp:Label>
    </td>
    <td colspan="2">
        <asp:TextBox ID="TextBoxInvoiceNote" runat="server" TextMode="MultiLine" 
            MaxLength="1000" Rows="2" Width="100%"></asp:TextBox>
    </td>
</tr>
</table>
</asp:Panel>

<asp:Panel ID="PanelAdvancePayments" runat="server">
    <uc2:WebUserControlEditAdvancePayments ID="WebUserControlEditAdvancePayments1" 
    runat="server" />

</asp:Panel>

<asp:Panel ID="PanelPreviewInvoice" runat="server">
<asp:Label ID="LabelPreviewInvoice" runat="server" Text="Printvoorbeeld order" CssClass="SubMenuHeader"></asp:Label>
    <br />
  <iframe id="FrameShowInvoice" scrolling="auto" runat="server" height="400px" width="100%">
  </iframe>
</asp:Panel>

<table width="100%">
<tr>
<td>
 <asp:Button ID="ButtonRevert" runat="server" Text="Vorige stap" onclick="ButtonRevert_Click" 
     />
<asp:Button ID="ButtonContinue" runat="server" Text="Volgende stap" onclick="ButtonContinue_Click" 
     />
<asp:Button ID="ButtonPrintAndProcess" runat="server" Text="Factuur aanmaken" onclick="ButtonPrintAndProcess_Click" 
     />
<asp:Button ID="ButtonDestroyOrderAndBack" runat="server" 
    Text="Factuur aanpassen" onclick="ButtonDestroyOrderAndBack_Click"  />
<asp:Button ID="ButtonNewOrder" runat="server" Text="Nieuwe factuur maken" onclick="ButtonNewOrder_Click" 
     />
</td>
<td align="right">

    <uc1:URLPopUpControl ID="URLPopUpControlInvoice" runat="server" 
        Text="Open aangemaakte factuur" />

</td>
</tr>
</table>

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
from RelationSet as it inner join OrderSet as os on it.Id = os.Relation.Id
where (it.IsActive = true) and ( (it.CustomerType = @CustomerType) || (it.CustomerType = &quot;Both&quot;) ) and
(os.OrderType=@InvoiceType) and (os.IsCorrected = false) and ((os.Location.Id = @IdLoc) or (cast(@IdLoc as System.String)=&quot;00000000-0000-0000-0000-000000000000&quot;)) and (os.Invoice is null)

" EntityTypeFilter="">
    <CommandParameters>
        <asp:ControlParameter ControlID="DropDownListLocations" Name="IdLoc" 
            PropertyName="SelectedValue" 
            DefaultValue="00000000-0000-0000-0000-000000000000" DbType="Guid" />
        <asp:ControlParameter ControlID="LabelCustomerType" DefaultValue="" 
            Name="CustomerType" PropertyName="Text" Type="String" />
        <asp:ControlParameter ControlID="LabelInvoiceType" DefaultValue="" 
            Name="InvoiceType" PropertyName="Text" Type="String" />
    </CommandParameters>
</cc11:ClassEntityDataSource>
<cc11:ClassEntityDataSource ID="EntityDataSourceOpenOrders" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EntitySetName="OrderSet" 
    OrderBy="it.CreateDateTime desc" 
    Select="it.[CreateDateTime], it.[Id], it.[OrderStatus], it.[ModifyDateTime], it.[Description], it.[CreateUser], it.[ModifyUser], it.[OrderNumber], it.[OrderType], it.[BookingDateTime], it.[TotalPrice], it.[TotalAmount], it.[YourTruckPlate], it.[YourDriverName], it.[FreightID], it.[GroupCode], it.[IsCorrected], it.[DeterminePriceDuringInvoicing]" 
    
    
    
    
    Where="(it.OrderType=@OrderType) and (it.IsCorrected = false) and (it.Relation.Id = @Id) and ((it.Location.Id = @IdLoc) or (cast(@IdLoc as System.String)=&quot;00000000-0000-0000-0000-000000000000&quot;)) and (it.Invoice is null)">
    <WhereParameters>
        <asp:ControlParameter ControlID="DropDownListCustomers" DbType="Guid" Name="Id" 
            PropertyName="SelectedValue" DefaultValue="" />
        <asp:ControlParameter ControlID="DropDownListLocations" DbType="Guid" 
            Name="IdLoc" PropertyName="SelectedValue" 
            DefaultValue="00000000-0000-0000-0000-000000000000" />
        <asp:ControlParameter ControlID="LabelInvoiceType" Name="OrderType" 
            PropertyName="Text" Type="String" />
    </WhereParameters>
</cc11:ClassEntityDataSource>


<asp:TextBox ID="TextBoxCurrentPage" runat="server" Visible="False">1</asp:TextBox>



<asp:Label ID="LabelGeneratedInvoiceId" runat="server" Visible="False"></asp:Label>
<asp:Label ID="LabelInvoiceType" runat="server" Visible="False" Text="Buy"></asp:Label>
<asp:Label ID="LabelCustomerType" runat="server" Visible="False" Text="Creditor"></asp:Label>




