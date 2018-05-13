using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TMS_Recycling
{
    public partial class WebUserControlBookKeepingClosureBase : ClassTMSUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetName = "LedgerClosure";
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (DataItem != null)
            {
                LedgerClosure ls = DataItem as LedgerClosure;

                LabelLedgerDate.Text = ls.ClosureDate.ToString();
                if (ls.Ledger != null)
                {
                    LabelLedgerName.Text = ls.Ledger.Description;
                }
                else
                {
                    LabelLedgerName.Text = ls.LedgerBookingCode.Description;
                }

                TextBox_LedgerLevel.Enabled = ls.ClosureDate.AddDays(1) > Common.CurrentClientDateTime(Session);
            }
        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            StandardButtonCancelClickHandler(sender, e);
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            if (TextBox_LedgerLevel.Enabled)
            {
                CheckBox_IsCorrection_Checked.Checked = true;
            }

            StandardButtonSaveClickHandler(sender, e);
        }

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            StandardButtonDeleteClickHandler(sender, e);
        }
    }
}