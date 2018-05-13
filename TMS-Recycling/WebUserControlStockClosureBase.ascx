<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlStockClosureBase.ascx.cs" Inherits="TMS_Recycling.WebUserControlStockClosureBase" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="URLPopUpControl.ascx" tagname="URLPopUpControl" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc2" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<table width="100%">
<tr>
 <td colspan="2">
     <asp:Label ID="LabelBasisgegevens" runat="server" 
         Text="Basisgegevens sluitstand" CssClass="SubMenuHeader"></asp:Label> &nbsp; 
     <asp:Label ID="Label_ClosureDateTime" runat="server" Text="..."></asp:Label>&nbsp;
     <asp:Label ID="LabelMaterialName" runat="server" Text="..."></asp:Label>
    </td>
 <td>    &nbsp;</td>
 <td>   &nbsp;</td>
</tr>
<tr>
<td>
    <asp:Label ID="LabelName" runat="server" Text="Naam"></asp:Label> </td>
    <td width="300px"> 
        <asp:TextBox ID="TextBox_Description_Text" runat="server" Width="90%" 
            MaxLength="250" Enabled="False" ></asp:TextBox>
    </td>
    <td><asp:Label ID="LabelIsActive" runat="server" Text="Is gecorrigeerd"></asp:Label> </td>
        <td>
            <asp:CheckBox ID="CheckBox_IsCorrected_Checked" Text="" runat="server" 
                Checked="True" Enabled="False" /></td>
</tr>



    <tr>
    <td><asp:Label ID="Label18" runat="server" Text="Voorraadniveau"></asp:Label> </td>
    <td> 

        <asp:TextBox ID="TextBox_MaterialStockLevel" runat="server" 
            MaxLength="250" Enabled="False" ></asp:TextBox>

    </td>
    <td>
        <asp:Label ID="Label19" runat="server" Text="Voorraadprijs"></asp:Label> </td>
    <td>

        <asp:TextBox ID="TextBox_MaterialStockPrice" runat="server" 
            MaxLength="250" Enabled="False" ></asp:TextBox>

    </td>
    </tr>
    <tr>
    <td><asp:Label ID="Label20" runat="server" Text="Totaal gekocht"></asp:Label> </td>
    <td> 

        <asp:TextBox ID="TextBox_MaterialTotalBought" runat="server" 
            MaxLength="250" Enabled="False" ></asp:TextBox>

    </td>
    <td>
        <asp:Label ID="Label21" runat="server" Text="Prijs gekocht"></asp:Label> </td>
    <td>

        <asp:TextBox ID="TextBox_MaterialTotalBoughtPrice" runat="server" 
            MaxLength="250" Enabled="False" ></asp:TextBox>

    </td>
    </tr>



<tr>
    <td><asp:Label ID="Label15" runat="server" Text="Totaal verkocht"></asp:Label> </td>
    <td> 

        <asp:TextBox ID="TextBox_MaterialTotalSold" runat="server" 
            MaxLength="250" Enabled="False" ></asp:TextBox>

    </td>
    <td>
        <asp:Label ID="Label17" runat="server" Text="Prijs verkocht"></asp:Label> </td>
    <td>

        <asp:TextBox ID="TextBox_MaterialTotalSoldPrice" runat="server" 
            MaxLength="250" Enabled="False" ></asp:TextBox>

    </td>
</tr>

    <tr>
    <td><asp:Label ID="Label16" runat="server" Text="Originele (ongecorrigeerde) waardes"></asp:Label> </td>
    <td colspan="3"> 
        <asp:TextBox ID="TextBox_OriginalValues" runat="server" Width="90%" 
            MaxLength="2000" Rows="6" TextMode="MultiLine" Enabled="False" ></asp:TextBox>
    </td>
    </tr>

    <tr>
    <td><asp:Label ID="Label22" runat="server" Text="Totaal gekocht deze dag"></asp:Label> </td>
    <td> 

        <asp:TextBox ID="TextBox_MaterialTotalBoughtDay" runat="server" 
            MaxLength="250" Enabled="False" ></asp:TextBox>

    </td>
    <td>
        <asp:Label ID="Label23" runat="server" Text="Prijs gekocht deze dag"></asp:Label> </td>
    <td>

        <asp:TextBox ID="TextBox_MaterialTotalBoughtPriceDay" runat="server" 
            MaxLength="250" Enabled="False" ></asp:TextBox>

    </td>
    </tr>
    <tr>
    <td><asp:Label ID="Label24" runat="server" Text="Totaal verkocht deze dag"></asp:Label> </td>
    <td> 

        <asp:TextBox ID="TextBox_MaterialTotalSoldDay" runat="server" 
            MaxLength="250" Enabled="False" ></asp:TextBox>

    </td>
    <td>
        <asp:Label ID="Label25" runat="server" Text="Prijs verkocht deze dag"></asp:Label> </td>
    <td>

        <asp:TextBox ID="TextBox_MaterialTotalSoldPriceDay" runat="server" 
            MaxLength="250" Enabled="False" ></asp:TextBox>

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
</nobr>
</td>
<td style="width:90%;text-align:right;">
    &nbsp;</td></tr>
</table>


    <cc11:ClassEntityDataSource ID="EntityDataSourceLocations" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EntitySetName="LocationSet" 
    OrderBy="it.Description" Select="it.[Id], it.[Description], it.[IsActive]" 
    Where="it.IsActive">
</cc11:ClassEntityDataSource>



    
