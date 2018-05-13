<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TMS_Recycling.Login" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Label ID="LabelLogin" runat="server" Text="Aanmelden"></asp:Label>
<input id="TimeZoneOffset" type="hidden" runat="server"/>
<script type = "text/javascript" >
    document.getElementById('MainContent_TimeZoneOffset').innerText = String(new Date().getTimezoneOffset());
</script>
<br />
<br />
<table>
    <tr>
        <td width="200px">
            <asp:Label ID="LabelUserName" runat="server" Text="Gebruikersnaam"></asp:Label>
        </td>
        <td width="300px">
            <asp:TextBox ID="TextBoxUserName" runat="server" Width="100%" 
                AutoCompleteType="Disabled"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td width="200px">
            <asp:Label ID="LabelPassword" runat="server" Text="Wachtwoord"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="TextBoxPassword" runat="server" Width="100%" 
                AutoCompleteType="Disabled" TextMode="Password"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td width="200px">
            &nbsp;</td>
        <td align="right">
            <asp:Button ID="ButtonLogin" runat="server" onclick="ButtonLogin_Click" 
                Text="Aanmelden" />
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Label ID="LabelLoginResultFailed" runat="server" 
                Text="Het inloggen is mislukt. Controleer aub of uw gebruikersnaam en wachtwoord kloppen. " 
                Visible="False"></asp:Label>
            <br />
            <asp:Label ID="LabelMessage" runat="server" Text=""></asp:Label>
            </td>
    </tr>
</table>
</asp:Content>
