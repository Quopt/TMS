<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlBookingCorrection.ascx.cs" Inherits="TMS_Recycling.WebUserControlBookingCorrection" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Label ID="Label1" runat="server" Text="Stand kasboek correctie "></asp:Label><asp:Label
    ID="LabelName" runat="server" Text="..."></asp:Label>
<table cssclass="detailpanel" >
<tr>
<td>
    <asp:Label ID="LabelAction" runat="server" Text="Uit te voeren actie"></asp:Label>
</td>
<td>

<asp:RadioButtonList ID="RadioButtonListBuyOrSell" runat="server"   >
    <asp:ListItem Selected="True" Value="Buy">Bijplaatsen geld</asp:ListItem>
    <asp:ListItem Value="Sell">Afwaarderen geld</asp:ListItem>
</asp:RadioButtonList>


</td>
</tr>
<tr>
<td>
    <asp:Label ID="LabelCurrentLevel" runat="server" 
        Text="Huidig niveau"></asp:Label>
</td>
<td>
    <asp:Label ID="LabelCurrentBookingCodeLevel" runat="server" 
        Text="..."></asp:Label>
    </td>
</tr>
<tr>
<td>
    <asp:Label ID="LabelAmount" runat="server" 
        Text="Hoeveelheid bij te plaatsen of af te waarderen"></asp:Label>
</td>
<td>
    <asp:TextBox ID="TextBoxAmount" runat="server"></asp:TextBox>
</td>
</tr>
<tr>
<td>
    <asp:Label ID="LabelComments" runat="server" 
        Text="Toelichting"></asp:Label>
    </td>
<td>
        <asp:TextBox ID="TextBoxComments" runat="server" Width="300px" 
            MaxLength="2000" Rows="4" TextMode="MultiLine" ></asp:TextBox>
    </td>
</tr>

<tr>
<td>
</td>
<td>
    <asp:Button ID="ButtonProcess" runat="server" Text="Doorvoeren mutatie" 
        onclick="ButtonProcess_Click" />
</td>
</tr>

</table>

