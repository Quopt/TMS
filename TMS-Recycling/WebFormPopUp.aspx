<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebFormPopUp.aspx.cs" Inherits="TMS_Recycling.WebFormPopUp" %>
<%@ Register assembly="TMS-Recycling" namespace="TMS_Recycling" tagprefix="cc11" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>
<body>
  <form id="form1" runat="server">
  <div class="page" style="width: 95%">
        <asp:PlaceHolder ID="PlaceHolderEmbeddedControl" runat="server"></asp:PlaceHolder>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
  </div>
  </form>
</body>
</html>
