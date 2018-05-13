<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="URLPopUpControl.ascx.cs" Inherits="TMS_Recycling.URLPopUpControl" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Button ID="ButtonOpenPopUp" runat="server" onclick="ButtonOpenPopUp_Click" 
    Text="..." />
<div id="divPopup" class="TMSPopUp" style="DISPLAY: none;" runat="server">
</div> 
<div id="divPopupContent" class="TMSPopUpContent" style="DISPLAY: none; z-index:101; text-align : right;" runat="server">
    <asp:TextBox ID="TextBoxPopUpName" runat="server" CssClass="TMSPopUpHeader"></asp:TextBox>
    <asp:Button ID="ButtonClosePopUp" runat="server" Text="X Sluiten popup" 
        onclick="ButtonClosePopUp_Click" /><br />
    <iframe class="TMSPopUpIFrame" id="IFramePopUp" src="" runat="server"></iframe>
</div>

<asp:Label ID="LabelURLToPopUp" runat="server" Visible="False"></asp:Label>
<asp:Label ID="LabelMaxPopupDepth" runat="server" Visible="False" Text="4"></asp:Label>


