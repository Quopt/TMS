﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlLedgerBase.ascx.cs" Inherits="TMS_Recycling.WebUserControlLedgerBase" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit.HTMLEditor" tagprefix="cc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc2" %>
<%@ Register src="URLPopUpControl.ascx" tagname="URLPopUpControl" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<table width="100%">
<tr>
 <td colspan="2"><asp:Label ID="LabelBasicData" runat="server" 
         Text="Basisgegevens dagboek" CssClass="SubMenuHeader"></asp:Label></td>
 <td>    <asp:Label ID="LabelLocation" runat="server" Text="Dagboek voor lokatie"></asp:Label> </td>
 <td>    
        <cc2:ClassComboBoxLocation AutoCompleteMode="SuggestAppend" 
            DropDownStyle="DropDownList" ID="DropDownListLimitToLocation" runat="server" 
            DataSourceID="EntityDataSourceLocations" DataTextField="Description" 
            DataValueField="Id">
            <asp:ListItem Value="">- bruikbaar voor alle lokaties -</asp:ListItem>
        </cc2:ClassComboBoxLocation> 
    </td>
 <td>    <asp:Label ID="LabelIsActive" runat="server" Text="Actief"></asp:Label> 
     <br />
        <asp:Label ID="LabelIsDebugLedgerCode" runat="server" Text="Is debug ledger" 
         Visible="False"></asp:Label> </td>
 <td>   
            <asp:CheckBox ID="CheckBox_IsActive_Checked" Text="" runat="server" Checked="True" /> 
            <br />
            <asp:CheckBox ID="CheckBox_IsDebugLedger_Checked" Text="" runat="server" 
                Checked="True" Visible="False" /> </td>
</tr>

<tr>
<td>
    <asp:Label ID="LabelName" runat="server" Text="Dagboeknaam"></asp:Label> </td>
    <td> <asp:TextBox ID="TextBox_Description_Text" runat="server" Width="200px" 
            MaxLength="250" ></asp:TextBox>
    </td>
    <td><asp:Label ID="LabelLedgerType" runat="server" Text="Dagboek type"></asp:Label> </td>
        <td>
        <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" ID="DropDownList_LedgerType_SelectedValue" runat="server" 
                Width="89px">
        </cc11:ClassComboBox> 
        </td>
    <td><asp:Label ID="LabelLedgerType0" runat="server" Text="Dagboek valuta"></asp:Label> </td>
        <td>
        <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" ID="DropDownList_LedgerCurrency_SelectedValue" runat="server" 
                Width="89px">
            <asp:ListItem Selected="True" Value="Eur">Euro</asp:ListItem>
        </cc11:ClassComboBox> &nbsp;<asp:Label ID="LabelLedgerTevel" runat="server" 
                Text="Huidig niveau : "></asp:Label> <asp:Label ID="Label_LedgerLevel" 
                runat="server" Text="..."></asp:Label>  
            &nbsp;<nobr><uc1:URLPopUpControl ID="URLPopUpControlCorrect" runat="server" Text="Corrigeer niveau" OnPopUpClosed="URLPopUpControlCorrect_OnPopupClosed" OnBeforePopUpOpened="URLPopUpControlCorrect_OnBeforePopUpOpened"  />
</nobr>
    </td>
</tr>

<tr>
    <td><asp:Label ID="LabelBank" runat="server" Text="Bank naam"></asp:Label> </td>
    <td><asp:TextBox ID="TextBox_Bank" runat="server" Width="90%" MaxLength="40" ></asp:TextBox></td>
    <td><asp:Label ID="LabelBankAccount" runat="server" Text="Bank rekening"></asp:Label> </td>
    <td> 
        <asp:TextBox ID="TextBox_BankAccount" runat="server" Width="90%" 
            MaxLength="40" ></asp:TextBox> </td>
    <td><asp:Label ID="LabelBankIbanBic" runat="server" Text="Bank IBAN/BIC"></asp:Label> </td>
    <td><asp:TextBox ID="TextBox_BankIBAN" runat="server" Width="150px"></asp:TextBox>/<asp:TextBox ID="TextBox_BankBIC" 
            runat="server" Width="150px"></asp:TextBox></td>
</tr>


<tr>
    <td><asp:Label ID="LabelComments" runat="server" Text="Opmerkingen"></asp:Label> </td>
    <td colspan=5> 
        <asp:TextBox ID="TextBox_Comments" runat="server" Width="95%" 
            MaxLength="2000" Rows="4" TextMode="MultiLine" ></asp:TextBox>
    </td>

</tr>
</table>

<asp:Button ID="ButtonCancel" runat="server" Text="Wijzigingen annuleren" 
    onclick="ButtonCancel_Click" />
<asp:Button ID="ButtonSave" runat="server" Text="Wijzigingen opslaan" 
    onclick="ButtonSave_Click" />
<asp:Button ID="ButtonDelete" runat="server" Text="Dagboek verwijderen" OnClientClick="return confirm('Weet u het zeker dat u dit wilt?');"
    onclick="ButtonDelete_Click" />


<cc11:ClassEntityDataSource ID="EntityDataSourceLocations" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EnableFlattening="False" 
    EntitySetName="LocationSet" Select="it.[Id], it.[Description], it.IsActive" 
    Where="it.IsActive = true">
</cc11:ClassEntityDataSource>



