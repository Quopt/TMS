<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlStockClosuresOverview.ascx.cs" Inherits="TMS_Recycling.WebUserControlStockClosuresOverview" %>
<%@ Register src="~/CalendarControl.ascx" tagname="CalendarControl" tagprefix="uc1" %><%@ Register src="~/WebUserControlOrderBase.ascx" tagname="WebUserControlOrderBase" tagprefix="uc2" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="WebUserControlRentLedgerBase.ascx" tagname="WebUserControlRentLedgerBase" tagprefix="uc3" %>
<%@ Register src="ComboBoxLocation.ascx" tagname="ComboBoxLocation" tagprefix="uc4" %>
<%@ Register src="WebUserControlStockClosureBase.ascx" tagname="WebUserControlStockClosureBase" tagprefix="uc5" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

    <asp:Label ID="LabelObjectName" runat="server" Text="..." 
        CssClass="SubMenuHeader"></asp:Label>
  <table width="100%" class="filterpanel">
    <tr><td><asp:Label ID="LabelFilter" runat="server" Text="Filter" 
            CssClass="filterheader"></asp:Label></td></tr>
            <tr>
            <td>
                <asp:Label ID="LabelStartDate" runat="server" Text="Startdatum selectie"></asp:Label>
            </td><td width="200px">
                    <uc1:CalendarControl ID="CalendarControlStartDate" runat="server" />
            </td>
            <td>
                <asp:Label ID="LabelEndDate" runat="server" Text="Einddatum selectie"></asp:Label>
            </td><td>
                <uc1:CalendarControl ID="CalendarControlEndDate" runat="server" />
                </td>
            <td>
                            <asp:Button ID="ButtonSearch" runat="server" Text="Zoeken/verversen" 
                                CssClass="sleekbutton" onclick="ButtonSearch_Click" />
            </td>
            </tr>

    </table>

<table class="resultspanel"><tr><td>

    <cc11:ClassGridView ID="GridViewResults" runat="server" AllowPaging="True" 
        AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="Id" 
        DataSourceID="EntityDataSourceMaterialClosures" 
        onselectedindexchanged="GridViewResults_SelectedIndexChanged">
        <Columns>
            <asp:CommandField SelectText="Selecteer" ShowSelectButton="True" />
            <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" 
                SortExpression="Id" Visible="False" />
            <asp:BoundField DataField="Description" HeaderText="Toelichting" 
                ReadOnly="True" SortExpression="Description" />
            <asp:BoundField DataField="ClosureDateTime" HeaderText="Sluit datum/tijd" 
                ReadOnly="True" SortExpression="ClosureDateTime" />
            <asp:BoundField DataField="MaterialStockLevel" HeaderText="Sluit gewicht (kg)" 
                ReadOnly="True" SortExpression="MaterialStockLevel" />
            <asp:BoundField DataField="MaterialStockPrice" HeaderText="Sluit prijs" 
                ReadOnly="True" SortExpression="MaterialStockPrice" />
            <asp:BoundField DataField="MaterialTotalBoughtDay" 
                HeaderText="Totaal gekocht (kg)" ReadOnly="True" 
                SortExpression="MaterialTotalBoughtDay" />
            <asp:BoundField DataField="MaterialTotalBoughtPriceDay" 
                HeaderText="Totaal gekocht prijs" ReadOnly="True" 
                SortExpression="MaterialTotalBoughtPriceDay" />
            <asp:BoundField DataField="MaterialTotalSoldDay" HeaderText="Totaal verkocht (kg)" 
                ReadOnly="True" SortExpression="MaterialTotalSoldDay" />
            <asp:BoundField DataField="MaterialTotalSoldPriceDay" 
                HeaderText="Totaal verkocht prijs" ReadOnly="True" 
                SortExpression="MaterialTotalSoldPriceDay" />
            <asp:CheckBoxField DataField="IsCorrected" HeaderText="Gecorrigeerd" 
                SortExpression="IsCorrected" />
        </Columns>
    </cc11:ClassGridView>

</td></tr></table>

<table class="detailpanel"><tr><td>
<uc5:WebUserControlStockClosureBase ID="WebUserControlStockClosureBase1" 
        runat="server" Visible="false" />
&nbsp;</td></tr></table>
<cc11:ClassEntityDataSource ID="EntityDataSourceMaterialClosures" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EntitySetName="MaterialClosureSet" 
    OrderBy="it.ClosureDateTime desc, it.Description" 
    Select="it.[Id], it.[IsCorrected], it.[Description], it.[ClosureDateTime], it.[MaterialStockLevel], it.[MaterialTotalBought], it.[MaterialTotalSold], it.[MaterialTotalBoughtPrice], it.[MaterialTotalSoldPrice], it.[MaterialStockPrice], it.[MaterialTotalBoughtDay], it.[MaterialTotalSoldDay], it.[MaterialTotalBoughtPriceDay], it.[MaterialTotalSoldPriceDay]" 
    Where="(it.ClosureDateTime between @StartDate and @EndDate) and (it.Material.Id = @MaterialID)">
    <WhereParameters>
        <asp:Parameter Name="StartDate" Type="DateTime" />
        <asp:Parameter Name="EndDate" Type="DateTime" />
        <asp:Parameter Name="MaterialID" DbType="Guid" />
    </WhereParameters>
</cc11:ClassEntityDataSource>
