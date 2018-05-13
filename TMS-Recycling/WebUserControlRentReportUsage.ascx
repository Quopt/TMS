﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlRentReportUsage.ascx.cs" Inherits="TMS_Recycling.WebUserControlRentReportUsage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="CalendarControl.ascx" tagname="CalendarControl" tagprefix="uc1" %>
<%@ Register src="ComboBoxLocation.ascx" tagname="ComboBoxLocation" tagprefix="uc2" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<table width="100%" class="filterpanel">
<tr>
<td>
    <asp:Label ID="LabelLocation" runat="server" Text="Lokatie"></asp:Label>
</td>
<td>
    <asp:Label ID="LabelMaterialType" runat="server" Text="Soort materiaal"></asp:Label>
</td>
<td>
    <asp:Label ID="LabelSpecificMaterial" runat="server" Text="Specifiek materiaal"></asp:Label>
</td>
</tr>

<tr>
<td>

    <uc2:comboboxlocation ID="ComboBoxSelectedLocation" runat="server" 
        AutoPostback="True" />

</td>
<td>

    <cc11:ClassComboBox ID="ComboBoxMaterialType" runat="server" AutoCompleteMode="SuggestAppend" 
        DropDownStyle="DropDownList" AppendDataBoundItems="True" 
        DataSourceID="EntityDataSourceRentalType" DataTextField="Description" 
        DataValueField="Id" MaxLength="0" style="display: inline;" 
        AutoPostBack="True">
        <asp:ListItem Value="00000000-0000-0000-0000-000000000000">-Alle items-</asp:ListItem>
    </cc11:ClassComboBox>

</td>
<td>

    <cc11:ClassComboBox ID="ComboBoxSpecificMaterial" runat="server" AutoCompleteMode="SuggestAppend" 
        DropDownStyle="DropDownList" AppendDataBoundItems="True" 
        DataSourceID="EntityDataSourceSpecificMaterials" DataTextField="Description" 
        DataValueField="Id" MaxLength="0" style="display: inline;">
        <asp:ListItem Value="">-Alle items-</asp:ListItem>
    </cc11:ClassComboBox>

    </td>
</tr>
</table>

<table width="100%" class="filterpanel">
<tr>
<td>
    <asp:Label ID="LabelDates" runat="server" Text="Datumselectie"></asp:Label>
</td>
<td>
<nobr>
    <asp:RadioButtonList ID="BulletedListDateSelection" runat="server" 
        RepeatColumns="4">
        <asp:ListItem Value="Today" Selected="True">Vandaag</asp:ListItem>
        <asp:ListItem Value="Yesterday">Gisteren</asp:ListItem>
        <asp:ListItem Value="ThisMonth">Deze maand</asp:ListItem>
        <asp:ListItem Value="PreviousMonth">Vorige maand</asp:ListItem>
        <asp:ListItem Value="ThisYear">Dit jaar</asp:ListItem>
        <asp:ListItem Value="All">Alles</asp:ListItem>
        <asp:ListItem Value="Period">Hiernaast aangegeven periode</asp:ListItem>
    </asp:RadioButtonList>
</nobr>
</td>
<td>
<nobr>
    <asp:Label ID="LabelPeriod" runat="server" Text="Periode van "></asp:Label>
    <uc1:CalendarControl ID="CalendarControlStartPeriod" runat="server" />
    <asp:Label ID="LabelUpToAndIncluding" runat="server" Text="tot"></asp:Label>
    <uc1:CalendarControl ID="CalendarControlEndPeriod" runat="server" />
</nobr>
</td>
</tr>
<tr>
<td></td><td>
    </td>
<td align="right">
    <asp:Button ID="ButtonShowReport" runat="server" Text="Rapport tonen" 
        onclick="ButtonShowReport_Click" />
    </td>
</tr>
</table>

 &nbsp;<table width="100%" class="resultspanel"><tr><td>
   <iframe id="FrameShowReport" scrolling="auto" runat="server" height="400px" width="100%">
  </iframe>
 </td></tr></table>

     <asp:Label ID="LabelURL" runat="server" Visible="False"></asp:Label>
     <asp:Label ID="LabelValChecker" runat="server" Visible="False"></asp:Label>
     <cc11:ClassEntityDataSource ID="EntityDataSourceRentalType" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EntitySetName="RentalTypeSet" 
    Select="it.[Id], it.[Description], it.[IsActive]" OrderBy="it.Description" 
    Where="it.IsActive">
</cc11:ClassEntityDataSource>

     <cc11:ClassEntityDataSource ID="EntityDataSourceSpecificMaterials" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EntitySetName="RentalItemSet" 
    Select="it.[Description], it.[Id], it.[IsActive]" EntityTypeFilter="" 
    OrderBy="it.Description" 
    
    Where="it.IsActive and ((it.RentalType.Id = @MaterialTypeId) or (@MaterialTypeId = Guid'00000000-0000-0000-0000-000000000000' ) ) and it.Location.Description like @LocationName">
         <WhereParameters>
             <asp:ControlParameter ControlID="ComboBoxMaterialType" DbType="Guid" 
                 Name="MaterialTypeId" PropertyName="SelectedValue" />
             <asp:Parameter DbType="String" Name="LocationName" />
         </WhereParameters>
</cc11:ClassEntityDataSource>


     