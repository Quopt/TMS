using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace TMS_Recycling
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.Request.Url.LocalPath.ToLower().IndexOf("login.aspx") > 0)
            {
                NavigationMenu.Visible = false;
            }

            if (ContentPlaceHolderPath.Controls.Count > 0)
            {
                LocateControl4PageTitle(ContentPlaceHolderPath.Controls);
            }
        }

        protected void LocateControl4PageTitle(ControlCollection ctrlContainer)
        {
            foreach (Control ctrl in ctrlContainer)
            {
                if (ctrl.GetType() == typeof(Label))
                {
                    string TempTitle = "";
                    TempTitle = (ctrl as Label).Text.Trim();
                    if (TempTitle.IndexOf('\\') > 0)
                    {
                        TempTitle = TempTitle.Substring( TempTitle.LastIndexOf('\\')+1).Trim();
                    }
                    Page.Title = TempTitle;

                    Session[Page.Request.Url.LocalPath] = TempTitle;
                    break;
                }
                if (ctrl.HasControls())
                {
                    LocateControl4PageTitle(ctrl.Controls);
                }
            }
        }


    }
}
