using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TMS_Recycling
{
    public partial class WebFormSaleCredit : ClassTMSWebPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                WebUserControlNonCashPurchase1.SwitchPurchaseType(InvoiceType.Sell);
            }

        }
    }
}