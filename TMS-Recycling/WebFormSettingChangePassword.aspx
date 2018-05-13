<%@ Page Title="" Language="C#" MasterPageFile="~/SiteSetting.master" AutoEventWireup="true" CodeBehind="WebFormSettingChangePassword.aspx.cs" Inherits="TMS_Recycling.WebFormSettingChangePassword" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPathLink" runat="server">
    <asp:Label ID="LabelPath" runat="server" Text="Systeembeheer \ Wijzig wachtwoord"></asp:Label>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="LabelChangePassword" runat="server" CssClass="SubMenuHeader" 
        Text="Wijzig wachtwoord"></asp:Label><br /><br />
<table>
    <tr><td><asp:Label ID="LabelOldPassword" runat="server" Text="Oud wachtwoord"></asp:Label></td><td>
        <asp:TextBox ID="TextBoxOldPassword" runat="server" TextMode="Password" 
            Width="200px"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidatorOldPwd" runat="server" 
            ControlToValidate="TextBoxOldPassword" CssClass="validation" 
            ErrorMessage="&lt;&lt;&lt; Dit veld moet u invullen"></asp:RequiredFieldValidator>
        </td></tr>
    <tr><td><asp:Label ID="LabelNewPassword" runat="server" Text="Nieuw wachtwoord"></asp:Label></td><td>
        <asp:TextBox ID="TextBoxNewPassword" runat="server" TextMode="Password" 
            Width="200px"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidatorNewPwd" runat="server" 
            ControlToValidate="TextBoxNewPassword" CssClass="validation" 
            ErrorMessage="&lt;&lt;&lt; Dit veld moet u invullen"></asp:RequiredFieldValidator>
        </td></tr>
    <tr><td><asp:Label ID="LabelNewPassword2" runat="server" Text="Nieuw wachtwoord (nogmaals ter bevestiging)"></asp:Label></td><td>
        <asp:TextBox ID="TextBoxNewPassword2" runat="server" TextMode="Password" 
            Width="200px"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidatorNewPwd2" runat="server" 
            ControlToValidate="TextBoxNewPassword2" CssClass="validation" 
            ErrorMessage="&lt;&lt;&lt; Dit veld moet u invullen"></asp:RequiredFieldValidator>
        </td></tr>
    <tr><td><asp:Button ID="ButtonChangePassword" runat="server" 
            Text="Wijzig wachtwoord" onclick="ButtonChangePassword_Click" /></td></tr>
</table>
    <cc11:ClassEntityDataSource ID="EntityDataSourceCurrentUser" runat="server" 
        ConnectionString="name=ModelTMSContainer" 
        DefaultContainerName="ModelTMSContainer" EntitySetName="StaffMemberSet" 
        Where="Id = @Id">
        <WhereParameters>
            <asp:SessionParameter Name="Id" SessionField="CurrentUserId" />
        </WhereParameters>
    </cc11:ClassEntityDataSource>
</asp:Content>
