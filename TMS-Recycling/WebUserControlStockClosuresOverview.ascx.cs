using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace TMS_Recycling
{
    public partial class WebUserControlStockClosuresOverview : ClassTMSUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CalendarControlStartDate.SelectedDate = Common.CurrentClientDate(Session).AddDays(-31);
                CalendarControlEndDate.SelectedDate = Common.CurrentClientDate(Session);
                ButtonSearch_Click(null, null);

                // check if we are actual with material closures
                MaterialSet.CheckMaterialClosures(ControlObjectContext, Page);

                // get the parent description
                ModelTMSContainer _ControlObjectContext = new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session);
                EntityKey TempKey = new EntityKey("ModelTMSContainer.MaterialSet", "Id", Guid.Parse(Request.Params["Id"]));
                Material TempObj = _ControlObjectContext.GetObjectByKey(TempKey) as Material;
                LabelObjectName.Text = TempObj.Description;
            }
        }

        void Page_PreRender(object sender, EventArgs e)
        {
            if (WebUserControlStockClosureBase1.RefreshRequired)
            {
                WebUserControlStockClosureBase1.RefreshRequired = false;
                WebUserControlStockClosureBase1.Visible = false;
                ButtonSearch_Click(sender, e);
            }
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            EntityDataSourceMaterialClosures.DefaultContainerName = EntityDataSourceMaterialClosures.DefaultContainerName;

            EntityDataSourceMaterialClosures.WhereParameters["StartDate"].DefaultValue = CalendarControlStartDate.SelectedDate.ToString();
            EntityDataSourceMaterialClosures.WhereParameters["EndDate"].DefaultValue = CalendarControlEndDate.SelectedDate.AddDays(1).ToString();
            EntityDataSourceMaterialClosures.WhereParameters["MaterialID"].DefaultValue = Request.Params["Id"];

            EntityDataSourceMaterialClosures.DataBind();

            //WebUserControlStockMutationsBase1.Visible = false;
        }

        protected void GridViewResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            WebUserControlStockClosureBase1.KeyID = new System.Guid(GridViewResults.SelectedDataKey.Value.ToString());
            WebUserControlStockClosureBase1.Visible = true;
        }
    }
}