using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TMS_Recycling
{
    public partial class WebUserControlRentLedgerBase : ClassTMSUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetName = "RentalItemActivity";

            if (!IsPostBack)
            {
                Common.AddRentLedgerStatusList(DropDownList_InvoiceStatus_SelectedValue.Items, true);
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            bool SetVis = false;

            // if there is a dataitem with a valid invoice then switch screen elements on/off
            if ((DataItem != null) && ((DataItem as RentalItemActivity).InvoiceLine != null))
            {
                // poup button
                URLPopUpControl_RentInvoice.URLToPopup = "WebFormPopup.aspx?uc=InvoiceBase&Id=" + (DataItem as RentalItemActivity).InvoiceLine.Invoice.Id.ToString();
                URLPopUpControl_RentInvoice.Visible = true;

                // if Invoice is corrected or processed then disable price adjustments
                RentalItemActivity ria = (DataItem as RentalItemActivity);
                SetVis = ( (!ria.InvoiceLine.Invoice.IsCorrected) && (ria.InvoiceLine.Invoice.InvoiceStatus == "Open") );
            }
            else
            {
                URLPopUpControl_RentInvoice.Visible = false;
            }

            ButtonRecalc.Visible = SetVis;
            CheckBoxChangeInvoiceLine.Visible = SetVis;
            CheckBoxBasedOnOfficialRent.Visible = SetVis;

            TextBox_CalculatedRentPrice.Enabled = SetVis;
            TextBox_DiscountPercentage.Enabled = SetVis;
            CalendarControl_RentStartDateTime_SelectedDateTime.Enabled = SetVis;
            CalendarControl_RentEndStartDateTime_SelectedDateTime.Enabled = SetVis;
        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            StandardButtonCancelClickHandler(sender, e);
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            (DataItem as RentalItemActivity).RecalcRentPrice();
            StandardButtonSaveClickHandler(sender, e);
        }

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            StandardButtonDeleteClickHandler(sender, e);
        }

        protected void ButtonRecalc_Click(object sender, EventArgs e)
        {
            RentalItemActivity ria = (DataItem as RentalItemActivity);

            SaveDataIntoDataItemFromControls();

            ria.RecalcRentPrice(CheckBoxBasedOnOfficialRent.Checked);
            if (CheckBoxChangeInvoiceLine.Checked)
            {
                ria.UpdateLinkedInvoiceLine();
            }
            ria.UpdateAdvancePaymentStatus(ControlObjectContext, CheckBoxChangeInvoiceLine.Checked, CheckBox_IsTreatedAsAdvancePayment_Checked.Checked);

            RebindControls();

            StandardButtonSaveClickHandler(sender, e);
        }
    }
}