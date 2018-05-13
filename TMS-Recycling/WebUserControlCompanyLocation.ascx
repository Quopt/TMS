<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlCompanyLocation.ascx.cs" Inherits="TMS_Recycling.WebUserControlCompanyLocation" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="URLPopUpControl.ascx" tagname="URLPopUpControl" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<table width="100%">
<tr>
 <td colspan="4"><asp:Label ID="LabelBasisgegevens" runat="server" Text="Lokatiegegevens" CssClass="SubMenuHeader"></asp:Label></td>
 <td>    <asp:Label ID="LabelLocationNumber" runat="server" Text="Lokatienummer"></asp:Label> </td>
 <td>   <asp:Label ID="Label_LocationNumber_Text" runat="server"></asp:Label> </td>
</tr>
<tr>
<td>
    <asp:Label ID="LabelName" runat="server" Text="Naam"></asp:Label> </td>
    <td colspan="3"> <asp:TextBox ID="TextBox_Description_Text" runat="server" Width="90%" 
            MaxLength="250" ></asp:TextBox>
    </td>
    <td><asp:Label ID="LabelIsActive" runat="server" Text="Actief"></asp:Label> </td>
        <td>
            <asp:CheckBox ID="CheckBox_IsActive_Checked" Text="" runat="server" Checked="True" /></td>
</tr>
<tr>
    <td><asp:Label ID="LabelEMail" runat="server" Text="E-mail adres"></asp:Label> </td>
    <td> 
        <asp:TextBox ID="TextBoxEMail_EMail" runat="server" 
            Width="200px" MaxLength="40" ></asp:TextBox>
    </td>
    <td><asp:Label ID="LabelPhoneNumber" runat="server" Text="Telefoon"></asp:Label> </td>
    <td> 
        <asp:TextBox ID="TextBox_PhoneNumber" runat="server" Width="150px" 
            MaxLength="40" ></asp:TextBox>
    </td>
    <td><asp:Label ID="LabelVATNumber" runat="server" Text="BTW Nummer"></asp:Label> </td>
    <td> 
        <asp:TextBox ID="TextBox_VATNumber" runat="server" Width="150px" 
            MaxLength="40" ></asp:TextBox>
    </td>

</tr>
<tr>
    <td><asp:Label ID="LabelContactPerson" runat="server" Text="Contactpersoon"></asp:Label> </td>
    <td> 
        <asp:TextBox ID="TextBox_ContactPerson" runat="server" 
            Width="200px" MaxLength="40" ></asp:TextBox>
    </td>
    <td>&nbsp;</td>
    <td> 
        &nbsp;</td>
    <td><asp:Label ID="LabelPreferredCurrency" runat="server" Text="Voorkeursmunteenheid"></asp:Label> </td>
    <td> 
        <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" ID="DropDownList_PreferredCurrency_SelectedValue" runat="server">
        </cc11:ClassComboBox>
    </td>
</tr>

<tr>
<td>
    <asp:Label ID="LabelAddressLine1" runat="server" Text="Adres regel 1"></asp:Label>
    </td>
<td colspan="5">
    <asp:TextBox ID="TextBox_AdressLine1" runat="server" Width="90%"></asp:TextBox>
    </td>
</tr>
<tr>
<td>
    <asp:Label ID="LabelAddressLine2" runat="server" Text="Adres regel 2"></asp:Label>
    </td>
<td colspan="5">
    <asp:TextBox ID="TextBox_AdressLine2" runat="server" Width="90%"></asp:TextBox>
    </td>
</tr>
<tr>
<td>
    <asp:Label ID="LabelAddressLine3" runat="server" Text="Adres regel 3"></asp:Label>
    </td>
<td colspan="5">
    <asp:TextBox ID="TextBox_AdressLine3" runat="server" Width="90%"></asp:TextBox>
    </td>
</tr>

<tr>
<td>
    <asp:Label ID="LabelZIPCode" runat="server" Text="Postcode"></asp:Label>
    </td>
<td>
    <asp:TextBox ID="TextBox_ZIPcode" runat="server"></asp:TextBox>
    </td>
<td>
    <asp:Label ID="LabelCity" runat="server" Text="Stad"></asp:Label>
</td>
<td>
    <asp:TextBox ID="TextBox_City" runat="server"></asp:TextBox>
</td>
<td>
    <asp:Label ID="LabelCountry" runat="server" Text="Land"></asp:Label>
</td>
<td>

    <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" ID="DropDownList_Country_SelectedValue" runat="server">
    </cc11:ClassComboBox>

    </td>
</tr>

<tr><td><asp:Label ID="LabelInvoiceSettings" runat="server" CssClass="SubMenuHeader" Text="Factuurinstellingen"></asp:Label></td><td colspan="5"><hr /></td></tr>

<tr>
<td>

    <asp:Label ID="LabelInvoiceAddress" runat="server" Text="Factuuradres"></asp:Label>

</td>
<td colspan="2">
<asp:TextBox ID="TextBox_InvoiceAddress" runat="server" Rows="3" 
        TextMode="MultiLine" Width="90%"></asp:TextBox>
</td>
<td>
    <asp:Label ID="LabelInvoiceFooter" runat="server" Text="Factuur voettekst"></asp:Label>
</td>
<td colspan="2">
<asp:TextBox ID="TextBox_InvoiceFooter" runat="server" Rows="3" 
        TextMode="MultiLine" Width="90%"></asp:TextBox>
</td>
</tr>



<tr>
<td></td>
<td colspan="2">
            <asp:CheckBox ID="CheckBox_InvoicePrintLogo_Checked" Text="Logo op factuur afdrukken" runat="server" 
        Checked="True" />
</td>
<td>
    <asp:Label ID="LabelInvoiceFooterPageText" runat="server" Text="Factuurtekst onder elke pagina"></asp:Label>
</td>
<td colspan="2">
<asp:TextBox ID="TextBox_InvoiceFooterPerPage" runat="server" Rows="3" 
        TextMode="MultiLine" Width="90%"></asp:TextBox>
</td>
</tr>

<tr>
<td>

    <asp:Label ID="LabelInvoiceLogo" runat="server" Text="Factuurlogo"></asp:Label>

    </td>
<td colspan="2">
            <asp:Image ID="ImageInvoiceLogo" runat="server" /><br />
    <asp:FileUpload ID="FileUploadInvoiceLogo" runat="server" />
            <asp:Button ID="ButtonUploadInvoiceLogo"
        runat="server" Text="Upload" onclick="ButtonUploadInvoiceLogo_Click" />&nbsp;</td>

<td>
    <asp:Label ID="LabelMembershipsLogo" runat="server" Text="Lidmaatschapslogo"></asp:Label>
</td>
<td colspan="2">
    <asp:Image ID="ImageMembershipsLogo" runat="server" /><br />
    <asp:FileUpload ID="FileUploadMembershipsLogo" runat="server" />
    <asp:Button ID="ButtonUploadMembershipsLogo"
        runat="server" Text="Upload" onclick="ButtonUploadMembershipsLogo_Click" />
</td>
</tr>

<tr><td><asp:Label ID="LabelBookKeepingSettings" runat="server" CssClass="SubMenuHeader" Text="Boekhoud instellingen"></asp:Label></td><td colspan="5"><hr /></td></tr>

<tr>
<td>
<asp:Label ID="LabelDefaultVATPercentage" runat="server" Text="Standaard BTW percentage"></asp:Label>
</td>
<td>
<asp:TextBox ID="TextBox_DefaultVATPercentage" runat="server" Width="90px"></asp:TextBox>
</td>
<td>
<asp:Label ID="LabelDefaultHourlyRate" runat="server" Text="Standaard uurtarief"></asp:Label>
</td>
<td>
<asp:TextBox ID="TextBox_DefaultHourlyRate" runat="server" Width="90px"></asp:TextBox>
</td>
<td>
<asp:Label ID="LabelTariffWeighingOnly" runat="server" Text="Standaard weegtarief (ex BTW)"></asp:Label>
</td>
<td>
<asp:TextBox ID="TextBox_DefaultWeighingTariff" runat="server" Width="90px"></asp:TextBox>
</td>
</tr>

<tr>
<td>
</td>
<td>
</td>
<td>
</td>
<td>
</td>
<td>
<asp:Label ID="LabelDefaultWeighingBookingCode" runat="server" Text="Code voor weegtarief"></asp:Label>
</td>
<td>
    <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" 
        ID="DropDownList_DefaultWeighingTariffBookingCode" runat="server" Width="200px" 
        DataSourceID="EntityDataSourceBookingCodes" 
        DataTextField="Description" DataValueField="Id" MaxLength="0" 
        style="display: inline;"></cc11:ClassComboBox>
</td>
</tr>

<tr>
<td>
<asp:Label ID="LabelDefaultCashLedger" runat="server" Text="Standaard kasboek voor deze lokatie"></asp:Label>
</td>
<td>
    <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" ID="DropDownList_CashLedger" runat="server" Width="200px" 
        DataSourceID="EntityDataSourceCashLedgers" DataTextField="Description" 
        DataValueField="Id"></cc11:ClassComboBox></td>
<td>
<nobr>    
    <uc1:URLPopUpControl ID="URLPopUpControlAddCashLedgerForThisLocation" 
        runat="server" Text="Nieuw" URLToPopup="WebFormStock.aspx" OnPopUpClosed="GeneralOnPopupClosed" OnBeforePopUpOpened="URLPopUpControlAddCashLedgerForThisLocation_OnBeforePopUpOpened" />
    <uc1:URLPopUpControl ID="URLPopUpControlEditCashLedger" 
        runat="server" Text="Bewerk" URLToPopup="WebFormStock.aspx" OnPopUpClosed="GeneralOnPopupClosed" OnBeforePopUpOpened="URLPopUpControlEditCashLedger_OnBeforePopUpOpened" />
</nobr>
</td>
<td>
<asp:Label ID="LabelDefaultBankLedger" runat="server" Text="Standaard bankboek voor deze lokatie"></asp:Label>
</td>
<td>
    <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" ID="DropDownList_BankLedger" runat="server" Width="200px" 
        DataSourceID="EntityDataSourceBankLedgers" DataTextField="Description" 
        DataValueField="Id"></cc11:ClassComboBox>
</td>
<td>
<nobr>
    <uc1:URLPopUpControl ID="URLPopUpControlAddBankLedgerForThisLocation" 
        runat="server" Text="Nieuw" URLToPopup="WebFormStock.aspx" OnPopUpClosed="GeneralOnPopupClosed" OnBeforePopUpOpened="URLPopUpControlAddBankLedgerForThisLocation_OnBeforePopUpOpened" />
    <uc1:URLPopUpControl ID="URLPopUpControlEditBankLedger" 
        runat="server" Text="Bewerk" URLToPopup="WebFormStock.aspx" OnPopUpClosed="GeneralOnPopupClosed" OnBeforePopUpOpened="URLPopUpControlEditBankLedger_OnBeforePopUpOpened" />
</nobr>
</td>
</tr>

<tr>
<td>
<asp:Label ID="LabelDefaultMaterialDirt" runat="server" Text="Standaard materiaal voor vuil"></asp:Label>
</td>
<td>
    <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" ID="DropDownList_MaterialForDirt" runat="server" Width="200px" 
        DataSourceID="EntityDataSourceMaterialsForThisLocation" 
        DataTextField="Description" DataValueField="Id"></cc11:ClassComboBox>
</td>
<td>
<nobr>
    <uc1:URLPopUpControl ID="URLPopUpControlAddMaterialForThisLocation" 
        runat="server" Text="Nieuw" URLToPopup="WebFormStock.aspx" OnPopUpClosed="GeneralOnPopupClosed" OnBeforePopUpOpened="URLPopUpControlAddMaterialForThisLocation_OnBeforePopUpOpened" />
    <uc1:URLPopUpControl ID="URLPopUpControlEditMaterial" 
        runat="server" Text="Bewerk" URLToPopup="WebFormStock.aspx" OnPopUpClosed="GeneralOnPopupClosed" OnBeforePopUpOpened="URLPopUpControlEditMaterial_OnBeforePopUpOpened" />
</nobr>
</td>
<td>
<asp:Label ID="LabelDefaultMaterialHours" runat="server" Text="Code voor Uren"></asp:Label>
</td>
<td>
    <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" ID="DropDownList_MaterialForWork" runat="server" Width="200px" 
        DataSourceID="EntityDataSourceHourMaterialsForThisLocation" 
        DataTextField="Description" DataValueField="Id"></cc11:ClassComboBox>
</td>
<td>
<nobr>
    <uc1:URLPopUpControl ID="URLPopUpControlAddWorkMaterialForThisLocation" 
        runat="server" Text="Nieuw" URLToPopup="WebFormStock.aspx" OnPopUpClosed="GeneralOnPopupClosed" OnBeforePopUpOpened="URLPopUpControlAddWorkMaterialForThisLocation_OnBeforePopUpOpened" />
    <uc1:URLPopUpControl ID="URLPopUpControlEditWorkMaterial" 
        runat="server" Text="Bewerk" URLToPopup="WebFormStock.aspx" OnPopUpClosed="GeneralOnPopupClosed" OnBeforePopUpOpened="URLPopUpControlEditWorkMaterial_OnBeforePopUpOpened" />
</nobr>
</td>

</tr>

<tr>
<td>
<asp:Label ID="LabelDefaultSaleCustomer" runat="server" 
        Text="Standaard inkoopklant"></asp:Label>
</td>
<td>
    <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" 
        ID="DropDownList_RelationBuy" runat="server" Width="200px" 
        DataSourceID="EntityDataSourceCustomers" 
        DataTextField="Description" DataValueField="Id"></cc11:ClassComboBox>
</td>
<td>
<nobr>
    <uc1:URLPopUpControl ID="URLPopUpControlAddBuyCustomer" 
        runat="server" Text="Nieuw" URLToPopup="WebFormStock.aspx" 
        OnPopUpClosed="GeneralOnPopupClosed" 
        
        OnBeforePopUpOpened="URLPopUpControlAddBuyCustomer_OnBeforePopUpOpened" />
    <uc1:URLPopUpControl ID="URLPopUpControlEditBuyCustomer" 
        runat="server" Text="Bewerk" URLToPopup="WebFormStock.aspx" 
        OnPopUpClosed="GeneralOnPopupClosed" 
        OnBeforePopUpOpened="URLPopUpControlEditBuyCustomer_OnBeforePopUpOpened" />
</nobr>
</td>
<td>
<asp:Label ID="LabelDefaultPurchaseCustomer" runat="server" 
        Text="Standaard verkoopklant"></asp:Label>
</td>
<td>
    <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" 
        ID="DropDownList_RelationSale" runat="server" Width="200px" 
        DataSourceID="EntityDataSourceCustomers" 
        DataTextField="Description" DataValueField="Id"></cc11:ClassComboBox>
</td>
<td>
<nobr>
    <uc1:URLPopUpControl ID="URLPopUpControlAddSaleCustomer" 
        runat="server" Text="Nieuw" URLToPopup="WebFormStock.aspx" 
        OnPopUpClosed="GeneralOnPopupClosed" 
        OnBeforePopUpOpened="URLPopUpControlAddSaleCustomer_OnBeforePopUpOpened" />
    <uc1:URLPopUpControl ID="URLPopUpControlEditSaleCustomer" 
        runat="server" Text="Bewerk" URLToPopup="WebFormStock.aspx" 
        OnPopUpClosed="GeneralOnPopupClosed" 
        
        OnBeforePopUpOpened="URLPopUpControlEditSaleCustomer_OnBeforePopUpOpened" />
</nobr>
</td>
</tr>

<tr>
<td>
<asp:Label ID="LabelDefaultBailPriceBookingCode" runat="server" 
        Text="Code voor borg"></asp:Label>
</td>
<td>
    <cc11:ClassComboBox AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" 
        ID="DropDownList_DefaultBailPriceBookingCode" runat="server" Width="200px" 
        DataSourceID="EntityDataSourceBookingCodes" 
        DataTextField="Description" DataValueField="Id" MaxLength="0" 
        style="display: inline;"></cc11:ClassComboBox>
</td>
<td>
</td>
<td>
</td>
<td>
</td>
<td>
</td>
</tr>

<tr><td><asp:Label ID="Label1" runat="server"  CssClass="SubMenuHeader" Text="Vrachtinstellingen"></asp:Label></td><td colspan="5"><hr /></td></tr>
<tr>
<td>
<asp:Label ID="LabelVIHBCode" runat="server" 
        Text="VIHB code"></asp:Label>
</td>
<td>
<asp:TextBox ID="TextBox_VIHBCode" runat="server" Width="200px" MaxLength="20"></asp:TextBox>
</td>
<td>
</td>
<td>
</td>
<td>
</td>
<td>
</td>
</tr>
<tr><td colspan="6"><hr /></td></tr>

<tr>
    <td><asp:Label ID="Label15" runat="server" Text="Opmerkingen"></asp:Label> </td>
    <td colspan="5"> 
        <asp:TextBox ID="TextBox_Comments" runat="server" Width="90%" 
            MaxLength="2000" Rows="4" TextMode="MultiLine" ></asp:TextBox>
    </td>


</tr>

</table>

<asp:Button ID="ButtonCancel" runat="server" Text="Wijzigingen annuleren" 
    onclick="ButtonCancel_Click" />
<asp:Button ID="ButtonSave" runat="server" Text="Wijzigingen opslaan" 
    onclick="ButtonSave_Click" />
<asp:Button ID="ButtonDelete" runat="server" onclick="ButtonDelete_Click" OnClientClick="return confirm('Weet u het zeker dat u dit wilt?');"
    Text="Lokatie verwijderen" />

<cc11:ClassEntityDataSource ID="EntityDataSourceBankLedgers" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EntitySetName="LedgerSet" 
    EntityTypeFilter="" Select="it.[Id], it.[IsActive], it.Description" 
    
    Where="it.IsActive and (it.LedgerType=&quot;Bank&quot;) and ( (it.LimitToLocationId is null) or (it.LimitToLocationId = @LocID) )" 
    OrderBy="it.Description">
    <WhereParameters>
        <asp:ControlParameter ControlID="LabelID" DbType="Guid" 
            DefaultValue="00000000-0000-0000-0000-000000000000" Name="LocID" 
            PropertyName="Text" />
    </WhereParameters>
</cc11:ClassEntityDataSource>

<cc11:ClassEntityDataSource ID="EntityDataSourceCashLedgers" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EntitySetName="LedgerSet" 
    EntityTypeFilter="" Select="it.[Id], it.[IsActive], it.Description" 
    
    Where="it.IsActive and (it.LedgerType=&quot;Cash&quot;) and ( (it.LimitToLocationId is null) or (it.LimitToLocationId = @LocID) )" 
    OrderBy="it.Description">
    <WhereParameters>
        <asp:ControlParameter ControlID="LabelID" DbType="Guid" 
            DefaultValue="00000000-0000-0000-0000-000000000000" Name="LocID" 
            PropertyName="Text" />
    </WhereParameters>
</cc11:ClassEntityDataSource>


    

<cc11:ClassEntityDataSource ID="EntityDataSourceCustomers" 
    runat="server" ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EnableFlattening="False" 
    EntitySetName="RelationSet" OrderBy="it.Description" 
    Select="it.[Id], it.[Description], it.[IsActive]" 
    Where="it.IsActive" EntityTypeFilter="">

</cc11:ClassEntityDataSource>


<cc11:ClassEntityDataSource ID="EntityDataSourceMaterialsForThisLocation" 
    runat="server" ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EnableFlattening="False" 
    EntitySetName="MaterialSet" OrderBy="it.Description" 
    Select="it.[Id], it.[Description], it.[IsActive]" 
    Where="it.IsActive and (it.Location.Id = @Id)" EntityTypeFilter="">
    <WhereParameters>
        <asp:ControlParameter ControlID="LabelID" DbType="Guid" Name="Id" DefaultValue="00000000-0000-0000-0000-000000000000" 
            PropertyName="Text" />
    </WhereParameters>
</cc11:ClassEntityDataSource>

<cc11:ClassEntityDataSource ID="EntityDataSourceHourMaterialsForThisLocation" 
    runat="server" ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EnableFlattening="False" 
    EntitySetName="MaterialSet" OrderBy="it.Description" 
    Select="it.[Id], it.[Description], it.[IsActive]" 
    Where="it.IsActive and (it.Location.Id = @Id)" EntityTypeFilter="">
    <WhereParameters>
        <asp:ControlParameter ControlID="LabelID" DbType="Guid" Name="Id" DefaultValue="00000000-0000-0000-0000-000000000000" 
            PropertyName="Text" />
    </WhereParameters>
</cc11:ClassEntityDataSource>

<cc11:ClassEntityDataSource ID="EntityDataSourceBookingCodes" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EnableFlattening="False" 
    EntitySetName="LedgerBookingCodeSet" 
    Select="it.[Id], it.[Description], it.[IsActive]" Where="it.IsActive=true">
</cc11:ClassEntityDataSource>

<asp:Label ID="LabelID" runat="server" Visible="False"></asp:Label>
<asp:CheckBox ID="CheckBoxRefreshRequired" runat="server" Visible="False" />




    




