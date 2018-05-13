using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Objects;

namespace TMS_Recycling
{
    public partial class WebFormStock : ClassTMSWebPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (WebUserControlStockOverview1.DetailVisible())
            {
                TreeView tv = this.Master.GetTreeView();
                foreach (TreeNode tn in tv.Nodes[0].ChildNodes[0].ChildNodes)
                {
                    if (tn.Value.IndexOf("StockDetail") == 0)
                    {
                        tn.SelectAction = TreeNodeSelectAction.Select;

                        string BaseURL = tn.NavigateUrl;
                        if (BaseURL.IndexOf("?") > 0)
                        {
                            BaseURL = BaseURL.Substring(0, BaseURL.IndexOf("?"));
                        }
                        BaseURL = BaseURL + "?Id=" + WebUserControlStockOverview1.DetailKeyId();
                        tn.NavigateUrl = BaseURL;
                    }
                }
            }
        }

    }
}