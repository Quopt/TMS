using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TMS_Recycling
{
    public partial class WebFormLedgerReportPurchasedMaterials : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                WebUserControlLedgerReportMaterialMovement1.OrderTypeToShow = OrderType.Buy;
            }
        }
    }
}