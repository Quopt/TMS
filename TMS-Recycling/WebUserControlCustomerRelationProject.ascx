<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlCustomerRelationProject.ascx.cs" Inherits="TMS_Recycling.WebUserControlCustomerRelationProject" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="URLPopUpControl.ascx" tagname="URLPopUpControl" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<table width="100%">
<tr>
 <td colspan="4"><asp:Label ID="LabelBasisgegevens" runat="server" 
         Text="Basisgegevens project" CssClass="SubMenuHeader"></asp:Label></td>
 <td>    <asp:Label ID="LabelRelationNumber" runat="server" Text="Projectnummer"></asp:Label> </td>
 <td>   <asp:Label ID="Label_ProjectNumber_Text" runat="server"></asp:Label> </td>
</tr>
<tr>
<td>
    <asp:Label ID="LabelName" runat="server" Text="Naam"></asp:Label> </td>
    <td colspan="3" width="300px"> <asp:TextBox ID="TextBox_Description_Text" runat="server" Width="90%" 
            MaxLength="250" ></asp:TextBox>
    </td>
    <td><asp:Label ID="LabelIsActive" runat="server" Text="Actief"></asp:Label> </td>
        <td>
            <asp:CheckBox ID="CheckBox_IsActive_Checked" Text="" runat="server" Checked="True" /></td>
</tr>



<tr>
    <td><asp:Label ID="Label15" runat="server" Text="Opmerkingen"></asp:Label> </td>
    <td colspan="5"> 
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
<asp:Button ID="ButtonDelete" runat="server" onclick="ButtonDelete_Click" OnClientClick="return confirm('Weet u het zeker dat u dit wilt?');"
    Text="Project verwijderen" />
</nobr>
</td>
<td style="width:90%;text-align:right;">
    <uc1:URLPopUpControl ID="URLPopUpControlLink" runat="server" 
        Text="Toon gekoppelde orders" />
</td></tr>
</table>


    




