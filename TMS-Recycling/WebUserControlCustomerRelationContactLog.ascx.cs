using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TMS_Recycling
{
    public partial class WebUserControlCustomerRelationContactLog : ClassTMSUserControl
    {
        public WebUserControlCustomerRelationContactLog()
        {
            SetName = "RelationContactLog";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Common.AddRelationContactFollowUpStateList(DropDownList_FollowUpState_SelectedValue.Items, true);
                Common.AddRelationContactTypeList(DropDownList_ContactType_SelectedValue.Items, true); 
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