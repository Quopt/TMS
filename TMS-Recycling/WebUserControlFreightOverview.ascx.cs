using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TMS_Recycling
{
    public partial class WebUserControlFreightOverview : ClassTMSUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // set the correct filters & fill the combos
                Common.AddFreightStatusList(DropDownListFreightStatus.Items, false);
                Common.AddFreightTypeList(DropDownListFreightType.Items, false);
                CalendarControlStartDate.SelectedDate = new DateTime(Common.CurrentClientDateTime(Session).Year, 1, 1);
                CalendarControlEndDate.SelectedDate = DateTime.Now.Date;

                ButtonSearch_Click(sender, e);

                if (Request.Params["FreightNumber"] != null)
                {
                    TextBoxFreightNo.Text = Request.Params["FreightNumber"];
                    EntityDataSourceFreights.CommandParameters["FreightNumber"].DefaultValue = TextBoxFreightNo.Text;
                    EntityDataSourceFreights.DataBind();

                    if (GridViewSelectedFreights.Rows.Count == 1)
                    {
                        GridViewSelectedFreights.SelectedIndex = 0;
                        GridViewSelectedFreights_SelectedIndexChanged(null, null);
                    }
                }
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (WebUserControlFreightBase1.RefreshRequired)
            {
                WebUserControlFreightBase1.RefreshRequired = false;
                WebUserControlFreightBase1.Visible = false;
                ButtonSearch_Click(sender, e);
            }
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            EntityDataSourceFreights.DefaultContainerName = EntityDataSourceFreights.DefaultContainerName;

            EntityDataSourceFreights.CommandParameters["Description"].DefaultValue = TextBoxFilterName.Text == "" ? "%" : "%" + TextBoxFilterName.Text + "%";
            EntityDataSourceFreights.CommandParameters["StartDate"].DefaultValue = CalendarControlStartDate.SelectedDate.ToString();
            EntityDataSourceFreights.CommandParameters["EndDate"].DefaultValue = CalendarControlEndDate.SelectedDate.AddDays(1).ToString();

            EntityDataSourceFreights.CommandParameters["FreightNumber"].DefaultValue = TextBoxFreightNo.Text == "" ? "%" : "%" + TextBoxFreightNo.Text + "%";
            EntityDataSourceFreights.CommandParameters["FreightStatus"].DefaultValue = DropDownListFreightStatus.SelectedValue == "" ? "%" : DropDownListFreightStatus.SelectedValue;
            EntityDataSourceFreights.CommandParameters["FreightType"].DefaultValue = DropDownListFreightType.SelectedValue == "" ? "%" : DropDownListFreightType.SelectedValue;
            EntityDataSourceFreights.CommandParameters["ActiveStatus"].DefaultValue = DropDownListCorrected.SelectedValue == "" ? "%" : DropDownListCorrected.SelectedValue;
            EntityDataSourceFreights.CommandParameters["RelationDescription"].DefaultValue = TextBoxCustomer.Text == "" ? "%" : "%" + TextBoxCustomer.Text + "%";
            EntityDataSourceFreights.CommandParameters["LocationDescription"].DefaultValue = TextBoxLocation.Text == "" ? "%" : "%" + TextBoxLocation.Text + "%";

            EntityDataSourceFreights.DataBind();

            WebUserControlFreightBase1.Visible = false;
        }

        protected void GridViewSelectedFreights_SelectedIndexChanged(object sender, EventArgs e)
        {
            WebUserControlFreightBase1.KeyID = new System.Guid(GridViewSelectedFreights.SelectedDataKey.Value.ToString());
            WebUserControlFreightBase1.InitUserControl();
            WebUserControlFreightBase1.Visible = true;
            WebUserControlFreightBase1.DataBind();
        }
    }
}