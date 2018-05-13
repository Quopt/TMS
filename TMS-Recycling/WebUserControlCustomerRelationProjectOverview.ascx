<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlCustomerRelationProjectOverview.ascx.cs" Inherits="TMS_Recycling.WebUserControlRelationProjectOverview" %>
<%@ Register src="WebUserControlCustomerRelationProject.ascx" tagname="WebUserControlCustomerRelationProject" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

    <asp:Label ID="LabelObjectName" runat="server" Text="..." 
        CssClass="SubMenuHeader"></asp:Label>

    <table width="100%" class="filterpanel">
    <tr><td><asp:Label ID="LabelFilter" runat="server" Text="Filter" 
            CssClass="filterheader"></asp:Label></td></tr>
    <tr><td>
        <asp:Label ID="Label1" runat="server" Text="Naam"></asp:Label> &nbsp; <asp:TextBox ID="TextBoxFilterName"
            runat="server"></asp:TextBox></td><td>
                <asp:CheckBox
                    ID="CheckBoxFilterIsActive" runat="server" Text="Is actief" 
                    Checked="True" /></td>

                        <td> 
                            <asp:Button ID="ButtonSearch" runat="server" Text="Zoeken/verversen" 
                                CssClass="sleekbutton" onclick="ButtonSearch_Click" />
                            
        </td>
                        </tr>
    </table>
    <table class="resultspanel"><tr><td>
        <cc11:ClassGridView ID="GridViewResults" runat="server" AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" 
            DataSourceID="EntityDataSourceRelation" 
            DataMember="DefaultView" EmptyDataText="Er zijn geen gegevens gevonden die voldoen aan uw zoekcriteria." 
            onselectedindexchanged="GridViewResults_SelectedIndexChanged" 
            DataKeyNames="Id" ShowHeaderWhenEmpty="True">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Id" 
                    SortExpression="Id" ReadOnly="True" Visible="False" />
                <asp:CommandField SelectText="Bewerk" ShowSelectButton="True" />
                <asp:BoundField DataField="Description" HeaderText="Toelichting" ReadOnly="True" 
                    SortExpression="Description" />
                <asp:CheckBoxField DataField="IsActive" HeaderText="Actief" ReadOnly="True" 
                    SortExpression="IsActive" />
            </Columns>
        </cc11:ClassGridView>
        <br />
        <asp:Button ID="ButtonNew" runat="server" onclick="ButtonNew_Click" 
            Text="Nieuw project toevoegen" />
    </td></tr></table>
    <table class="detailpanel"><tr><td>
        <uc1:WebUserControlCustomerRelationProject ID="WebUserControlCustomerRelationProject1" 
            runat="server" Visible="False" />
        </td></tr></table>
    <cc11:ClassEntityDataSource ID="EntityDataSourceRelation" runat="server" 
        ConnectionString="name=ModelTMSContainer" 
        DefaultContainerName="ModelTMSContainer" EntitySetName="RelationProjectSet" 
        
    Where="(it.[Description] like &quot;%&quot; + @Description + &quot;%&quot;) and (it.[IsActive] = @IsActive) and (it.[Relation].[Id] = @Id )" 
    
        Select="it.[Id], it.[Description], it.[IsActive]" 
        OrderBy="it.[Description]" EntityTypeFilter="">
        <WhereParameters>
            <asp:ControlParameter ControlID="TextBoxFilterName" DefaultValue="%" 
                Name="Description" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="CheckBoxFilterIsActive" DefaultValue="True" 
                Name="IsActive" PropertyName="Checked" Type="Boolean" />
            <asp:QueryStringParameter DbType="Guid" 
                DefaultValue="00000000-0000-0000-0000-000000000000" Name="Id" 
                QueryStringField="Id" />
        </WhereParameters>
    </cc11:ClassEntityDataSource>

