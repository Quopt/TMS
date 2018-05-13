<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlCustomerRelationContactOverview.ascx.cs" Inherits="TMS_Recycling.WebUserControlCustomerRelationContactOverview" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit.HTMLEditor" tagprefix="cc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="WebUserControlCustomerRelationContact.ascx" tagname="WebUserControlCustomerRelationContact" tagprefix="uc1" %>
<%@ Register src="WebUserControlCustomerRelationContactLog.ascx" tagname="WebUserControlCustomerRelationContactLog" tagprefix="uc2" %>
<%@ Register src="WebUserControlCustomerRelationContactLogOverview.ascx" tagname="WebUserControlCustomerRelationContactLogOverview" tagprefix="uc3" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

    <asp:Label ID="LabelObjectName" runat="server" Text="..." 
        CssClass="SubMenuHeader"></asp:Label>

    <table width="100%" class="filterpanel">
    <tr><td><asp:Label ID="LabelFilter" runat="server" Text="Filter" 
            CssClass="filterheader"></asp:Label></td></tr>
    <tr><td>
        <asp:Label ID="LabelNaam" runat="server" Text="Naam"></asp:Label> &nbsp; <asp:TextBox ID="TextBoxFilterName"
            runat="server"></asp:TextBox></td><td><asp:Label ID="LabelStatus" 
                runat="server" Text="Type relatie"></asp:Label> &nbsp; 
                <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" ID="DropDownListRelationType" runat="server">
                </cc11:ClassComboBox>
        </td>

                        <td> 
                            <asp:Button ID="ButtonSearch" runat="server" Text="Zoeken/verversen" 
                                CssClass="sleekbutton" onclick="ButtonSearch_Click" /></td>
                        </tr>
    </table>
    <table class="resultspanel"><tr><td>
        <cc11:ClassGridView ID="GridViewResults" runat="server" AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" 
            DataSourceID="EntityDataSourceRelation" 
            DataMember="DefaultView" EmptyDataText="Er zijn geen gegevens beschikbaar" 
            onselectedindexchanged="GridViewResults_SelectedIndexChanged" 
            DataKeyNames="Id">
            <Columns>
                <asp:CommandField SelectText="Bewerk" ShowSelectButton="True" />
                <asp:BoundField DataField="Id" HeaderText="Id" 
                    SortExpression="Id" ReadOnly="True" Visible="False" />
                <asp:BoundField DataField="Description" HeaderText="Naam" ReadOnly="True" 
                    SortExpression="Description" />
                <asp:BoundField DataField="RelationType" HeaderText="Relatie type" 
                    ReadOnly="True" SortExpression="RelationType" />
                <asp:BoundField DataField="PhoneNumber" HeaderText="Telefoon" 
                    ReadOnly="True" SortExpression="PhoneNumber" />
                <asp:BoundField DataField="MobilePhone" HeaderText="Mobiel" 
                    ReadOnly="True" SortExpression="MobilePhone" />
                <asp:BoundField DataField="EMail" HeaderText="E-mail" 
                    ReadOnly="True" SortExpression="EMail" />
            </Columns>
        </cc11:ClassGridView>
        <br />
        <br />
        <asp:Button ID="ButtonNew" runat="server" onclick="ButtonNew_Click" 
            Text="Nieuw contactpersoon toevoegen" />
    </td></tr></table>
    <table id="DetailTable" runat="server" class="detailpanel"><tr><td>
        <uc1:WebUserControlCustomerRelationContact ID="WebUserControlCustomerRelationContact1" 
            runat="server" Visible="false" />
<br />
        <br />
        <uc3:WebUserControlCustomerRelationContactLogOverview ID="WebUserControlCustomerRelationContactLogOverview1" 
            runat="server" />
<br />

        </td></tr></table>
    <cc11:ClassEntityDataSource ID="EntityDataSourceRelation" runat="server" 
        ConnectionString="name=ModelTMSContainer" 
        DefaultContainerName="ModelTMSContainer" EntitySetName="RelationContactSet" 
        
    Where="(it.[Description] like &quot;%&quot; + @Description + &quot;%&quot;) and (it.[RelationType]  like &quot;%&quot; + @RelationType + &quot;%&quot;) and (it.[Relation].[Id] = @Id )" 
    
        Select="it.[Id], it.[Description], it.RelationType, it.PhoneNumber, it.MobilePhone, it.EMail" 
        OrderBy="it.[Description]" EntityTypeFilter="">
        <WhereParameters>
            <asp:ControlParameter ControlID="TextBoxFilterName" DefaultValue="%" 
                Name="Description" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="DropDownListRelationType" DefaultValue="%" 
                Name="RelationType" PropertyName="SelectedValue" Type="String" />
            <asp:QueryStringParameter DbType="Guid" 
                DefaultValue="00000000-0000-0000-0000-000000000000" Name="Id" 
                QueryStringField="Id" />
        </WhereParameters>
    </cc11:ClassEntityDataSource>

    <asp:Label ID="LabelContractID" runat="server" 
        Text="00000000-0000-0000-0000-000000000000" Visible="False"></asp:Label>
    <asp:Label ID="LabelDataBindDetails" runat="server" 
        Text="" Visible="False"></asp:Label>
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

