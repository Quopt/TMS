﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlFreightWeighing.ascx.cs" Inherits="TMS_Recycling.WebUserControlFreightWeighing" %>
    <%@ Register src="CalendarWithTimeControl.ascx" tagname="CalendarWithTimeControl" tagprefix="uc1" %>
    <%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
    <%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc1" %>
    <%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

    <%@ Register src="URLPopUpControl.ascx" tagname="URLPopUpControl" tagprefix="uc2" %>

    <asp:Panel ID="PanelCustomerInformation" runat="server">
        <asp:Label ID="LabelBasicData" runat="server" CssClass="SubMenuHeader" 
            Text="Start weging"></asp:Label>
        <table style="width: 100%;">
        <tr>
        <td width="20%">
            <asp:Label ID="LabelFirstOrSecondWeighing" runat="server" 
                Text="Wat wilt u wegen?"></asp:Label>
            </td>
        <td>
            <asp:RadioButtonList ID="RadioButtonListWeighingType" runat="server">
                <asp:ListItem Value="1" Selected="True">Eerste weging (bruto gewicht)</asp:ListItem>
                <asp:ListItem Value="2">Tweede weging (netto gewicht)</asp:ListItem>
            </asp:RadioButtonList>
            </td>
        </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="PanelSecondWeighingOrderNumber" runat="server">
        <asp:Label ID="Label1" runat="server" CssClass="SubMenuHeader" 
            Text="Tweede weging / weegbonnummer"></asp:Label>
        <table style="width: 100%;">
        <tr>
        <td width="20%">

            <asp:Label ID="LabelOrderNumber" runat="server" Text="Geef het weegbon nummer"></asp:Label>

        </td>
        <td>
            <asp:TextBox ID="TextBoxOrderNumber" runat="server">0</asp:TextBox>
            <asp:MaskedEditExtender ID="TextBoxOrderNumber_MaskedEditExtender" 
                runat="server" Mask="9999999" TargetControlID="TextBoxOrderNumber">
            </asp:MaskedEditExtender>
        </td>
        </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="PanelFirstWeighing" runat="server">
        <asp:Label ID="LabelFirstWeighing" runat="server" CssClass="SubMenuHeader" 
            Text="Eerste weging"></asp:Label>
        <table style="width: 100%;">
        <tr>
        <td width="20%">
            <asp:Label ID="LabelBuyOrSell" runat="server" 
                Text="Is deze weging voor de in- of verkoop"></asp:Label>
            </td>
        <td>
            <asp:RadioButtonList ID="RadioButtonListBuyOrSell" runat="server" 
                AutoPostBack="True" 
                onselectedindexchanged="RadioButtonListBuyOrSell_SelectedIndexChanged">
                <asp:ListItem Value="Buy" Selected="True">Weging tbv inkoop of niet van toepassing</asp:ListItem>
                <asp:ListItem Value="Sell">Weging tbv verkoop</asp:ListItem>
            </asp:RadioButtonList>
        </td>
        </tr>
        <tr>
        <td width="20%">
            <asp:Label ID="LabelLocation" runat="server" 
                Text="Weeglokatie"></asp:Label></td>
        <td>
            <cc1:ClassComboBoxLocation ID="ComboBoxWeighingLocation" runat="server" 
                AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" 
                DataSourceID="EntityDataSourceLocations" DataTextField="Description" 
                DataValueField="Id" MaxLength="0" AutoPostBack="true" 
                style="display: inline;" 
                onselectedindexchanged="ComboBoxWeighingLocation_SelectedIndexChanged"></cc1:ClassComboBoxLocation>
        </td>
        </tr>
                <tr>
        <td width="20%">
            <asp:Label ID="LabelDateTime1" runat="server" 
                Text="Datum en tijd"></asp:Label></td>
        <td>
            <uc1:CalendarWithTimeControl ID="CalendarWithTimeControlDateTime1" runat="server" />
        </td>
        </tr>
        <tr>
        <td width="20%">
            <asp:Label ID="LabelKey1" runat="server" 
                Text="Key"></asp:Label></td>
        <td>
            <asp:TextBox ID="TextBoxKey1" runat="server"></asp:TextBox>
        </td>
        </tr>
        <tr>
        <td width="20%">
            <asp:Label ID="LabelGrossWeight" runat="server" 
                Text="Gewicht vol (bruto)"></asp:Label></td>
        <td>
            <asp:TextBox ID="TextBoxGrossWeight" runat="server"></asp:TextBox>
            <asp:MaskedEditExtender ID="TextBoxGrossWeight_MaskedEditExtender" 
                runat="server" Mask="999999" TargetControlID="TextBoxGrossWeight">
            </asp:MaskedEditExtender>
            &nbsp;&nbsp;
            <asp:CheckBox ID="CheckBoxIsDriverInTruck" runat="server" 
                Text="Chauffeur zit in de truck" />
        </td>
        </tr>
        <tr>
        <td width="20%">
            <asp:Label ID="LabelPMV" runat="server" 
                Text="PMV"></asp:Label></td>
        <td>
            <asp:TextBox ID="TextBoxPMV" runat="server"></asp:TextBox>
        </td>
        </tr>
        <tr>
        <td width="20%">
            <asp:Label ID="LabelOurPlate" runat="server" 
                Text="Kenteken (indien gewogen met één van onze trucks)"></asp:Label></td>
        <td>
            <cc11:ClassComboBox ID="ComboBoxOurPlate" runat="server" AutoCompleteMode="SuggestAppend" 
                DropDownStyle="DropDownList" DataSourceID="EntityDataSourceTrucks" 
                DataTextField="Description" DataValueField="Id" MaxLength="0" 
                style="display: inline;" AppendDataBoundItems="True">
                <asp:ListItem Selected="True" Value="">-nvt-</asp:ListItem>
            </cc11:ClassComboBox>
        </td>
        </tr>
        <tr>
        <td width="20%">
            <asp:Label ID="LabelCustomerPlate" runat="server" 
                Text="Kenteken klant"></asp:Label></td>
        <td>
            <asp:TextBox ID="TextBoxCustomerPlate" runat="server"></asp:TextBox>
        </td>
        </tr>
        <tr>
        <td width="20%">
            <asp:Label ID="LabelCustomerID" runat="server" 
                Text="Identificatie klant"></asp:Label></td>
        <td>
            <asp:TextBox ID="TextBoxCustomerID" runat="server"></asp:TextBox>
        </td>
        </tr>
            <tr>
                <td width="20%">
                    <asp:Label ID="LabelCustomer" runat="server" Text="Klant"></asp:Label>
                </td>
                <td>
                    <cc11:ClassComboBox ID="ComboBoxCustomer" runat="server" AutoCompleteMode="SuggestAppend" 
                        DropDownStyle="DropDownList" DataSourceID="EntityDataSourceCustomers" 
                        DataTextField="Description" DataValueField="Id" MaxLength="0" 
                        style="display: inline;"></cc11:ClassComboBox>
                </td>
            </tr>
            <tr>
                <td width="20%">
                    <asp:Label ID="LabelProduct" runat="server" Text="Gewogen product (hoofdzakelijk)
                    "></asp:Label>&nbsp;</td>
                <td>
                    <cc11:ClassComboBox ID="ComboBoxProduct" runat="server" 
                        AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" 
                        DataSourceID="EntityDataSourceMaterials" DataTextField="Description" 
                        DataValueField="Id" MaxLength="0" style="display: inline;"></cc11:ClassComboBox>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="PanelFirstWeighingNr" runat="server">
        <asp:Label ID="LabelAssignedOrderNrPanel" runat="server" CssClass="SubMenuHeader" 
            Text="Toegekend volgnummer"></asp:Label><br />
        <asp:Label ID="LabelAssignedOrderNr" runat="server" 
            Text="Toegekend volgnummer : "></asp:Label>
            <asp:Label ID="LabelFreightNr" runat="server" Text="..."></asp:Label><br />
        <asp:Label ID="LabelAssignedOrderNrExplanation" runat="server" Text="Noteer dit toegekende order nummer of print de onderstaande bon uit. U heeft dit nodig om de tweede weging in te kunnen voeren. "></asp:Label>
            </asp:Panel><br />
    <asp:Panel ID="PanelSecondWeighing" runat="server">
        <asp:Label ID="LabelSecondWeighingData" runat="server" CssClass="SubMenuHeader" 
            Text="Tweede weging"></asp:Label>
        <table style="width: 100%;">
        <tr>
        <td width="20%">
            <asp:Label ID="LabelDateTime2" runat="server" 
                Text="Datum en tijd"></asp:Label></td>
        <td>
            <uc1:CalendarWithTimeControl ID="CalendarWithTimeControl2" runat="server" />
        </td>
        </tr>
        <tr>
        <td width="20%">
            <asp:Label ID="Label5" runat="server" 
                Text="Key"></asp:Label></td>
        <td>
            <asp:TextBox ID="TextBoxKey2" runat="server"></asp:TextBox>
        </td>
        </tr>
        <tr>
        <td width="20%">
            <asp:Label ID="Label6" runat="server" 
                Text="Gewicht leeg (tarra)"></asp:Label></td>
        <td>
            <asp:TextBox ID="TextBoxWeight2" runat="server"></asp:TextBox>
            <asp:MaskedEditExtender ID="MaskedEditExtender2" 
                runat="server" TargetControlID="TextBoxWeight2" 
                Mask="999999">
            </asp:MaskedEditExtender>
            &nbsp;
            </td>
        </tr>
            <tr>
                <td valign="top" width="20%">
                    <asp:Label ID="LabelDescription" runat="server" Text="Omschrijving vracht"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBoxDescription" runat="server" MaxLength="200" Width="90%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td valign="top" width="20%">
                    <asp:Label ID="LabelComments" runat="server" Text="Opmerkingen"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBoxComments" runat="server" MaxLength="1000" 
                        TextMode="MultiLine" Width="90%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="20%" valign="top">
                    <asp:Label ID="LabelWhatElse" runat="server" 
                        Text="Wat moet nog meer met deze weegbon gebeuren?"></asp:Label>
                </td>
                <td>
                    <nobr><asp:CheckBox ID="CheckBoxWeighingActionPay" runat="server" Text="Voor deze weegbon moet het reguliere weegtarief worden betaald." />&nbsp;<asp:Label runat="server" Text="Dagboek"></asp:Label>
                    <cc11:ClassComboBox runat="server" ID="ComboBoxLedger" 
                        AutoCompleteMode="SuggestAppend" DataSourceID="EntityDataSourceLedgers" 
                        DataTextField="Description" DataValueField="Id" DropDownStyle="DropDownList"></cc11:ClassComboBox></nobr><br />
                    <asp:CheckBox ID="CheckBoxWeighingActionSort" runat="server" Text="Deze weegbon moet verder worden uitgezocht voor deze klant." /><br />
                    <asp:CheckBox ID="CheckBoxWeighingActionInvoice" runat="server" Text="Deze weegbon hoeft niet meer te worden uitgezocht maar moet als inkoop of verkoop worden gefactureerd." /><br />
                </td>
            </tr>
        </table>
    </asp:Panel>

<asp:Panel ID="PanelInvoice" runat="server">
<asp:Label ID="LabelInvoice" runat="server" Text="Printvoorbeeld weegbescheiden" 
        CssClass="SubMenuHeader"></asp:Label>
    <br />
    <nobr>
  <iframe id="FrameShowInvoiceA" scrolling="auto" runat="server" height="400px" width="50%">
  </iframe>
  <iframe id="FrameShowInvoiceB" scrolling="auto" runat="server" height="400px" width="50%">
  </iframe>
  </nobr>
</asp:Panel>

<table width="100%">
<tr>
<td>
    <asp:Button ID="ButtonRevert" runat="server" Text="Vorige stap" 
        onclick="ButtonRevert_Click" />
    <asp:Button ID="ButtonContinue" runat="server" Text="Volgende stap" 
        onclick="ButtonContinue_Click" />
    <asp:Button ID="ButtonPrintAndProcess" runat="server" Text="Weging verwerken" 
        onclick="ButtonPrintAndProcess_Click" />
    <asp:Button ID="ButtonDestroyAndBack" runat="server" 
        Text="Weging aanpassen" onclick="ButtonDestroyOrderAndBack_Click" />
    <asp:Button ID="ButtonNew" runat="server" Text="Nieuwe weging" 
        onclick="ButtonNewOrder_Click" />
    <asp:Button ID="ButtonSecondWeighing" runat="server" Text="Tweede weging invoeren" 
        onclick="ButtonSecondOrder_Click" />
    <uc2:URLPopUpControl ID="URLPopUpControlLegalDocuments" runat="server" 
        Text="Transportdocumenten printen" />
</td>
<td align="right">
    <asp:Button ID="ButtonInvoiceOrder" runat="server" 
        Text="Gewogen materiaal factureren" onclick="ButtonInvoiceOrder_Click" />
    <asp:Button ID="ButtonSortOrder" runat="server" 
        Text="Weging op sorteerbon plaatsen" onclick="ButtonSortOrder_Click" 
         />
</td>
</tr>
</table>


<asp:Label ID="LabelCurrentPageNr" runat="server" Text="1" Visible="False"></asp:Label>
<asp:Label ID="LabelCurrentOrderId" runat="server" Visible="False"></asp:Label>
<asp:Label ID="LabelCustomerGuid" runat="server" Visible="False"></asp:Label>
<asp:Label ID="LabelCustomerType" runat="server" Text="Creditor" Visible="False"></asp:Label>
<asp:Label ID="LabelMaterialInvoiceType" runat="server" Text="Buy" Visible="False"></asp:Label>
<asp:Label ID="LabelGeneratedInvoiceId" runat="server" Visible="False"></asp:Label>
<asp:Label ID="LabelWeighingLoaded" runat="server" Text="" Visible="False"></asp:Label>

<cc11:ClassEntityDataSource ID="EntityDataSourceLocations" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EnableFlattening="False" 
    EntitySetName="LocationSet" Select="it.[Description], it.[Id], it.[IsActive]" 
    Where="it.IsActive = true">
</cc11:ClassEntityDataSource>


<cc11:ClassEntityDataSource ID="EntityDataSourceCustomers" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EnableFlattening="False" 
    EntitySetName="RelationSet" 
    
    
    Where="(it.IsActive = true) and ( (it.CustomerType = @CustomerType) || (it.CustomerType = &quot;Both&quot;)  || (it.CustomerType = &quot;Other&quot;) )" 
    Select="it.Id, it.Description" OrderBy="it.Description">
    <WhereParameters>
        <asp:ControlParameter ControlID="LabelCustomerType" Name="CustomerType" 
            PropertyName="Text" Type="String" />
    </WhereParameters>
</cc11:ClassEntityDataSource>

<cc11:ClassEntityDataSource ID="EntityDataSourceTrucks" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EntitySetName="TruckSet" 
    OrderBy="it.Description" Where="it.IsActive" 
    Select="it.Id, It.Description, it.IsActive">
</cc11:ClassEntityDataSource>

<cc11:ClassEntityDataSource ID="EntityDataSourceMaterials" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EntitySetName="MaterialSet" 
    OrderBy="it.Description" Select="it.[Id], it.[IsActive], it.[Description]" 
    
    
    Where="it.IsActive &amp;&amp; ( (it.InvoiceType= @InvoiceType) || (it.InvoiceType=&quot;Both&quot;)) &amp;&amp; (!it.IsWorkInsteadOfMaterial) &amp;&amp; (it.Location.Id = @LocationId)">
    <WhereParameters>
        <asp:ControlParameter ControlID="LabelMaterialInvoiceType" Name="InvoiceType" 
            PropertyName="Text" Type="String" />
        <asp:ControlParameter ControlID="ComboBoxWeighingLocation" DbType="Guid" 
            Name="LocationId" PropertyName="SelectedValue" />
    </WhereParameters>
</cc11:ClassEntityDataSource>

<cc11:ClassEntityDataSource ID="EntityDataSourceLedgers" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EnableFlattening="False" 
    EntitySetName="LedgerSet" OrderBy="it.Description" 
    Select="it.[Id], it.[IsActive], it.[Description]" 
    
    Where="(it.LimitToLocation is NULL) or (it.LimitToLocation.Id == @LocationID) and (it.IsActive)" 
    EntityTypeFilter="">
    <WhereParameters>
        <asp:ControlParameter ControlID="ComboBoxWeighingLocation" DbType="Guid" 
            DefaultValue="{00000000-0000-0000-0000-000000000000}" Name="LocationID" 
            PropertyName="SelectedValue" />
    </WhereParameters>
</cc11:ClassEntityDataSource>










