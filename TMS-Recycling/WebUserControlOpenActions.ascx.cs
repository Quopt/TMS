using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TMS_Recycling
{
    public partial class WebUserControlOpenActions : ClassTMSUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // set entity data source parameters
                EntityDataSourceOpenActions.CommandParameters["Handler"].DefaultValue = Session["CurrentUserID"].ToString();
                EntityDataSourceOpenActions.CommandParameters["ClientDateTime"].DefaultValue = Common.CurrentClientDateTime(Session).ToString();

                EntityDataSourceOpenActions.DefaultContainerName = EntityDataSourceOpenActions.DefaultContainerName;
                ClassGridViewOpenActions.DataBind();
            }
        }

        protected void ClassGridViewOpenActions_SelectedIndexChanged(object sender, EventArgs e)
        {
            string SelectedId = ClassGridViewOpenActions.SelectedDataKey.Values[0].ToString();
            string SelectedRelationContactId = ClassGridViewOpenActions.SelectedDataKey.Values[1].ToString();
            string SelectedRelationId = ClassGridViewOpenActions.SelectedDataKey.Values[2].ToString();

            Response.Redirect( "WebFormCustomerRelationContacts.aspx?Id=" +SelectedRelationId+ "&RelationContactId=" +SelectedRelationContactId+ "&RelationContactLogId=" + SelectedId );
        }
    }
}