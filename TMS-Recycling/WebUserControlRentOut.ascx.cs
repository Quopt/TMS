using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Transactions;
using System.Data;

namespace TMS_Recycling
{
    public partial class WebUserControlRentOut : ClassTMSUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GenerateDescription();
                CalendarControlStart.SelectedDateTime = Common.CurrentClientDateTime(Session);
                CalendarControlEnd.SelectedDateTime = Common.CurrentClientDateTime(Session).AddDays(1);

                CheckBoxDatesOpenEndDate.Attributes.Add("onclick", "toggle(this,'MainContent_MainContent_WebUserControlRentOut1_CalendarControlEnd_TextBoxDate'); toggle(this,'MainContent_MainContent_WebUserControlRentOut1_CalendarControlEnd_ButtonSetDate');");

                DropDownListCustomers.DataBind();
                DropDownListLocations.DataBind();

                bool CustIdSet = false;
                bool LocIdSet = false;
                bool DescSet = false;

                if (Request.Params["CustId"] != null)
                {
                    string CustId = Request.Params["CustId"].ToString();
                    ListItem li = DropDownListCustomers.Items.FindByValue(CustId);
                    if (li != null)
                    {
                        li.Selected = true;
                        CustIdSet = true;
                    }
                }

                if (Request.Params["LocId"] != null)
                {
                    string LocId = Request.Params["LocId"].ToString();
                    ListItem li = DropDownListLocations.Items.FindByValue(LocId);
                    if (li != null)
                    {
                        li.Selected = true;
                        LocIdSet = true;
                    }
                }

                if (Request.Params["Description"] != null)
                {
                    TextBox_Description.Text = Request.Params["Description"].ToString();
                    DescSet = TextBox_Description.Text.Trim() != "";
                }

                if (Request.Params["Identification"] != null)
                {
                    TextBox_DriverId.Text = Request.Params["Identification"].ToString();
                    DescSet = TextBox_Description.Text.Trim() != "";
                }

                if (LocIdSet && CustIdSet && DescSet)
                {
                    CurrentPageNr = 2;
                }
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            EnableCorrectScreenElements();
        }

        protected void ButtonRefresh_Click(object sender, EventArgs e)
        {
            GenerateDescription();
        }

        protected void GenerateDescription()
        {
            if (LabelRentMode.Text == "Cash")
            {
                TextBox_Description.Text = "Verhuur per kas dd " + Common.CurrentClientDateTime(Session);
            }
            else
            {
                TextBox_Description.Text = "Verhuur op krediet dd " + Common.CurrentClientDateTime(Session);
            }
        }

        public void SwitchLedgerType(UserControlLedgerType lt)
        {
            switch (lt)
            {
                case (UserControlLedgerType.Cash) :
                    LabelRentMode.Text = "Cash";
                    break;
                case (UserControlLedgerType.Bank):
                    LabelRentMode.Text = "Bank";
                    break;
            }
        }

        public void EnableCorrectScreenElements()
        {
            PanelCustomerInformation.Visible = true;
            PanelOrderLines.Visible = false;
            PanelOrderLines.Enabled = false;
            PanelCustomerInformation.Enabled = false;
            PanelInvoice.Visible = false;
            PanelOrderSummary.Visible = false;
            PanelInvoice.Enabled = false;
            PanelOrderSummary.Enabled = false;

            ButtonContinue.Visible = false;
            ButtonRevert.Visible = false;
            ButtonDestroyOrderAndBack.Visible = false;
            ButtonNewOrder.Visible = false;
            ButtonPrintAndProcess.Visible = false;
            ButtonRefresh.Visible = false;

            CheckBoxProFormaInvoice.Visible = (LabelRentMode.Text != "Cash");
            TextBoxInvoiceDiscount.Enabled = (LabelRentMode.Text == "Cash") || (CheckBoxProFormaInvoice.Checked) ;
            TextBoxBail.Enabled = (LabelRentMode.Text == "Cash") || (CheckBoxProFormaInvoice.Checked) ;
            TextBoxInvoiceNote.Enabled = (LabelRentMode.Text == "Cash") || (CheckBoxProFormaInvoice.Checked);

            switch (CurrentPageNr)
            {
                case 1:
                    PanelCustomerInformation.Enabled = true;

                    ButtonContinue.Visible = true;
                    break;
                case 2 :
                    if (CheckBoxDatesOpenEndDate.Checked)
                    {
                        CalendarControlEnd.SelectedDateTime = new DateTime(2100, 1, 1);
                    }


                    if (TextBox_DriverId.Text == "")
                    {
                        CurrentPageNr--;
                        EnableCorrectScreenElements();
                        Common.InformUser(Page, "Geef een identificatie van de verhuurder op aub. Bv naam en rijbewijs met rijbewijs nummer, of bedrijfspasnaam en bedrijfspasnummer.");
                        break;
                    }
                    ButtonContinue.Visible = true;
                    ButtonRevert.Visible = true;

                    PanelOrderLines.Enabled = true;
                    PanelOrderLines.Visible = true;

                    WebUserControlRentMaterials1.StartRentDate = CalendarControlStart.SelectedDateTime;
                    WebUserControlRentMaterials1.EndRentDate = CalendarControlEnd.SelectedDateTime;
                    if (CheckBoxDatesOpenEndDate.Checked) { WebUserControlRentMaterials1.EndRentDate = new DateTime(2100, 1, 1); }
                    WebUserControlRentMaterials1.CustomerID = new Guid(DropDownListCustomers.SelectedValue);
                    WebUserControlRentMaterials1.LocationID = new Guid(DropDownListLocations.SelectedValue);

                    ButtonContinue.Visible =  WebUserControlRentMaterials1.HasOrderLines();
                    break;
                case 3:
                    ButtonPrintAndProcess.Visible = true;
                    ButtonRevert.Visible = true;

                    PanelOrderLines.Visible = true;

                    PanelOrderSummary.Enabled = true;
                    PanelOrderSummary.Visible = true;

                    RecalcTotals();

                    ButtonPrintAndProcess.Visible = LabelTotalItemsValue.Text != "0";
                    break;
                case 4:
                    PanelOrderLines.Visible = true;
                    PanelOrderSummary.Visible = true;

                    ButtonDestroyOrderAndBack.Visible = true;
                    ButtonNewOrder.Visible = true;

                    PanelInvoice.Visible = (LabelRentMode.Text == "Cash") || (CheckBoxProFormaInvoice.Checked);;
                    PanelInvoice.Enabled = true;

                    // show reports. Invoice & contracts.
                    FrameShowInvoiceA.Attributes["src"] = "";
                    FrameShowInvoiceB.Attributes["src"] = "";
                    FrameShowInvoiceA.Visible = (LabelRentMode.Text == "Cash") || (CheckBoxProFormaInvoice.Checked);
                    FrameShowInvoiceB.Visible = (LabelRentMode.Text == "Cash") || (CheckBoxProFormaInvoice.Checked);
                    if ((LabelRentMode.Text == "Cash") || (CheckBoxProFormaInvoice.Checked))
                    {
                        FrameShowInvoiceA.Attributes["src"] = "WebFormPopup.aspx?UC=ShowReport&d=DataSetInvoice&r=ReportInvoiceRentA4&Id=" + CurrentRentInvoiceId.ToString();
                        FrameShowInvoiceB.Attributes["src"] = "WebFormPopup.aspx?UC=ShowReport&d=DataSetRent&r=ReportRentContractsA4&Id=" + CurrentRentInvoiceId.ToString();
                    }

                    break;
            }

        }

        public int CurrentPageNr
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

        public Guid CurrentRentInvoiceId
        {
            get
            {
                if (LabelCurrentInvoiceId.Text != "")
                {
                    return new Guid(LabelCurrentInvoiceId.Text);
                }
                else
                {
                    return Guid.Empty;
                }
            }
            set
            {
                LabelCurrentOrderId.Text = value.ToString();
            }
        }

        protected void ButtonRevert_Click(object sender, EventArgs e)
        {
            CurrentPageNr--;
        }

        protected void ButtonContinue_Click(object sender, EventArgs e)
        {
            CurrentPageNr++;
        }

        protected void ButtonPrintAndProcess_Click(object sender, EventArgs e)
        {
            bool Success = false;

            ModelTMSContainer _ControlObjectContext = new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session);

            // start transaction
            using (TransactionScope TS = new TransactionScope())
            {
                try
                {
                    // expand rented items if required
                    WebUserControlRentMaterials1.ExpandOrderLines();

                    // by default we have no invoice 
                    LabelCurrentInvoiceId.Text = "";

                    // create rentledger entry and link up the rentalitemactivities
                    RentLedger TempRentLedger = new RentLedger();
                    _ControlObjectContext.AddToRentLedgerSet(TempRentLedger);

                    if (LabelGroupId.Text == "") { LabelGroupId.Text = TempRentLedger.Id.ToString(); }

                    TempRentLedger.Relation = _ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.RelationSet", "Id", Guid.Parse(DropDownListCustomers.SelectedValue))) as Relation;
                    TempRentLedger.Location = _ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.LocationSet", "Id", Guid.Parse(DropDownListLocations.SelectedValue))) as Location;
                    TempRentLedger.Description = TextBox_Description.Text;
                    WebUserControlRentMaterials1.AddRentMaterialsToRentLedgerAndRentalItemActivitySet(_ControlObjectContext, TempRentLedger);

                    // create invoice if required and link this up to the rentledger
                    if ((LabelRentMode.Text == "Cash") || (CheckBoxProFormaInvoice.Checked))
                    {
                        Invoice TempInvoice = new Invoice();
                        // copy the group code if this is a correction from an old invoice
                        Guid OldGuid = Guid.Parse(LabelCurrentOrderId.Text);
                        if (OldGuid != Guid.Empty)
                        {
                            try
                            {
                                Invoice OldInvoice = _ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.InvoiceSet", "Id", OldGuid)) as Invoice;
                                TempInvoice.GroupCode = OldInvoice.GroupCode;
                            }
                            catch { };
                        }

                        // reset the invoice
                        LabelCurrentOrderId.Text = Guid.Empty.ToString();
                        // continue processing
                        TempInvoice.Relation = _ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.RelationSet", "Id", Guid.Parse(DropDownListCustomers.SelectedValue))) as Relation;
                        TempInvoice.Location = _ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.LocationSet", "Id", Guid.Parse(DropDownListLocations.SelectedValue))) as Location;

                        TempInvoice.Description = TextBox_Description.Text;
                        TempInvoice.InvoiceType = "Sell";
                        TempInvoice.InvoiceSubType = "Rent";
                        TempInvoice.BookingDateTime = Common.CurrentClientDateTime(Session);
                        if (LabelRentMode.Text == "Cash")
                        {
                            TempInvoice.Ledger = TempInvoice.Location.CashLedger;
                        }
                        else
                        {
                            TempInvoice.Ledger = TempInvoice.Location.BankLedger;
                        }
                        TempInvoice.GroupCode = new Guid(LabelGroupId.Text);
                        TempInvoice.DiscountPercentage = Convert.ToDouble(TextBoxInvoiceDiscount.Text);
                        if (LabelRentMode.Text != "Cash")
                        {
                            TempInvoice.Ledger = TempInvoice.Location.BankLedger;
                        }
                        TempInvoice.InvoiceNote = TextBoxInvoiceNote.Text;
                        _ControlObjectContext.AddToInvoiceSet(TempInvoice);
                        TempRentLedger.Invoice.Add(TempInvoice);

                        // add the order to the invoice
                        TempRentLedger.AddRentMaterialsToInvoice(_ControlObjectContext, TempInvoice);

                        // add bail to the invoice
                        double Bail = Convert.ToDouble(TextBoxBail.Text); // except if this fails, this should be a valid number
                        if (Bail != 0)
                        {
                            InvoiceLine iline = new InvoiceLine();
                            iline.Description = "Borg";
                            iline.OriginalPrice = Bail;
                            iline.AllowDiscount = false;
                            iline.VATPercentage = 0;
                            iline.VATPrice = 0;
                            iline.TotalPrice = Bail;
                            iline.Invoice = TempInvoice;
                            iline.LineNumber = iline.Invoice.InvoiceLine.Count;
                            iline.LedgerBookingCode = iline.Invoice.Location.DefaultBailPriceBookingCode;
                        }

                        // all invoice lines to the same ledger
                        TempInvoice.AllInvoiceLinesToSameLedger(TempInvoice.Ledger);
                        TempInvoice.RecalcTotals();

                        // process order & invoice if this is a cash invoice
                        if (LabelRentMode.Text == "Cash")
                        {
                            TempInvoice.ProcessInvoice(_ControlObjectContext, TempInvoice.GroupCode, true, Common.CurrentClientDateTime(Session));
                        }
                        else
                        {
                            TempInvoice.GenerateInvoiceNumber(_ControlObjectContext);
                        }

                        LabelCurrentInvoiceId.Text = TempInvoice.Id.ToString();
                    }

                    TempRentLedger.CheckRentLedgerBeforeSave(_ControlObjectContext);

                    // and save to persistent storage
                    _ControlObjectContext.SaveChanges(System.Data.Objects.SaveOptions.DetectChangesBeforeSave);

                    // and save this invoice
                    LabelCurrentOrderId.Text = TempRentLedger.Id.ToString();

                    // commit the transaciton
                    TS.Complete();
                    Success = true;
                }
                catch (Exception ex) // commit or procedure failed somewhere
                {
                    // rollback transaction 
                    TS.Dispose();

                    // inform user
                    Common.InformUserOnTransactionFail(ex, Page);
                }
            }

            if (Success)
            {
                // when success advance panel
                CurrentPageNr = CurrentPageNr + 1;
                EnableCorrectScreenElements();
            }
        }

        protected void ButtonDestroyOrderAndBack_Click(object sender, EventArgs e)
        {
            bool Success = false;

            if (LabelCurrentOrderId.Text != Guid.Empty.ToString())
            {
                ModelTMSContainer _ControlObjectContext = new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session);

                // start transaction
                using (TransactionScope TS = new TransactionScope())
                {
                    try
                    {
                        // roll back order

                        // correct invoice if there
                        if (LabelCurrentInvoiceId.Text != "")
                        {
                            Invoice CorrInvoice = _ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.InvoiceSet", "Id", Guid.Parse(LabelCurrentInvoiceId.Text))) as Invoice;

                            // unprocess
                            CorrInvoice.UnprocessInvoice(_ControlObjectContext, CorrInvoice.GroupCode, Common.CurrentClientDateTime(Session));
                        }

                        // destroy rentledger
                        RentLedger TempRentLedger = _ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.RentLedgerSet", "Id", Guid.Parse(LabelCurrentOrderId.Text))) as RentLedger;
                        TempRentLedger.DestroyRentalItemActivities(_ControlObjectContext);
                        _ControlObjectContext.DeleteObject(TempRentLedger);

                        // and save to persistent storage
                        _ControlObjectContext.SaveChanges(System.Data.Objects.SaveOptions.DetectChangesBeforeSave);

                        // commit
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
            }

            if (Success)
            {
                // when success revert 
                CurrentPageNr--;
                EnableCorrectScreenElements();
            }

        }

        public void RecalcTotals()
        {
            WebUserControlRentMaterials1.LoadOrderLines();
            WebUserControlRentMaterials1.RecalcTotals();

            LabelTotalItemsValue.Text = WebUserControlRentMaterials1.TotalRentalItemAmount.ToString();
            LabelTotalPriceValue.Text = WebUserControlRentMaterials1.TotalRentPrice.ToString();
            TextBoxBail.Text = WebUserControlRentMaterials1.TotalBailPrice.ToString();
        }

        protected void ButtonNewOrder_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsoluteUri); 
        }
    }
}