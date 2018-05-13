using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Objects;

namespace TMS_Recycling
{
    public partial class WebUserControlStockUnitsOverview : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ButtonSearch_Click(sender, e);
            }
        }


        void Page_PreRender(object sender, EventArgs e)
        {
            if (WebUserControlStockUnitsBase1.RefreshRequired)
            {
                WebUserControlStockUnitsBase1.RefreshRequired = false;
                WebUserControlStockUnitsBase1.Visible = false;
                ButtonSearch_Click(sender, e);
            }

        }

        protected void GridViewResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            WebUserControlStockUnitsBase1.Visible = true;
            WebUserControlStockUnitsBase1.DataBind();
            WebUserControlStockUnitsBase1.KeyID = new System.Guid(GridViewResults.SelectedDataKey.Value.ToString());
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            EntityDataSourceGridBase.DefaultContainerName = EntityDataSourceGridBase.DefaultContainerName;

            EntityDataSourceGridBase.WhereParameters["Description"].DefaultValue = TextBoxFilterName.Text == "" ? "%" : "%" + TextBoxFilterName.Text + "%";
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

        protected void ButtonNew_Click(object sender, EventArgs e)
        {
            MaterialUnit NewMat = new MaterialUnit();
            ModelTMSContainer Temp = new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session);

            Temp.AddToMaterialUnitSet(NewMat);
            NewMat.Description = "Materiaaleenheid";

            Temp.SaveChanges(SaveOptions.DetectChangesBeforeSave);

            WebUserControlStockUnitsBase1.KeyID = NewMat.Id;
            WebUserControlStockUnitsBase1.Visible = true;
        }
    }
}