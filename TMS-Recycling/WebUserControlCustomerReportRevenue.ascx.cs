using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TMS_Recycling
{
    public partial class WebUserControlCustomerReportRevenue : System.Web.UI.UserControl
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
            string CustomerId, InvoiceType, LocationName, OrderStatus, InvoiceStatus, StartDate, EndDate, ReportName, DataSetName, URL;

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
                StartDate = new DateTime(Common.CurrentClientDate(Session).Year , 1, 1).ToString();
                EndDate = new DateTime(Common.CurrentClientDate(Session).Year, 12, 31).ToString();
            }
            if (BulletedListDateSelection.SelectedValue == "All")
            {
                StartDate = new DateTime(2000,1,1).ToString();
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
            if (RadioButtonListOrderStatus.SelectedValue == "ToBeInvoiced")
            {
                OrderStatus = "Open";
                InvoiceStatus = "NoneXXX";
            }
            if (RadioButtonListOrderStatus.SelectedValue == "OpenInvoices")
            {
                OrderStatus = "NoneXXX";
                InvoiceStatus = "Open";
            }
            if (RadioButtonListOrderStatus.SelectedValue == "ClosedInvoices")
            {
                OrderStatus = "NoneXXX";
                InvoiceStatus = "Paid";
            }

            ReportName = "ReportRelationRevenueTotals";
            DataSetName = "DataSetRelationRevenue";
            switch (RadioButtonListShowCat.SelectedValue)
            {
                case "Totals" : 
                        ReportName = "ReportRelationRevenueTotals";
                        DataSetName = "DataSetRelationRevenue";
                        break;
                case "MatCat" : 
                        ReportName = "ReportRelationMaterialCategory";
                        DataSetName = "DataSetRelationRevenue";
                        break;
                case "UserCat" : 
                        ReportName = "ReportRelationOwnCategory";
                        DataSetName = "DataSetRelationRevenue";
                        break;
                case "Material" :
                        ReportName = "ReportRelationMaterial";
                        DataSetName = "DataSetRelationRevenue";
                        break;
                case "InvoiceDetails" :
                        ReportName = "ReportRelationInvoiceDetails";
                        DataSetName = "DataSetRelationRevenue";
                        break;
                case "TradeDetails":
                        ReportName = "ReportRelationTradeDetails";
                        DataSetName = "DataSetRelationRevenue";
                        break;
            }

            // load up the iframe 
            URL = "WebFormPopup.aspx?UC=ShowReport&d=" + DataSetName +
                 "&r=" + ReportName +
                 "&CustomerId=" + CustomerId +
                 "&LocationName=" + LocationName +
                 "&InvoiceType=" + InvoiceType +
                 "&ProjectName=" +
                 "&InvoiceStatus=" + InvoiceStatus +
                 "&OrderStatus=" + OrderStatus +
                 "&StartDate=" + StartDate +
                 "&EndDate=" + EndDate;
            LabelURL.Text = URL;
            FrameShowReport.Attributes["src"] = URL;
        }

        protected void RadioButtonListShowCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            // enable the order status depending on what is selected
            RadioButtonListOrderStatus.Items[2].Enabled = ((RadioButtonListShowCat.SelectedIndex == 0) || (RadioButtonListShowCat.SelectedIndex == 4));
            RadioButtonListOrderStatus.Items[3].Enabled = ((RadioButtonListShowCat.SelectedIndex == 0) || (RadioButtonListShowCat.SelectedIndex == 4));

            // set correct selection if an old selection was set
            if ( ((RadioButtonListOrderStatus.Items[2].Enabled) && (RadioButtonListOrderStatus.Items[2].Selected)) ||
                 ((RadioButtonListOrderStatus.Items[3].Enabled) && (RadioButtonListOrderStatus.Items[3].Selected)) )
            {
                RadioButtonListOrderStatus.SelectedIndex = 0;
            }

            // by default do not show the same report again, the user wants to see something else
            FrameShowReport.Attributes["src"] = "";
        }
    }
}