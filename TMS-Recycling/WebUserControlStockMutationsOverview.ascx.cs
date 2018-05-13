using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace TMS_Recycling
{
    public partial class WebUserControlStockMutationsOverview : ClassTMSUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // set the correct filters
                CalendarControlStartDate.SelectedDate = new DateTime(Common.CurrentClientDateTime(Session).Year, 1, 1);
                CalendarControlEndDate.SelectedDate = Common.CurrentClientDateTime(Session).Date;

                ButtonSearch_Click(sender, e);

                if (Request.Params["MutationNumber"] != null)
                {
                    TextBoxOrderNo.Text = Request.Params["MutationNumber"];
                    EntityDataSourceStockMutationLedger.CommandParameters["MutationNumber"].DefaultValue = TextBoxOrderNo.Text;
                    EntityDataSourceStockMutationLedger.DataBind();

                    if (GridViewSelectedMutations.Rows.Count == 1)
                    {
                        GridViewSelectedMutations.SelectedIndex = 0;
                        GridViewSelectedMutations_SelectedIndexChanged(null, null);
                    }
                }

                if (Request.Params["Id"] != null)
                {
                    // load material to show in the header
                    Material Mat = ControlObjectContext.GetObjectByKey( new EntityKey("ModelTMSContainer.MaterialSet", "Id", new Guid(Request.Params["Id"]) ) ) as Material;
                    LabelObjectName.Text = Mat.Description;
                }

            }
        }


        void Page_PreRender(object sender, EventArgs e)
        {
            if (WebUserControlStockMutationsBase1.RefreshRequired)
            {
                WebUserControlStockMutationsBase1.RefreshRequired = false;
                WebUserControlStockMutationsBase1.Visible = false;
                ButtonSearch_Click(sender, e);
            }
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            EntityDataSourceStockMutationLedger.DefaultContainerName = EntityDataSourceStockMutationLedger.DefaultContainerName;

            EntityDataSourceStockMutationLedger.CommandParameters["Id"].DefaultValue = Guid.Empty.ToString();
            if (Request.Params["Id"] != null)
            {
                EntityDataSourceStockMutationLedger.CommandParameters["Id"].DefaultValue = Request.Params["Id"];
            }

            EntityDataSourceStockMutationLedger.CommandParameters["Description"].DefaultValue = TextBoxFilterName.Text == "" ? "%" : "%" + TextBoxFilterName.Text + "%";
            EntityDataSourceStockMutationLedger.CommandParameters["StartDate"].DefaultValue = CalendarControlStartDate.SelectedDate.ToString();
            EntityDataSourceStockMutationLedger.CommandParameters["EndDate"].DefaultValue = CalendarControlEndDate.SelectedDate.AddDays(1).ToString();

            EntityDataSourceStockMutationLedger.CommandParameters["MutationNumber"].DefaultValue = TextBoxOrderNo.Text == "" ? "%" : "%" + TextBoxOrderNo.Text + "%";
            EntityDataSourceStockMutationLedger.CommandParameters["MutationType"].DefaultValue = DropDownListBookingType.SelectedValue == "" ? "%" : DropDownListBookingType.SelectedValue;

            EntityDataSourceStockMutationLedger.CommandParameters["CorrectedStatus"].DefaultValue = DropDownListCorrected.SelectedValue == "" ? "%" : DropDownListCorrected.SelectedValue;
            EntityDataSourceStockMutationLedger.CommandParameters["EmployeeName"].DefaultValue = TextBoxUserName.Text == "" ? "%" : "%" + TextBoxUserName.Text + "%";
            EntityDataSourceStockMutationLedger.CommandParameters["RelationDescription"].DefaultValue = TextBoxCustomer.Text == "" ? "%" : "%" + TextBoxCustomer.Text + "%";
            EntityDataSourceStockMutationLedger.CommandParameters["LocationDescription"].DefaultValue = ComboBoxLocationSelection.Text == "" ? "%" : "%" + ComboBoxLocationSelection.Text + "%";

            EntityDataSourceStockMutationLedger.DataBind();

            WebUserControlStockMutationsBase1.Visible = false;
        }

        protected void GridViewSelectedMutations_SelectedIndexChanged(object sender, EventArgs e)
        {
            WebUserControlStockMutationsBase1.KeyID = new System.Guid(GridViewSelectedMutations.SelectedDataKey.Value.ToString());
            WebUserControlStockMutationsBase1.Visible = true;
        }
 

    }
}