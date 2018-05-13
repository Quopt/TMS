﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlRentMaterialTypeOverview.ascx.cs" Inherits="TMS_Recycling.WebUserControlRentMaterialTypeOverview" %>
    <%@ Register src="WebUserControlRentMaterialTypeBase.ascx" tagname="WebUserControlRentMaterialTypeBase" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<style type="text/css">


a:link, a:visited
{
    color: #034af3;
}

</style>
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

    <table width="100%" class="resultspanel">
    <tr><td>
        <cc11:ClassGridView ID="GridViewResults" runat="server" AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" 
            DataSourceID="EntityDataSourceGridBase" 
            DataMember="DefaultView" EmptyDataText="Er zijn geen gegevens beschikbaar" 
            onselectedindexchanged="GridViewResults_SelectedIndexChanged" 
    DataKeyNames="Id">
            <Columns>
                <asp:CommandField SelectText="Bewerk" ShowSelectButton="True" />
                <asp:BoundField DataField="Id" HeaderText="Id" 
                    SortExpression="Id" ReadOnly="True" Visible="False" />
                <asp:BoundField DataField="Description" HeaderText="Naam" 
                    ReadOnly="True" SortExpression="Description" />
                <asp:CheckBoxField DataField="IsActive" HeaderText="IsActive" ReadOnly="True" 
                    SortExpression="IsActive" />
            </Columns>
        </cc11:ClassGridView>
        <br />
        <asp:Button ID="ButtonNew" runat="server" Text="Nieuw materiaal toevoegen" 
            onclick="ButtonNew_Click" />
    </td></tr></table>

    <table width="100%" class="detailpanel">
    <tr><td>
    <uc1:WebUserControlRentMaterialTypeBase ID="WebUserControlRentMaterialTypeBase1" 
    runat="server" Visible="False" />
    </td></tr></table>

    <cc11:ClassEntityDataSource ID="EntityDataSourceGridBase" runat="server" 
        ConnectionString="name=ModelTMSContainer" 
        DefaultContainerName="ModelTMSContainer" EntitySetName="RentalTypeSet" 
        
    Where="(it.[Description] like &quot;%&quot; + @Description + &quot;%&quot;) and (it.[IsActive] = @IsActive) " 
    
        Select="it.[Id], it.[Description], it.[IsActive]" 
        OrderBy="it.[Description]" EntityTypeFilter="">
        <WhereParameters>
            <asp:Parameter DefaultValue="%" Name="Description" Type="String" />
            <asp:Parameter DefaultValue="True" Name="IsActive" Type="Boolean" />
        </WhereParameters>
    </cc11:ClassEntityDataSource>
