using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TMS_Recycling
{
    public partial class WebUserControlRentLedgerOverview : ClassTMSUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CalendarControlStartDate.SelectedDate = new DateTime( Common.CurrentClientDateTime(Session).Year,1,1);
                CalendarControlEndDate.SelectedDate = Common.CurrentClientDate(Session);
                Common.AddRentLedgerStatusList(ComboBoxInvoiceStatus.Items, false);
                ButtonSearch_Click(null, null);
            }
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            EntityDataSourceLedgerBase.DefaultContainerName = EntityDataSourceLedgerBase.DefaultContainerName;

            EntityDataSourceLedgerBase.CommandParameters["Description"].DefaultValue = TextBoxFilterName.Text == "" ? "%" : "%" + TextBoxFilterName.Text + "%";
            EntityDataSourceLedgerBase.CommandParameters["LocationDescription"].DefaultValue = ComboBoxLocationDescription.Text == "" ? "%" : "%" + ComboBoxLocationDescription.Text + "%";
            EntityDataSourceLedgerBase.CommandParameters["StartDate"].DefaultValue = CalendarControlStartDate.SelectedDate.ToString();
            EntityDataSourceLedgerBase.CommandParameters["EndDate"].DefaultValue = CalendarControlEndDate.SelectedDate.AddDays(1).ToString();

            EntityDataSourceLedgerBase.CommandParameters["RentNumber"].DefaultValue = TextBoxOrderNo.Text == "" ? "%" : "%" + TextBoxOrderNo.Text + "%";
            EntityDataSourceLedgerBase.CommandParameters["MaterialType"].DefaultValue = TextBoxFilterMaterialType.Text == "" ? "%" : "%" + TextBoxFilterMaterialType.Text + "%";
            EntityDataSourceLedgerBase.CommandParameters["Relation"].DefaultValue = TextBoxCustomer.Text == "" ? "%" : "%" + TextBoxCustomer.Text + "%";
            EntityDataSourceLedgerBase.CommandParameters["Material"].DefaultValue = TextBoxFilterName.Text == "" ? "%" : "%" + TextBoxFilterName.Text + "%";
            EntityDataSourceLedgerBase.CommandParameters["InvoiceStatus"].DefaultValue = ComboBoxInvoiceStatus.SelectedValue == "" ? "%" : "%" + ComboBoxInvoiceStatus.SelectedValue + "%";
            
            EntityDataSourceLedgerBase.DataBind();

            WebUserControlRentLedgerBase1.Visible = false;
        }

        void Page_PreRender(object sender, EventArgs e)
        {
            if (WebUserControlRentLedgerBase1.RefreshRequired)
            {
                WebUserControlRentLedgerBase1.RefreshRequired = false;
                WebUserControlRentLedgerBase1.Visible = false;
                ButtonSearch_Click(sender, e);
            }
        }
        
        protected void GridViewRentLedger_SelectedIndexChanged(object sender, EventArgs e)
        {
            WebUserControlRentLedgerBase1.KeyID = new System.Guid(GridViewRentLedger.SelectedDataKey.Value.ToString());
            WebUserControlRentLedgerBase1.Visible = true;
        }
    }
}