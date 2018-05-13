using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Transactions;

namespace TMS_Recycling
{
    public partial class WebUserControlBookKeepingCheckBase : ClassTMSUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetName = "LedgerCheck";

            if (!IsPostBack)
            {
                UpdateLedgerLevel();
            }
        }

        private void UpdateLedgerLevel()
        {
            LedgerBookingCode lbc = null;
            Ledger lg = null;

            // get the ledger or ledgerbookingcode we have to link to
            if (Request.Params["LedgerBookingCodeId"] != null)
            {
                lbc = ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.LedgerBookingCodeSet", "Id", new Guid(Request.Params["LedgerBookingCodeId"]))) as LedgerBookingCode;
                LabelCurrentLevel.Text = String.Format(LabelCurrentLevelBase.Text, lbc.LedgerLevel);
            }
            else
            { // assume Id
                lg = ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.LedgerSet", "Id", new Guid(Request.Params["Id"]))) as Ledger;
                LabelCurrentLevel.Text = String.Format(LabelCurrentLevelBase.Text, lg.LedgerLevel);
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (DataItem != null)
            {
                LedgerCheck lc = (DataItem as LedgerCheck);

                bool Enabled = lc.CreateDateTime.AddDays(7) > Common.CurrentClientDateTime(Session);
                CalendarWithTimeControl_CheckDate_SelectedDateTime.Enabled = Enabled;
                TextBox_Description.Enabled = Enabled;

                ButtonCorrectNow.Enabled = !(lc.IsLedgerCorrected);
                TextBox_CorrectionAmount.Enabled = !(lc.IsLedgerCorrected);
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

        protected void ButtonCorrectNow_Click(object sender, EventArgs e)
        {
            // first get the correction amount
            Double CorrectionAmount = 0;
            try
            {
                CorrectionAmount = Convert.ToDouble(TextBox_CorrectionAmount.Text);
            }
            catch
            {
            }
            
            // correct now
            if (CorrectionAmount.ToString() != TextBox_CorrectionAmount.Text)
            {
                Common.InformUser(Page, "Het correctiebedrag kan niet goed worden herkend. Voer dit opnieuw in en probeer het nogmaals.");
            }
            else
            {
                // start transaction
                using (TransactionScope TS = new TransactionScope())
                {
                    try
                    {
                        // save current record
                        StandardSaveHandler(null, null, false);

                        // update the ledgercheck & create the mutation
                        LedgerCheck lc = (DataItem as LedgerCheck);

                        LedgerBookingCode lbc = null;
                        Ledger lg = null;
                        String Description = "";

                        // get the ledger or ledgerbookingcode we have to link to
                        if (Request.Params["LedgerBookingCodeId"] != null)
                        {
                            lbc = ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.LedgerBookingCodeSet", "Id", new Guid(Request.Params["LedgerBookingCodeId"]))) as LedgerBookingCode;
                            Description = lbc.Description;
                        }
                        else
                        { // assume Id
                            lg = ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.LedgerSet", "Id", new Guid(Request.Params["Id"]))) as Ledger;
                            Description = lg.Description;
                        }

                        LedgerMutation lm = new LedgerMutation();
                        ControlObjectContext.AddToLedgerMutationSet(lm);

                        lm.Description = "CORR / Correctie " + Description;
                        lm.IsCorrection = true;
                        lm.BookingType = "Buy";
                        lm.LedgerBookingCode = lbc;
                        lm.Ledger = lg;
                        lm.AmountEXVat = CorrectionAmount;
                        lm.VATAmount = 0;
                        lm.TotalAmount = lm.AmountEXVat;
                        lm.Process(ControlObjectContext);

                        lc.IsLedgerCorrected = true;

                        ControlObjectContext.SaveChanges();

                        TS.Complete();

                        // relado data
                        RebindControls();
                        UpdateLedgerLevel();

                        // inform user
                        Common.InformUser(Page, "De correctie is succesvol verwerkt.");
                    }
                    catch (Exception ex) // commit or procedure failed somewhere
                    {
                        // rollback transaction 
                        TS.Dispose();

                        // inform user
                        Common.InformUserOnTransactionFail(ex, Page);
                    }
                }
            }

        }
    }
}