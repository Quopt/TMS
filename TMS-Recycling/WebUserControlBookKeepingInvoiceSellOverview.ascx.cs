﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TMS_Recycling
{
    public partial class WebUserControlBookKeepingInvoiceSellOverview : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            WebUserControlInvoiceOverview1.SwitchPurchaseType(InvoiceType.SellLedger);

        }
    }
}