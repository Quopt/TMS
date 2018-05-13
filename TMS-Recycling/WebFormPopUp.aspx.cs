using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TMS_Recycling
{
    public partial class WebFormPopUp : ClassTMSWebPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AddUserControl(); // always add the user control for callback handling
        }

        private void AddUserControl()
        {
            string UserControlName = "WebUserControl" + Request.Params["UC"] + ".ascx";

            ClassTMSUserControl TempControl = LoadControl(UserControlName) as ClassTMSUserControl;
            TempControl.ID = "ThisControl" + Request.Params["UC"];

            PlaceHolderEmbeddedControl.Controls.Add(TempControl);
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (Request.Params["Id"] != null)
            {
                ClassTMSUserControl TempControl = PlaceHolderEmbeddedControl.FindControl("ThisControl" + Request.Params["UC"]) as ClassTMSUserControl;

                if ((TempControl != null) && (TempControl.KeyID != Guid.Parse(Request.Params["Id"])))
                {
                    // make sure the data is loaded 
                    TempControl.KeyID = Guid.Parse(Request.Params["Id"]); //TempControl.KeyID;

                    if (TempControl.RefreshRequired)
                    {
                        TempControl.Visible = false;
                    }
                }
            }
        }
    }
}