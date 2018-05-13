<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlBookKeepingClosuresOverview.ascx.cs" Inherits="TMS_Recycling.WebUserControlBookKeepingClosuresOverview" %>
<%@ Register src="~/CalendarControl.ascx" tagname="CalendarControl" tagprefix="uc1" %><%@ Register src="~/WebUserControlOrderBase.ascx" tagname="WebUserControlOrderBase" tagprefix="uc2" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit.HTMLEditor" tagprefix="cc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>
<%@ Register src="ComboBoxLocation.ascx" tagname="ComboBoxLocation" tagprefix="uc4" %>
<%@ Register src="WebUserControlBookKeepingClosureBase.ascx" tagname="WebUserControlBookKeepingClosureBase" tagprefix="uc3" %>
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
                    <uc1:calendarcontrol ID="CalendarControlStartDate" runat="server" />
            </td>
            <td>
                <asp:Label ID="LabelEndDate" runat="server" Text="Einddatum selectie"></asp:Label>
            </td><td>
                <uc1:calendarcontrol ID="CalendarControlEndDate" runat="server" />
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
        DataSourceID="EntityDataSourceClosures" 
        onselectedindexchanged="GridViewResults_SelectedIndexChanged">
        <Columns>
            <asp:CommandField SelectText="Selecteer" ShowSelectButton="True" />
            <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" 
                SortExpression="Id" Visible="False" />
            <asp:BoundField DataField="Description" HeaderText="Toelichting" 
                ReadOnly="True" SortExpression="Description" />
            <asp:BoundField DataField="ClosureDate" HeaderText="Sluitdatum" 
                SortExpression="ClosureDate" />
            <asp:BoundField DataField="LedgerLevel" HeaderText="Niveau" 
                SortExpression="LedgerLevel" />
            <asp:BoundField DataField="LedgerDelta" HeaderText="Mutatie" 
                SortExpression="LedgerDelta" />
            <asp:CheckBoxField DataField="IsCorrection" HeaderText="Gecorrigeerd" 
                SortExpression="IsCorrection" />
        </Columns>
    </cc11:ClassGridView>

</td></tr></table>

<table class="detailpanel"><tr><td>
    <uc3:WebUserControlBookKeepingClosureBase ID="WebUserControlBookKeepingClosureBase1" 
        runat="server" Visible="False" />
    </td></tr></table>
<cc11:ClassEntityDataSource ID="EntityDataSourceClosures" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EntitySetName="LedgerClosureSet" 
    OrderBy="it.ClosureDate desc, it.Description" 
    Select="it.[Id], it.[IsCorrection], it.[Description], it.[ClosureDate], it.[LedgerLevel], it.LedgerDelta" 
    
    Where="(it.ClosureDate between @StartDate and @EndDate) and ((it.Ledger.Id = @ID) or (it.LedgerBookingCode.Id = @ID))" 
    EntityTypeFilter="">
    <WhereParameters>
        <asp:Parameter Name="StartDate" Type="DateTime" />
        <asp:Parameter Name="EndDate" Type="DateTime" />
        <asp:Parameter Name="ID" DbType="Guid" />
    </WhereParameters>
</cc11:ClassEntityDataSource>
