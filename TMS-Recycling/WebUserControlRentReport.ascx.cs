using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TMS_Recycling
{
    public partial class WebUserControlRentReport : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void ButtonShowReport_Click(object sender, EventArgs e)
        {
            string LocationName, ReportName, DataSetName, URL, MaterialId;

            LocationName = ComboBoxSelectedLocation.Text;

            MaterialId = ComboBoxMaterialType.SelectedValue;

            ReportName = "ReportRentReport";
            DataSetName = "DataSetRentMaterials";

            // load up the iframe 
            URL = "WebFormPopup.aspx?UC=ShowReport&d=" + DataSetName +
                 "&r=" + ReportName +
                 "&MaterialId=" + MaterialId +
                 "&LocationName=" + LocationName;
            LabelURL.Text = URL;
            FrameShowReport.Attributes["src"] = URL;
        }

    }
}