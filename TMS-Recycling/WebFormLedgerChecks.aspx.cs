using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TMS_Recycling
{
    public partial class WebFormLedgerChecks : ClassTMSWebPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        void Page_PreRender(object sender, EventArgs e)
        {
            TreeView tv = this.Master.GetTreeView();
            foreach (TreeNode tn in tv.Nodes[1].ChildNodes)
            {
                tn.SelectAction = TreeNodeSelectAction.Select;

                string BaseURL = tn.NavigateUrl;
                if (BaseURL.IndexOf("?") > 0)
                {
                    BaseURL = BaseURL.Substring(0, BaseURL.IndexOf("?"));
                }
                BaseURL = BaseURL + "?LedgerBookingCodeId=" + Request.Params["LedgerBookingCodeId"];
                tn.NavigateUrl = BaseURL;
            }
        }

    }
}