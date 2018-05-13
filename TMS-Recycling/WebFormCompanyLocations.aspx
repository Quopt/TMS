<%@ Page Title="" Language="C#" MasterPageFile="~/SiteSetting.master" AutoEventWireup="true" CodeBehind="WebFormCompanyLocations.aspx.cs" Inherits="TMS_Recycling.WebFormCompanyLocations" %>
<%@ Register src="WebUserControlCompanyLocation.ascx" tagname="WebUserControlCompanyLocation" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPathLink" runat="server">
    <asp:Label ID="LabelPath" runat="server" 
        Text="Systeembeheer \ Bedrijfslokaties beheren" CssClass="Header2"></asp:Label>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

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
            DataSourceID="EntityDataSourceCompanyLocation" 
            DataMember="DefaultView" EmptyDataText="Er zijn geen gegevens beschikbaar" 
            onselectedindexchanged="GridViewResults_SelectedIndexChanged" 
            DataKeyNames="Id">
            <Columns>
                <asp:CommandField SelectText="Bewerk" ShowSelectButton="True" />
                <asp:BoundField DataField="Id" HeaderText="Id" 
                    SortExpression="Id" ReadOnly="True" Visible="False" />
                <asp:BoundField DataField="LocationNumber" HeaderText="Lokatienr" ReadOnly="True" 
                    SortExpression="LocationNumber" />
                <asp:BoundField DataField="Description" HeaderText="Omschrijving" ReadOnly="True" 
                    SortExpression="Description" />
                <asp:BoundField DataField="EMail" HeaderText="E-mail adres" ReadOnly="True" 
                    SortExpression="EMail" />
                <asp:BoundField DataField="PhoneNumber" HeaderText="Telefoon nummer" 
                    ReadOnly="True" SortExpression="PhoneNumber" />
                <asp:CheckBoxField DataField="IsActive" HeaderText="Actief" ReadOnly="True" 
                    SortExpression="IsActive" />
            </Columns>
        </cc11:ClassGridView>
        <br />
        <asp:Button ID="ButtonNew" runat="server" onclick="ButtonNew_Click" 
            Text="Nieuwe lokatie toevoegen" />
    </td></tr></table>
    <table class="detailpanel"><tr><td>
        <uc1:WebUserControlCompanyLocation ID="WebUserControlCompanyLocation1" 
            runat="server" Visible="false" />
        </td></tr></table>
    <cc11:ClassEntityDataSource ID="EntityDataSourceCompanyLocation" runat="server" 
        ConnectionString="name=ModelTMSContainer" 
        DefaultContainerName="ModelTMSContainer" EntitySetName="LocationSet" 
        
    Where="(it.Description like &quot;%&quot; + @Description + &quot;%&quot;) and (it.IsActive = @IsActive)" 
    EnableFlattening="False" 
    
        Select="it.[Id], it.[Description], it.[LocationNumber], it.[EMail], it.[PhoneNumber], it.[IsActive]" 
        OrderBy="it.[Description]">
        <WhereParameters>
            <asp:ControlParameter ControlID="TextBoxFilterName" DefaultValue="%" 
                Name="Description" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="CheckBoxFilterIsActive" DefaultValue="True" 
                Name="IsActive" PropertyName="Checked" Type="Boolean" />
        </WhereParameters>
    </cc11:ClassEntityDataSource>


</asp:Content>
