using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Objects;

namespace TMS_Recycling
{
    public partial class WebUserControlBookKeepingOverview : ClassTMSUserControl
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
            if (WebUserControlLedgerBase1.RefreshRequired)
            {
                WebUserControlLedgerBase1.RefreshRequired = false;
                WebUserControlLedgerBase1.Visible = false;
                ButtonSearch_Click(sender, e);
            }
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            EntityDataSourceGridBase.DefaultContainerName = EntityDataSourceGridBase.DefaultContainerName;
            EntityDataSourceGridBase.CommandParameters["Description"].DefaultValue = "%" + TextBoxFilterName.Text + "%";
            EntityDataSourceGridBase.CommandParameters["LocationDescription"].DefaultValue = ComboBoxLocationDescription.Text == "" ? "%" : "%" + ComboBoxLocationDescription.Text + "%";

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
            WebUserControlLedgerBase1.DataBind();
            WebUserControlLedgerBase1.SetNewKeyID( new System.Guid(GridViewResults.SelectedDataKey.Value.ToString()));
            WebUserControlLedgerBase1.Visible = true;
        }

        protected void ButtonNew_Click(object sender, EventArgs e)
        {
            Ledger NewMat = new Ledger();
            ModelTMSContainer Temp = new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session);

            //NewMat.LimitToLocation = Temp.LocationSet.First();

            Temp.AddToLedgerSet(NewMat);
            Temp.SaveChanges(SaveOptions.DetectChangesBeforeSave);

            WebUserControlLedgerBase1.SetNewKeyID( NewMat.Id);
            WebUserControlLedgerBase1.Visible = true;
        }

        public bool DetailVisible()
        {
            return WebUserControlLedgerBase1.Visible;
        }

        public Guid DetailKeyId()
        {
            return WebUserControlLedgerBase1.KeyID;
        }

    }
}