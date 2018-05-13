<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlOrderBase.ascx.cs" Inherits="TMS_Recycling.WebUserControlOrderBase" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="CalendarWithTimeControl.ascx" tagname="CalendarWithTimeControl" tagprefix="uc1" %>
<%@ Register src="URLPopUpControl.ascx" tagname="URLPopUpControl" tagprefix="uc2" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<table width="100%">
<tr>
 <td colspan="2">
     <asp:Label ID="LabelBasicData" runat="server" 
         Text="Basisgegevens order" CssClass="SubMenuHeader"></asp:Label></td>
 <td>    &nbsp;</td>
 <td>    
        &nbsp;</td>
 <td>     
     <br />
    </td>
 <td>   
            <nobr>
            <asp:CheckBox ID="CheckBox_IsCorrected_Checked" Text="Is gecorrigeerd" runat="server" 
                Checked="True" /> 
            </nobr>
            <br />
            <nobr>
            <asp:CheckBox ID="CheckBox_DeterminePriceDuringInvoicing_Checked" Text="Bepaal prijs tijdens factureren" runat="server" 
                Checked="True" /> 
            </nobr>
    </td>
</tr>

<tr>
<td>
    <asp:Label ID="LabelName" runat="server" Text="Orderomschrijving"></asp:Label> </td>
    <td colspan="3"> <asp:TextBox ID="TextBox_Description_Text" runat="server" Width="90%" 
            MaxLength="250" ></asp:TextBox>
    </td>
    <td>
    <nobr>
    <asp:Label ID="LabelOrderNumber" runat="server" Text="Order nummer"></asp:Label> 
    </nobr>
    </td>
        <td>
    <asp:Label ID="Label_OrderNumber" runat="server" Text="..."></asp:Label> 
    </td>
</tr>

<tr>
<td>
    <asp:Label ID="LabelOrderStatus" runat="server" Text="Order status"></asp:Label> 
</td>
<td style="vertical-align:top;">
        <nobr>
        <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" ID="DropDownList_OrderStatus_SelectedValue" runat="server" 
                Enabled="False">
        </cc11:ClassComboBox> 
        <asp:Button ID="ButtonToProcessed" runat="server" Text="Status naar geleverd" 
            onclick="ButtonToProcessed_Click"></asp:Button>
        </nobr>
        <br />
            <asp:Label ID="LabelProcessingInfo" runat="server" Text="LET OP : Zet de status van de order op geleverd en u kunt niets meer aan deze order wijzigen."></asp:Label>
</td>
<td>
    <asp:Label ID="LabelOrderDate" runat="server" Text="Boekdatum"></asp:Label> 
</td>
<td>
    <uc1:CalendarWithTimeControl ID="CalendarWithTimeControl_BookingDateTime_SelectedDateTime" runat="server" />
</td>
<td>
</td>
<td>
</td>
</tr>

<tr>
    <td>    <asp:Label ID="LabelRelation" runat="server" Text="Relatie"></asp:Label> </td>
    <td>
        <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" ID="DropDownList_Relation" runat="server" 
            DataSourceID="EntityDataSourceRelations" DataTextField="Description" 
            DataValueField="Id" AutoPostBack="True">
            <asp:ListItem Value="00000000-0000-0000-0000-000000000000">- nog niet bekend -</asp:ListItem>
        </cc11:ClassComboBox>  
        </td>
    <td><asp:Label ID="LabelLedgerType" runat="server" Text="Onze lokatie"></asp:Label> </td>
    <td> 
        <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" ID="DropDownList_Location" runat="server" 
                DataSourceID="EntityDataSourceLocations" 
                DataTextField="Description" DataValueField="Id">
        </cc11:ClassComboBox> 
    </td>
    <td><asp:Label ID="LabelLedgerType0" runat="server" Text="Onze verkoper"></asp:Label> </td>
    <td>
        <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" ID="DropDownList_StaffMemberPurchaser" runat="server" 
                DataSourceID="EntityDataSourceStaffMembers" 
                DataTextField="Description" DataValueField="Id">
        </cc11:ClassComboBox>  
        </td>
</tr>

<tr>
    <td><asp:Label ID="LabelRelationLocation" runat="server" Text="Klantlokatie"></asp:Label> </td>
    <td>
        <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" 
            ID="DropDownList_RelationLocation" runat="server" 
                DataSourceID="EntityDataSourceRelationLocations" 
            DataTextField="Description" DataValueField="Id" 
            AppendDataBoundItems="True">
            <asp:ListItem Value="">-nvt-</asp:ListItem>
        </cc11:ClassComboBox> 
        </td>
    <td><asp:Label ID="LabelCustomerProject" runat="server" Text="Klantproject"></asp:Label> </td>
    <td> 
        <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" ID="DropDownList_RelationProject" runat="server" 
                DataSourceID="EntityDataSourceRelationProjects" 
            DataTextField="Description" DataValueField="Id" AppendDataBoundItems="True">
            <asp:ListItem Value="">-nvt-</asp:ListItem>
        </cc11:ClassComboBox> 
    </td>
    <td><asp:Label ID="LabelCustomerContact" runat="server" Text="Klantcontact"></asp:Label> </td>
    <td>
        <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" ID="DropDownList_RelationContact" runat="server" 
                DataSourceID="EntityDataSourceRelationContacts" 
            DataTextField="Description" DataValueField="Id" AppendDataBoundItems="True">
            <asp:ListItem Value="">-nvt-</asp:ListItem>
        </cc11:ClassComboBox> 
        </td>
</tr>


<tr>
    <td><asp:Label ID="YourTruckPlate" runat="server" Text="Kenteken brenger"></asp:Label> </td>
    <td>
        <asp:TextBox ID="TextBox_YourTruckPlate" runat="server" Width="150px" 
            MaxLength="250" ></asp:TextBox>
        </td>
    <td><asp:Label ID="LabelYourDriverName" runat="server" Text="Naam brenger"></asp:Label> </td>
    <td> 
        <asp:TextBox ID="TextBox_YourDriverName" runat="server" Width="150px" 
            MaxLength="250" ></asp:TextBox>
    </td>
    <td>
        </td>
    <td> 
    </td>
</tr>

<tr>
    <td>
        <asp:Label ID="LabelFreight" runat="server" Text="Vracht"></asp:Label> 
    </td>
    <td>
        <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" ID="DropDownList_Freight" runat="server" 
                DataSourceID="EntityDataSourceRelationFreights" 
            DataTextField="Description" DataValueField="Id">
        </cc11:ClassComboBox> 
        </td>
    <td>
        <asp:Label ID="LabelFreightID" runat="server" Text="PMV"></asp:Label> 
    </td>
    <td>
        <asp:TextBox ID="TextBox_FreightID" runat="server" Width="150px" 
            MaxLength="250" ></asp:TextBox>
    </td>
    <td>
    </td>
    <td> 
    </td>


</tr>

<tr>
<td>
        <asp:Label ID="LabelTotalAmount" runat="server" Text="Totaal bedrag"></asp:Label> 
</td>
<td>
        <asp:Label ID="Label_TotalPrice" runat="server" Text="..."></asp:Label> 
</td>
<td>
        <asp:Label ID="LabelTotalWeight" runat="server" Text="Totaal gewicht"></asp:Label> 
</td>
<td>
        <asp:Label ID="Label_TotalAmount" runat="server" Text="..."></asp:Label> 
</td>
</tr>
</table>
<br />
<asp:Label ID="LabelMaterialOverview" runat="server" Text="Overzicht materialen in deze order"  CssClass="SubMenuHeader"></asp:Label>
<hr />
<table>
<tr>
<td colspan="6">
    <cc11:ClassGridView ID="GridViewOrderLines" runat="server" AllowPaging="True" 
        AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="Id" 
        DataSourceID="EntityDataSourceOrderLines" 
        onrowdeleting="GridViewOrderLines_RowDeleting" 
        onrowupdating="GridViewOrderLines_RowUpdating">
        <Columns>
            <asp:CommandField DeleteText="Verwijderen" ShowDeleteButton="True" />
            <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" 
                SortExpression="Id" Visible="False" />
            <asp:BoundField DataField="Description" HeaderText="Omschrijving" 
                SortExpression="Description" />
            <asp:BoundField DataField="Amount" HeaderText="Hoeveelheid" 
                SortExpression="Amount" />
            <asp:BoundField DataField="PricePerUnit" HeaderText="Prijs per eenheid" 
                SortExpression="PricePerUnit" />
            <asp:BoundField DataField="PriceExVAT" HeaderText="Prijs ex BTW" 
                SortExpression="PriceExVAT" ReadOnly="True" />
            <asp:BoundField DataField="AlreadyDeliveredAmount" 
                HeaderText="Al geleverde hoeveelheid" 
                SortExpression="AlreadyDeliveredAmount" />
            <asp:CommandField CancelText="Annuleren " EditText="Bewerken" 
                ShowEditButton="True" UpdateText="Bijwerken" />
        </Columns>
    </cc11:ClassGridView>
</td>
</tr>
<tr>
<td>
    <asp:Label ID="LabelMaterial" runat="server" Text="Materiaal"></asp:Label></td><td>
        <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" ID="DropDownListMaterials" runat="server" 
            DataSourceID="EntityDataSourceMaterialsContractsAndAgreements" DataTextField="Description" 
            DataValueField="Id" AutoPostBack="True" 
            onselectedindexchanged="DropDownListMaterials_SelectedIndexChanged">
        </cc11:ClassComboBox><br />

    </td><td>
        <asp:Label ID="LabelAmount" runat="server" Text="Hoeveelheid"></asp:Label></td><td>
            <asp:TextBox ID="TextBoxAmount" runat="server"></asp:TextBox></td><td>
                <asp:Label ID="LabelPrice" runat="server" Text="Prijs per eenheid"></asp:Label></td><td>
                    <asp:TextBox ID="TextBoxPrice" runat="server"></asp:TextBox></td>
<td>
    <asp:Button ID="ButtonAddMaterial" runat="server" 
        Text="Materiaal toevoegen aan order" onclick="ButtonAddMaterial_Click" />
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
<hr />
<br />

<table  width="100%">
<tr>
    <td><asp:Label ID="LabelComments" runat="server" Text="Opmerkingen"></asp:Label> </td>
    <td width="60%"> 
        <asp:TextBox ID="TextBox_Comments" runat="server" Width="95%" 
            MaxLength="2000" Rows="4" TextMode="MultiLine" ></asp:TextBox>
    </td>
<td>
        <asp:Label ID="LabelLinkedInvoice" runat="server" Text="Koppeling naar factuur"></asp:Label> 
</td>
<td>

    <uc2:URLPopUpControl ID="URLPopUpControlInvoice" runat="server" 
        EnableViewState="False" Text="Open factuur" ViewStateMode="Inherit" />

</td>

</tr>
</table>



<asp:Button ID="ButtonCancel" runat="server" Text="Wijzigingen annuleren" 
    onclick="ButtonCancel_Click" />
<asp:Button ID="ButtonSave" runat="server" Text="Wijzigingen opslaan" 
    onclick="ButtonSave_Click" />
<uc2:URLPopUpControl ID="URLPopUpControlShowOrder" runat="server" Text="Toon orderoverzicht"/>
<asp:Button ID="ButtonDelete" runat="server" Text="Order corrigeren" OnClientClick="return confirm('Door de order te corrigeren wordt deze inkoop in het magazijn teruggedraaid. U kunt vervolgens de order klonen naar een nieuwe order om deze opnieuw te kunnen verwerken. Weet u het zeker dat u dit wilt?');"
    onclick="ButtonDelete_Click"  />
<asp:Button ID="ButtonCloneOrder" runat="server" OnClientClick="return confirm('Hiermee maakt u een nieuwe order aan met exact dezelfde inhoud als deze order. Weet u het zeker dat u dit wilt?');"
    Text="Order klonen naar onverwerkt" Visible ="false" 
    onclick="ButtonCloneOrder_Click"/>
<asp:Label ID="LabelDeleteOrderWarning" runat="server" Text="Let op: u kunt deze order alleen corrigeren door de gehele factuur te corrigeren waarop deze order is geplaatst! Klik op de link naar factuur om deze order te corrigeren." Visible="false"></asp:Label>

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


<cc11:ClassEntityDataSource ID="EntityDataSourceLocations" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EnableFlattening="False" 
    EntitySetName="LocationSet" Select="it.[Description], it.[Id], it.IsActive" 
    
    Where="(it.IsActive = true) ">
    <WhereParameters>
        <asp:ControlParameter ControlID="LabelOrderType" DbType="String" 
            DefaultValue="Creditor" Name="RelationType" PropertyName="Text" />
    </WhereParameters>
</cc11:ClassEntityDataSource>


<cc11:ClassEntityDataSource ID="EntityDataSourceStaffMembers" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EnableFlattening="False" 
    EntitySetName="StaffMemberSet" Select="it.[Description], it.[Id], it.IsActive" 
    
    Where="(it.IsActive = true) ">
    <WhereParameters>
        <asp:ControlParameter ControlID="LabelOrderType" DbType="String" 
            DefaultValue="Creditor" Name="RelationType" PropertyName="Text" />
    </WhereParameters>
</cc11:ClassEntityDataSource>


<cc11:ClassEntityDataSource ID="EntityDataSourceRelationLocations" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EnableFlattening="False" 
    EntitySetName="RelationLocationSet" Select="it.[Description], it.[Id], it.IsActive" 
    
    Where="(it.IsActive = true) and (it.Relation.Id = @RelationID)" 
        EntityTypeFilter="">
    <WhereParameters>
        <asp:ControlParameter ControlID="DropDownList_Relation" DbType="Guid" 
            DefaultValue="" Name="RelationID" PropertyName="SelectedValue" />
    </WhereParameters>
</cc11:ClassEntityDataSource>


<cc11:ClassEntityDataSource ID="EntityDataSourceRelationProjects" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EnableFlattening="False" 
    EntitySetName="RelationProjectSet" Select="it.[Description], it.[Id], it.IsActive" 
    
    Where="(it.IsActive = true) and (it.Relation.Id = @RelationID)" 
        EntityTypeFilter="">
    <WhereParameters>
        <asp:ControlParameter ControlID="DropDownList_Relation" DbType="Guid" 
            DefaultValue="" Name="RelationID" PropertyName="SelectedValue" />
    </WhereParameters>
</cc11:ClassEntityDataSource>


<cc11:ClassEntityDataSource ID="EntityDataSourceRelationContacts" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EnableFlattening="False" 
    EntitySetName="RelationContactSet" Select="it.[Description], it.[Id], it.IsActive" 
    
    Where="(it.IsActive = true) and (it.Relation.Id = @RelationID)" 
        EntityTypeFilter="">
    <WhereParameters>
        <asp:ControlParameter ControlID="DropDownList_Relation" DbType="Guid" 
            DefaultValue="" Name="RelationID" PropertyName="SelectedValue" />
    </WhereParameters>
</cc11:ClassEntityDataSource>


<cc11:ClassEntityDataSource ID="EntityDataSourceRelationFreights" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EnableFlattening="False" 
    EntitySetName="FreightSet" Select="it.[Description], it.[Id]" 
    
    
    Where="(it.IsActive = true) and ( (it.FromRelation.Id = @RelationID) || ((it.ToRelation.Id = @RelationID)))" 
    EntityTypeFilter="">
    <WhereParameters>
        <asp:ControlParameter ControlID="DropDownList_Relation" DbType="Guid" 
            DefaultValue="{00000000-0000-0000-0000-000000000000}" Name="RelationID" PropertyName="SelectedValue" />
    </WhereParameters>
</cc11:ClassEntityDataSource>


<cc11:ClassEntityDataSource ID="EntityDataSourceOrderLines" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EntitySetName="OrderLineSet" 
    Where="it.Order.Id = @OrderID">
    <WhereParameters>
        <asp:ControlParameter ControlID="LabelID" DbType="Guid" Name="OrderID" 
            PropertyName="Text" />
    </WhereParameters>
</cc11:ClassEntityDataSource>

<cc11:ClassEntityDataSource ID="EntityDataSourceMaterialsContractsAndAgreements" 
    runat="server" CommandText="
(
select it.[Id], &quot;MAT&quot; as LineType, it.[Description] as Description
from MaterialSet as it
where it.IsActive and (it.IsWorkInsteadOfMaterial = false) and (it.InvoiceType &lt;&gt; @MaterialType)
)

union all 

(
select cm.[Id], &quot;CON&quot; as LineType, @ContractText + SqlServer.STR(it.[OurReference]) + &quot; / &quot; + it.[Description] + &quot; / &quot; + cm.Description + &quot; (&quot; + cm.Material.Description + &quot;)&quot; as Description
from RelationContractSet as it join RelationContractMaterialSet as cm on it.Id = cm.RelationContract.Id 
WHERE
(it.ContractStatus = &quot;Open&quot;) and (it.Relation.Id = @CustID) and (it.ContractType = @OrderType) and (CurrentDateTime() between it.ContractStartDate and it.ContractEndDate) 
)

union all 

(
SELECT it.[Id], &quot;PAS&quot; as LineType, @PriceAgreementText + it.[Description] + &quot; (&quot; + it.Material.Description +&quot;)&quot; as Description
FROM RelationPriceAgreementSet as it
WHERE (it.IsActive) and (it.Relation.Id = @CustID) and (it.AgreementType = @OrderType) and (CurrentDateTime() between it.StartDateTime and it.EndDateTime  ) 
)" ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" 
    Select="it.[Id], it.LineType,  it.[Description]" EntitySetName="">
    <CommandParameters>
        <asp:ControlParameter ControlID="LabelCustID" DbType="Guid" Name="CustID" 
            PropertyName="Text" />
        <asp:ControlParameter ControlID="LabelPriceAgreement" Name="PriceAgreementText" 
            PropertyName="Text" Type="String" />
        <asp:ControlParameter ControlID="LabelOrderType" Name="OrderType" 
            PropertyName="Text" Type="String" />
        <asp:ControlParameter ControlID="LabelMaterialType" Name="MaterialType" 
            PropertyName="Text" Type="String" />
        <asp:ControlParameter ControlID="LabelContract" Name="ContractText" 
            PropertyName="Text" Type="String" />
    </CommandParameters>
</cc11:ClassEntityDataSource>

<asp:Label ID="LabelRelationType" runat="server" Text="Creditor" 
    Visible="False"></asp:Label>
<asp:Label ID="LabelOrderType" runat="server" Text="Creditor" 
    Visible="False"></asp:Label>
<asp:Label ID="LabelMaterialType" runat="server" Text="Creditor" 
    Visible="False"></asp:Label>
<asp:CheckBox ID="CheckBoxRefreshRequired" Visible="false" runat="server" />
<asp:Label ID="LabelID" runat="server" Visible="False" 
    Text="{00000000-0000-0000-0000-000000000000}"></asp:Label>
<asp:Label ID="LabelPriceAgreement" runat="server" Text="Prijsovereenkomst " Visible="False"></asp:Label>
<asp:Label ID="LabelContract" runat="server" Text="Contract " Visible="False"></asp:Label>
<asp:Label ID="LabelCustID" runat="server" 
    Text="{00000000-0000-0000-0000-000000000000}" Visible="False"></asp:Label>
<asp:Label ID="LabelDeliveryPeriode" runat="server" Text="Leverperiode : " Visible="False"></asp:Label>
<asp:Label ID="LabelInStock" runat="server" Text="Op voorraad : " Visible="False"></asp:Label>
<asp:Label ID="LabelDeliveredAmount" runat="server" Text="Al geleverd : " Visible="False"></asp:Label>
<asp:Label ID="LabelContractAmount" runat="server" Text="Te leveren hoeveelheid : " Visible="False"></asp:Label>
<asp:Label ID="LabelMaxContractAmount" runat="server" Text="Max nog te leveren : " Visible="False"></asp:Label>
