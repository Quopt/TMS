<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlFreightLegalDocuments.ascx.cs" Inherits="TMS_Recycling.WebUserControlFreightLegalDocuments" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

    <asp:Panel ID="PanelFirstWeighing" runat="server">
        <asp:Label ID="LabelFirstWeighing" runat="server" CssClass="SubMenuHeader" 
            Text="Wettelijke bescheiden vereiste aanvullende informatie"></asp:Label>

        <br />
        <table border="1px" cellpadding="1px">
        <tr>
        <td>
        </td>
        <td>
        </td>
        <td>
                <asp:Label ID="Label1" runat="server" Text="Begeleidingsbrief"></asp:Label>
        </td>
        <td>
                <asp:Label ID="Label2" runat="server" Text="CMR"></asp:Label>
        </td>
        <td>
                <asp:Label ID="Label3" runat="server" Text="Bijlage 1A"></asp:Label>
        </td>
        <td>
                <asp:Label ID="Label4" runat="server" Text="Bijlage 1B"></asp:Label>
        </td>
        <td>
                <asp:Label ID="Label5" runat="server" Text="Bijlage 7"></asp:Label>
        </td>
        </tr>

        <tr>
        <td>

            <asp:Label ID="Label6" runat="server" Text="Transportrol"></asp:Label>

        </td>
        <td>
            <cc11:ClassComboBox ID="ComboBox_TransportRole_SelectedValue" runat="server" AutoCompleteMode="SuggestAppend" 
                DropDownStyle="DropDownList">
            </cc11:ClassComboBox>
        </td>
        <td>
            x</td>
        <td>&nbsp;
        </td>
        <td>&nbsp;
        </td>
        <td>&nbsp;
        </td>
        <td>&nbsp;
        </td>
        </tr>

        <tr>
        <td>
            <asp:Label ID="Label7" runat="server" Text="Ontdoener"></asp:Label>
        </td>
        <td>
            <cc11:ClassComboBox ID="ComboBox_TransportUltimateSourceCustomer" runat="server" AutoCompleteMode="SuggestAppend" 
                DropDownStyle="DropDownList" AppendDataBoundItems="True" 
                DataSourceID="EntityDataSourceCustomers" DataTextField="Description" 
                DataValueField="Id">
                <asp:ListItem Value="">-door ons-</asp:ListItem>
            </cc11:ClassComboBox>
            &nbsp;</td>
        <td>
            x</td>
        <td>
            x</td>
        <td>
            x</td>
        <td>
            x</td>
        <td>
            x</td>
        </tr>

        <tr>
        <td>
            <asp:Label ID="Label8" runat="server" Text="Lokatie van herkomst"></asp:Label>
        </td>
        <td>
            <cc11:ClassComboBox ID="ComboBox_TransportSourceCustomer" runat="server" AutoCompleteMode="SuggestAppend" 
                DropDownStyle="DropDownList" AppendDataBoundItems="True" 
                DataSourceID="EntityDataSourceCustomers" DataTextField="Description" 
                DataValueField="Id">
                <asp:ListItem Value="">-door ons-</asp:ListItem>
            </cc11:ClassComboBox>
            &nbsp;</td>
        <td>
            x</td>
        <td>
            x</td>
        <td>
            x</td>
        <td>
            x</td>
        <td>
            x</td>
        </tr>

        <tr>
        <td>
            <asp:Label ID="Label9" runat="server" Text="Uitbesteed vervoerder"></asp:Label>
        </td>
        <td>
            <cc11:ClassComboBox ID="ComboBox_TransportCompanyCustomer" runat="server" AutoCompleteMode="SuggestAppend" 
                DropDownStyle="DropDownList" AppendDataBoundItems="True" 
                DataSourceID="EntityDataSourceTransportCustomers" DataTextField="Description" 
                DataValueField="Id">
                <asp:ListItem Value="">-door ons-</asp:ListItem>
            </cc11:ClassComboBox>
            &nbsp;</td>
        <td>
            x</td>
        <td>
            x</td>
        <td>
            x</td>
        <td>
            x</td>
        <td>
            x</td>
        </tr>

        <tr>
        <td>
            <asp:Label ID="Label10" runat="server" Text="Lokatie van bestemming"></asp:Label>
        </td>
        <td>
            <cc11:ClassComboBox ID="ComboBox_TransportDestinationCustomer" runat="server" AutoCompleteMode="SuggestAppend" 
                DropDownStyle="DropDownList" AppendDataBoundItems="True" 
                DataSourceID="EntityDataSourceCustomers" DataTextField="Description" 
                DataValueField="Id">
                <asp:ListItem Value="">-door ons-</asp:ListItem>
            </cc11:ClassComboBox>
            &nbsp;</td>
        <td>
            x</td>
        <td>
            x</td>
        <td>
            x</td>
        <td>
            x</td>
        <td>
            x</td>
        </tr>

        <tr>
        <td>
            <asp:Label ID="Label11" runat="server" Text="Verwijderingsinrichting"></asp:Label>
        </td>
        <td>
            <cc11:ClassComboBox ID="ComboBox_TransportDestructorCustomer" runat="server" AutoCompleteMode="SuggestAppend" 
                DropDownStyle="DropDownList" AppendDataBoundItems="True" 
                DataSourceID="EntityDataSourceCustomers" DataTextField="Description" 
                DataValueField="Id">
                <asp:ListItem Value="">-door ons-</asp:ListItem>
            </cc11:ClassComboBox>
            &nbsp;</td>
        <td>
            x</td>
        <td>
            x</td>
        <td>
            x</td>
        <td>
            x</td>
        <td>
            x</td>
        </tr>

        <tr>
        <td>
            <asp:Label ID="Label12" runat="server" Text="Totaal aantal geplande overbrengingen"></asp:Label>
        </td>
        <td>
            <cc11:ClassComboBox ID="ComboBox_TransportNotificationTransportType_SelectedValue" runat="server" AutoCompleteMode="SuggestAppend" 
                DropDownStyle="DropDownList">
            </cc11:ClassComboBox>
        </td>
        <td>&nbsp;
        </td>
        <td>&nbsp;
        </td>
        <td>
            x</td>
        <td>&nbsp;
        </td>
        <td>&nbsp;
        </td>
        </tr>

        <tr>
        <td>
            <asp:Label ID="Label13" runat="server" Text="Doel van transport"></asp:Label>
        </td>
        <td>
            <cc11:ClassComboBox ID="ComboBox_TransportNotificationRemovalType_SelectedValue" runat="server" AutoCompleteMode="SuggestAppend" 
                DropDownStyle="DropDownList">
            </cc11:ClassComboBox>
        </td>
        <td>&nbsp;
        </td>
        <td>&nbsp;
        </td>
        <td>
            x</td>
        <td>&nbsp;
        </td>
        <td>
            x</td>
        </tr>

        <tr>
        <td>
            <asp:Label ID="Label14" runat="server" Text="Vooraf goedgekeurde inrichting voor nuttige toepassing"></asp:Label>
        </td>
        <td>
            <asp:CheckBox ID="CheckBox_TransportNotificationApprovedDestructor_Checked" 
                runat="server" Text="" />
        </td>
        <td>&nbsp;
        </td>
        <td>&nbsp;
        </td>
        <td>
            x</td>
        <td>&nbsp;
        </td>
        <td>&nbsp;
        </td>
        </tr>

        <tr>
        <td>
            <asp:Label ID="Label15" runat="server" Text="Totaal aantal geplande overbrengingen"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="TextBox_TransportPlannedTransports" runat="server"></asp:TextBox>
            <asp:MaskedEditExtender ID="TextBox_TransportPlannedTransports_MaskedEditExtender" 
                runat="server" Mask="99" TargetControlID="TextBox_TransportPlannedTransports">
            </asp:MaskedEditExtender>
        </td>
        <td>&nbsp;
        </td>
        <td>&nbsp;
        </td>
        <td>
            x</td>
        <td>&nbsp;
        </td>
        <td>&nbsp;
        </td>
        </tr>

        <tr>
        <td>
            <asp:Label ID="Label16" runat="server" Text="Wijze van verpakking"></asp:Label>
        </td>
        <td>
            <cc11:ClassComboBox ID="ComboBox_TransportWrapping_SelectedValue" runat="server" AutoCompleteMode="SuggestAppend" 
                DropDownStyle="DropDownList">
            </cc11:ClassComboBox>
        </td>
        <td>&nbsp;
        </td>
        <td>&nbsp;
        </td>
        <td>
            x</td>
        <td>
            x</td>
        <td>&nbsp;
        </td>
        </tr>

        <tr>
        <td>
            <asp:Label ID="Label17" runat="server" Text="Bijzondere behandelingseisen"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="TextBox_TransportSpecialTreatment" runat="server" Width="200px" 
                MaxLength="40"></asp:TextBox>
        </td>
        <td>&nbsp;
        </td>
        <td>&nbsp;
        </td>
        <td>
            x</td>
        <td>&nbsp;
        </td>
        <td>&nbsp;
        </td>
        </tr>

        <tr>
        <td>
            <asp:Label ID="Label18" runat="server" Text="D-code/R-code"></asp:Label>
        </td>
        <td>
            <cc11:ClassComboBox ID="ComboBox_TransportDRCode_SelectedValue" runat="server" AutoCompleteMode="SuggestAppend" 
                DropDownStyle="DropDownList">
            </cc11:ClassComboBox>
        </td>
        <td>&nbsp;
        </td>
        <td>&nbsp;
        </td>
        <td>
            x</td>
        <td>
            x</td>
        <td>
            x</td>
        </tr>

        <tr>
        <td>
            <asp:Label ID="Label19" runat="server" Text="Gebruikte technologie"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="TextBox_TransportUsedTechnology" runat="server" MaxLength="40"></asp:TextBox>
        </td>
        <td>&nbsp;
        </td>
        <td>&nbsp;
        </td>
        <td>
            x</td>
        <td>&nbsp;
        </td>
        <td>&nbsp;
        </td>
        </tr>

        <tr>
        <td>
            <asp:Label ID="Label20" runat="server" Text="Reden voor uitvoer"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="TextBox_TransportReasonForExport" runat="server" MaxLength="40"></asp:TextBox>
        </td>
        <td>&nbsp;
        </td>
        <td>&nbsp;
        </td>
        <td>
            x</td>
        <td>&nbsp;
        </td>
        <td>&nbsp;
        </td>
        </tr>

        <tr>
        <td>
            <asp:Label ID="Label21" runat="server" Text="Handeling tot verwijdering/nuttige toepassing"></asp:Label>
        </td>
        <td>
            <cc11:ClassComboBox ID="ComboBox_TransportDestructionAction_SelectedValue" runat="server" AutoCompleteMode="SuggestAppend" 
                DropDownStyle="DropDownList">
            </cc11:ClassComboBox>
        </td>
        <td>&nbsp;
        </td>
        <td>&nbsp;
        </td>
        <td>&nbsp;
        </td>
        <td>
            x</td>
        <td>&nbsp;
        </td>
        </tr>

        <tr>
        <td>
            <asp:Label ID="Label22" runat="server" Text="Vervoerswijze"></asp:Label>
        </td>
        <td>
            <cc11:ClassComboBox ID="ComboBox_TransportType_SelectedValue" runat="server" AutoCompleteMode="SuggestAppend" 
                DropDownStyle="DropDownList">
            </cc11:ClassComboBox>
        </td>
        <td>&nbsp;
        </td>
        <td>&nbsp;
        </td>
        <td>
            x</td>
        <td>&nbsp;
        </td>
        <td>&nbsp;
        </td>
        </tr>

        </table>
    </asp:Panel>

<asp:Panel ID="PanelForms" runat="server" Visible="False">
<asp:Label ID="LabelForms" runat="server" Text="Printvoorbeeld wettelijke bescheiden" 
        CssClass="SubMenuHeader"></asp:Label>
    <br />
  <nobr>
  <table width="100%"><tr><td width="50%" align="right">
  <asp:Label ID="LabelAppendix7" runat="server" Text="Bijlage 7" 
        CssClass="SubMenuHeader"></asp:Label>
  </td><td width="50%" align="right">
  <asp:Label ID="LabelCMR" runat="server" Text="CMR (op basis van voorgedrukt formulier)" 
        CssClass="SubMenuHeader"></asp:Label>
  </td></tr></table>
  <iframe id="FrameShowFormA" scrolling="auto" runat="server" height="400px" width="50%">
  </iframe>
    <iframe id="FrameShowFormB" scrolling="auto" runat="server" height="400px" width="50%">
  </iframe>
  </nobr>
  <table width="100%"><tr><td width="50%" align="right">
  <asp:Label ID="LabelGuidanceLetter" runat="server" Text="Begeleidingsbrief (op basis van voorgedrukt formulier)" 
        CssClass="SubMenuHeader"></asp:Label>
  </td><td width="50%" align="right">
  <asp:Label ID="LabelAppendix1a" runat="server" Text="Bijlage 1A" 
        CssClass="SubMenuHeader"></asp:Label>
  </td></tr></table>  
  <nobr>
    <iframe id="FrameShowFormC" scrolling="auto" runat="server" height="400px" width="50%">
  </iframe>
      <iframe id="FrameShowFormD" scrolling="auto" runat="server" height="400px" width="50%">
  </iframe>
  </nobr>
  <table width="100%"><tr><td width="50%" align="right">
  <asp:Label ID="LabelAppendix1B" runat="server" Text="Bijlage 1B" 
        CssClass="SubMenuHeader"></asp:Label>
  </td><td width="50%" align="right">
  <asp:Label ID="LabelCMRWithForm" runat="server" Text="" 
        CssClass="SubMenuHeader"></asp:Label>
  </td></tr></table>  

  <nobr>
      <iframe id="FrameShowFormE" scrolling="auto" runat="server" height="400px" width="50%">
  </iframe>
  </nobr>
</asp:Panel>

<table width="100%">
    <tr>
    <td>
    <asp:Button ID="ButtonPrevious" runat="server" Text="Vorige stap" onclick="ButtonPrevious_Click" 
        Visible=false />    
    <asp:Button ID="ButtonContinue" runat="server" Text="Volgende stap" 
        onclick="ButtonContinue_Click" />
    </td>
    <td align="right">
        &nbsp;</td>
    </tr>
    </table>
<asp:Label ID="LabelCurrentPageNr" runat="server" Text="1" Visible="False"></asp:Label>
<asp:Label ID="LabelCurrentOrderId" runat="server" Visible="False"></asp:Label>
<asp:Label ID="LabelCustomerType" runat="server" Text="Creditor" Visible="False"></asp:Label>

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


<cc11:ClassEntityDataSource ID="EntityDataSourceTransportCustomers" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" EnableFlattening="False" 
    EntitySetName="RelationSet" 
    
    
    Where="(it.IsActive = true) and (it.CustomerType = &quot;Transport&quot;) " 
    Select="it.Id, it.Description" OrderBy="it.Description">
</cc11:ClassEntityDataSource>
