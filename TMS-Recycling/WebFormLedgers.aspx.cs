using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TMS_Recycling
{
    public partial class WebFormLedgers : ClassTMSWebPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        void Page_PreRender(object sender, EventArgs e)
        {
            TreeView tv = this.Master.GetTreeView();

            // bookkeeping links 
            foreach (TreeNode tn in tv.Nodes[1].ChildNodes)
            {
                tn.SelectAction = WebUserControlBookingCodeOverview1.DetailVisible() ? TreeNodeSelectAction.Select : TreeNodeSelectAction.None;

                if (WebUserControlBookingCodeOverview1.DetailVisible())
                {
                    string BaseURL = tn.NavigateUrl;
                    if (BaseURL.IndexOf("?") > 0)
                    {
                        BaseURL = BaseURL.Substring(0, BaseURL.IndexOf("?"));
                    }
                    BaseURL = BaseURL + "?LedgerBookingCodeId=" + WebUserControlBookingCodeOverview1.DetailKeyId();
                    tn.NavigateUrl = BaseURL;
                }
            }
        }

    }
}