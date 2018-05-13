using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Common;

namespace TMS_Recycling
{
    public partial class WebUserControlSecurityRole : ClassTMSUserControl
    {

        void Page_Load(object sender, EventArgs e)
        {
            SetName = "SecurityRole";

            if (!IsPostBack)
            {
                LabelIDResultSet.Text = "";
            }
        }


        protected void GridViewRoleObjectAccess_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DbDataRecord sroa = e.Row.DataItem as DbDataRecord;

                e.Row.FindControl("CheckBoxCreate").Visible = sroa.GetValue(8).ToString().IndexOf("C") >= 0;
                e.Row.FindControl("CheckBoxRead").Visible = sroa.GetValue(8).ToString().IndexOf("R") >= 0;
                e.Row.FindControl("CheckBoxUpdate").Visible = sroa.GetValue(8).ToString().IndexOf("U") >= 0;
                e.Row.FindControl("CheckBoxDelete").Visible = sroa.GetValue(8).ToString().IndexOf("D") >= 0;
                e.Row.FindControl("CheckBoxExecute").Visible = sroa.GetValue(8).ToString().IndexOf("X") >= 0;

                /*
                (e.Row.FindControl("CheckBoxCreate") as CheckBox).Checked = Convert.ToBoolean(sroa.GetValue(3));
                (e.Row.FindControl("CheckBoxRead") as CheckBox).Checked  = Convert.ToBoolean( sroa.GetValue(4));
                (e.Row.FindControl("CheckBoxUpdate") as CheckBox).Checked  = Convert.ToBoolean(sroa.GetValue(5));
                (e.Row.FindControl("CheckBoxDelete") as CheckBox).Checked  = Convert.ToBoolean(sroa.GetValue(6));
                (e.Row.FindControl("CheckBoxExecute") as CheckBox).Checked = Convert.ToBoolean(sroa.GetValue(7));
                 * */

                LabelIDResultSet.Text = LabelIDResultSet.Text + sroa.GetValue(0).ToString() + ",";
            }
        }

        void Page_PreRender(object sender, EventArgs e)
        {
            if (DataItem != null)
            {
                SecurityRole sr = (DataItem as SecurityRole);
                if ((LabelRoleID.Text != sr.Id.ToString()) || (GridViewRoleObjectAccess.Rows.Count == 0) )
                {
                    // the role ID has changed. Check the SecurityRole
                    LabelIDResultSet.Text = ""; // clear the ID cache
                    sr.CheckSecurityRolObjectAccess(ControlObjectContext);
                    ControlObjectContext.SaveChanges();
                    LabelRoleID.Text = sr.Id.ToString();
                    GridViewRoleObjectAccess.DataBind();
                }
            }
        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            StandardButtonCancelClickHandler(sender, e);
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            // save the object access settings for this role
            if (GridViewRoleObjectAccess.Rows.Count != 0)
            {
                // get the IDs of the security objects
                String[] IDResultSet = LabelIDResultSet.Text.Split(',');
                int RowCounter = 0;

                // loop through all rows
                foreach (GridViewRow gvr in GridViewRoleObjectAccess.Rows)
                {
                    if (gvr.RowType == DataControlRowType.DataRow)
                    {
                        DbDataRecord dbdr = gvr.DataItem as DbDataRecord;
                        
                        // get the row id
                        //Guid RowID = new Guid(dbdr.GetValue(0).ToString());
                        Guid RowID = new Guid(IDResultSet[RowCounter]);
                        RowCounter ++;
                        
                        // update the row with the new settings
                        Boolean Create, Read, Update, Delete, Execute;
                        Create = (gvr.FindControl("CheckBoxCreate") as CheckBox).Checked ;
                        Read = (gvr.FindControl("CheckBoxRead") as CheckBox).Checked;
                        Update = (gvr.FindControl("CheckBoxUpdate") as CheckBox).Checked;
                        Delete = (gvr.FindControl("CheckBoxDelete") as CheckBox).Checked;
                        Execute = (gvr.FindControl("CheckBoxExecute") as CheckBox).Checked;

                        // locate the object access & update
                        SecurityRoleObjectAccess sroa = ControlObjectContext.GetObjectByKey(new System.Data.EntityKey("ModelTMSContainer.SecurityRoleObjectAccessSet", "Id", RowID)) as SecurityRoleObjectAccess;
                        sroa.HasCreateAccess = Create;
                        sroa.HasReadAccess = Read;
                        sroa.HasUpdateAccess = Update;
                        sroa.HasDeleteAccess = Delete;
                        sroa.HasExecuteAccess = Execute;
                    }
                }
            }

            StandardButtonSaveClickHandler(sender, e);
        }

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            StandardButtonDeleteClickHandler(sender, e);
        }


    }
}