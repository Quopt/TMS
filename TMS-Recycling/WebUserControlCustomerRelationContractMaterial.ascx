<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlCustomerRelationContractMaterial.ascx.cs" Inherits="TMS_Recycling.WebUserControlCustomerRelationContractMaterial" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>
<%@ Register src="CalendarControl.ascx" tagname="CalendarControl" tagprefix="uc1" %>

<table width="100%">
<tr>
 <td colspan="4">
     <asp:Label ID="LabelBasisgegevens" runat="server" 
         Text="Basisgegevens contract materiaal" CssClass="SubMenuHeader"></asp:Label></td>
 <td>    &nbsp;</td>
 <td>   &nbsp;</td>
</tr>

<tr>
<td>
    <asp:Label ID="LabelName" runat="server" Text="Naam"></asp:Label> </td>
    <td colspan="3"> <asp:TextBox ID="TextBox_Description_Text" runat="server" Width="90%" 
            MaxLength="250" ></asp:TextBox>
    </td>
    <td>&nbsp;</td>
        <td>
            &nbsp;</td>
</tr>


<tr>
<td>
    <asp:Label ID="LabelMinAmout" runat="server" Text="Minimum hoeveelheid"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="TextBox_MinAmount" runat="server"></asp:TextBox>
    </td>
<td>
    <asp:Label ID="LabelMaxAmount" runat="server" Text="Maximum hoeveelheid"></asp:Label>
</td>
<td>
    <asp:TextBox ID="TextBox_MaxAmount" runat="server"></asp:TextBox>
</td>
<td>
    <asp:Label ID="LabelAlreadyDelivered" runat="server" Text="Al geleverde hoeveelheid"></asp:Label><br />
    <asp:Label ID="LabelOnContract" runat="server" 
        Text="Onder contractbegeleiding verkregen (eenheden)"></asp:Label><br />

    <asp:Label ID="LabelOnContractPrice" runat="server" 
        Text="Onder contractbegeleiding verkregen (gem. prijs)"></asp:Label>
</td>
<td>
    <asp:Label ID="Label_DeliveredAmount" runat="server" Text="..."></asp:Label><br />
    <asp:Label ID="LabelOnContract_AvgStockUnits" runat="server" Text="..."></asp:Label><br />
    <asp:Label ID="LabelOnContractAvgPricePerUnit" runat="server" Text="..."></asp:Label>
</td>
</tr>

<tr>
<td>
    <asp:Label ID="LabelMaterial" runat="server" Text="Materiaal"></asp:Label>
</td>
<td>

    <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" ID="DropDownList_Material" runat="server" 
        DataSourceID="EntityDataSourceMaterials" DataTextField="Description" 
        DataValueField="Id">
    </cc11:ClassComboBox>

</td>
<td>
    <asp:Label ID="LabelPrice" runat="server" Text="Prijs per materiaaleenheid"></asp:Label>
</td>
<td>
    <asp:TextBox ID="TextBox_PricePerUnit" runat="server"></asp:TextBox>
</td>
<td>
    <asp:Label ID="LabelRequiredProfitContractGuidance" runat="server" 
        Text="Vereiste winst per eenheid bij contractbegeleiding"></asp:Label>
</td>
<td>
    <asp:TextBox ID="TextBox_AvgRequiredProfitPerUnit" runat="server"></asp:TextBox>
    </td>
    </tr>
<tr>
    <td><asp:Label ID="Label15" runat="server" Text="Opmerkingen"></asp:Label> </td>
    <td colspan="5"> 
        <asp:TextBox ID="TextBox_Comments" runat="server" Width="80%" 
            MaxLength="2000" Rows="4" TextMode="MultiLine" ></asp:TextBox>
    </td>


</tr>

</table>
<asp:Button ID="ButtonCancel" runat="server" Text="Wijzigingen annuleren" 
    onclick="ButtonCancel_Click" />
<asp:Button ID="ButtonSave" runat="server" Text="Wijzigingen opslaan" 
    onclick="ButtonSave_Click" />
<asp:Button ID="ButtonDelete" runat="server" onclick="ButtonDelete_Click" OnClientClick="return confirm('Weet u het zeker dat u dit wilt?');"
    Text="Contract materiaal verwijderen" />
<cc11:ClassEntityDataSource ID="EntityDataSourceMaterials" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EntitySetName="MaterialSet" 
    Select="it.[Id], it.[IsActive], it.[Description]" 
    Where="it.IsActive and (it.IsWorkInsteadOfMaterial = false)" 
    OrderBy="it.Description">
</cc11:ClassEntityDataSource>



    




