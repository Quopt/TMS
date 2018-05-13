using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TMS_Recycling
{
    public partial class WebUserControlRentReportUsage : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            // set the locationname parameter 
            string BaseVal, BaseText; 
            BaseText = ComboBoxSpecificMaterial.Items[0].Text;
            BaseVal = ComboBoxSpecificMaterial.Items[0].Value;
            ComboBoxSpecificMaterial.Items.Clear();
            ComboBoxSpecificMaterial.Items.Add(new ListItem(BaseText, BaseVal));
            EntityDataSourceSpecificMaterials.WhereParameters["LocationName"].DefaultValue = ComboBoxSelectedLocation.Text == "" ? "%" : ComboBoxSelectedLocation.Text;

            // check if something in the selection has changed
            string ValChecker; 
            ValChecker = ComboBoxSelectedLocation.Text + ComboBoxMaterialType.SelectedValue + ComboBoxSpecificMaterial.SelectedValue;
            if (ValChecker != LabelValChecker.Text)
            {
                FrameShowReport.Attributes["src"] = "";
            }
            LabelValChecker.Text = ValChecker;
        }

        protected void ButtonShowReport_Click(object sender, EventArgs e)
        {
            string LocationName, ReportName, DataSetName, URL, MaterialTypeId, RentMaterialTypeId, StartDate, EndDate;

            LocationName = ComboBoxSelectedLocation.Text;

            MaterialTypeId = ComboBoxMaterialType.SelectedValue;
            RentMaterialTypeId = ComboBoxSpecificMaterial.SelectedValue;

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

            ReportName = "ReportRentReportUsage";
            DataSetName = "DataSetRentMaterialsUsage";

            // load up the iframe 
            URL = "WebFormPopup.aspx?UC=ShowReport&d=" + DataSetName +
                 "&r=" + ReportName +
                 "&MaterialTypeId=" + MaterialTypeId +
                 "&RentMaterialId=" + RentMaterialTypeId +
                 "&StartDate=" + StartDate +
                 "&EndDate=" + EndDate +
                 "&LocationName=" + LocationName;
            LabelURL.Text = URL;
            FrameShowReport.Attributes["src"] = URL;
        }


    }
}