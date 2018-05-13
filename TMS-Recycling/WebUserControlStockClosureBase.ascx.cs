using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TMS_Recycling
{
    public partial class WebUserControlStockClosureBase : ClassTMSUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetName = "MaterialClosure";
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (DataItem != null) 
            {
                MaterialClosure mc = (DataItem as MaterialClosure);

                LabelMaterialName.Text = mc.Material.Description;

                TextBox_MaterialStockLevel.Enabled = (mc.ClosureDateTime > Common.CurrentClientDate(Session).AddDays(-1));
                TextBox_MaterialStockPrice.Enabled = (mc.ClosureDateTime > Common.CurrentClientDate(Session).AddDays(-1));
                TextBox_MaterialTotalBought.Enabled = (mc.ClosureDateTime > Common.CurrentClientDate(Session).AddDays(-1));
                TextBox_MaterialTotalBoughtPrice.Enabled = (mc.ClosureDateTime > Common.CurrentClientDate(Session).AddDays(-1));
                TextBox_MaterialTotalSold.Enabled = (mc.ClosureDateTime > Common.CurrentClientDate(Session).AddDays(-1));
                TextBox_MaterialTotalSoldPrice.Enabled = (mc.ClosureDateTime > Common.CurrentClientDate(Session).AddDays(-1));
            }
        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            StandardButtonCancelClickHandler(sender, e);
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            if (TextBox_MaterialStockLevel.Enabled)
            {
                CheckBox_IsCorrected_Checked.Checked = true;
            }

            StandardButtonSaveClickHandler(sender, e);
        }

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            StandardButtonDeleteClickHandler(sender, e);
        }
    }
}