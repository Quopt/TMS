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
    public partial class WebUserControlRentMaterialOverview : ClassTMSUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // get the relation description
                ModelTMSContainer _ControlObjectContext = new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session);
                EntityKey TempKey = new EntityKey("ModelTMSContainer.RentalTypeSet", "Id", Guid.Parse(Request.Params["Id"]));
                RentalType TempObj = _ControlObjectContext.GetObjectByKey(TempKey) as RentalType;
                LabelObjectName.Text = TempObj.Description;

                // load up the combo
                Common.AddRentalItemStateList(ComboBoxItemState.Items, false);

                ButtonSearch_Click(null, null);
            }
        }

        void Page_PreRender(object sender, EventArgs e)
        {
            if (WebUserControlRentMaterialBase1.RefreshRequired)
            {
                WebUserControlRentMaterialBase1.RefreshRequired = false;
                WebUserControlRentMaterialBase1.Visible = false;
                ButtonSearch_Click(sender, e);
            }

        }

        protected void GridViewResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            WebUserControlRentMaterialBase1.DataBind();
            WebUserControlRentMaterialBase1.KeyID = new System.Guid(GridViewResults.SelectedDataKey.Value.ToString());
            WebUserControlRentMaterialBase1.Visible = true;
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            EntityDataSourceRelation.CommandParameters["Description"].DefaultValue = TextBoxFilterName.Text == "" ? "%" : "%" + TextBoxFilterName.Text + "%";
            EntityDataSourceRelation.CommandParameters["LocationDescription"].DefaultValue = ComboBoxLocationDescription.Text == "" ? "%" : "%" + ComboBoxLocationDescription.Text + "%";
            EntityDataSourceRelation.CommandParameters["ItemState"].DefaultValue = ComboBoxItemState.SelectedValue == "" ? "%" : "%" + ComboBoxItemState.SelectedValue + "%";
            EntityDataSourceRelation.CommandParameters["Id"].DefaultValue = Request.Params["Id"];
            
            if (CheckBoxFilterIsActive.Checked)
            {
                EntityDataSourceRelation.CommandParameters["IsActive"].DefaultValue = "true";
            }
            else
            {
                EntityDataSourceRelation.CommandParameters["IsActive"].DefaultValue = "false";
            }

            EntityDataSourceRelation.DefaultContainerName = EntityDataSourceRelation.DefaultContainerName;
            EntityDataSourceRelation.DataBind();
        }

        protected void ButtonNew_Click(object sender, EventArgs e)
        {
            RentalItem NewObj = new RentalItem();
            ModelTMSContainer Temp = new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session);

            Temp.AddToRentalItemSet(NewObj);

            EntityKey TempKey = new EntityKey("ModelTMSContainer.RentalTypeSet", "Id", Guid.Parse(Request.Params["Id"]));
            RentalType TempObj = Temp.GetObjectByKey(TempKey) as RentalType;

            NewObj.RentalType = TempObj;
            NewObj.Location = Temp.LocationSet.First<Location>();
            NewObj.AssignRentalItemNumber(Temp);
            NewObj.Description = NewObj.RentalType.Description + " " + NewObj.ItemNumber;
            Temp.SaveChanges(SaveOptions.DetectChangesBeforeSave);

            WebUserControlRentMaterialBase1.KeyID = NewObj.Id;
            WebUserControlRentMaterialBase1.Visible = true;
        }

        protected void GridViewResults_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem != null)
            {
                Common.TranslateEnumValue(e.Row.Cells[3].Text, ComboBoxItemState.Items);
            }
        }




    }
}