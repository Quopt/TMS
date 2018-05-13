using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TMS_Recycling
{
    public partial class WebUserControlLedgerReportMaterialMovement : System.Web.UI.UserControl
    {
        public OrderType OrderTypeToShow
        {
            get
            {
                switch (LabelOrderType.Text)
                {
                    case "Buy": 
                        return OrderType.Buy;
                    case "Sell": 
                        return OrderType.Sell;
                    default :
                        return OrderType.Both;
                }

            }
            set
            {
                switch (value)
                {
                    case OrderType.Buy:
                        LabelOrderType.Text = "Buy";
                        break;
                    case OrderType.Sell:
                        LabelOrderType.Text = "Sell";
                        break;
                    case OrderType.Both:
                        LabelOrderType.Text = "";
                        break;
                }
            }
        }

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
            string CustomerId, LocationName, StartDate, EndDate, ReportName, DataSetName, URL;

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

            LocationName = ComboBoxSelectedLocation.Text;

            CustomerId = "";
            if (RadioButtonListCustomerSelection.SelectedValue == "Select")
            {
                CustomerId = ComboBoxCustomerSelection.SelectedValue;
            }

            ReportName = "ReportMaterialMovement";
            DataSetName = "DataSetMaterialMovement";

            // load up the iframe 
            URL = "WebFormPopup.aspx?UC=ShowReport&d=" + DataSetName +
                 "&r=" + ReportName +
                 "&CustomerId=" + CustomerId +
                 "&LocationName=" + LocationName +
                 "&OrderType=" + LabelOrderType.Text +
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
    }
}