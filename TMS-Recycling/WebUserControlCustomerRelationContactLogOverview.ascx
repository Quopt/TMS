<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlCustomerRelationContactLogOverview.ascx.cs" Inherits="TMS_Recycling.WebUserControlCustomerRelationContactLogOverview" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit.HTMLEditor" tagprefix="cc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="WebUserControlCustomerRelationContact.ascx" tagname="WebUserControlCustomerRelationContact" tagprefix="uc1" %>
<%@ Register src="WebUserControlCustomerRelationContactLog.ascx" tagname="WebUserControlCustomerRelationContactLog" tagprefix="uc2" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Label ID="LabelContactOverview" runat="server" Text="Contactmomenten overzicht" 
            CssClass="SubMenuHeader"></asp:Label>
        <cc11:ClassGridView ID="GridViewContractMaterials" runat="server" 
            AutoGenerateColumns="False" 
            DataSourceID="EntityDataSourceContactLog" 
            onselectedindexchanged="GridViewContractMaterials_SelectedIndexChanged" 
            DataKeyNames="Id" AllowSorting="True">
            <Columns>
                <asp:CommandField SelectText="Bewerk" ShowSelectButton="True" />
                <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" 
                    SortExpression="Id" Visible="False" />
                <asp:BoundField DataField="Description" HeaderText="Toelichting" 
                    ReadOnly="True" SortExpression="Description" />
                <asp:BoundField DataField="ContactDateTime" HeaderText="Contact datum" 
                    ReadOnly="True" SortExpression="ContactDateTime" />
                <asp:BoundField DataField="ContactType" HeaderText="Soort contact" 
                    ReadOnly="True" SortExpression="ContactType" />
                <asp:BoundField DataField="FollowUpState" HeaderText="Follow-up status" 
                    ReadOnly="True" SortExpression="FollowUpState" />
                <asp:BoundField DataField="FollowUpDateTime" HeaderText="Follow-up datum" 
                    ReadOnly="True" SortExpression="FollowUpDateTime" />
                <asp:BoundField DataField="PausedUntilDateTime" 
                    HeaderText="Gepauzeerd tot datum" ReadOnly="True" 
                    SortExpression="PausedUntilDateTime" />
            </Columns>
        </cc11:ClassGridView>
        <br />
        <br />
        <asp:Button ID="ButtonNewContractMaterial" runat="server" 
            onclick="ButtonNewContractMaterial_Click" 
            Text="Nieuw contactmoment toevoegen" /><asp:Button
                ID="ButtonRefresh" runat="server" Text="Lijst verversen" 
            onclick="ButtonRefresh_Click" />
        <br />
        <uc2:WebUserControlCustomerRelationContactLog ID="WebUserControlCustomerRelationContactLog1" 
            runat="server" Visible="false" />
        <br />
        </td></tr></table>

    <asp:Label ID="LabelContractID" runat="server" 
        Text="00000000-0000-0000-0000-000000000000" Visible="False"></asp:Label>
    <cc11:ClassEntityDataSource ID="EntityDataSourceContactLog" runat="server"
        ConnectionString="name=ModelTMSContainer" 
        DefaultContainerName="ModelTMSContainer" EntitySetName="RelationContactLogSet" 
        
    Where="(it.[RelationContact].[Id] = @Id )" 
    
        Select="it.[Id], it.[Description], it.ContactDateTime, it.ContactType,  it.FollowUpState, it.FollowUpDateTime, it.PausedUntilDateTime" 
        OrderBy="it.FollowUpDateTime DESC" EntityTypeFilter="">
        <WhereParameters>
            <asp:ControlParameter ControlID="LabelContractID" DbType="Guid" 
                DefaultValue="00000000-0000-0000-0000-000000000000" Name="Id" 
                PropertyName="Text" />
        </WhereParameters>
    </cc11:ClassEntityDataSource>

