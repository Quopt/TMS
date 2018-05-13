using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TMS_Recycling
{
    public partial class WebUserControlCustomerReportProjectRevenue : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CalendarControlStartPeriod.SelectedDate = Common.CurrentClientDate(Session).AddDays(-31);
                CalendarControlEndPeriod.SelectedDate = Common.CurrentClientDate(Session);
                RadioButtonListShowCat_SelectedIndexChanged(null, null);
            }
        }

        protected void ButtonShowReport_Click(object sender, EventArgs e)
        {
            string CustomerId, InvoiceType, LocationName, OrderStatus, InvoiceStatus, StartDate, EndDate,
                ReportName, DataSetName, URL, ProjectName, NoInvoices, NoOrders;

            StartDate = CalendarControlStartPeriod.SelectedDate.ToString();
            EndDate = CalendarControlEndPeriod.SelectedDate.ToString();
            if (BulletedListDateSelection.SelectedValue == "Today")
            {
                StartDate = Common.CurrentClientDate(Session).ToString();
                EndDate = Common.CurrentClientDate(Session).ToString();
            }
            if (BulletedListDateSelection.SelectedValue == "Yesterday")
            {
                StartDate = Common.CurrentClientDate(Session).AddDays(-1).ToString();
                EndDate = Common.CurrentClientDate(Session).AddDays(-1).ToString();
            }
            if (BulletedListDateSelection.SelectedValue == "ThisMonth")
            {
                DateTime BaseDate = new DateTime(Common.CurrentClientDate(Session).Year, Common.CurrentClientDate(Session).Month, 1);
                StartDate = BaseDate.ToString();
                EndDate = BaseDate.AddMonths(1).AddDays(-1).ToString();
            }
            if (BulletedListDateSelection.SelectedValue == "PreviousMonth")
            {
                DateTime BaseDate = new DateTime(Common.CurrentClientDate(Session).Year, Common.CurrentClientDate(Session).Month, 1);
                StartDate = BaseDate.AddMonths(-1).ToString();
                EndDate = BaseDate.AddDays(-1).ToString();
            }
            if (BulletedListDateSelection.SelectedValue == "ThisYear")
            {
                StartDate = new DateTime(Common.CurrentClientDate(Session).Year, 1, 1).ToString();
                EndDate = new DateTime(Common.CurrentClientDate(Session).Year, 12, 31).ToString();
            }
            if (BulletedListDateSelection.SelectedValue == "All")
            {
                StartDate = new DateTime(2000, 1, 1).ToString();
                EndDate = new DateTime(2100, 1, 1).ToString();
            }


            CustomerId = "";
            if (RadioButtonListCustomerSelection.SelectedValue == "Select")
            {
                CustomerId = ComboBoxCustomerSelection.SelectedValue;
            }

            InvoiceType = RadioButtonListTradeType.SelectedValue;

            LocationName = ComboBoxSelectedLocation.Text;

            OrderStatus = "";
            InvoiceStatus = "";
            NoInvoices = "F";
            NoOrders = "F";
            if (RadioButtonListOrderStatus.SelectedValue == "ToBeInvoiced")
            {
                OrderStatus = "Open";
                InvoiceStatus = "";
                NoInvoices = "T";
            }
            if (RadioButtonListOrderStatus.SelectedValue == "OpenInvoices")
            {
                OrderStatus = "";
                InvoiceStatus = "Open";
            }
            if (RadioButtonListOrderStatus.SelectedValue == "ClosedInvoices")
            {
                OrderStatus = "";
                InvoiceStatus = "Paid";
            }
            if (RadioButtonListOrderStatus.SelectedValue == "AllInvoices")
            {
                OrderStatus = "";
                InvoiceStatus = "";
                NoOrders = "T";
            }

            ProjectName = "";
            if (RadioButtonListShowCat.SelectedValue == "Specific")
            {
                ProjectName = ComboBoxCustomerProjectSelection.SelectedValue;
            }

            ReportName = "ReportRelationProjectTotalsPerMaterial";
            DataSetName = "DataSetRelationRevenue";

            // load up the iframe 
            URL = "WebFormPopup.aspx?UC=ShowReport&d=" + DataSetName +
                 "&r=" + ReportName +
                 "&CustomerId=" + CustomerId +
                 "&LocationName=" + LocationName +
                 "&InvoiceType=" + InvoiceType +
                 "&InvoiceStatus=" + InvoiceStatus +
                 "&ProjectName="+ ProjectName +
                 "&OrderStatus=" + OrderStatus +
                 "&NoInvoices=" + NoInvoices +
                 "&NoOrders=" + NoOrders +
                 "&StartDate=" + StartDate +
                 "&EndDate=" + EndDate;
            LabelURL.Text = URL;
            FrameShowReport.Attributes["src"] = URL;
        }

        protected void RadioButtonListShowCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            // by default do not show the same report again, the user wants to see something else
            FrameShowReport.Attributes["src"] = "";
        }

        protected void RadioButtonListCustomerSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadioButtonListShowCat_SelectedIndexChanged(null, null); // disable the current report

            RadioButtonListShowCat.Enabled = RadioButtonListCustomerSelection.SelectedValue == "Select";
            ComboBoxCustomerProjectSelection.Enabled = RadioButtonListCustomerSelection.SelectedValue == "Select";
            if (!RadioButtonListShowCat.Enabled)
            {
                RadioButtonListShowCat.SelectedValue = "All";
            }
        }
    }
}