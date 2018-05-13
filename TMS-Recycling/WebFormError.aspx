<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WebFormError.aspx.cs" Inherits="TMS_Recycling.WebFormError" %>
<%@ MasterType virtualpath="Site.Master" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderPath" runat="server">
    <span class="Header2">
    <asp:Label ID="LabelPath" runat="server" 
        Text="Foutinformatie"></asp:Label>
    </span>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<table style="width:100%;">
<tr>
<td>
    <asp:Label ID="LabelError" runat="server" Text="Er is een fout opgetreden in deze applicatie. U kunt verder werken. Hieronder treft u de foutinformatie aan. De leverancier is op de hoogte gesteld van deze fout. Maak een keuze uit één van de bovenstaande menu's om verder te gaan."></asp:Label>
</td>
</tr>
<tr>
<td>

    <asp:TextBox ID="TextBoxError" runat="server" style="width:100%;height:600px;" 
        ReadOnly="True" TextMode="MultiLine"></asp:TextBox>

</td>
</tr>
</table>
</asp:Content>
