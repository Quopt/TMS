using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Objects;
using System.Data;

namespace TMS_Recycling
{
    public partial class WebUserControlRentVATOverview : ClassTMSUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ButtonSearch_Click(sender, e);

                // get the parent description
                ModelTMSContainer _ControlObjectContext = new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session);
                EntityKey TempKey = new EntityKey("ModelTMSContainer.RentalTypeSet", "Id", Guid.Parse(Request.Params["Id"]));
                RentalType TempObj = _ControlObjectContext.GetObjectByKey(TempKey) as RentalType;
                LabelObjectName.Text = TempObj.Description;
            }
        }

        void Page_PreRender(object sender, EventArgs e)
        {
            if (WebUserControlRentVATBase1.RefreshRequired)
            {
                WebUserControlRentVATBase1.RefreshRequired = false;
                WebUserControlRentVATBase1.Visible = false;
                ButtonSearch_Click(sender, e);
            }

        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            EntityDataSourceGridBase.DefaultContainerName = EntityDataSourceGridBase.DefaultContainerName;
            EntityDataSourceGridBase.CommandParameters["Description"].DefaultValue = TextBoxFilterName.Text == "" ? "%" : "%" + TextBoxFilterName.Text + "%";
            EntityDataSourceGridBase.CommandParameters["LocationDescription"].DefaultValue = ComboBoxLocationDescription.Text == "" ? "%" : "%" + ComboBoxLocationDescription.Text + "%";
            EntityDataSourceGridBase.CommandParameters["Id"].DefaultValue = Request.Params["Id"];
            if (CheckBoxFilterIsActive.Checked)
            {
                EntityDataSourceGridBase.CommandParameters["IsActive"].DefaultValue = "true";
            }
            else
            {
                EntityDataSourceGridBase.CommandParameters["IsActive"].DefaultValue = "false";
            }
            EntityDataSourceGridBase.DataBind();
        }

        protected void GridViewResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            WebUserControlRentVATBase1.Visible = true;
            WebUserControlRentVATBase1.DataBind();
            WebUserControlRentVATBase1.KeyID = new System.Guid(GridViewResults.SelectedDataKey.Value.ToString());
        }

        protected void ButtonNew_Click(object sender, EventArgs e)
        {
            RentalTypeVAT NewMat = new RentalTypeVAT();
            ModelTMSContainer Temp = new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session);

            Temp.AddToRentalTypeVATSet(NewMat);
            NewMat.Description = "BTW" ;

            EntityKey TempKey = new EntityKey("ModelTMSContainer.RentalTypeSet", "Id", Guid.Parse(Request.Params["Id"]));
            RentalType TempObj = Temp.GetObjectByKey(TempKey) as RentalType;
            NewMat.RentalType = TempObj;
            NewMat.IsActive = false;

            NewMat.Location = Temp.LocationSet.First<Location>();

            Temp.SaveChanges(SaveOptions.DetectChangesBeforeSave);

            WebUserControlRentVATBase1.KeyID = NewMat.Id;
            WebUserControlRentVATBase1.Visible = true;
        }

        public bool DetailVisible()
        {
            return WebUserControlRentVATBase1.Visible;
        }

        public Guid DetailKeyId()
        {
            return WebUserControlRentVATBase1.KeyID;
        }
    }
}