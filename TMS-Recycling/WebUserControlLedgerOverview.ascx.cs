using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TMS_Recycling
{
    public partial class WebUserControlLedgerOverview : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // set the correct filters
                CalendarControlStartDate.SelectedDate = new DateTime(Common.CurrentClientDateTime(Session).Year, 1, 1);
                CalendarControlEndDate.SelectedDate = Common.CurrentClientDateTime(Session).Date;

                ButtonSearch_Click(sender, e);

                if (Request.Params["OrderNumber"] != null)
                {
                    TextBoxOrderNo.Text = Request.Params["OrderNumber"];
                    EntityDataSourcePurchaseLedger.CommandParameters["OrderNumber"].DefaultValue = TextBoxOrderNo.Text;
                    EntityDataSourcePurchaseLedger.DataBind();

                    if (GridViewSelectedPurchases.Rows.Count == 1)
                    {
                        GridViewSelectedPurchases.SelectedIndex = 0;
                        GridViewSelectedPurchases_SelectedIndexChanged(null, null);
                    }
                }
            }
        }


        void Page_PreRender(object sender, EventArgs e)
        {
            if (WebUserControlOrderBase1.RefreshRequired)
            {
                WebUserControlOrderBase1.RefreshRequired = false;
                WebUserControlOrderBase1.Visible = false;
                ButtonSearch_Click(sender, e);
            }
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            EntityDataSourcePurchaseLedger.DefaultContainerName = EntityDataSourcePurchaseLedger.DefaultContainerName;

            EntityDataSourcePurchaseLedger.CommandParameters["Description"].DefaultValue = TextBoxFilterName.Text == "" ? "%" : "%" + TextBoxFilterName.Text + "%";
            EntityDataSourcePurchaseLedger.CommandParameters["StartDate"].DefaultValue = CalendarControlStartDate.SelectedDate.ToString();
            EntityDataSourcePurchaseLedger.CommandParameters["EndDate"].DefaultValue = CalendarControlEndDate.SelectedDate.AddDays(1).ToString();

            EntityDataSourcePurchaseLedger.CommandParameters["OrderNumber"].DefaultValue = TextBoxOrderNo.Text == "" ? "%" : "%" + TextBoxOrderNo.Text + "%";
            EntityDataSourcePurchaseLedger.CommandParameters["OrderStatus"].DefaultValue = DropDownListBookingType.SelectedValue == "" ? "%" : DropDownListBookingType.SelectedValue;
            EntityDataSourcePurchaseLedger.CommandParameters["InvoiceStatus"].DefaultValue = DropDownListInvoiced.SelectedValue == "" ? "%" : DropDownListInvoiced.SelectedValue;
            EntityDataSourcePurchaseLedger.CommandParameters["MaterialDescription"].DefaultValue = DropDownListMaterial.SelectedValue == "" ? "%" : DropDownListMaterial.SelectedValue;
            EntityDataSourcePurchaseLedger.CommandParameters["CorrectedStatus"].DefaultValue = DropDownListCorrected.SelectedValue == "" ? "%" : DropDownListCorrected.SelectedValue;
            EntityDataSourcePurchaseLedger.CommandParameters["EmployeeName"].DefaultValue = TextBoxUserName.Text == "" ? "%" : "%" + TextBoxUserName.Text + "%";
            EntityDataSourcePurchaseLedger.CommandParameters["RelationDescription"].DefaultValue = TextBoxCustomer.Text == "" ? "%" : "%" + TextBoxCustomer.Text + "%";
            EntityDataSourcePurchaseLedger.CommandParameters["IDDelivery"].DefaultValue = TextBoxDriverOrPlate.Text == "" ? "%" : "%" + TextBoxDriverOrPlate.Text + "%";
            EntityDataSourcePurchaseLedger.CommandParameters["LocationDescription"].DefaultValue = ComboBoxLocationSelection.Text == "" ? "%" : "%" + ComboBoxLocationSelection.Text + "%";

            EntityDataSourcePurchaseLedger.DataBind();

            WebUserControlOrderBase1.Visible = false;
        }

        protected void GridViewSelectedPurchases_SelectedIndexChanged(object sender, EventArgs e)
        {
            WebUserControlOrderBase1.InitUserControl();
            WebUserControlOrderBase1.OrderType = InvoiceType.Buy;
            WebUserControlOrderBase1.KeyID = new System.Guid(GridViewSelectedPurchases.SelectedDataKey.Value.ToString());
            WebUserControlOrderBase1.Visible = true;
        }
 
        public void SwitchPurchaseType(InvoiceType it)
        {
            switch (it)
            {
                case InvoiceType.Buy:
                    LabelInvoiceType.Text = "Buy";
                    break;
                case InvoiceType.Sell:
                    LabelInvoiceType.Text = "Sell";
                    break;
            }
            WebUserControlOrderBase1.SwitchPurchaseType(it);
        }

    }
}