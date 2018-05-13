using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TMS_Recycling
{
    public partial class WebUserControlCustomerReportLabels : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Common.AddCustomerRelationAdressTypeList(ComboBoxAdressType.Items, true);
                ComboBoxAdressType.SelectedValue = "Invoice";
            }
        }

        protected void ButtonShowReport_Click(object sender, EventArgs e)
        {
            string URL;

            // load up the iframe 
            URL = "WebFormPopup.aspx?UC=ShowReport&d=DataSetRelation&r=ReportRelationLabels&AdressType=" + ComboBoxAdressType.SelectedValue;
            LabelURL.Text = URL;
            FrameShowReport.Attributes["src"] = URL;
        }
    }
}