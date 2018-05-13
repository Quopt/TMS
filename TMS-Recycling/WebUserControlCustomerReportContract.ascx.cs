using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TMS_Recycling
{
    public partial class WebUserControlCustomerReportContract : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CalendarControlStartPeriod.SelectedDate = Common.CurrentClientDate(Session).AddDays(-31);
                CalendarControlEndPeriod.SelectedDate = Common.CurrentClientDate(Session); 
            }
        }

        protected void ButtonShowReport_Click(object sender, EventArgs e)
        {
            string CustomerId, ContractType, LocationName, ContractStatus, StartDate, EndDate,
                ReportName, DataSetName, URL, ShowReadyContracts;

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

            ContractType = RadioButtonListContractType.SelectedValue;
            ContractStatus = RadioButtonListContractStatus.SelectedValue;
            ShowReadyContracts = "F";
            if (ContractStatus == "Ready")
            {
                ContractStatus = "Open";
                ShowReadyContracts = "T";
            }

            LocationName = ComboBoxSelectedLocation.Text;

            ReportName = "ReportRelationContracts";
            DataSetName = "DataSetRelationContracts";

            // load up the iframe 
            URL = "WebFormPopup.aspx?UC=ShowReport&d=" + DataSetName +
                 "&r=" + ReportName +
                 "&CustomerId=" + CustomerId +
                 "&LocationName=" + LocationName +
                 "&ContractType="+ ContractType + 
                 "&ContractStatus="+ ContractStatus + 
                 "&ShowReadyContracts=" +  ShowReadyContracts + 
                 "&StartDate=" + StartDate +
                 "&EndDate=" + EndDate;
            LabelURL.Text = URL;
            FrameShowReport.Attributes["src"] = URL;
        }
    }
}