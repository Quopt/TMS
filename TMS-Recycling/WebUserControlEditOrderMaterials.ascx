<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlEditOrderMaterials.ascx.cs" Inherits="TMS_Recycling.WebUserControlEditOrderMaterials" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit.HTMLEditor" tagprefix="cc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Label ID="LabelMaterials" runat="server" CssClass="SubMenuHeader" 
    Text="Materialen in deze order"></asp:Label><asp:Label ID="LabelOrderNr" 
    runat="server" CssClass="SubMenuHeader" Visible="False"></asp:Label>
<table style="width: 100%;">
    <tr>
        <td>
            <asp:Label ID="LabelMaterial" runat="server" Text="Materiaal"></asp:Label>
        </td>
        <td>
            <cc11:ClassComboBox ID="DropDownListMaterials" runat="server" 
                AutoCompleteMode="SuggestAppend" AutoPostBack="True" 
                DataSourceID="EntityDataSourceMaterialsContractsAndAgreements" 
                DataTextField="Description" DataValueField="Id" DropDownStyle="DropDownList" 
                MaxLength="0" 
                onselectedindexchanged="DropDownListMaterials_SelectedIndexChanged" 
                style="display: inline;">
             </cc11:ClassComboBox>
            <br />
        </td>
        <td>
            <asp:Label ID="LabelAmount" runat="server" Text="Hoeveelheid"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="TextBoxAmount" runat="server"></asp:TextBox>
        </td>
        <td>
            <asp:Label ID="LabelPrice" runat="server" Text="Prijs per eenheid"></asp:Label>
            <asp:Label ID="LabelPriceBase" runat="server" Text="Prijs per eenheid" Visible="false"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="TextBoxPrice" runat="server">0</asp:TextBox>
        </td>
        <td>
            <asp:Button ID="ButtonAddMaterial" runat="server" 
                onclick="ButtonAddMaterial_Click" Text="Materiaal toevoegen aan order" 
                EnableViewState="False" />
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
            <asp:Label ID="LabelMaterialDescription" runat="server"></asp:Label>
        </td>
    </tr>
</table>
<cc11:ClassGridView ID="GridViewOrderMaterials" runat="server" 
    AutoGenerateColumns="False" DataKeyNames="Id" 
    DataSourceID="XmlDataSourceOrderLines" 
    onrowdeleting="GridViewOrderMaterials_RowDeleting" 
    onrowupdating="GridViewOrderMaterials_RowUpdating" PageSize="99999">
    <Columns>
        <asp:CommandField DeleteText="Verwijder regel" ShowDeleteButton="True" />
        <asp:BoundField DataField="id" HeaderText="id" SortExpression="id" 
            Visible="False" />
        <asp:BoundField DataField="materialid" HeaderText="materialid" 
            Visible="False" />
        <asp:BoundField DataField="contractmaterialid" HeaderText="contractmaterialid" 
            Visible="False" />
        <asp:BoundField DataField="priceagreementid" HeaderText="priceagreementid" 
            Visible="False" />
        <asp:BoundField DataField="description" HeaderText="Materiaal" 
            ReadOnly="True" />
        <asp:BoundField DataField="amount" HeaderText="Hoeveelheid" />
        <asp:BoundField DataField="priceperunit" HeaderText="Prijs per eenheid" />
        <asp:BoundField DataField="alreadydeliveredamount" 
            HeaderText="Reeds geleverde hoeveelheid" Visible="false"/>
        <asp:BoundField DataField="totalamount" HeaderText="Totaal bedrag (ex BTW)" 
            ReadOnly="True" />
        <asp:CommandField CancelText="Wijzigingen annuleren" EditText="Bewerken" 
            ShowEditButton="True" UpdateText="Opslaan" />
    </Columns>
</cc11:ClassGridView>


<asp:Panel ID="PanelPrices" runat="server" Visible="False">
<table>
<tr><td>
    <asp:Label ID="LabelTotalWeightExplanation" runat="server" 
        Text="Totaal gewicht order (kg)"></asp:Label>
    </td>
    <td>
    <asp:Label ID="LabelTotalWeight" runat="server" Text="000"></asp:Label>
    </td>
    <td>
    <asp:Label ID="LabelTotalPriceExplanation" runat="server" 
        Text="/ Totaal prijs order (ex BTW)"></asp:Label>
    </td>
    <td>
    <asp:Label ID="LabelTotalPrice" runat="server" Text="000"></asp:Label>
    </td>
    <td>
    <asp:Label ID="LabelTotalPriceExplanationVAT" runat="server" 
        Text="/ Totaal prijs order (incl BTW)"></asp:Label>
    </td>
    <td>
    <asp:Label ID="LabelTotalPriceVAT" runat="server" Text="000"></asp:Label>
    </td>
</tr>
</table>
</asp:Panel>

<asp:Button ID="ButtonSave" runat="server" Text="Wijzigingen opslaan" 
    onclick="ButtonSave_Click" Visible="False" EnableViewState="False" />

<cc11:ClassEntityDataSource ID="EntityDataSourceMaterialsContractsAndAgreements" 
    runat="server" CommandText="
select it.Id, it.LineType, it.Description, it.SortDateTime
from 
(

(
select it.[Id], &quot;MAT&quot; as LineType, it.[Description] as Description, CurrentDateTime() as SortDateTime, it.Description as DescriptionBase
from MaterialSet as it
where it.IsActive and (it.IsWorkInsteadOfMaterial = false) and (it.InvoiceType &lt;&gt; @ReverseInvoiceType) and ( (it.Location.Id = @LocationID) or (@LocationID = cast( &quot;{00000000-0000-0000-0000-000000000000}&quot; as System.Guid)  ) )
)

union all 

(
select cm.[Id], &quot;CON&quot; as LineType, cm.Material.Description + &quot; / &quot; + @ContractText + SqlServer.STR(it.[OurReference]) + &quot; / &quot; + it.[Description] + &quot; / &quot; + cm.Description  as Description, it.ContractStartDate as SortDateTime, cm.Material.Description as DescriptionBase
from RelationContractSet as it join RelationContractMaterialSet as cm on it.Id = cm.RelationContract.Id 
WHERE
(it.ContractStatus = &quot;Open&quot;) and (it.Relation.Id = @CustID) and (it.ContractType = @InvoiceType) and (CurrentDateTime() between it.ContractStartDate and it.ContractEndDate) 
and ((cm.Material.Location.Id = @LocationID)  or (@LocationID = cast( &quot;{00000000-0000-0000-0000-000000000000}&quot; as System.Guid)  ) )
and ( cm.DeliveredAmount &lt; cm.MaxAmount )
)

union all 

(
SELECT it.[Id], &quot;PAS&quot; as LineType,  it.Material.Description + &quot; / &quot; + @PriceAgreementText + it.[Description]  as Description, it.StartDateTime as SortDateTime, it.Material.Description as DescriptionBase
FROM RelationPriceAgreementSet as it
WHERE (it.IsActive) and (it.Relation.Id = @CustID) and (it.AgreementType = @InvoiceType) and (CurrentDateTime() between it.StartDateTime and it.EndDateTime  ) 
and ( (it.Material.Location.Id = @LocationID) or (@LocationID = cast( &quot;{00000000-0000-0000-0000-000000000000}&quot; as System.Guid)  ) )
)

) as it
order by it.DescriptionBase, it.SortDateTime" ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EntitySetName="">
    <CommandParameters>
        <asp:ControlParameter ControlID="LabelCustID" DbType="Guid" Name="CustID" 
            PropertyName="Text" />
        <asp:ControlParameter ControlID="LabelPriceAgreement" Name="PriceAgreementText" 
            PropertyName="Text" Type="String" />
        <asp:ControlParameter ControlID="LabelContract" Name="ContractText" 
            PropertyName="Text" Type="String" />
        <asp:ControlParameter ControlID="LabelPriority" Name="PriorityText" 
            PropertyName="Text" Type="String" />
        <asp:ControlParameter ControlID="LabelInvoiceType" Name="InvoiceType" 
            PropertyName="Text" Type="String" />
        <asp:ControlParameter ControlID="LabelReverseInvoiceType" 
            Name="ReverseInvoiceType" PropertyName="Text" Type="String" />
        <asp:ControlParameter ControlID="LabelLocationID" DbType="Guid" DefaultValue="" 
            Name="LocationID" PropertyName="Text" />
    </CommandParameters>
</cc11:ClassEntityDataSource>


<cc11:ClassEntityDataSource ID="EntityDataSourceOrderLines" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EntitySetName="OrderLineSet" 
    Where="" 
    Select="it.[Id], it.[CreateDateTime], it.[ModifyDateTime], it.[Description], it.[CreateUser], it.[ModifyUser], it.[PriceExVAT], it.[Amount], it.[PricePerUnit], it.[AlreadyDeliveredAmount]">
    <WhereParameters>
        <asp:FormParameter DbType="Guid" 
            DefaultValue="{00000000-0000-0000-0000-000000000000}" 
            FormField="LabelOrderId" Name="Order_Id" />
    </WhereParameters>
</cc11:ClassEntityDataSource>



<asp:XmlDataSource ID="XmlDataSourceOrderLines" runat="server" 
    EnableCaching="False" EnableViewState="False">
    <Data>
<OrderLines>
 <OrderLine id="" amount="0" contractmaterialid="" description="test 1" materialid="" 
        priceagreementid="" priceperunit="0" totalamount="0" vatamount="0" 
        alreadydeliveredamount="0" />
 <OrderLine id="" amount="0" contractmaterialid="" description="test 2" materialid="" 
        priceagreementid="" priceperunit="0" totalamount="0" vatamount="0" 
        alreadydeliveredamount="0" />
</OrderLines></Data>
</asp:XmlDataSource>



<asp:Label ID="LabelOrderData" runat="server" 
    Text="" Visible="False"></asp:Label>


<asp:Label ID="LabelDeliveryPeriode" runat="server" Text="Leverperiode : " Visible="False"></asp:Label>
<asp:Label ID="LabelInStock" runat="server" Text="Op voorraad : " Visible="False"></asp:Label>
<asp:Label ID="LabelDeliveredAmount" runat="server" Text="Al geleverd : " Visible="False"></asp:Label>
<asp:Label ID="LabelContractAmount" runat="server" Text="Te leveren hoeveelheid : " Visible="False"></asp:Label>
<asp:Label ID="LabelContractGuidanceAmount" runat="server" Text="Verkregen vanuit contractbegeleiding : " Visible="False"></asp:Label>
<asp:Label ID="LabelMaxContractAmount" runat="server" Text="Max nog te leveren : " Visible="False"></asp:Label>
<asp:Label ID="LabelPriceAgreement" runat="server" Text="Prijsovereenkomst " Visible="False"></asp:Label>
<asp:Label ID="LabelContract" runat="server" Text="Contract " Visible="False"></asp:Label>
<asp:Label ID="LabelCustID" runat="server" Text="{00000000-0000-0000-0000-000000000000}" Visible="False"></asp:Label>
<asp:Label ID="LabelOrderID" runat="server" Text="{00000000-0000-0000-0000-000000000000}" Visible="False"></asp:Label>
<asp:Label ID="LabelLocationID" runat="server" Text="{00000000-0000-0000-0000-000000000000}" Visible="False"></asp:Label>

<asp:Label ID="LabelPriority" runat="server" Text="Prioriteit" Visible="False"></asp:Label>

<asp:Label ID="LabelInvoiceType" runat="server" Text="Buy" Visible="False"></asp:Label>
<asp:Label ID="LabelReverseInvoiceType" runat="server" Text="Sell" Visible="False"></asp:Label>



