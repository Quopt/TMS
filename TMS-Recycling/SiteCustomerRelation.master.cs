using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TMS_Recycling
{
    public partial class SiteCustomerRelation : System.Web.UI.MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            Load += new EventHandler(Page_Load); 
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Params["Id"] != null)
                {
                    SetURLLinksToCustomerId(Request.Params["Id"]);
                }
            }
        }

        public void SetURLLinksToCustomerId(string NewId)
        {
            GetTreeView().Nodes[0].ChildNodes[0].NavigateUrl = "WebFormCustomerRelation.aspx?Id=" + NewId;

            TreeView tv = GetTreeView();
            foreach (TreeNode tn in tv.Nodes[0].ChildNodes[0].ChildNodes)
            {
                if (tn.Value.IndexOf("RelationsOverview_") == 0)
                {
                    string BaseURL = tn.NavigateUrl;
                    if (BaseURL.IndexOf("?") > 0)
                    {
                        BaseURL = BaseURL.Substring(0, BaseURL.IndexOf("?"));
                    }
                    BaseURL = BaseURL + "?Id=" + NewId;
                    tn.NavigateUrl = BaseURL;
                    tn.SelectAction = TreeNodeSelectAction.Select;
                }
            }
        }

        public TreeView GetTreeView()
        {
            return TreeViewCustomerRelation;
        }
    }
}