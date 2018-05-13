<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlShowLinks.ascx.cs" Inherits="TMS_Recycling.WebUserControlShowLinks" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<cc11:ClassGridView ID="GridView1" runat="server" 
    DataSourceID="EntityDataSourceLinks" EnableViewState="False" 
    onrowdatabound="GridView1_RowDataBound" AllowSorting="True" 
    EmptyDataText="Er zijn geen gegevens gevonden die voldoen aan uw zoekcriteria." 
    ShowHeaderWhenEmpty="True">
    <Columns>
        <asp:TemplateField HeaderText="Link"></asp:TemplateField>
    </Columns>
</cc11:ClassGridView>
<cc11:ClassEntityDataSource ID="EntityDataSourceLinks" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" Include="" 
    oncontextcreated="EntityDataSourceLinks_ContextCreated">
</cc11:ClassEntityDataSource>
<cc11:ClassEntityDataSource ID="EntityDataSourceTest" runat="server" CommandText="select it.Id,it.Description,it.OrderNumber,it.BookingDateTime from OrderSet as it inner join MaterialMutationSet as mms on it.id=mms.Order.Id inner join ContractGuidanceMaterialMutationSet as cmms on cmms.Id=mms.ContractGuidanceMaterialMutation.Id inner join RelationContractMaterialSet as rcms on rcms.Id=cmms.RelationContractMaterial.Id inner join RelationContractSet as rcs on rcs.Id=rcms.RelationContract.Id where rcs.Id=&quot;482f12c3-cbfc-46f4-8c05-1e7ca35148e3&quot; order by it.BookingDateTime desc" ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EntitySetName="" EntityTypeFilter="" 
    Select="">
</cc11:ClassEntityDataSource>

