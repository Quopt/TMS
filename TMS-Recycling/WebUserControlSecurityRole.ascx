﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControlSecurityRole.ascx.cs" Inherits="TMS_Recycling.WebUserControlSecurityRole" %>
<%@ Register src="URLPopUpControl.ascx" tagname="URLPopUpControl" tagprefix="uc1" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<table width="100%">
<tr>
 <td colspan="2">
     <asp:Label ID="LabelBasisgegevens" runat="server" 
         Text="Basisgegevens lokatie" CssClass="SubMenuHeader"></asp:Label></td>
 <td>    &nbsp;</td>
 <td>   &nbsp;</td>
</tr>
<tr>
<td>
    <asp:Label ID="LabelName" runat="server" Text="Naam"></asp:Label> </td>
    <td width="300px"> <asp:TextBox ID="TextBox_Description_Text" runat="server" Width="90%" 
            MaxLength="250" ></asp:TextBox>
    </td>
    <td><asp:Label ID="LabelIsActive" runat="server" Text="Actief"></asp:Label> 
        <br />
        <asp:Label ID="LabelUnlimitedAccess" runat="server" Text="Heeft onbeperkte toegang"></asp:Label> </td>
        <td>
            <asp:CheckBox ID="CheckBox_IsActive_Checked" Text="" runat="server" Checked="True" />
            <br />
            <asp:CheckBox ID="CheckBox_HasUnlimitedAccess_Checked" Text="" runat="server" 
                Checked="True" />
            <br />
    </td>
</tr>



<tr>
    <td><asp:Label ID="Label15" runat="server" Text="Opmerkingen"></asp:Label> </td>
    <td colspan="3"> 
        <asp:TextBox ID="TextBox_Comments" runat="server" Width="90%" 
            MaxLength="2000" Rows="4" TextMode="MultiLine" ></asp:TextBox>
    </td>


</tr>

</table>

<cc11:ClassGridView ID="GridViewRoleObjectAccess" runat="server" 
    AutoGenerateColumns="False" 
    DataSourceID="EntityDataSourceRoleObjectAccess" 
    onrowdatabound="GridViewRoleObjectAccess_RowDataBound" PageSize="99999">
    <Columns>
        <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" 
            SortExpression="Id" Visible="False" />
        <asp:BoundField DataField="ObjectName" HeaderText="Code" ReadOnly="True" 
            SortExpression="ObjectName" />
        <asp:BoundField DataField="Description" HeaderText="Omschrijving" 
            ReadOnly="True" SortExpression="Description" />
        <asp:TemplateField HeaderText="Mag aanmaken" SortExpression="HasCreateAccess">
            <ItemTemplate>
                <asp:CheckBox ID="CheckBoxCreate" runat="server" 
                    Checked='<%# Bind("HasCreateAccess") %>' Enabled="true" Visible="true" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Mag lezen" SortExpression="HasReadAccess">
            <ItemTemplate>
                <asp:CheckBox ID="CheckBoxRead" runat="server" 
                    Checked='<%# Bind("HasReadAccess") %>' Enabled="true" Visible="true"  />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Mag bijwerken" SortExpression="HasUpdateAccess">
            <ItemTemplate>
                <asp:CheckBox ID="CheckBoxUpdate" runat="server" 
                    Checked='<%# Bind("HasUpdateAccess") %>' Enabled="true" Visible="true" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Mag verwijderen" 
            SortExpression="HasDeleteAccess">
            <ItemTemplate>
                <asp:CheckBox ID="CheckBoxDelete" runat="server" 
                    Checked='<%# Bind("HasDeleteAccess") %>' Enabled="true" Visible="true" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Mag uitvoeren" SortExpression="HasExecuteAccess">
            <ItemTemplate>
                <asp:CheckBox ID="CheckBoxExecute" runat="server" 
                    Checked='<%# Bind("HasExecuteAccess") %>' Enabled="true" Visible="true" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="SettableAccessTypes" 
            HeaderText="SettableAccessTypes" ReadOnly="True" 
            SortExpression="SettableAccessTypes" Visible="False" />
    </Columns>
</cc11:ClassGridView>

<table style="width:100%;">
<tr>
<td>
<nobr>
<asp:Button ID="ButtonCancel" runat="server" Text="Wijzigingen annuleren" 
    onclick="ButtonCancel_Click" />
<asp:Button ID="ButtonSave" runat="server" Text="Wijzigingen opslaan" 
    onclick="ButtonSave_Click" />
<asp:Button ID="ButtonDelete" runat="server" onclick="ButtonDelete_Click" 
    Text="Rol verwijderen" />
</nobr>
</td>
<td style="width:90%;text-align:right;">
    &nbsp;</td></tr></table>


    





<cc11:ClassEntityDataSource ID="EntityDataSourceRoleObjectAccess" runat="server" 
    ConnectionString="name=ModelTMSContainer" 
    DefaultContainerName="ModelTMSContainer" 
    EntitySetName="SecurityRoleObjectAccessSet" 
    
    Select="it.[Id], it.[Description], it.[ObjectName], it.[HasExecuteAccess], it.[HasDeleteAccess], it.[HasUpdateAccess], it.[HasReadAccess], it.[HasCreateAccess], it.SettableAccessTypes" 
    OrderBy="it.ObjectName" EntityTypeFilter="" Where="it.SecurityRole.Id = @Id">
    <WhereParameters>
        <asp:ControlParameter ControlID="LabelRoleID" DbType="Guid" Name="Id" 
            PropertyName="Text" />
    </WhereParameters>
</cc11:ClassEntityDataSource>



    





<asp:Label ID="LabelRoleID" Visible="false" runat="server" Text="00000000-0000-0000-0000-000000000000"></asp:Label>




    





<asp:Label ID="LabelIDResultSet" runat="server" Text="..." Visible="False"></asp:Label>





    




