using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Objects;

namespace TMS_Recycling
{
    public partial class WebFormBookKeeping : ClassTMSWebPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        void Page_PreRender(object sender, EventArgs e)
        {
            TreeView tv = this.Master.GetTreeView();

            // bookkeeping links 
            foreach (TreeNode tn in tv.Nodes[0].ChildNodes)
            {
                tn.SelectAction = WebUserControlBookKeepingOverview1.DetailVisible() ? TreeNodeSelectAction.Select : TreeNodeSelectAction.None;

                if (WebUserControlBookKeepingOverview1.DetailVisible())
                {
                    string BaseURL = tn.NavigateUrl;
                    if (BaseURL.IndexOf("?") > 0)
                    {
                        BaseURL = BaseURL.Substring(0, BaseURL.IndexOf("?"));
                    }
                    BaseURL = BaseURL + "?Id=" + WebUserControlBookKeepingOverview1.DetailKeyId();
                    tn.NavigateUrl = BaseURL;
                }
            }
        }

    }
}