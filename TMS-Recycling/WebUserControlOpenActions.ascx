<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlOpenActions.ascx.cs" Inherits="TMS_Recycling.WebUserControlOpenActions" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>


<cc11:ClassGridView ID="ClassGridViewOpenActions" runat="server" 
    AutoGenerateColumns="False" DataSourceID="EntityDataSourceOpenActions" 
    EmptyDataText="Er zijn geen gegevens gevonden die voldoen aan uw zoekcriteria." 
    ShowHeaderWhenEmpty="True" AllowPaging="True" AllowSorting="True" 
    DataKeyNames="Id,rcsId,rsId" 
    onselectedindexchanged="ClassGridViewOpenActions_SelectedIndexChanged">
    <Columns>
        <asp:CommandField SelectText="Selecteer" ShowSelectButton="True" />
        <asp:BoundField DataField="Relation" HeaderText="Klant" ReadOnly="True" 
            SortExpression="Relation" />
        <asp:BoundField DataField="RelationName" HeaderText="Contact" 
            ReadOnly="True" SortExpression="RelationName" />
        <asp:BoundField DataField="Description" HeaderText="Toelichting" 
            ReadOnly="True" SortExpression="Description" />
        <asp:BoundField DataField="ContactDateTime" HeaderText="Datum/tijd" 
            ReadOnly="True" SortExpression="ContactDateTime" />
        <asp:BoundField DataField="FollowUpDateTime" HeaderText="Actie datum/tijd" 
            ReadOnly="True" SortExpression="FollowUpDateTime" />
        <asp:BoundField DataField="PausedUntilDateTime" HeaderText="Gepauzeerd tot" 
            ReadOnly="True" SortExpression="PausedUntilDateTime" />
    </Columns>
</cc11:ClassGridView>
<cc11:ClassEntityDataSource ID="EntityDataSourceOpenActions" 
    runat="server" ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" 
    EntityTypeFilter="" Where="" CommandText="select it.Id, it.[Description], it.[ContactDateTime], it.[ContactType], it.[FollowUpDateTime], it.[FollowUpState], it.[PausedUntilDateTime], it.[Handler], rcs.Description as RelationName, rs.Description as Relation, rcs.Id as rcsId, rs.Id as rsId
from RelationContactLogSet as it inner join RelationContactSet as rcs on it.RelationContact.Id = rcs.Id 
inner join RelationSet as rs on rcs.Relation.Id = rs.Id
where ((it.FollowUpState = &quot;Unhandled&quot; and it.FollowUpDateTime &lt;= @ClientDateTime) or 
(it.FollowUpState = &quot;Paused&quot; and it.PausedUntilDateTime &lt;= @ClientDateTime))
and (it.Handler = @Handler)" OrderBy="it.FollowUpDateTime desc">
    <CommandParameters>
        <asp:Parameter DbType="DateTime" Name="ClientDateTime" />
        <asp:Parameter DbType="Guid" Name="Handler" />
    </CommandParameters>
</cc11:ClassEntityDataSource>

