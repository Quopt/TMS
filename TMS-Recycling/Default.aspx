<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="TMS_Recycling._Default" %>

<%@ Register src="WebUserControlBookKeepingOverview.ascx" tagname="WebUserControlBookKeepingOverview" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<%@ Register src="WebUserControlOpenActions.ascx" tagname="WebUserControlOpenActions" tagprefix="uc2" %>

<%@ Register src="WebUserControlTakeCall.ascx" tagname="WebUserControlTakeCall" tagprefix="uc3" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <p>
        <asp:Label ID="LabelIntro" runat="server" Text="Welkom bij het TMS systeem specifiek voor de Recyclingssector."></asp:Label>
        
    </p>
    <p>
        <asp:Label ID="LabelInstruction" runat="server" Text="Kies uit het bovenstaande menu de activiteit die u wilt uitvoeren. Kunt u niet op een activiteit klikken? Dan heeft u geen rechten om deze activiteit te mogen uitvoeren. Raadpleeg uw systeembeheerder."></asp:Label>
        
        </p>
        <asp:Label ID="LabelCurrentActions" runat="server" Text="U heeft de volgende openstaande actiepunten. Klik op 'Selecteer' om een actiepunt te bekijken."></asp:Label>


    <br />
    <uc2:WebUserControlOpenActions ID="WebUserControlOpenActions1" runat="server" />


<br />


</asp:Content>
<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolderPath">
    <span class="Header2">Home</span>
</asp:Content>

