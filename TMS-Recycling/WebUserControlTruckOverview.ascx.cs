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
    public partial class WebUserControlTruckOverview : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ButtonSearch_Click(null, null);
            }
        }

        void Page_PreRender(object sender, EventArgs e)
        {
            if (WebUserControlTruckBase1.RefreshRequired)
            {
                WebUserControlTruckBase1.RefreshRequired = false;
                WebUserControlTruckBase1.Visible = false;
                ButtonSearch_Click(sender, e);
            }

        }

        protected void GridViewResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            WebUserControlTruckBase1.DataBind();
            WebUserControlTruckBase1.KeyID = new System.Guid(GridViewResults.SelectedDataKey.Value.ToString());
            WebUserControlTruckBase1.Visible = true;
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            EntityDataSourceTrucks.CommandParameters["HomeTruckLocation"].DefaultValue = ComboBoxHomeLocation.Text == "" ? "%" : "%" + ComboBoxHomeLocation.Text + "%";

            EntityDataSourceTrucks.DefaultContainerName = EntityDataSourceTrucks.DefaultContainerName;
            EntityDataSourceTrucks.DataBind();
        }

        protected void ButtonNew_Click(object sender, EventArgs e)
        {
            Truck NewObj = new Truck();
            ModelTMSContainer Temp = new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session);


            NewObj.CurrentTruckLocation = Temp.LocationSet.First<Location>();
            NewObj.HomeTruckLocation = NewObj.CurrentTruckLocation;

            Temp.SaveChanges(SaveOptions.DetectChangesBeforeSave);

            WebUserControlTruckBase1.KeyID = NewObj.Id;
            WebUserControlTruckBase1.Visible = true;

        }
    }
}