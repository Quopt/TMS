<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlRentMaterials.ascx.cs" Inherits="TMS_Recycling.WebUserControlRentMaterials" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="URLPopUpControl.ascx" tagname="URLPopUpControl" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Label ID="LabelRentMaterials" runat="server" CssClass="SubMenuHeader" 
    Text="Materialen in deze verhuur"></asp:Label>
<table style="width: 100%;">
    <tr>
        <td>
            <asp:Label ID="LabelMaterial" runat="server" Text="Materiaal type"></asp:Label>
        </td>
        <td>
            <cc11:ClassComboBox ID="ComboBoxMaterialType" runat="server" 
                AutoCompleteMode="SuggestAppend" AutoPostBack="True" 
                DataSourceID="EntityDataSourceMaterialType" 
                DataTextField="Description" DataValueField="Id" DropDownStyle="DropDownList" 
                MaxLength="0" 
                onselectedindexchanged="DropDownListMaterials_SelectedIndexChanged" 
                style="display: inline;"></cc11:ClassComboBox>
            <br />
        </td>
        <td>
            <asp:Label ID="LabelSpecificMaterialOr" runat="server" Text="Specifiek materiaal of"></asp:Label>
            <br />
            <asp:Label ID="LabelAmount" runat="server" Text="Hoeveelheid"></asp:Label>
        </td>
        <td>
            <asp:RadioButtonList ID="RadioButtonListSpecificOrAmount" runat="server">
                <asp:ListItem Selected="True" Value="Specific">Specifiek materiaal</asp:ListItem>
                <asp:ListItem Value="Amount">Hoeveelheid materiaal</asp:ListItem>
            </asp:RadioButtonList>
        </td>
        <td>
            <cc11:ClassComboBox ID="ComboBoxMaterials" runat="server" AutoCompleteMode="SuggestAppend" 
                DropDownStyle="DropDownList" DataSourceID="EntityDataSourceMaterials" 
                DataTextField="DescriptionX" DataValueField="Id" MaxLength="0" 
                style="display: inline;"></cc11:ClassComboBox>
            <br />
            <asp:TextBox ID="TextBoxAmount" runat="server"></asp:TextBox>
            <asp:MaskedEditExtender ID="TextBoxAmount_MaskedEditExtender" runat="server" 
                Mask="999" TargetControlID="TextBoxAmount">
            </asp:MaskedEditExtender>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
    <td>
    </td>
    <td colspan="2"  style="border-bottom:solid 1px black; border-left:solid 1px black">
        <uc1:URLPopUpControl ID="URLPopUpControlRentMaterialTypes" runat="server" 
            Text="Toon materiaaltypes" 
            URLToPopup="webformpopup.aspx?uc=RentMaterialTypeOverview"  
            OnPopUpClosed="URLPopUpControlRentMaterialTypes_PopupClosed" 
            EnableViewState="True"  />
    </td>
    <td colspan="2" style="border-bottom:solid 1px black; border-left:solid 1px black">
        <uc1:URLPopUpControl ID="URLPopUpControlAlternativeMaterials" runat="server" 
            Text="Toon alternatieve materiaaltypes" Visible="False" 
            EnableViewState="True" />&nbsp;
    </td>
    <td >
        &nbsp;</td>
    </tr>
    <tr>
    <td>
    </td>
    <td>
            <asp:Label ID="LabelPrice" runat="server" Text="Klantlokatie"></asp:Label>
    </td>
    <td>
            <cc11:ClassComboBox ID="ComboBoxCustomerLocation" runat="server" AutoCompleteMode="SuggestAppend" 
                DropDownStyle="DropDownList" 
                DataSourceID="EntityDataSourceCustomerLocations" DataTextField="Description" 
                DataValueField="Id" AppendDataBoundItems="True">
                <asp:ListItem Value="">-niet bekend-</asp:ListItem>
            </cc11:ClassComboBox>
    </td>
    <td>

            <asp:Label ID="LabelDiscount" runat="server" Text="Kortingspercentage"></asp:Label>

    </td>
    <td>
            <asp:TextBox ID="TextBoxDiscountPercentage" runat="server">0</asp:TextBox>
            <asp:MaskedEditExtender ID="TextBoxDiscountPercentage_MaskedEditExtender" runat="server" 
                Mask="99" TargetControlID="TextBoxDiscountPercentage"></asp:MaskedEditExtender>
    </td>
    <td>
    </td>
    </tr>
    <tr>
    <td>
    </td>
    <td colspan="2" style="border-bottom:solid 1px black; border-left:solid 1px black">
        <uc1:URLPopUpControl ID="URLPopUpControlCustomerRelationLocation" 
            runat="server" Text="Beheer klantlokaties" 
            URLToPopup="webformpopup.aspx?uc=CustomerRelationLocationOverview&amp;Id=" 
            OnPopUpClosed="URLPopUpControlCustomerRelationLocation_PopupClosed" 
            EnableViewState="True" />
    </td>
    <td  style="text-align:right; border-bottom:solid 1px black; border-left:solid 1px black" colspan="2">
    &nbsp;
        <asp:CheckBox ID="CheckBoxTreatAsAdvancePayment" runat="server" 
            Text="Behandelen als voorschot" />
    </td>
    <td>
            <asp:Button ID="ButtonAddMaterial" runat="server" 
                onclick="ButtonAddMaterial_Click" Text="Materiaal toevoegen" EnableViewState="False" 
                 />
    </td>
    </tr>
</table>
<hr />
<cc11:ClassGridView ID="GridViewOrderMaterials" runat="server" 
    AutoGenerateColumns="False" 
    DataSourceID="XmlDataSourceOrderLines" PageSize="99999" 
    onrowdeleting="GridViewOrderMaterials_RowDeleting" DataKeyNames="id">
    <Columns>
        <asp:CommandField DeleteText="Verwijder regel" ShowDeleteButton="True" />
        <asp:BoundField DataField="id" HeaderText="id" Visible="False" />
        <asp:BoundField DataField="rentaltypeid" HeaderText="RentalTypeId" SortExpression="rentaltypeid" 
            Visible="False" />
        <asp:BoundField DataField="rentalitemid" HeaderText="RentalItemId" 
            Visible="False" SortExpression="rentalitemid" />
        <asp:BoundField DataField="rentaltype" HeaderText="Materiaal type" 
            SortExpression="rentaltype" ReadOnly="True" />
        <asp:BoundField DataField="rentalitem" HeaderText="Specifiek materiaal" 
            SortExpression="rentalitem" ReadOnly="True" />
        <asp:BoundField DataField="rentalitemamount" HeaderText="Hoeveelheid" 
            SortExpression="rentalitemamount" ReadOnly="True" />
        <asp:BoundField DataField="rentprice" HeaderText="Prijs" 
            SortExpression="rentprice" ReadOnly="True" />
        <asp:BoundField DataField="discountpercentage" HeaderText="Kortingspercentage" 
            SortExpression="discountpercentage" />
        <asp:BoundField DataField="vat" 
            HeaderText="BTW" SortExpression="vat" ReadOnly="True"/>
        <asp:BoundField DataField="totalrentprice" HeaderText="Totaalprijs" 
            SortExpression="totalrentprice" ReadOnly="True" />
        <asp:BoundField DataField="customerlocation" HeaderText="Verhuurlokatie" 
            SortExpression="customerlocation" ReadOnly="True" />
        <asp:CheckBoxField DataField="treatasadvancepayment" 
            HeaderText="Behandelen als voorschot" SortExpression="treatasadvancepayment" />
    </Columns>
</cc11:ClassGridView>


<asp:Panel ID="PanelPrices" runat="server" Visible="False">
<table>
<tr>
    <td>
    <asp:Label ID="LabelTotalPriceExplanation" runat="server" 
        Text="Totaal prijs order (ex BTW)"></asp:Label>
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
<hr />
<asp:Label ID="LabelCustomerID" runat="server" Visible="False"></asp:Label>
<asp:Label ID="LabelLocationID" runat="server" Visible="False" Text="00000000-0000-0000-0000-000000000000"></asp:Label>
<asp:Label ID="LabelStartRentDate" runat="server" Visible="False"></asp:Label>
<asp:Label ID="LabelEndRentDate" runat="server" Visible="False"></asp:Label>
<asp:Label ID="LabelOrderLines" runat="server" Visible="False"></asp:Label>

<cc11:ClassEntityDataSource ID="EntityDataSourceMaterialType" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EntitySetName="RentalTypeSet" 
    OrderBy="it.Description" Select="it.[Id], it.[Description]" Where="it.IsActive">
</cc11:ClassEntityDataSource>

<cc11:ClassEntityDataSource ID="EntityDataSourceMaterials" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" 
    OrderBy="" Where="" CommandText="
select distinct it.Id, it.Description, it.Description +&quot; (&quot; + CAST( it.BaseRentalPrice as System.String) + &quot;)&quot; as DescriptionX
from RentalItemSet as it 
where 
it.IsActive and 
(it.RentalType.Id = @RentalType) and (it.Location.Id = @LocationID) and
( (it.ItemState = &quot;Available&quot;) or (it.ItemState = &quot;Rented&quot;) ) and
( it not in (
	select value itx2.RentalItem 
	from RentalItemActivitySet as itx2 
	where itx2.RentalItem.Id = it.Id and
	 ( 
	   ((@StartDate &lt;= itx2.RentStartDateTime) and (@EndDate &gt;= itx2.RentStartDateTime)) 
	 ) or
	 (
	   ((@StartDate &gt;= itx2.RentStartDateTime) and (@StartDate &lt;= itx2.RentEndStartDateTime))
	 )
  )
)
order by DescriptionX" EntitySetName="" EntityTypeFilter="" Select="">
    <CommandParameters>
        <asp:Parameter Name="RentalType" DbType="Guid" />
        <asp:Parameter Name="LocationId" DbType="Guid" />
        <asp:Parameter Name="StartDate" DbType="DateTime" />
        <asp:Parameter Name="EndDate" DbType="DateTime" />
        <asp:Parameter Name="BorderEndDate" DbType="DateTime" />
    </CommandParameters>
</cc11:ClassEntityDataSource>

<cc11:ClassEntityDataSource ID="EntityDataSourceCustomerLocations" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EntitySetName="RelationLocationSet" 
    OrderBy="it.Description" Select="it.[Id], it.[Description]" 
    Where="it.IsActive and it.Relation.Id = @Id">
    <WhereParameters>
        <asp:ControlParameter ControlID="LabelCustomerID" DbType="Guid" 
            Name="Id" PropertyName="Text" />
    </WhereParameters>
</cc11:ClassEntityDataSource>











<asp:XmlDataSource ID="XmlDataSourceOrderLines" runat="server" 
    EnableCaching="False" EnableViewState="False">
    <Data>
<OrderLines>
 <OrderLine id="" discountpercentage="0" rentalitem="Drill" rentalitemamount="1" 
        rentalitemid="" rentaltype="Large drills (500 watt)" rentaltypeid="" 
        rentprice="0" totalrentprice="0" vat="0" customerlocation="lok1" 
        customerlocationid="" bailprice="0" treatasadvancepayment="false" />
 <OrderLine id="" discountpercentage="0" rentalitem="" rentalitemamount="10" 
        rentalitemid="" rentaltype="Large drills (500 watt)" rentaltypeid="" 
        rentprice="0" totalrentprice="0" vat="0" customerlocation="lok2" 
        customerlocationid="" bailprice="0" treatasadvancepayment="true" />
</OrderLines></Data>
</asp:XmlDataSource>












