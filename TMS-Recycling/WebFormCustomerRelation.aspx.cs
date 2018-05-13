using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Objects;

namespace TMS_Recycling
{
    public partial class WebFormCustomerRelation : ClassTMSWebPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (WebUserControlCustomerRelationOverview1.DetailVisible())
            {
                Master.SetURLLinksToCustomerId(WebUserControlCustomerRelationOverview1.DetailKeyId().ToString());
            }
        }

   }
}