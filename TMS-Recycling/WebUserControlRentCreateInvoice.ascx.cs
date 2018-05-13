using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Common;
using System.Data;
using System.Transactions;

namespace TMS_Recycling
{
    public partial class WebUserControlRentCreateInvoice : ClassTMSUserControl
    {
        int CurrentPageNr
        {
            get
            {
                return Convert.ToInt32(LabelCurrentPageNr.Text);
            }
            set
            {
                LabelCurrentPageNr.Text = value.ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TextBoxDescription.Text = "Verhuurfactuur dd " + Common.CurrentClientDateTime(Session).ToString();
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            EnableCurrentPageElements();
        }

        public void EnableCurrentPageElements()
        {
            PanelCustomerInformation.Enabled = false;
            PanelRentalSelection.Enabled = false;
            PanelInvoiceDetails.Enabled = false;
            PanelRentalSelection.Visible = false;
            PanelInvoiceDetails.Visible = false;
            PanelPreviewInvoice.Visible = false;
            ButtonCorrect.Visible = false;
            ButtonNew.Visible = false;
            ButtonNext.Visible = false;
            ButtonPrevious.Visible = false;
            ButtonNext2.Visible = false;
            URLPopUpOpenInvoice.Visible = false;

            switch (CurrentPageNr)
            {
                case 1:
                    PanelCustomerInformation.Enabled = true;
                    ButtonNext.Visible = true;
                    break;
                case 2:
                    PanelRentalSelection.Visible = true;
                    PanelRentalSelection.Enabled = true;

                    ButtonNext.Visible = true;
                    ButtonPrevious.Visible = true;

                    GridViewSelectedRentOuts.DataBind();

                    break;
                case 3:
                    PanelRentalSelection.Visible = true;
                    PanelInvoiceDetails.Visible = true;
                    PanelInvoiceDetails.Enabled = true;
                    
                    ButtonNext2.Visible = true;
                    ButtonPrevious.Visible = true;

                    // check if there is a selection. If not revert to level 2
                    if (CheckForSelection() == 0)
                    {
                        CurrentPageNr = 2;
                        EnableCurrentPageElements();
                        Common.InformUser(Page, "Selecteer aub minimaal één verhuring om te factureren.");
                    }

                    break;
                case 4:
                    // controleer de getallen
                    bool Success = true;

                    try
                    {
                        double Test = Convert.ToDouble(TextBoxBail.Text);
                    }
                    catch
                    {
                        Success = false;
                        Common.InformUser(Page, "Vul bij borg een getal in!");
                    }

                    if (Success)
                    {

                        // maak de factuur aan
                        CreateNewInvoice();

                        // toon de factuur
                        PanelPreviewInvoice.Visible = true;
                        FrameShowInvoice.Attributes["src"] = "WebFormPopup.aspx?UC=ShowReport&d=DataSetInvoice&r=ReportInvoiceRentA4&Id=" + LabelGeneratedInvoiceId.Text;

                        ButtonCorrect.Visible = true;
                        ButtonNew.Visible = true;
                        URLPopUpOpenInvoice.Visible = true;
                        URLPopUpOpenInvoice.URLToPopup = "WebFormPopup.aspx?UC=InvoiceBase&Id=" + LabelGeneratedInvoiceId.Text;
                    }
                    else
                    {
                        CurrentPageNr--;
                        EnableCurrentPageElements();
                    }

                    break;

            }
        }

        public int CheckForSelection()
        {
            int CountSelect = 0;

            foreach (GridViewRow gvr in GridViewSelectedRentOuts.Rows)
            {
                if ((gvr.Cells[0].Controls[1] as CheckBox).Checked) 
                {
                    CountSelect++;
                }
            }

            return CountSelect;
        }

        public void CreateNewInvoice()
        {
            bool Success = false;

            // start transaction
            using (TransactionScope TS = new TransactionScope())
            {
                try
                {
                    Invoice inv = new Invoice();

                    ControlObjectContext.AddToInvoiceSet(inv);

                    foreach (GridViewRow gvr in GridViewSelectedRentOuts.Rows)
                    {
                        if ((gvr.Cells[0].Controls[1] as CheckBox).Checked)
                        {
                            String RentID = (gvr.Cells[0].Controls[1] as CheckBox).ToolTip;

                            RentalItemActivity ria = ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.RentalItemActivitySet", "Id", new System.Guid(RentID))) as RentalItemActivity;

                            ria.AddRentToInvoice(ControlObjectContext, inv);
                            inv.Location = ria.RentLedger.Location;
                        }
                    }
                    inv.GenerateInvoiceNumber(ControlObjectContext);

                    inv.Relation = ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.RelationSet", "Id", Guid.Parse(DropDownListCustomers.SelectedValue))) as Relation;

                    inv.Description = TextBoxDescription.Text;
                    inv.InvoiceType = "Sell";
                    inv.InvoiceSubType = "Rent";
                    inv.BookingDateTime = Common.CurrentClientDateTime(Session);
                    inv.Ledger = inv.Location.BankLedger;
                    try
                    {
                        inv.DiscountPercentage = Convert.ToDouble("0" + TextBoxInvoiceDiscount.Text);
                    }
                    catch {}

                    // add bail to the invoice
                    double Bail = Convert.ToDouble(TextBoxBail.Text); // except if this fails, this should be a valid number
                    if (Bail != 0)
                    {
                        InvoiceLine iline = new InvoiceLine();
                        iline.Description = "Borg";
                        iline.OriginalPrice = -Bail;
                        iline.AllowDiscount = false;
                        iline.VATPercentage = 0;
                        iline.VATPrice = 0;
                        iline.TotalPrice = -Bail;
                        iline.Invoice = inv;
                        iline.LineNumber = iline.Invoice.InvoiceLine.Count;
                        iline.LedgerBookingCode = iline.Invoice.Location.DefaultBailPriceBookingCode;
                    }


                    LabelGeneratedInvoiceNr.Text = inv.InvoiceNumber.ToString();
                    LabelGeneratedInvoiceId.Text = inv.Id.ToString();

                    if (LabelGroupId.Text == "") { LabelGroupId.Text = inv.GroupCode.ToString(); }

                    inv.GroupCode = new Guid(LabelGroupId.Text);
                    inv.InvoiceNote = TextBoxInvoiceNote.Text;

                    // all invoice lines to the same ledger
                    inv.AllInvoiceLinesToSameLedger(inv.Ledger);
                    inv.RecalcTotals();

                    // save the data
                    ControlObjectContext.SaveChanges(System.Data.Objects.SaveOptions.DetectChangesBeforeSave);

                    // commit the transaciton
                    TS.Complete();

                    Success = true;
                }
                catch (Exception ex)
                {
                    // rollback transaction 
                    TS.Dispose();

                    // inform user
                    Common.InformUserOnTransactionFail(ex, Page);
                }
            }

            if (!Success)
            {
                CurrentPageNr--;
                EnableCurrentPageElements();
            }
        }

        protected void DropDownListLocations_SelectedIndexChanged(object sender, EventArgs e)
        {
            EntityDataSourceCustomers.DataBind();
        }

        protected void EntityDataSourceCustomers_Selecting(object sender, EntityDataSourceSelectingEventArgs e)
        {

        }

        protected void GridViewSelectedRentOuts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem != null)
            {
                (e.Row.Cells[0].Controls[1] as CheckBox).ToolTip = (e.Row.DataItem as DbDataRecord).GetValue(10).ToString();
            }
        }

        protected void ButtonPrevious_Click(object sender, EventArgs e)
        {
            CurrentPageNr--;
        }

        protected void ButtonNext_Click(object sender, EventArgs e)
        {
            CurrentPageNr++;
        }

        protected void ButtonNew_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsoluteUri); 
        }

        protected void ButtonCorrect_Click(object sender, EventArgs e)
        {
            // start transaction
            using (TransactionScope TS = new TransactionScope())
            {
                try
                {
                    Invoice inv = ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.InvoiceSet", "Id", new System.Guid(LabelGeneratedInvoiceId.Text))) as Invoice;

                    inv.UnprocessInvoice(ControlObjectContext, new System.Guid(LabelGroupId.Text), Common.CurrentClientDateTime(Session));

                    // save the data
                    ControlObjectContext.SaveChanges(System.Data.Objects.SaveOptions.DetectChangesBeforeSave);

                    // commit the transaciton
                    TS.Complete();

                    CurrentPageNr--;
                    EnableCurrentPageElements();
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

    }
}