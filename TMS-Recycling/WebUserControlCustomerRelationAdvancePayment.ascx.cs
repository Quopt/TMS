using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Transactions;
using System.Data.Objects;

namespace TMS_Recycling
{
    public partial class WebUserControlCustomerRelationAdvancePayment : ClassTMSUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetName = "RelationAdvancePayment";

            if (!IsPostBack)
            {
                //Common.AddPriceAgreementTypeList(DropDownList_AgreementType_SelectedValue.Items, true);
                Common.AddAdvancePaymentTypeList(DropDownList_PaymentType_SelectedValue.Items, true);
            }
            URLPopUpControlLink.URLToPopup = "WebFormPopup.aspx?UC=ShowLinks&UCE=InvoiceBase&BO=InvoiceSet as it inner join InvoiceLineSet as il on il.Invoice.Id = it.Id inner join RelationAdvancePaymentSet as ra on il.RelationAdvancePayment.Id = ra.Id&SF=it.Id,it.Description,it.InvoiceNumber,it.BookingDateTime&DF=x,Omschrijving,Factuurnummer,Boekdatum en -tijd&SEL=ra.Id&ORD=it.BookingDateTime desc&LNK=Id&ID=" + KeyID.ToString();
            URLPopUpControlContract.URLToPopup = "WebFormPopup.aspx?UC=ShowReport&d=DataSetRelationAdvancePayment&r=ReportAdvancePayment&Id=" + KeyID.ToString();
        }

        void Page_PreRender(object sender, EventArgs e)
        {
            Boolean IsPaidOut = (GetDataItem(KeyID) as RelationAdvancePayment).IsPaidOut;
            Boolean IsPaidBack = (GetDataItem(KeyID) as RelationAdvancePayment).IsPaidBack;

            CalendarControl_PaymentDateTime_SelectedDate.Enabled = !IsPaidOut;
            TextBox_Amount.Enabled = !IsPaidOut;
            DropDownList_Ledger.Enabled = !IsPaidOut;
            DropDownList_LedgerBookingCode.Enabled = !IsPaidOut;
            TextBox_Description_Text.Enabled = !IsPaidOut;
            DropDownList_PaymentType_SelectedValue.Enabled = !IsPaidOut;

            ButtonPayBack.Visible = ((IsPaidOut) && (!IsPaidBack));
            ButtonPayOut.Visible = !IsPaidOut;
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
            if (AllowDelete() && (DataItem as RelationAdvancePayment).IsPaidOut)
            {
                ProcessPayment(true);
            }

            StandardButtonDeleteClickHandler(sender, e);
        }

        protected void ButtonPayOut_Click(object sender, EventArgs e)
        {
            //ButtonSave_Click(null, null);
            ProcessPayment(false);
        }

        private void ProcessPayment(Boolean IsCorrection)
        {
            // start transaction
            using (TransactionScope TS = new TransactionScope())
            {
                // process payment
                try
                {
                    ClassCustomBinding.BindToObject("ModelTMSContainer." + SetName + "Set", KeyID, Controls, DataItem, ControlObjectContext);

                    if (!IsCorrection)
                    {
                        (DataItem as RelationAdvancePayment).PayOut(ControlObjectContext, Common.CurrentClientDateTime(Session));
                    }
                    else
                    {
                        (DataItem as RelationAdvancePayment).PayBack(ControlObjectContext, Common.CurrentClientDateTime(Session));
                    }
                    ControlObjectContext.SaveChanges(SaveOptions.DetectChangesBeforeSave);

                    // commit transaction
                    TS.Complete();
                }
                catch (Exception ex)
                {
                    // rollback transaction 
                    TS.Dispose();

                    // inform user
                    Common.InformUserOnTransactionFail(ex, Page);
                }
            }
        }

        protected void ButtonPayBack_Click(object sender, EventArgs e)
        {
            //ButtonSave_Click(null, null);
            ProcessPayment(true);
        }
    }
}