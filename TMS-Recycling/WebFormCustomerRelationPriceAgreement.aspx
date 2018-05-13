<%@ Page Title="" Language="C#" MasterPageFile="~/SiteCustomerRelation.master" AutoEventWireup="true" CodeBehind="WebFormCustomerRelationPriceAgreement.aspx.cs" Inherits="TMS_Recycling.WebFormCustomerRelationPriceAgreement" %>
<%@ MasterType virtualpath="SiteCustomerRelation.master" %>
<%@ Register src="WebUserControlCustomerRelationPriceAgreement.ascx" tagname="WebUserControlCustomerRelationPriceAgreement" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPathLink" runat="server">
    <span class="Header2">
        <asp:Label ID="LabelPath" runat="server" Text="Relaties \ Relatie \ Prijsafspraken overzicht"></asp:Label></span>
</asp:Content>
<asp:Content ID="MainContentSection" ContentPlaceHolderID="MainContent" 
    runat="server">
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
            Text="Nieuwe prijsafspraak toevoegen" />
    </td></tr></table>
    <table class="detailpanel"><tr><td>
        <uc1:WebUserControlCustomerRelationPriceAgreement ID="WebUserControlCustomerRelationPriceAgreement1" 
            runat="server" Visible="False"  />
        </td></tr></table>
    <cc11:ClassEntityDataSource ID="EntityDataSourceRelation" runat="server" 
        ConnectionString="name=ModelTMSContainer" 
        DefaultContainerName="ModelTMSContainer" EntitySetName="RelationPriceAgreementSet" 
        
    Where="(it.[Description] like &quot;%&quot; + @Description + &quot;%&quot;) and (it.[IsActive] = @IsActive) and (it.[Relation].[Id] = @Id )" 
    
        Select="it.[Id], it.[Description], it.[IsActive]" 
        OrderBy="it.[Description]">
        <WhereParameters>
            <asp:ControlParameter ControlID="TextBoxFilterName" DefaultValue="%" 
                Name="Description" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="CheckBoxFilterIsActive" DefaultValue="True" 
                Name="IsActive" PropertyName="Checked" Type="Boolean" />
            <asp:QueryStringParameter Name="Id" QueryStringField="Id" DbType="Guid" />
        </WhereParameters>
    </cc11:ClassEntityDataSource>

    </asp:Content>
