using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TMS_Recycling
{
    public partial class WebUserControlLedgerReportComparison : System.Web.UI.UserControl
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
            string CustomerId, LocationName, ReportName, DataSetName, URL;
            DateTime StartDate, EndDate, StartDate1, EndDate1, StartDate2, EndDate2, StartDate3, EndDate3;
            

            StartDate = CalendarControlStartPeriod.SelectedDate;
            EndDate = CalendarControlEndPeriod.SelectedDate;
            if (BulletedListDateSelection.SelectedValue == "Today")
            {
                StartDate = Common.CurrentClientDate(Session);
                EndDate = Common.CurrentClientDate(Session);
            }
            if (BulletedListDateSelection.SelectedValue == "Yesterday")
            {
                StartDate = Common.CurrentClientDate(Session).AddDays(-1);
                EndDate = Common.CurrentClientDate(Session).AddDays(-1);
            }
            if (BulletedListDateSelection.SelectedValue == "ThisMonth")
            {
                DateTime BaseDate = new DateTime(Common.CurrentClientDate(Session).Year, Common.CurrentClientDate(Session).Month, 1);
                StartDate = BaseDate;
                EndDate = BaseDate.AddMonths(1).AddDays(-1);
            }
            if (BulletedListDateSelection.SelectedValue == "PreviousMonth")
            {
                DateTime BaseDate = new DateTime(Common.CurrentClientDate(Session).Year, Common.CurrentClientDate(Session).Month, 1);
                StartDate = BaseDate.AddMonths(-1);
                EndDate = BaseDate.AddDays(-1);
            }
            if (BulletedListDateSelection.SelectedValue == "ThisYear")
            {
                StartDate = new DateTime(Common.CurrentClientDate(Session).Year, 1, 1);
                EndDate = Common.CurrentClientDate(Session);
            }
            if (BulletedListDateSelection.SelectedValue == "All")
            {
                StartDate = new DateTime(2000, 1, 1);
                EndDate = new DateTime(2100, 1, 1);
            }
            StartDate1 = StartDate.AddYears(-1);
            EndDate1 = EndDate.AddYears(-1);
            StartDate2 = StartDate.AddYears(-2);
            EndDate2 = EndDate.AddYears(-2);
            StartDate3 = StartDate.AddYears(-3);
            EndDate3 = EndDate.AddYears(-3);

            LocationName = ComboBoxSelectedLocation.Text;

            CustomerId = "";
            if (RadioButtonListCustomerSelection.SelectedValue == "Select")
            {
                CustomerId = ComboBoxCustomerSelection.SelectedValue;
            }

            ReportName = "";
            DataSetName = ""; 
            switch (RadioButtonListReportType.SelectedValue)
            {
                case "All" :
                    ReportName = "ReportComparisonTotals";
                    DataSetName = "DataSetComparisonTotals"; 
                    break;
                case "Material":
                    ReportName = "ReportComparisonMaterialTotals";
                    DataSetName = "DataSetComparisonMaterialTotals"; 
                    break;
                case "Customer":
                    ReportName = "ReportComparisonCustomerTotals";
                    DataSetName = "DataSetComparisonCustomerTotals"; 
                    break;
            }

            // load up the iframe 
            URL = "WebFormPopup.aspx?UC=ShowReport&d=" + DataSetName +
                 "&r=" + ReportName +
                 "&CustomerId=" + CustomerId +
                 "&LocationName=" + LocationName +
                 "&StartDate=" + StartDate.ToString() +
                 "&EndDate=" + EndDate.ToString() +
                 "&StartDate1=" + StartDate1.ToString() +
                 "&EndDate1=" + EndDate1.ToString() +
                 "&StartDate2=" + StartDate2.ToString() +
                 "&EndDate2=" + EndDate2.ToString() +
                 "&StartDate3=" + StartDate3.ToString() +
                 "&EndDate3=" + EndDate3.ToString() ;
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