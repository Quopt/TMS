using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TMS_Recycling
{
    public partial class WebUserControlStockMutationsBase : ClassTMSUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetName = "MaterialMutation";
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (DataItem != null)
            {
                URLPopUpControlLink.Visible = (DataItem as MaterialMutation).Order != null;
                if (URLPopUpControlLink.Visible)
                {
                    URLPopUpControlLink.URLToPopup = "WebFormPopup.aspx?UC=OrderBase&OrderId=" + (DataItem as MaterialMutation).Order.Id.ToString();
                }
            }
        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            StandardButtonCancelClickHandler(sender, e);
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            StandardButtonSaveClickHandler(sender, e);
        }

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            StandardButtonDeleteClickHandler(sender, e);
        }


    }
}