<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlEditAdvancePayments.ascx.cs" Inherits="TMS_Recycling.WebUserControlEditAdvancePayments" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit.HTMLEditor" tagprefix="cc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Label ID="LabelAdvancePayments" runat="server" Text="Correctie van vooruitbetalingen" CssClass="SubMenuHeader"></asp:Label>
<table style="width: 100%;"><tr>
<td>    
    <asp:Label ID="LabelAdvancePaymentsDesc" runat="server" Text="Openstaande voorschotten"></asp:Label>
</td>
<td>
        <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" ID="DropDownListAdvancePayments" runat="server" 
            DataSourceID="EntityDataSourceAdvancePayments" DataTextField="Description" 
            DataValueField="Id" AutoPostBack="True" 
            onselectedindexchanged="DropDownListAdvancePayments_SelectedIndexChanged" >
        </cc11:ClassComboBox>
</td>
<td>
    <asp:Label ID="LabelAdvancePaymentAmountToBeCorrected" runat="server" 
        Text="Totaal te corrigeren bedrag"></asp:Label>
    &nbsp;
    <asp:TextBox ID="TextBoxAdvancePaymentAmountToBeCorrected" runat="server">0</asp:TextBox>
    &nbsp;
    <asp:Button ID="ButtonAddAdvancePaymentCorrection" runat="server" 
        onclick="ButtonAddAdvancePaymentCorrection_Click" 
        Text="Correctiebedrag in mindering brengen op order" 
        EnableViewState="False" />
</td>
</tr>
<tr>
<td>
</td>
<td>
    <asp:Label ID="LabelAdvancePaymentInformation" runat="server" Text=""></asp:Label>
</td>
</tr>
</table>
    <cc11:ClassGridView ID="GridViewAdvancePaymentCorrections" runat="server" 
        AutoGenerateColumns="False" AllowPaging="True" 
        DataSourceID="XmlDataSourceAdvancePaymentCorrections" DataKeyNames="Id" onrowdeleting="GridViewAdvancePaymentCorrections_RowDeleting" 
        >
        <Columns>
            <asp:BoundField DataField="id" HeaderText="id" 
                Visible="False" SortExpression="id" />
            <asp:CommandField DeleteText="Verwijder regel" ShowDeleteButton="True" />
            <asp:BoundField DataField="description" 
                HeaderText="Toelichting" SortExpression="description" />
            <asp:BoundField DataField="amount" HeaderText="Bedrag" 
                SortExpression="amount" />
        </Columns>
    </cc11:ClassGridView>
<asp:Label ID="LabelAPTotalAmount" runat="server" Text="Totaal voorschot : " 
    Visible="False"></asp:Label>
<asp:Label ID="LabelAPPaidBackAmout" runat="server" Text="Al terugbetaald : " 
    Visible="False"></asp:Label>
<asp:Label ID="LabelAPStillOpen" runat="server" Text="Nog terug te betalen : " 
    Visible="False"></asp:Label>
<asp:Label ID="LabelAPPayDate" runat="server" Text="Uitbetaald op : " 
    Visible="False"></asp:Label>
<asp:Label ID="LabelAPVATWarning" runat="server" Text="Let op : {0}% BTW wordt nog extra berekend over het af te trekken werk derden bedrag." 
    Visible="False"></asp:Label>
<asp:Label ID="LabelAdvancePaymentData" runat="server" 
    Text="" Visible="False"></asp:Label>
<asp:Label ID="LabelWorkText" runat="server" Text="Werk derden " Visible="False"></asp:Label>
<asp:Label ID="LabelAPText" runat="server" Text="Voorschot " Visible="False"></asp:Label>

<asp:Label ID="LabelCustID" runat="server" Text="{00000000-0000-0000-0000-000000000000}" Visible="False"></asp:Label>
<asp:Label ID="LabelInvoiceID" runat="server" Text="{00000000-0000-0000-0000-000000000000}" Visible="False"></asp:Label>
<asp:Label ID="LabelWorkType" runat="server" Text="ByUs" Visible="False"></asp:Label>
<asp:Label ID="LabelPaymentType" runat="server" Text="Paid" Visible="False"></asp:Label>
<asp:XmlDataSource ID="XmlDataSourceAdvancePaymentCorrections" runat="server" 
    EnableCaching="False" EnableViewState="False">
    <Data>
<AdvancePaymentLines>
<AdvancePayment description="1" id="" amount="100"/>
</AdvancePaymentLines></Data>
</asp:XmlDataSource>

<cc11:ClassEntityDataSource ID="EntityDataSourceAdvancePayments" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" OrderBy="it.Description" 
    
    Where="" CommandText="(
select it.[Id], @APText + it.[Description] as Description 
from RelationAdvancePaymentSet as it 
where
(it.Relation.Id = @CustID) and (it.IsPaidOut = true) and (it.IsPaidBack = false) and (it.PaymentType = @PaymentType)
)
union all 
(
select it.[Id], @WorkText + it.Description as Description
from RelationWorkSet as it
where
(it.Relation.Id = @CustID) and (it.IsActive) and (it.IsTreatedAsAdvancePayment) and (it.WorkType = @WorkType)
)">
    <CommandParameters>
        <asp:ControlParameter ControlID="LabelCustID" DbType="Guid" Name="CustID" 
            PropertyName="Text" />
        <asp:ControlParameter ControlID="LabelWorkText" Name="WorkText" 
            PropertyName="Text" Type="String" />
        <asp:ControlParameter ControlID="LabelAPText" Name="APText" PropertyName="Text" 
            Type="String" />
        <asp:ControlParameter ControlID="LabelPaymentType" Name="PaymentType" 
            PropertyName="Text" Type="String" />
        <asp:ControlParameter ControlID="LabelWorkType" Name="WorkType" 
            PropertyName="Text" Type="String" />
    </CommandParameters>
</cc11:ClassEntityDataSource>

