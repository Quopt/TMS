using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace TMS_Recycling
{
    public partial class WebUserControlInvoicePartialPayment : ClassTMSUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetName = "Invoice";

            if (!IsPostBack)
            {
                KeyID = new Guid(Request.Params["Id"]);
                Invoice inv = (DataItem as Invoice);
                
                DropDownListLocation.DataBind();
                ListItem li = DropDownListLocation.Items.FindByValue(inv.Location.Id.ToString());
                if ( li != null)
                {
                    li.Selected = true;
                }

                DropDownListLedger.DataBind();
                li = DropDownListLedger.Items.FindByValue(inv.Ledger.Id.ToString());
                if (li != null)
                {
                    li.Selected = true;
                }

                RebindControls();
            }
        }

        protected void ButtonProcess_Click(object sender, EventArgs e)
        {
            double CorrectionAmount=0;

            try
            {
                CorrectionAmount = Convert.ToDouble(TextBoxPaidAmount.Text);
            }
            catch
            {
            }

            if (CorrectionAmount.ToString() != TextBoxPaidAmount.Text)
            {
                Common.InformUser(Page, "Het bedrag wat u heeft ingevuld is niet juist. Controleer het bedrag.");
                TextBoxPaidAmount.Text = CorrectionAmount.ToString();
            }
            else
            {
                LedgerMutation lm = new LedgerMutation();
                ControlObjectContext.AddToLedgerMutationSet(lm);

                Invoice inv = (DataItem as Invoice);

                lm.Ledger = ControlObjectContext.GetObjectByKey( new EntityKey ("ModelTMSContainer.LedgerSet", "Id", new Guid(DropDownListLedger.SelectedValue))) as Ledger;
                lm.Location = ControlObjectContext.GetObjectByKey( new EntityKey ("ModelTMSContainer.LocationSet", "Id", new Guid(DropDownListLocation.SelectedValue))) as Location;
                lm.Description = "INV / PPaid / Vooruitbetaald bedrag factuur " + inv.InvoiceNumber.ToString();
                lm.GroupCode = inv.GroupCode;
                lm.BookingType = inv.InvoiceType;
                lm.AmountEXVat = CorrectionAmount;
                lm.VATAmount = 0;
                lm.TotalAmount = CorrectionAmount;
                lm.Process(ControlObjectContext);

                inv.AlreadyPaid = inv.AlreadyPaid + CorrectionAmount;
                inv.InvoiceStatus = inv.AlreadyPaid != 0 ? "PPaid" : "Open";

                ControlObjectContext.SaveChanges();
                Common.InformUser(Page, "De vooruitbetaling is verwerkt. U kunt dit scherm nu sluiten.");
            }
        }
    }
}