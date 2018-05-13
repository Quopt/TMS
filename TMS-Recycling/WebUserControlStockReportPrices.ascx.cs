using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TMS_Recycling
{
    public partial class WebUserControlStockReportPrices : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void ButtonShowReport_Click(object sender, EventArgs e)
        {
            string LocationName,  ReportName, DataSetName, URL;

            LocationName = ComboBoxSelectedLocation.Text;

            ReportName = "ReportMaterialStockPrices";
            DataSetName = "DataSetMaterialStock";
            if (RadioButtonListPriceListType.SelectedValue == "Sale")
            {
                ReportName = "ReportMaterialStockSalePrices";
            }

            // load up the iframe 
            URL = "WebFormPopup.aspx?UC=ShowReport&d=" + DataSetName +
                 "&r=" + ReportName +
                 "&LocationName=" + LocationName;
            LabelURL.Text = URL;
            FrameShowReport.Attributes["src"] = URL;
        }

    }
}