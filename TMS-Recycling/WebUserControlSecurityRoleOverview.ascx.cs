using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Objects;

namespace TMS_Recycling
{
    public partial class WebUserControlSecurityRoleOverview : ClassTMSUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ButtonSearch_Click(sender, e);
        }

        void Page_PreRender(object sender, EventArgs e)
        {
            if (WebUserControlSecurityRole1.RefreshRequired)
            {
                WebUserControlSecurityRole1.RefreshRequired = false;
                WebUserControlSecurityRole1.Visible = false;
                ButtonSearch_Click(sender, e);
            }
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            EntityDataSourceGridBase.DefaultContainerName = EntityDataSourceGridBase.DefaultContainerName;
            EntityDataSourceGridBase.WhereParameters["Description"].DefaultValue = "%" + TextBoxFilterName.Text + "%";
            if (CheckBoxFilterIsActive.Checked)
            {
                EntityDataSourceGridBase.WhereParameters["IsActive"].DefaultValue = "true";
            }
            else
            {
                EntityDataSourceGridBase.WhereParameters["IsActive"].DefaultValue = "false";
            }
            EntityDataSourceGridBase.DataBind();
        }

        protected void GridViewResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            WebUserControlSecurityRole1.KeyID = new System.Guid(GridViewResults.SelectedDataKey.Value.ToString());
            WebUserControlSecurityRole1.Visible = true;
            WebUserControlSecurityRole1.DataBind();
        }

        protected void ButtonNew_Click(object sender, EventArgs e)
        {
            SecurityRole NewMat = new SecurityRole();
            ModelTMSContainer Temp = new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session);

            NewMat.Description = "Rol " + Common.CurrentClientDateTime(Session).ToString();
            NewMat.CopyObjectAccessFromTemplateRole(Temp);

            Temp.AddToSecurityRoleSet(NewMat);
            Temp.SaveChanges(SaveOptions.DetectChangesBeforeSave);

            WebUserControlSecurityRole1.KeyID = NewMat.Id;
            WebUserControlSecurityRole1.Visible = true;
        }

        public bool DetailVisible()
        {
            return WebUserControlSecurityRole1.Visible;
        }

        public Guid DetailKeyId()
        {
            return WebUserControlSecurityRole1.KeyID;
        }
    }
}