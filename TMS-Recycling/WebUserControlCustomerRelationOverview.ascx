<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlCustomerRelationOverview.ascx.cs" Inherits="TMS_Recycling.WebUserControlCustomerRelationOverview" %>
<%@ Register src="WebUserControlCustomerRelation.ascx" tagname="WebUserControlCustomerRelation" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

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
                <asp:BoundField DataField="CustomerNumber" HeaderText="Klantnr" ReadOnly="True" 
                    SortExpression="CustomerNumber" />
                <asp:BoundField DataField="Description" HeaderText="Naam" ReadOnly="True" 
                    SortExpression="Description" />
                <asp:BoundField DataField="EMail" HeaderText="E-mail adres" ReadOnly="True" 
                    SortExpression="EMail" />
                <asp:BoundField DataField="PhoneNumber" HeaderText="Telefoon nummer" 
                    ReadOnly="True" SortExpression="PhoneNumber" />
                <asp:BoundField DataField="CustomerType" HeaderText="Klant type" 
                    ReadOnly="True" SortExpression="CustomerType" />
                <asp:CheckBoxField DataField="IsActive" HeaderText="Actief" ReadOnly="True" 
                    SortExpression="IsActive" />
            </Columns>
        </cc11:ClassGridView>
        <br />
        <asp:Button ID="ButtonNew" runat="server" onclick="ButtonNew_Click" 
            Text="Nieuwe relatie toevoegen" />
    </td></tr></table>
    <table class="detailpanel"><tr><td>
        <uc1:WebUserControlCustomerRelation ID="WebUserControlCustomerRelation1" 
            runat="server" Visible="False" />
        </td></tr></table>
    <cc11:ClassEntityDataSource ID="EntityDataSourceRelation" runat="server" 
        ConnectionString="name=ModelTMSContainer" 
        DefaultContainerName="ModelTMSContainer" EntitySetName="RelationSet" 
        
    Where="(it.Description like &quot;%&quot; + @Description + &quot;%&quot;) and (it.IsActive = @IsActive)" 
    EnableFlattening="False" 
    
        Select="it.[Id], it.[Description], it.[CustomerNumber], it.[EMail], it.[PhoneNumber], it.[IsActive], it.[CustomerType]" 
        OrderBy="it.[Description]">
        <WhereParameters>
            <asp:ControlParameter ControlID="TextBoxFilterName" DefaultValue="%" 
                Name="Description" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="CheckBoxFilterIsActive" DefaultValue="True" 
                Name="IsActive" PropertyName="Checked" Type="Boolean" />
        </WhereParameters>
    </cc11:ClassEntityDataSource>

