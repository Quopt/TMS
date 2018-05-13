using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TMS_Recycling
{
    public partial class WebUserControlBookingCodeBase : ClassTMSUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetName = "LedgerBookingCode";

            if (!IsPostBack)
            {
                Common.AddCurrencyList(DropDownList_LedgerCurrency_SelectedValue.Items, true);

                if (Request.Params["Debug"] != null)
                {
                    CheckBox_IsDebugLedgerCode_Checked.Visible = true;
                    LabelIsDebugLedgerCode.Visible = true;
                }
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (DataItem != null)
            {
                URLPopUpControlCorrect.URLToPopup = "WebFormPopUp.aspx?uc=BookingCodeCorrection&Id=" + (DataItem as LedgerBookingCode).Id.ToString();
            }
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


        protected void URLPopUpControlCorrect_OnBeforePopUpOpened(object sender, EventArgs e)
        {
            ButtonSave_Click(sender, e);
        }

        protected void URLPopUpControlCorrect_OnPopupClosed(object sender, EventArgs e)
        {
            RebindControls();
        }

    }
}