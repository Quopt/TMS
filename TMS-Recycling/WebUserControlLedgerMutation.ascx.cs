using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TMS_Recycling
{
    public partial class WebUserControlLedgerMutation : ClassTMSUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetName = "LedgerMutation";

            if (!IsPostBack)
            {
                Common.AddLedgerBookingTypeList(DropDownList_BookingType_SelectedValue.Items, true);
            }
        }

        void Page_PreRender(object sender, EventArgs e)
        {
            // enable the right controls if this is an editable mutation
            ButtonDelete.Enabled = false;
            TextBox_Description_Text.ReadOnly = true;
            TextBox_AmountEXVat.ReadOnly = true;
            TextBox_TotalAmount.ReadOnly = true;
            TextBox_VATAmount.ReadOnly = true;

            LedgerMutation lm = DataItem as LedgerMutation;

            if ((lm.IsEditable) && (lm.BookingDateTime.Date == Common.CurrentClientDate(Session)))
            {
                ButtonDelete.Enabled = true;
                ButtonSave.Enabled = true;
                ButtonCancel.Enabled = true;
                TextBox_Description_Text.ReadOnly = false;
                TextBox_AmountEXVat.ReadOnly = false;
                TextBox_TotalAmount.ReadOnly = false;
                TextBox_VATAmount.ReadOnly = false;
            }

            // link to a popup which determines linked objects based on group id 
            // TODO ! URLPopUpControlLink
        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            StandardButtonCancelClickHandler(sender, e);
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            StandardButtonSaveClickHandler(sender, e);
        }

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            StandardButtonDeleteClickHandler(sender, e);
        }
    }
}