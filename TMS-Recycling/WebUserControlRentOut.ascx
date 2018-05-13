<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlRentOut.ascx.cs" Inherits="TMS_Recycling.WebUserControlRentOut" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit.HTMLEditor" tagprefix="cc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="URLPopUpControl.ascx" tagname="URLPopUpControl" tagprefix="uc3" %>
<%@ Register src="CalendarControl.ascx" tagname="CalendarControl" tagprefix="uc4" %>
<%@ Register src="WebUserControlRentMaterials.ascx" tagname="WebUserControlRentMaterials" tagprefix="uc5" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc2" %>
<%@ Register src="CalendarWithTimeControl.ascx" tagname="CalendarWithTimeControl" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<style type="text/css">
    .style1
    {
        width: 200px;
    }
</style>
<script type="text/javascript">
    function toggle(chkbox, group) {
        var visSetting = (!chkbox.checked) ? "visible" : "hidden";
        document.getElementById(group).style.visibility = visSetting;
    }

</script> 
<asp:Panel ID="PanelCustomerInformation" runat="server">
<asp:Label ID="LabelBasicData" runat="server" Text="Basisgegevens order" CssClass="SubMenuHeader"></asp:Label>
    <table style="width: 100%;">

        <tr>
            <td  style="width:200px;">
                <asp:Label ID="LabelPurchaseLocation" runat="server" Text="Lokatie"></asp:Label>
            </td>
            <td>
                <cc2:ClassComboBoxLocation ID="DropDownListLocations" runat="server" 
                    AutoCompleteMode="SuggestAppend" DataSourceID="EntityDataSourceLocations" 
                    DataTextField="Description" DataValueField="Id" DropDownStyle="DropDownList">
                </cc2:ClassComboBoxLocation>
            </td>
            <td style="width:250px;">&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LabelCustomer" runat="server" Text="Klant"></asp:Label>
            </td>
            <td>
                <cc11:ClassComboBox ID="DropDownListCustomers" runat="server" 
                    AutoCompleteMode="SuggestAppend" DataSourceID="EntityDataSourceCustomers" 
                    DataTextField="Description" DataValueField="Id" DropDownStyle="DropDownList" 
                    MaxLength="0" style="display: inline;">
                </cc11:ClassComboBox>
            </td>
            <td>
                <uc3:URLPopUpControl ID="URLPopUpControlManageCustomers" runat="server" 
                    Text="Klanten beheren" 
                    URLToPopup="WebFormPopUp.aspx?uc=CustomerRelationOverview" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LabelDescription" runat="server" Text="Omschrijving verhuur"></asp:Label>

            </td>
            <td>
                <asp:TextBox ID="TextBox_Description" runat="server" Width="90%"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="ButtonRefresh" runat="server" Text="Genereer omschrijving" onclick="ButtonRefresh_Click" 
                    />
            </td>
        </tr>
        <tr>
        <td>
            <asp:Label ID="LabelRentDates" runat="server" Text="Periode verhuur"></asp:Label>
        </td>
        <td>
            <uc1:CalendarWithTimeControl ID="CalendarControlStart" runat="server" />
            &nbsp;<asp:Label ID="LabelRentDatesInfix" runat="server" Text="tot en met "></asp:Label>
            <uc1:CalendarWithTimeControl ID="CalendarControlEnd" runat="server" />
            &nbsp;<asp:CheckBox ID="CheckBoxDatesOpenEndDate" runat="server" 
                Text="Nog geen einddatum bekend"  />
        </td>
        <td>
        </td>
        </tr>

    <tr>
    <td>
        <asp:Label ID="LabelDriverId" runat="server" Text="Identificatie huurder"></asp:Label>
    </td>
    <td>
        <asp:TextBox ID="TextBox_DriverId" runat="server" Width="90%"></asp:TextBox>
    </td>
    <td>
    </td>
    </tr>
</table>

</asp:Panel>

<asp:Panel ID="PanelOrderLines" runat="server">
<uc5:WebUserControlRentMaterials ID="WebUserControlRentMaterials1" runat="server" 
        Visible="True" />
</asp:Panel>

<asp:Panel ID="PanelOrderSummary" runat="server">
<asp:Label ID="LabelOrderSummary" runat="server" Text="Samenvatting verhuurorder" CssClass="SubMenuHeader"></asp:Label>

<table style="width: 100%;">
<tr>
<td class="style1">
    <asp:Label ID="LabelTotalItems" runat="server" Text="# items"></asp:Label>
</td>
<td>
    <asp:Label ID="LabelTotalItemsValue" runat="server" Text="0"></asp:Label>
</td>
</tr><tr>
    <td class="style1">
        <asp:Label ID="LabelTotalPrice" runat="server" Text="Totaal verhuurprijs"></asp:Label>
    </td>
    <td>
        <asp:Label ID="LabelTotalPriceValue" runat="server" Text="0.00"></asp:Label>
    </td>
    </tr>
<tr>
        <td class="style1">
            &nbsp;</td>
        <td>
            <asp:CheckBox ID="CheckBoxProFormaInvoice" runat="server" 
                Text="Maak een pro forma factuur aan" AutoPostBack="True" />

        </td>
        </tr>
<tr>
        <td class="style1">
            <asp:Label ID="LabelTotalBail" runat="server" Text="Totaal borg"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="TextBoxBail" runat="server"></asp:TextBox>
        </td>
        </tr>            
    <tr>
            <td class="style1">
                <asp:Label ID="LabelInvoiceDiscount" runat="server" Text="Factuurkorting"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBoxInvoiceDiscount" Text="0" runat="server"></asp:TextBox>
                <asp:MaskedEditExtender ID="TextBoxInvoiceDiscount_MaskedEditExtender" 
                    runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                    Mask="999" TargetControlID="TextBoxInvoiceDiscount">
                </asp:MaskedEditExtender>
            </td>
        </tr>
    <tr>
        <td class="style1" valign="top">
            <asp:Label ID="LabelInvoiceNote" runat="server" Text="Factuur notitie"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="TextBoxInvoiceNote" runat="server" Rows="4" 
                TextMode="MultiLine" Width="90%"></asp:TextBox>
        </td>
    </tr>
</table>

</asp:Panel>

<asp:Panel ID="PanelInvoice" runat="server">
<asp:Label ID="LabelInvoice" runat="server" Text="Verhuurfactuur en contractvoorwaarden" CssClass="SubMenuHeader"></asp:Label><br />
    <nobr>
  <iframe id="FrameShowInvoiceA" scrolling="auto" runat="server" height="400px" width="50%">
  </iframe>
  <iframe id="FrameShowInvoiceB" scrolling="auto" runat="server" height="400px" width="50%">
  </iframe>
  </nobr>
</asp:Panel>

<asp:Button ID="ButtonRevert" runat="server" Text="Vorige stap" 
    onclick="ButtonRevert_Click" />
<asp:Button ID="ButtonContinue" runat="server" Text="Volgende stap" 
    onclick="ButtonContinue_Click" />
<asp:Button ID="ButtonPrintAndProcess" runat="server" Text="Verhuurorder verwerken" 
    onclick="ButtonPrintAndProcess_Click" />
<asp:Button ID="ButtonDestroyOrderAndBack" runat="server" 
    Text="Verhuurorder aanpassen" onclick="ButtonDestroyOrderAndBack_Click" />
<asp:Button ID="ButtonNewOrder" runat="server" Text="Nieuwe verhuring" 
    onclick="ButtonNewOrder_Click" />

<asp:Label ID="LabelRentMode" runat="server" Text="Cash" Visible="False"></asp:Label>
<asp:Label ID="LabelCurrentPageNr" runat="server" Text="1" Visible="False"></asp:Label>
<asp:Label ID="LabelCurrentOrderId" runat="server" Text="{00000000-0000-0000-0000-000000000000}" Visible="False"></asp:Label>
<asp:Label ID="LabelCurrentInvoiceId" runat="server" Text="{00000000-0000-0000-0000-000000000000}" Visible="False"></asp:Label>
<asp:Label ID="LabelGroupId" runat="server" Text="" Visible="False"></asp:Label>


<cc11:ClassEntityDataSource ID="EntityDataSourceLocations" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EntitySetName="LocationSet" 
    OrderBy="it.Description" Select="it.[Id], it.[Description], it.[IsActive]" 
    Where="it.IsActive">
</cc11:ClassEntityDataSource>
<cc11:ClassEntityDataSource ID="EntityDataSourceCustomers" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EnableFlattening="False" 
    EntitySetName="RelationSet" OrderBy="it.Description" 
    Select="it.[Id], it.[Description], it.[IsActive]" Where="it.IsActive">
</cc11:ClassEntityDataSource>



