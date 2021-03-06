﻿<%@ Page Title="" Language="C#" MasterPageFile="~/SiteCustomerRelation.master" AutoEventWireup="true" CodeBehind="WebFormCustomerRelationContracts.aspx.cs" Inherits="TMS_Recycling.WebFormCustomerRelationContracts" %>
<%@ MasterType virtualpath="SiteCustomerRelation.master" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="WebUserControlCustomerRelationContract.ascx" tagname="WebUserControlCustomerRelationContract" tagprefix="uc1" %>
<%@ Register src="WebUserControlCustomerRelationContractMaterial.ascx" tagname="WebUserControlCustomerRelationContractMaterial" tagprefix="uc2" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPathLink" runat="server">
    <span class="Header2">
        <asp:Label ID="LabelPath" runat="server" Text="Relaties \ Relatie \ In- en verkoopcontracten"></asp:Label></span>
</asp:Content>
<asp:Content ID="MainContentSection" ContentPlaceHolderID="MainContent" 
    runat="server">
    <asp:Label ID="LabelObjectName" runat="server" Text="..." 
        CssClass="SubMenuHeader"></asp:Label>

    <table width="100%" class="filterpanel">
    <tr><td><asp:Label ID="LabelFilter" runat="server" Text="Filter" 
            CssClass="filterheader"></asp:Label></td></tr>
    <tr><td>
        <asp:Label ID="LabelNaam" runat="server" Text="Naam"></asp:Label> &nbsp; <asp:TextBox ID="TextBoxFilterName"
            runat="server"></asp:TextBox></td><td><asp:Label ID="LabelStatus" runat="server" Text="Contract status"></asp:Label> &nbsp; 
                <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" ID="DropDownListContractStatus" runat="server">
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
            DataKeyNames="Id" onrowdatabound="GridViewResults_RowDataBound">
            <Columns>
                <asp:CommandField SelectText="Bewerk" ShowSelectButton="True" />
                <asp:BoundField DataField="Id" HeaderText="Id" 
                    SortExpression="Id" ReadOnly="True" Visible="False" />
                <asp:BoundField DataField="Description" HeaderText="Toelichting" ReadOnly="True" 
                    SortExpression="Description" />
                <asp:BoundField DataField="ContractType" HeaderText="Contract type" 
                    ReadOnly="True" SortExpression="ContractType" />
                <asp:BoundField DataField="YourReference" HeaderText="Uw referentie" 
                    ReadOnly="True" SortExpression="YourReference" />
                <asp:BoundField DataField="OurReference" HeaderText="Onze referentie" 
                    ReadOnly="True" SortExpression="OurReference" />
                <asp:BoundField DataField="ContractStartDate" HeaderText="Contract startdatum" 
                    ReadOnly="True" SortExpression="ContractStartDate" />
                <asp:BoundField DataField="ContractEndDate" HeaderText="Contract einddatum" 
                    ReadOnly="True" SortExpression="ContractEndDate" />
                <asp:BoundField DataField="ContractDate" HeaderText="Contract datum" 
                    ReadOnly="True" SortExpression="ContractDate" />
                <asp:BoundField DataField="ContractStatus" HeaderText="Contract status" 
                    ReadOnly="True" SortExpression="ContractStatus" />
                <asp:BoundField DataField="ContractPriority" HeaderText="Contract prioriteit" 
                    ReadOnly="True" SortExpression="ContractPriority" />
                <asp:CheckBoxField DataField="HasContractGuidance" 
                    HeaderText="Contractbegeleiding" SortExpression="HasContractGuidance" />
            </Columns>
        </cc11:ClassGridView>
        <br />
        <br />
        <asp:Button ID="ButtonNew" runat="server" onclick="ButtonNew_Click" 
            Text="Nieuw contract toevoegen" />
    </td></tr></table>
    <table id="DetailTable" runat="server" class="detailpanel"><tr><td>
        <uc1:WebUserControlCustomerRelationContract ID="WebUserControlCustomerRelationContract1" 
            runat="server" ViewStateMode="Inherit" Visible="False" />
<br />
<asp:Label ID="LabelContractMaterials" runat="server" Text="Materialen in dit contract" CssClass="SubMenuHeader"></asp:Label>
        <cc11:ClassGridView ID="GridViewContractMaterials" runat="server" 
            AutoGenerateColumns="False" DataKeyNames="Id" 
            DataSourceID="EntityDataSourceContractMaterials" 
            onselectedindexchanged="GridViewContractMaterials_SelectedIndexChanged" 
            EnableViewState="False" 
            onrowdatabound="GridViewContractMaterials_RowDataBound" PageSize="99999" 
            EmptyDataText="Er zijn geen gegevens gevonden die voldoen aan uw zoekcriteria." 
            ShowHeaderWhenEmpty="True">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" 
                    SortExpression="Id" Visible="False" />
                <asp:BoundField DataField="Description" HeaderText="Toelichting" 
                    ReadOnly="True" SortExpression="Description" />
                <asp:BoundField DataField="MinAmount" HeaderText="Min hoeveelheid" 
                    ReadOnly="True" SortExpression="MinAmount" />
                <asp:BoundField DataField="MaxAmount" HeaderText="Max hoeveelheid" 
                    ReadOnly="True" SortExpression="MaxAmount" />
                <asp:BoundField DataField="PricePerUnit" HeaderText="Prijs" 
                    SortExpression="PricePerUnit" />
                <asp:BoundField DataField="AvgRequiredProfitPerUnit" HeaderText="Winst" 
                    SortExpression="AvgRequiredProfitPerUnit" />
                <asp:BoundField DataField="DeliveredAmount" 
                    HeaderText="Al geleverde hoeveelheid" ReadOnly="True" 
                    SortExpression="DeliveredAmount" />
                <asp:BoundField DataField="AvgStockUnits" 
                    HeaderText="Verkregen vanuit contractbegeleiding" 
                    SortExpression="AvgStockUnits" />
                <asp:BoundField DataField="Material" HeaderText="Materiaal" ReadOnly="True" 
                    SortExpression="Material" />
                <asp:TemplateField HeaderText="Bekijken/bewerken"></asp:TemplateField>
            </Columns>
        </cc11:ClassGridView>
        <br />
        <br />
        <asp:Button ID="ButtonNewContractMaterial" runat="server" 
            onclick="ButtonNewContractMaterial_Click" Text="Nieuw contract materiaal" /><asp:Button
                ID="ButtonRefresh" runat="server" Text="Lijst verversen" 
            onclick="ButtonRefresh_Click" />
        </td></tr></table>
    <cc11:ClassEntityDataSource ID="EntityDataSourceRelation" runat="server" 
        ConnectionString="name=ModelTMSContainer" 
        DefaultContainerName="ModelTMSContainer" EntitySetName="RelationContractSet" 
        
    Where="(it.[Description] like &quot;%&quot; + @Description + &quot;%&quot;) and (it.[ContractStatus]  like &quot;%&quot; + @ContractStatus + &quot;%&quot;) and (it.[Relation].[Id] = @Id )" 
    
        Select="it.[Id], it.[Description], it.ContractType, it.YourReference, it.OurReference, it.ContractStartDate, it.ContractEndDate, it.ContractDate,  it.ContractStatus, it.ContractPriority, it.HasContractGuidance" 
        OrderBy="it.[Description]" EntityTypeFilter="">
        <WhereParameters>
            <asp:ControlParameter ControlID="TextBoxFilterName" DefaultValue="%" 
                Name="Description" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="DropDownListContractStatus" DefaultValue="%" 
                Name="ContractStatus" PropertyName="SelectedValue" Type="String" />
            <asp:QueryStringParameter DbType="Guid" 
                DefaultValue="00000000-0000-0000-0000-000000000000" Name="Id" 
                QueryStringField="Id" />
        </WhereParameters>
    </cc11:ClassEntityDataSource>

    <asp:Label ID="LabelContractID" runat="server" 
        Text="00000000-0000-0000-0000-000000000000" Visible="False"></asp:Label>
    <cc11:ClassEntityDataSource ID="EntityDataSourceContractMaterials" runat="server"
        ConnectionString="name=ModelTMSContainer" 
        DefaultContainerName="ModelTMSContainer" EntitySetName="RelationContractMaterialSet" 
        
    Where="(it.[RelationContract].[Id] = @Id )" 
    
        Select="it.[Id], it.[Description], it.MinAmount, it.MaxAmount, it.DeliveredAmount, it.Material.Description as Material, it.AvgStockUnits, it.PricePerUnit, it.AvgRequiredProfitPerUnit" 
        OrderBy="it.[Description]" EntityTypeFilter="">
        <WhereParameters>
            <asp:ControlParameter ControlID="LabelContractID" DbType="Guid" 
                DefaultValue="00000000-0000-0000-0000-000000000000" Name="Id" 
                PropertyName="Text" />
        </WhereParameters>
    </cc11:ClassEntityDataSource>

    </asp:Content>
