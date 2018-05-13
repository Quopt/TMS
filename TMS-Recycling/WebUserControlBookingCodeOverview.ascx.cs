using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Objects;

namespace TMS_Recycling
{
    public partial class WebUserControlBookingCodeOverview : ClassTMSUserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ButtonSearch_Click(sender, e);

                if (Request.Params["Debug"] != null)
                {
                    CheckBoxDebug.Visible = true;
                }
            }
        }

        void Page_PreRender(object sender, EventArgs e)
        {
            if (WebUserControlBookingCodeBase1.RefreshRequired)
            {
                WebUserControlBookingCodeBase1.RefreshRequired = false;
                WebUserControlBookingCodeBase1.Visible = false;
                ButtonSearch_Click(sender, e);
            }
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            EntityDataSourceGridBase.DefaultContainerName = EntityDataSourceGridBase.DefaultContainerName;
            EntityDataSourceGridBase.CommandParameters["Description"].DefaultValue = "%" + TextBoxFilterName.Text + "%";

            if (CheckBoxFilterIsActive.Checked)
            {
                EntityDataSourceGridBase.CommandParameters["IsActive"].DefaultValue = "true";
            }
            else
            {
                EntityDataSourceGridBase.CommandParameters["IsActive"].DefaultValue = "false";
            }

            if (CheckBoxDebug.Checked)
            {
                EntityDataSourceGridBase.CommandParameters["IsDebug"].DefaultValue = "true";
            }
            else
            {
                EntityDataSourceGridBase.CommandParameters["IsDebug"].DefaultValue = "false";
            }

            EntityDataSourceGridBase.DataBind();
        }

        protected void GridViewResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            WebUserControlBookingCodeBase1.Visible = true;
            WebUserControlBookingCodeBase1.DataBind();
            WebUserControlBookingCodeBase1.KeyID = new System.Guid(GridViewResults.SelectedDataKey.Value.ToString());
        }

        protected void ButtonNew_Click(object sender, EventArgs e)
        {
            LedgerBookingCode NewMat = new LedgerBookingCode();
            ModelTMSContainer Temp = new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session);

            NewMat.Description = "Boekingscode";

            Temp.AddToLedgerBookingCodeSet(NewMat);
            Temp.SaveChanges(SaveOptions.DetectChangesBeforeSave);

            WebUserControlBookingCodeBase1.KeyID = NewMat.Id;
            WebUserControlBookingCodeBase1.Visible = true;
        }

        public bool DetailVisible()
        {
            return WebUserControlBookingCodeBase1.Visible;
        }

        public Guid DetailKeyId()
        {
            return WebUserControlBookingCodeBase1.KeyID;
        }

    
    }
}