using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TMS_Recycling
{
    public partial class WebUserControlBookingCorrection : ClassTMSUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetName = "Ledger";

            if (!IsPostBack)
            {
                if (Request.Params["Id"] != null)
                {
                    KeyID = new Guid(Request.Params["Id"]);
                }
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (DataItem != null)
            {
                Ledger lbc = (DataItem as Ledger);
                LabelName.Text = lbc.Description;
                LabelCurrentBookingCodeLevel.Text = lbc.LedgerLevel.ToString();
            }
        }

        protected void ButtonProcess_Click(object sender, EventArgs e)
        {
            double CorrectionAmount = 0;

            try
            {
                CorrectionAmount = Convert.ToDouble(TextBoxAmount.Text);
            }
            catch
            {
            }

            if (CorrectionAmount.ToString() != TextBoxAmount.Text)
            {
                Common.InformUser(Page, "Het opgegeven correctiebedrag kan niet worden herkend. Geef aub een correct bedrag op.");
                TextBoxAmount.Text = CorrectionAmount.ToString();
            }
            else
            {
                LedgerMutation lm = new LedgerMutation();
                Ledger lb = (DataItem as Ledger);

                lm.Description = "CORR / Correctie " + lb.Description;
                lm.IsCorrection = true;
                lm.BookingType = RadioButtonListBuyOrSell.SelectedValue;
                lm.Ledger = lb;

                if (lm.BookingType == "Sell")
                {
                    CorrectionAmount = -CorrectionAmount;
                }

                lm.AmountEXVat = CorrectionAmount;
                lm.VATAmount = 0;
                lm.TotalAmount = lm.AmountEXVat;
                lm.Comments = TextBoxComments.Text;

                lm.Process(ControlObjectContext);

                // store the correction amount as negated number so the book closures will function properly for Buy mutations as well
                if (lm.BookingType == "Buy")
                {
                    CorrectionAmount = -CorrectionAmount;
                }

                ControlObjectContext.SaveChanges();

                Common.InformUser(Page, "U heeft de mutatie succesvol doorgevoerd. U kunt deze popup nu sluiten of nog een mutatie doorvoeren.");
            }
        }
    }
}