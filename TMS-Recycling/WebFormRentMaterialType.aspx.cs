using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TMS_Recycling
{
    public partial class WebFormRentMaterialType : ClassTMSWebPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            TreeView tv = this.Master.GetTreeView();
            foreach (TreeNode tn in tv.Nodes[3].ChildNodes[0].ChildNodes)
            {
                if (tn.Value.IndexOf("RentMaterialsOverview") == 0)
                {
                    tn.SelectAction = WebUserControlRentMaterialTypeOverview1.DetailVisible() ? TreeNodeSelectAction.Select : TreeNodeSelectAction.None;

                    if (WebUserControlRentMaterialTypeOverview1.DetailVisible())
                    {
                        string BaseURL = tn.NavigateUrl;
                        if (BaseURL.IndexOf("?") > 0)
                        {
                            BaseURL = BaseURL.Substring(0, BaseURL.IndexOf("?"));
                        }
                        BaseURL = BaseURL + "?Id=" + WebUserControlRentMaterialTypeOverview1.DetailKeyId();
                        tn.NavigateUrl = BaseURL;
                    }
                }
            }
        }

    }
}