using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Objects;

namespace TMS_Recycling
{
    public partial class WebFormSetting : ClassTMSWebPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ButtonSearch_Click(null, null);
            }
        }

        protected void GridViewResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            WebUserControlStaffMember1.DataBind();
            WebUserControlStaffMember1.KeyID = new System.Guid(GridViewResults.SelectedDataKey.Value.ToString());
            WebUserControlStaffMember1.Visible = true;
            WebUserControlStaffMember1.LoadUserRoles();
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            EntityDataSourceStaffMember.CommandParameters["Description"].DefaultValue = "%" + TextBoxFilterName.Text + "%";
            EntityDataSourceStaffMember.CommandParameters["LocationDescription"].DefaultValue = ComboBoxLocation1.Text == "" ? "%" :  ComboBoxLocation1.Text ;
            if (CheckBoxFilterIsActive.Checked)
            {
                EntityDataSourceStaffMember.CommandParameters["IsActive"].DefaultValue = "true";
            }
            else
            {
                EntityDataSourceStaffMember.CommandParameters["IsActive"].DefaultValue = "false";
            }

            EntityDataSourceStaffMember.DefaultContainerName = EntityDataSourceStaffMember.DefaultContainerName;
            EntityDataSourceStaffMember.DataBind();
        }

        protected void ButtonNew_Click(object sender, EventArgs e)
        {
            StaffMember NewObj = new StaffMember();
            ModelTMSContainer Temp = new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session);

            Temp.AddToStaffMemberSet(NewObj);
            NewObj.HomeLocation = Temp.LocationSet.First();
            NewObj.LimitAccessToThisLocation = null;
            Temp.SaveChanges(SaveOptions.DetectChangesBeforeSave);

            WebUserControlStaffMember1.KeyID = NewObj.Id;
            WebUserControlStaffMember1.Visible = true;
        }
    }
}