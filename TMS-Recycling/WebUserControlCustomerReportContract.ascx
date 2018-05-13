﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlCustomerReportContract.ascx.cs" Inherits="TMS_Recycling.WebUserControlCustomerReportContract" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="CalendarControl.ascx" tagname="CalendarControl" tagprefix="uc1" %>
<%@ Register src="ComboBoxLocation.ascx" tagname="ComboBoxLocation" tagprefix="uc2" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>


<table width="100%" class="filterpanel">
<tr>
<td>
    <asp:Label ID="LabelCustomers" runat="server" Text="Klanten"></asp:Label>
</td>
<td>
    <asp:Label ID="LabelTypeOfMerchandise" runat="server" Text="Soort contract"></asp:Label>
</td>
<td>
    <asp:Label ID="LabelOrderStatus" runat="server" Text="Status orders"></asp:Label>
</td>
<td>
    <asp:Label ID="LabelLocation" runat="server" Text="Lokatie"></asp:Label>
</td>
</tr>

<tr>
<td>

    <asp:RadioButtonList ID="RadioButtonListCustomerSelection" runat="server" >
        <asp:ListItem Selected="True" Value="All">Alle klanten</asp:ListItem>
        <asp:ListItem Value="Select">Alleen onderstaande klant</asp:ListItem>
    </asp:RadioButtonList>
    <cc11:ClassComboBox ID="ComboBoxCustomerSelection" runat="server" 
        AutoCompleteMode="SuggestAppend" DataSourceID="EntityDataSourceCustomer" 
        DataTextField="Description" DataValueField="Id" DropDownStyle="DropDownList" 
        MaxLength="0" style="display: inline;">
    </cc11:ClassComboBox>
</td>
<td>

    <asp:RadioButtonList ID="RadioButtonListContractType" runat="server">
        <asp:ListItem Selected="True" Value="">Alles</asp:ListItem>
        <asp:ListItem Value="Buy">Inkoopcontracten</asp:ListItem>
        <asp:ListItem Value="Sell">Verkoopcontracten</asp:ListItem>
    </asp:RadioButtonList>
</td>
<td>
    <asp:RadioButtonList ID="RadioButtonListContractStatus" runat="server">
        <asp:ListItem Selected="True" Value="">Alles</asp:ListItem>
        <asp:ListItem Value="Open">Openstaande contracten</asp:ListItem>
        <asp:ListItem Value="Closed">Gesloten contracten</asp:ListItem>
        <asp:ListItem Value="Paused">Gepauzeerde contracten</asp:ListItem>
        <asp:ListItem Value="Ready">Open & leverbaar vanuit contractbegeleiding</asp:ListItem>
    </asp:RadioButtonList>
</td>
<td>

    <uc2:ComboBoxLocation ID="ComboBoxSelectedLocation" runat="server" />

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
        <asp:ListItem Value="Today" >Vandaag</asp:ListItem>
        <asp:ListItem Value="Yesterday">Gisteren</asp:ListItem>
        <asp:ListItem Value="ThisMonth">Deze maand</asp:ListItem>
        <asp:ListItem Value="PreviousMonth">Vorige maand</asp:ListItem>
        <asp:ListItem Value="ThisYear">Dit jaar</asp:ListItem>
        <asp:ListItem Value="All" Selected="True">Alles</asp:ListItem>
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

 <table width="100%" class="resultspanel"><tr><td>
   <iframe id="FrameShowReport" scrolling="auto" runat="server" height="400px" width="100%">
  </iframe>
     <cc11:ClassEntityDataSource ID="EntityDataSourceCustomer" runat="server" 
         ConnectionString="name=ModelTMSContainer" 
         DefaultContainerName="ModelTMSContainer" EntitySetName="RelationSet" 
         Select="it.[Description], it.[Id], it.[IsActive]" Where="it.IsActive" 
         OrderBy="it.Description">
     </cc11:ClassEntityDataSource>
 </td></tr></table>

     <asp:Label ID="LabelURL" runat="server" Visible="false"></asp:Label>