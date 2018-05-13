<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlCashPurchase.ascx.cs" Inherits="TMS_Recycling.WebUserControlCashPurchase" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="WebUserControlEditOrderMaterials.ascx" tagname="WebUserControlEditOrderMaterials" tagprefix="uc1" %>
<%@ Register src="WebUserControlEditAdvancePayments.ascx" tagname="WebUserControlEditAdvancePayments" tagprefix="uc2" %>
<%@ Register src="URLPopUpControl.ascx" tagname="URLPopUpControl" tagprefix="uc3" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc2" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Panel ID="PanelCustomerInformation" runat="server">
<asp:Label ID="LabelBasicData" runat="server" Text="Basisgegevens order" CssClass="SubMenuHeader"></asp:Label>
    <table style="width: 100%;">
        <tr>
            <td  style="width:200px;">
                <asp:Label ID="LabelPurchaseLocation" runat="server" Text="Lokatie"></asp:Label>
            </td>
            <td>
                <cc2:ClassComboBoxLocation ID="DropDownListLocations" runat="server" 
                    AutoCompleteMode="SuggestAppend" DataSourceID="EntityDataSourceLocations" 
                    DataTextField="Description" DataValueField="Id" 
                    DropDownStyle="DropDownList" 
                    onselectedindexchanged="DropDownListLocations_SelectedIndexChanged" 
                    AutoPostBack="True">
                </cc2:ClassComboBoxLocation>
                </td>
            <td style="width:250px;">&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LabelCustomer" runat="server" Text="Klant"></asp:Label>
            </td>
            <td>
                <cc11:ClassComboBox ID="DropDownListCustomers" runat="server" 
                    AutoCompleteMode="SuggestAppend" DataSourceID="EntityDataSourceCustomers" 
                    DataTextField="Description" DataValueField="Id" DropDownStyle="DropDown" 
                    MaxLength="0" style="display: inline;">
                </cc11:ClassComboBox>

                <asp:TextBox ID="TextBoxCustomer" runat="server" Visible='false' ></asp:TextBox>
                <asp:AutoCompleteExtender ID="TextBoxCustomer_AutoCompleteExtender" 
                    runat="server" DelimiterCharacters="" Enabled="True" FirstRowSelected="True" 
                    ServicePath="WebServiceTMS.asmx" TargetControlID="TextBoxCustomer" 
                    CompletionInterval="500" ContextKey="Yes" ServiceMethod="GetRelationList" 
                    UseContextKey="True" MinimumPrefixLength="1">
                </asp:AutoCompleteExtender>
            </td>
            <td>
                <uc3:URLPopUpControl ID="URLPopUpControlManageCustomers" runat="server" 
                    Text="Klanten beheren" 
                    URLToPopup="WebFormPopUp.aspx?uc=CustomerRelationOverview" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LabelDescription" runat="server" Text="Omschrijving"></asp:Label>

            </td>
            <td>
                <asp:TextBox ID="TextBox_Description" runat="server" Width="90%"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="ButtonRefresh" runat="server" Text="Genereer omschrijving" 
                    onclick="ButtonRefresh_Click" />
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="PanelCustomerDetails" runat="server">
<asp:Label ID="LabelCustomerDetails" runat="server" Text="Detail gegevens klant" CssClass="SubMenuHeader"></asp:Label>
    <table style="width: 100%;">
        <tr>
            <td  style="width:200px;">
                <asp:Label ID="LabelProject" runat="server" Text="Project"></asp:Label>
            </td>
            <td>
                <cc11:ClassComboBox ID="DropDownListProjects" runat="server" 
                    AppendDataBoundItems="True" AutoCompleteMode="SuggestAppend" 
                    DataSourceID="EntityDataSourceCustomerProjects" DataTextField="Description" 
                    DataValueField="Id" DropDownStyle="DropDownList">
                    <asp:ListItem Selected="True" Value="">-Nvt-</asp:ListItem>
                </cc11:ClassComboBox>
            </td>
            <td  style="width:150px;">
                <uc3:URLPopUpControl ID="URLPopUpControlNewCustomerProject" runat="server" 
                    Text="Nieuw klantproject" 
                    URLToPopup="WebFormPopUp.aspx?uc=CustomerRelationOverview" OnPopUpClosed="URLPopUpControlNewCustomerProject_OnPopupClosed"/>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LabelCustomerLocation" runat="server" Text="Klant lokatie"></asp:Label>
            </td>
            <td>
                <cc11:ClassComboBox ID="DropDownListCustomerLocations" runat="server" 
                    AppendDataBoundItems="True" AutoCompleteMode="SuggestAppend" 
                    DataSourceID="EntityDataSourceCustomerLocations" DataTextField="Description" 
                    DataValueField="Id" DropDownStyle="DropDownList">
                    <asp:ListItem Selected="True" Value="">-Nvt-</asp:ListItem>
                </cc11:ClassComboBox>
            </td>
            <td width="250px">
                <uc3:URLPopUpControl ID="URLPopUpControlNewCustomerLocation" runat="server" 
                    Text="Nieuwe klantlokatie" 
                    URLToPopup="WebFormPopUp.aspx?uc=CustomerRelationOverview" OnPopUpClosed="URLPopUpControlNewCustomerLocation_OnPopupClosed" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LabelFreight" runat="server" Text="Vracht"></asp:Label>
            </td>
            <td>
                <cc11:ClassComboBox ID="DropDownListCustomerFreights" runat="server" 
                    AppendDataBoundItems="True" AutoCompleteMode="SuggestAppend" 
                    DataSourceID="EntityDataSourceCustomerFreights" DataTextField="Description" 
                    DataValueField="Id" DropDownStyle="DropDownList" AutoPostBack="True" 
                    onselectedindexchanged="DropDownListCustomerFreights_SelectedIndexChanged">
                    <asp:ListItem Selected="True" Value="">-Nvt-</asp:ListItem>
                </cc11:ClassComboBox>
                &nbsp;&nbsp;
                <uc3:URLPopUpControl ID="URLPopUpControlShowFreightData" runat="server" 
                    Text="Toon vrachtgegevens" />
                <asp:Button ID="ButtonReloadFreight" runat="server" 
                    onclick="ButtonReloadFreight_Click" Text="Vracht herladen" />
            </td>
            <td width="250px">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LabelLicensePlate" runat="server" Text="Kenteken klant"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox_YourTruckPlate" runat="server" MaxLength="40"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;
                <asp:Label ID="LabelYourDriverName" runat="server" 
                    Text="en/of Identificatie klant"></asp:Label>&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="TextBox_YourDriverName" runat="server" MaxLength="40"></asp:TextBox>
            </td>
            <td width="250px">
                &nbsp;</td>
        </tr>
</table>
</asp:Panel>

<asp:Panel ID="PanelMaterials" runat="server">
    <uc1:WebUserControlEditOrderMaterials ID="WebUserControlEditOrderMaterials1" 
    runat="server" />
    
</asp:Panel>

<asp:Panel ID="PanelTotals" runat="server">
<asp:Label ID="LabelTotals" runat="server" Text="Order totalen" CssClass="SubMenuHeader"></asp:Label>
<table  style="width: 100%;">
<tr><td width="250px">
    <asp:Label ID="LabelAddDirt" runat="server" Text="Aanvullen met vuil tot (kg)"></asp:Label>
    </td>
    <td>
        <asp:TextBox ID="TextBoxAddDirt" runat="server" Width="80px">0</asp:TextBox>
        
    </td>
    <td>
        <asp:Label ID="LabelFreightID" runat="server" Text="PMV"></asp:Label>
       
    </td>
    <td>
    <asp:TextBox ID="TextBoxFreightID" runat="server" Width="200px"></asp:TextBox>
                </td>
</tr>
<tr>
    <td>
    <asp:Label ID="LabelInvoiceNote" runat="server" Text="Factuur notitie"></asp:Label>
    </td>
    <td colspan="3">
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

<asp:Button ID="ButtonRevert" runat="server" Text="Vorige stap" 
    onclick="ButtonRevert_Click" />
<asp:Button ID="ButtonContinue" runat="server" Text="Volgende stap" 
    onclick="ButtonContinue_Click" />
<asp:Button ID="ButtonPrintAndProcess" runat="server" Text="Order verwerken" 
    onclick="ButtonPrintAndProcess_Click" />
<asp:Button ID="ButtonDestroyOrderAndBack" runat="server" 
    Text="Order aanpassen" onclick="ButtonDestroyOrderAndBack_Click" />
<asp:Button ID="ButtonNewOrder" runat="server" Text="Nieuwe order" 
    onclick="ButtonNewOrder_Click" />
<br />
<uc3:URLPopUpControl ID="URLPopUpControlRentOut" runat="server" 
    Text="Verhuurmateriaal plaatsen" />
<uc3:URLPopUpControl ID="URLPopUpControlRentSwap" runat="server" 
    Text="Verhuurmateriaal omwisselen" />



<cc11:ClassEntityDataSource ID="EntityDataSourceLocations" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EnableFlattening="False" 
    EntitySetName="LocationSet" Select="it.[Description], it.[Id], it.[IsActive]" 
    Where="it.IsActive = true">
</cc11:ClassEntityDataSource>


<cc11:ClassEntityDataSource ID="EntityDataSourceCustomers" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EnableFlattening="False" 
    EntitySetName="RelationSet" 
    Where="(it.IsActive = true) and ( (it.CustomerType = @CustomerType) || (it.CustomerType = &quot;Both&quot;) )" 
    EntityTypeFilter="" OrderBy="(case when it.PreferredLocation.Id = @LocationId then 0 else 1 end), it.Description" Include="" 
    Select="it.Id, it.Description + &quot; (&quot; + it.PreferredLocation.Description + &quot;)&quot; as Description, it.IsActive, it.PreferredLocation">
    <OrderByParameters>
        <asp:ControlParameter ControlID="DropDownListLocations" 
            DefaultValue="00000000-0000-0000-0000-000000000000" Name="LocationId" DbType="Guid" 
            PropertyName="SelectedValue" />
    </OrderByParameters>
    <WhereParameters>
        <asp:ControlParameter ControlID="LabelCustomerType" Name="CustomerType" 
            PropertyName="Text" Type="String" />
    </WhereParameters>
</cc11:ClassEntityDataSource>


<cc11:ClassEntityDataSource ID="EntityDataSourceCustomerProjects" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" 
    EntitySetName="RelationProjectSet" 
    Where="(it.IsActive = true) and (it.Relation.Id = @CustID)" 
    OrderBy="it.Description">
    <WhereParameters>
        <asp:ControlParameter ControlID="LabelCustID" DbType="Guid" 
            DefaultValue="{00000000-0000-0000-0000-000000000000}" Name="CustID" 
            PropertyName="Text" />
    </WhereParameters>
</cc11:ClassEntityDataSource>
<cc11:ClassEntityDataSource ID="EntityDataSourceCustomerLocations" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EntitySetName="RelationLocationSet"
    Where="(it.IsActive = true) and (it.Relation.Id = @CustID)" 
    OrderBy="it.Description">
    <WhereParameters>
        <asp:ControlParameter ControlID="LabelCustID" DbType="Guid" 
            DefaultValue="{00000000-0000-0000-0000-000000000000}" Name="CustID" 
            PropertyName="Text" />
    </WhereParameters>
</cc11:ClassEntityDataSource>
<cc11:ClassEntityDataSource ID="EntityDataSourceCustomerInvoiceAddress" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EntitySetName="RelationAddressSet"
    Where="(it.IsActive = true) and (it.Relation.Id = @CustID) and (it.AdressType='Invoice')" 
    OrderBy="it.CreateDateTime" EntityTypeFilter="" Select="">
    <WhereParameters>
        <asp:ControlParameter ControlID="LabelCustID" DbType="Guid" 
            DefaultValue="{00000000-0000-0000-0000-000000000000}" Name="CustID" 
            PropertyName="Text" />
    </WhereParameters>
</cc11:ClassEntityDataSource>

<cc11:ClassEntityDataSource ID="EntityDataSourceCustomerFreights" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EntitySetName="FreightSet"
    Where="(it.IsActive = true) and (it.FromRelation.Id = @CustID) and (it.Order IS NULL) and (it.FreightDirection = @FreightDirection) and (it.FreightStatus = &quot;To be invoiced&quot;)" 
    OrderBy="it.Description" EntityTypeFilter="">
    <WhereParameters>
        <asp:ControlParameter ControlID="LabelCustID" DbType="Guid" 
            DefaultValue="{00000000-0000-0000-0000-000000000000}" Name="CustID" 
            PropertyName="Text" />
        <asp:ControlParameter ControlID="LabelFreightType" DefaultValue="" 
            Name="FreightDirection" PropertyName="Text" Type="String" />
    </WhereParameters>
</cc11:ClassEntityDataSource>
<cc11:ClassEntityDataSource ID="EntityDataSourceMaterials" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EntitySetName="MaterialSet" 
    
    Where="(it.IsActive = true) and (it.IsWorkInsteadOfMaterial = false) and ( (it.InvoiceType=&quot;Both&quot;) or (it.InvoiceType = @InvoiceType))" 
    EntityTypeFilter="" Select="">
    <WhereParameters>
        <asp:ControlParameter ControlID="LabelInvoiceType" Name="InvoiceType" 
            PropertyName="Text" Type="String" />
    </WhereParameters>
</cc11:ClassEntityDataSource>
<cc11:ClassEntityDataSource ID="EntityDataSourceCustomerPriceAgreements" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EntitySetName="RelationPriceAgreementSet"
    
    
    
    
    Where="(it.IsActive = true) and (it.Relation.Id = @CustID) and (it.AgreementType = @InvoiceType) and (it.StartDateTime &lt; CurrentDateTime() ) and (it.EndDateTime &lt; CurrentDateTime() )" 
    Select="">
    <WhereParameters>
        <asp:FormParameter DbType="Guid" FormField="DropDownListCustomers"  DefaultValue="{00000000-0000-0000-0000-000000000000}" 
            Name="CustID" />
        <asp:ControlParameter ControlID="LabelInvoiceType" Name="InvoiceType" 
            PropertyName="Text" Type="String" />
    </WhereParameters>
</cc11:ClassEntityDataSource>
<cc11:ClassEntityDataSource ID="EntityDataSourcePurchaseContracts" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer"
    
    Where="" 
    Include="" CommandText="SELECT it.[Id], it.[Description], it.[ContractStatus], cm.MinAmount, cm.MaxAmount, sum(ol.Amount) as AlreadyDeliveredAmount
FROM RelationContractSet as it join RelationContractMaterialSet as cm on it.Id = cm.RelationContract.Id 
join OrderLineSet as ol on cm.Id = ol.RelationContractMaterial.Id
WHERE
(it.ContractStatus = &quot;Open&quot;) and (it.Relation.Id = @CustID) and (it.ContractType = @InvoiceType) and (cm.Material.Id = @MatID) and (it.ContractStartDate &gt; CurrentDateTime() ) and (it.ContractEndDate &lt; CurrentDateTime() ) 
GROUP BY it.[Id], it.[Description], it.[ContractStatus], cm.MinAmount, cm.MaxAmount
" EntityTypeFilter="">
    <CommandParameters>
        <asp:FormParameter DbType="Guid" 
            DefaultValue="{00000000-0000-0000-0000-000000000000}" 
            FormField="DropDownListCustomers" Name="CustID" />
        <asp:FormParameter DbType="Guid" 
            DefaultValue="{00000000-0000-0000-0000-000000000000}" 
            FormField="DropDownListMaterials" Name="MatID" />
        <asp:ControlParameter ControlID="LabelInvoiceType" Name="InvoiceType" 
            PropertyName="Text" Type="String" />
    </CommandParameters>

</cc11:ClassEntityDataSource>

<cc11:ClassEntityDataSource ID="EntityDataSourceGeneratedOrder" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EntitySetName="OrderSet" Where="" 
    AutoGenerateWhereClause="True" Include="">
    <WhereParameters>
        <asp:FormParameter FormField="LabelGeneratedOrderId" Name="Id" DbType="Guid" 
            DefaultValue="{00000000-0000-0000-0000-000000000000}" />
    </WhereParameters>
</cc11:ClassEntityDataSource>


<cc11:ClassEntityDataSource ID="EntityDataSourceGeneratedOrderLine" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EntitySetName="OrderLineSet" Where="">
    <WhereParameters>
        <asp:FormParameter DbType="Guid" 
            DefaultValue="{00000000-0000-0000-0000-000000000000}" 
            FormField="LabelGeneratedOrderId" Name="Order_Id" />
    </WhereParameters>
</cc11:ClassEntityDataSource>



<cc11:ClassEntityDataSource ID="EntityDataSourceMaterialsContractsAndAgreements" 
    runat="server" CommandText="
(
select it.[Id], &quot;MAT&quot; as LineType, it.[Description] as Description
from MaterialSet as it
where it.IsActive and (it.IsWorkInsteadOfMaterial = false) and (it.InvoiceType &lt;&gt; @ReverseInvoiceType)
)

union all 

(
select cm.[Id], &quot;CON&quot; as LineType, @ContractText + SqlServer.STR(it.[OurReference]) + &quot; / &quot; + it.[Description] + &quot; / &quot; + cm.Description + &quot; (&quot; + cm.Material.Description + &quot;)&quot; as Description
from RelationContractSet as it join RelationContractMaterialSet as cm on it.Id = cm.RelationContract.Id 
WHERE
(it.ContractStatus = &quot;Open&quot;) and (it.Relation.Id = @CustID) and (it.ContractType = @InvoiceType) and (CurrentDateTime() between it.ContractStartDate and it.ContractEndDate) 
)

union all 

(
SELECT it.[Id], &quot;PAS&quot; as LineType, @PriceAgreementText + it.[Description] + &quot; (&quot; + it.Material.Description +&quot;)&quot; as Description
FROM RelationPriceAgreementSet as it
WHERE (it.IsActive) and (it.Relation.Id = @CustID) and (it.AgreementType = @InvoiceType) and (CurrentDateTime() between it.StartDateTime and it.EndDateTime  ) 
)" ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EntitySetName="">
    <CommandParameters>
        <asp:ControlParameter ControlID="LabelCustID" DbType="Guid" Name="CustID" 
            PropertyName="Text" />
        <asp:ControlParameter ControlID="LabelPriceAgreement" Name="PriceAgreementText" 
            PropertyName="Text" Type="String" />
        <asp:ControlParameter ControlID="LabelContract" Name="ContractText" 
            PropertyName="Text" Type="String" />
        <asp:ControlParameter ControlID="LabelInvoiceType" Name="InvoiceType" 
            PropertyName="Text" Type="String" />
        <asp:ControlParameter ControlID="LabelReverseInvoiceType" 
            Name="ReverseInvoiceType" PropertyName="Text" Type="String" />
    </CommandParameters>
</cc11:ClassEntityDataSource>

<asp:Label ID="LabelDeliveryPeriode" runat="server" Text="Leverperiode : " Visible="False"></asp:Label>
<asp:Label ID="LabelInStock" runat="server" Text="Op voorraad : " Visible="False"></asp:Label>
<asp:Label ID="LabelDeliveredAmount" runat="server" Text="Al geleverd : " Visible="False"></asp:Label>
<asp:Label ID="LabelContractAmount" runat="server" Text="Te leveren hoeveelheid : " Visible="False"></asp:Label>
<asp:Label ID="LabelMaxContractAmount" runat="server" Text="Max nog te leveren : " Visible="False"></asp:Label>
<asp:Label ID="LabelPriceAgreement" runat="server" Text="Prijsovereenkomst " Visible="False"></asp:Label>
<asp:Label ID="LabelContract" runat="server" Text="Contract " Visible="False"></asp:Label>
<asp:Label ID="LabelOrderNr" runat="server" Text="" Visible="False"></asp:Label>

<asp:Label ID="LabelInvoiceType" runat="server" Text="Buy" Visible="False"></asp:Label>
<asp:Label ID="LabelReverseInvoiceType" runat="server" Text="Sell" Visible="False"></asp:Label>
<asp:Label ID="LabelCustomerType" runat="server" Text="Creditor" Visible="False"></asp:Label>
<asp:Label ID="LabelFreightType" runat="server" Text="To warehouse" Visible="False"></asp:Label>
<asp:Label ID="LabelFreightGuid" runat="server" Text="" Visible="False"></asp:Label>

<asp:Label ID="LabelCurrentPanelLevel" runat="server" Text="1" Visible="False"></asp:Label>
<asp:Label ID="LabelGeneratedInvoiceId" runat="server" 
    Text="{00000000-0000-0000-0000-000000000000}" Visible="False"></asp:Label>
<asp:Label ID="LabelCustID" runat="server" Text="{00000000-0000-0000-0000-000000000000}" Visible="False"></asp:Label>

<cc11:ClassEntityDataSource ID="EntityDataSourceGeneratedInvoiceLocation" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" 
    CommandText="" EntitySetName="LocationSet" Include="" Where="it.Id=@Id2" 
    EntityTypeFilter="" Select="">
    <WhereParameters>
        <asp:ControlParameter Name="Id2" ControlID="DropDownListLocations" DbType="Guid" 
            PropertyName="SelectedValue" />
    </WhereParameters>
</cc11:ClassEntityDataSource>


<cc11:ClassEntityDataSource ID="EntityDataSourceGeneratedInvoice" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EntitySetName="InvoiceSet" 
    Where="it.Id=@Id1" Include="" EntityTypeFilter="" Select="">
    <WhereParameters>
        <asp:ControlParameter ControlID="LabelGeneratedInvoiceId" DbType="Guid" 
            DefaultValue="{00000000-0000-0000-0000-000000000000}" Name="Id1" 
            PropertyName="Text" />
    </WhereParameters>
</cc11:ClassEntityDataSource>


<cc11:ClassEntityDataSource ID="EntityDataSourceGeneratedInvoiceLine" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" 
    Where="it.Invoice.Id = @Invoice_Id" 
    CommandText="select it.Id, it.Invoice, It.CreateDateTime, it.ModifyDateTime, it.description as Description, it.linenumber as LineNumber, it.price as Price, it.vatprice as VATPrice, 
           it.totalprice as TotalPrice, it.amount as Amount, it.priceperunit as PricePerUnit, it.vatpercentage as VATPercentage, oo.Ordernumber as OrderNumber
from InvoiceLineSet as it left join OrderLineSet as ol on it.Orderline.Id = ol.Id left join OrderSet as oo on ol.Order.Id = oo.Id" 
    OrderBy="it.linenumber" EntitySetName="">
    <WhereParameters>
        <asp:ControlParameter ControlID="LabelGeneratedInvoiceId" DbType="Guid" 
            DefaultValue="{00000000-0000-0000-0000-000000000000}" Name="Invoice_Id" 
            PropertyName="Text" />
    </WhereParameters>
</cc11:ClassEntityDataSource>

    
