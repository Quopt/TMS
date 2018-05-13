using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TMS_Recycling
{
    public partial class WebUserControlLedgerBase : ClassTMSUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetName = "Ledger";

            if (!IsPostBack)
            {
                Common.AddCurrencyList(DropDownList_LedgerCurrency_SelectedValue.Items, true);
                Common.AddLedgerTypeList(DropDownList_LedgerType_SelectedValue.Items, true);

                if (Request.Params["Debug"] != null)
                {
                    CheckBox_IsDebugLedger_Checked.Visible = true;
                    LabelIsDebugLedgerCode.Visible = true;
                }


            }
        }

        public void SetNewKeyID(Guid NewID)
        {
            KeyID = NewID;

            if (DropDownListLimitToLocation.Items[0].Value != "")
            {
                //DropDownListLimitToLocation.DataBind();
                Common.AddDefaultAllLocationText(DropDownListLimitToLocation.Items, false);
            }

            if ((DataItem != null) && ((DataItem as Ledger).LimitToLocation != null))
            {

                DropDownListLimitToLocation.SelectedValue = (DataItem as Ledger).LimitToLocation.Id.ToString();
            }
            else
            {
                DropDownListLimitToLocation.SelectedIndex = 0;
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (DataItem != null)
            {
                URLPopUpControlCorrect.URLToPopup = "WebFormPopUp.aspx?uc=BookingCorrection&Id=" + (DataItem as Ledger).Id.ToString();
            }
        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            StandardButtonCancelClickHandler(sender, e);
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            if (DropDownListLimitToLocation.SelectedIndex == 0)
            {
                (DataItem as Ledger).LimitToLocationId = null;
            }
            else
            {
                (DataItem as Ledger).LimitToLocationId = new System.Guid(DropDownListLimitToLocation.SelectedValue);
            }
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