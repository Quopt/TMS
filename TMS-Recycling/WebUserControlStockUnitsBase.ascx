<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlStockUnitsBase.ascx.cs" Inherits="TMS_Recycling.WebUserControlStockUnitsBase" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc2" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<table width="100%">
<tr>
 <td colspan="2">
     <asp:Label ID="LabelBasisgegevens" runat="server" 
         Text="Basisgegevens materiaaleenheid" CssClass="SubMenuHeader"></asp:Label></td>
 <td>    &nbsp;</td>
 <td>   &nbsp;</td>
</tr>
<tr>
<td>
    <asp:Label ID="LabelName" runat="server" Text="Naam"></asp:Label> </td>
    <td width="300px"> <asp:TextBox ID="TextBox_Description_Text" runat="server" Width="90%" 
            MaxLength="250" ></asp:TextBox>
    </td>
    <td><asp:Label ID="LabelIsActive" runat="server" Text="Actief"></asp:Label> </td>
        <td>
            <asp:CheckBox ID="CheckBox_IsActive_Checked" Text="" runat="server" Checked="True" /></td>
</tr>



<tr>
    <td><asp:Label ID="Label15" runat="server" Text="Materiaaleenheid"></asp:Label> </td>
    <td> 

        <asp:TextBox ID="TextBox_StockUnit" runat="server" 
            MaxLength="250" ></asp:TextBox>

    </td>
    <td>
        <asp:Label ID="LabelStockKgMultiplier" runat="server" Text="Kg vermenigvuldiger"></asp:Label>
    </td>
    <td>

        <asp:TextBox ID="TextBox_StockKgMultiplier" runat="server" 
            MaxLength="250" ></asp:TextBox>

    </td>
</tr>

<tr>
    <td><asp:Label ID="Label1" runat="server" Text="Opmerkingen"></asp:Label> </td>
    <td colspan="3"> 
        <asp:TextBox ID="TextBox_Comments" runat="server" Width="90%" 
            MaxLength="2000" Rows="4" TextMode="MultiLine" ></asp:TextBox>
    </td>
</tr>

</table>
<table style="width:100%;">
<tr>
<td>
<nobr>
<asp:Button ID="ButtonCancel" runat="server" Text="Wijzigingen annuleren" 
    onclick="ButtonCancel_Click" />
<asp:Button ID="ButtonSave" runat="server" Text="Wijzigingen opslaan" 
    onclick="ButtonSave_Click" />
<asp:Button ID="ButtonDelete" runat="server" onclick="ButtonDelete_Click" 
    Text="Materiaaleenheid verwijderen" />
</nobr>
</td>
<td style="width:90%;text-align:right;">
    &nbsp;</td></tr>
</table>


    



    
