using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TMS_Recycling
{
    public partial class WebUserControlBookingCodeCorrection : ClassTMSUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetName = "LedgerBookingCode";

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
                LedgerBookingCode lbc =  (DataItem as LedgerBookingCode);
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
                TextBoxAmount.Text = CorrectionAmount.ToString() ;
            }
            else
            {
                LedgerMutation lm = new LedgerMutation();
                LedgerBookingCode lbc =  (DataItem as LedgerBookingCode);

                lm.Description = "CORR / Correctie " + lbc.Description;
                lm.IsCorrection = true;
                lm.BookingType = RadioButtonListBuyOrSell.SelectedValue;
                lm.LedgerBookingCode = lbc;

                if (lm.BookingType == "Sell")
                {
                    CorrectionAmount = -CorrectionAmount;
                }

                lm.AmountEXVat = CorrectionAmount;
                lm.VATAmount = 0;
                lm.TotalAmount = lm.AmountEXVat;
                lm.Comments = TextBoxComments.Text;

                lm.Process(ControlObjectContext);

                ControlObjectContext.SaveChanges();

                if (lm.BookingType == "Buy")
                {
                    CorrectionAmount = -CorrectionAmount;
                }

                Common.InformUser(Page, "U heeft de mutatie succesvol doorgevoerd. U kunt deze popup nu sluiten of nog een mutatie doorvoeren.");
            }
        }

    }
}