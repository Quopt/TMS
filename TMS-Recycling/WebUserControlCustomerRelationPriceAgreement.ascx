<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlCustomerRelationPriceAgreement.ascx.cs" Inherits="TMS_Recycling.WebUserControlCustomerRelationPriceAgreement" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="CalendarControl.ascx" tagname="CalendarControl" tagprefix="uc1" %>
<%@ Register src="URLPopUpControl.ascx" tagname="URLPopUpControl" tagprefix="uc2" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<table width="100%">
<tr>
 <td colspan="4">
     <asp:Label ID="LabelBasisgegevens" runat="server" 
         Text="Basisgegevens prijsovereenkomst" CssClass="SubMenuHeader"></asp:Label></td>
 <td>    &nbsp;</td>
 <td>   &nbsp;</td>
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
<td>
    <asp:Label ID="LabelStartDate" runat="server" Text="Start datum"></asp:Label>
    </td>
<td>
    <uc1:CalendarControl ID="CalendarControl_StartDateTime_SelectedDate" runat="server" />
    </td>
<td>
    <asp:Label ID="LabelEndDate" runat="server" Text="Eind datum"></asp:Label>
</td>
<td>
    <uc1:CalendarControl ID="CalendarControl_EndDateTime_SelectedDate" runat="server" />
</td>
<td>
    <asp:Label ID="LabelContractType" runat="server" Text="Contract type"></asp:Label>
</td>
<td>
    <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" ID="DropDownList_AgreementType_SelectedValue" runat="server">
    </cc11:ClassComboBox>
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
</td>
<td>
</td>
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
    Text="Overeenkomst verwijderen" />
</nobr>
</td>
<td style="width:90%;text-align:right;">
    <uc2:urlpopupcontrol ID="URLPopUpControlShowContract" runat="server" 
        Text="Toon contract" />
    <uc2:urlpopupcontrol ID="URLPopUpControlLink" runat="server" 
        Text="Toon gekoppelde orders" />
</td></tr>
</table>


<cc11:ClassEntityDataSource ID="EntityDataSourceMaterials" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EntitySetName="MaterialSet" 
    Select="it.[Id], it.[IsActive], it.[Description]" 
    Where="it.IsActive and (it.IsWorkInsteadOfMaterial = false)">
</cc11:ClassEntityDataSource>





