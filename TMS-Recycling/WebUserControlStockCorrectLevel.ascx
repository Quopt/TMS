<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlStockCorrectLevel.ascx.cs" Inherits="TMS_Recycling.WebUserControlStockCorrectLevel" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<asp:Label ID="Label1" runat="server" Text="Voorraadcorrectie "></asp:Label><asp:Label
    ID="LabelMaterial" runat="server" Text="..."></asp:Label>
<table cssclass="detailpanel" >
<tr>
<td>
    <asp:Label ID="LabelAction" runat="server" Text="Uit te voeren actie"></asp:Label>
</td>
<td>

<asp:RadioButtonList ID="RadioButtonListBuyOrSell" runat="server" 
        AutoPostBack="True" 
        onselectedindexchanged="RadioButtonListBuyOrSell_SelectedIndexChanged">
    <asp:ListItem Selected="True" Value="Buy">Bijplaatsen voorraad</asp:ListItem>
    <asp:ListItem Value="Sell">Afwaarderen voorraad</asp:ListItem>
</asp:RadioButtonList>


</td>
</tr>
<tr>
<td>
    <asp:Label ID="LabelAmount" runat="server" Text="Hoeveelheid"></asp:Label>
</td>
<td>
    <asp:TextBox ID="TextBoxAmount" runat="server"></asp:TextBox>
</td>
</tr>
<tr>
<td>
    <asp:Label ID="LabelPricePerUnit" runat="server" Text="Herwaarderingsprijs per gewichtseenheid"></asp:Label>
</td>
<td>
    <asp:TextBox ID="TextBoxPrice" runat="server"></asp:TextBox>
</td>
</tr>
<tr>
<td>
    <asp:Label ID="LabelComments" runat="server" 
        Text="Toelichting"></asp:Label>
</td>
<td>
        <asp:TextBox ID="TextBoxC" runat="server" Width="300px" 
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

<tr>
<td colspan="2">
<asp:Label ID="LabelPleaseNote" runat="server" Text="LET OP : de financiele consequenties van de boeking worden niet in een kasboek verwerkt, maar wel op de boekingscode van het materiaal."></asp:Label>    </td>
</tr>

</table>