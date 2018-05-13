using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Objects;
using System.Transactions;
using System.Data;

namespace TMS_Recycling
{
    public partial class WebUserControlFreightWeighing : ClassTMSUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetName = "Freight";

            if (!IsPostBack)
            {
                if (Request.Params["FreightID"] != null)
                {
                    CurrentWeighingId = new Guid(Request.Params["FreightID"].ToString());
                    CurrentPageNr = 22;

                    // locate the freight number and display it in the proper box
                    Freight frg = Freight.SelectFreightByFreightId(CurrentWeighingId,  new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session));
                    if (frg != null)
                    {
                        TextBoxOrderNumber.Text = frg.OurReference.ToString();
                        //LoadFirstWeighingData(frg);
                    }

                    CurrentPageNr = 21;
                    EnableCorrectScreenElements();
                    CurrentPageNr = 22;
                    EnableCorrectScreenElements();

                    if (frg.FreightStatus != "2nd weighing")
                    {
                        Response.Redirect(Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.IndexOf("?")));
                    }
                }
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            EnableCorrectScreenElements();
        }

        private void EnableCorrectScreenElements()
        {
            // disable all screen elements
            //PanelCustomerInformation.Visible = true;
            PanelFirstWeighing.Visible = false;
            PanelFirstWeighingNr.Visible = false;
            PanelInvoice.Visible = false;
            PanelSecondWeighing.Visible = false;
            PanelSecondWeighingOrderNumber.Visible = false;
            PanelCustomerInformation.Enabled = false;
            PanelFirstWeighing.Enabled = false;
            PanelFirstWeighingNr.Enabled = false;
            PanelInvoice.Enabled = false;
            PanelSecondWeighing.Enabled = false;
            PanelSecondWeighingOrderNumber.Enabled = false;

            ButtonContinue.Visible = false;
            ButtonDestroyAndBack.Visible = false;
            ButtonInvoiceOrder.Visible = false;
            ButtonNew.Visible = false;
            ButtonPrintAndProcess.Visible = false;
            ButtonRevert.Visible = false;
            ButtonSecondWeighing.Visible = false;
            ButtonSortOrder.Visible = false;

            URLPopUpControlLegalDocuments.Visible = false;

            if (CurrentPageNr == 9) { CurrentPageNr = 1; }
            if (CurrentPageNr == 19) { CurrentPageNr = 1; }

            if (CurrentPageNr == 12) { CurrentPageNr = 11; } // error situation. Fix this way.
            if (CurrentPageNr > 23) { CurrentPageNr = 23; } // error situation. Fix this way.

            switch (CurrentPageNr)
            // note : 1 is general, 10 series is for first weighing, 20 is for second weighing.
            {
                case 1:
                    PanelCustomerInformation.Enabled = true;
                    ButtonContinue.Visible = true;
                    break;
                case 10:
                    PanelFirstWeighing.Visible = true;
                    PanelFirstWeighing.Enabled = true;

                    CalendarWithTimeControlDateTime1.SelectedDateTime = Common.CurrentClientDateTime(Session);

                    ComboBoxWeighingLocation.DataBind();
                    ComboBoxCustomer.DataBind();
                    ShowCorrectCustomer();

                    ButtonRevert.Visible = PanelCustomerInformation.Visible;
                    ButtonPrintAndProcess.Visible = true;
                    break;
                case 11:
                    if (Convert.ToInt64("0" + TextBoxGrossWeight.Text) <= 0)
                    {
                        CurrentPageNr--;
                        EnableCorrectScreenElements();
                        Common.InformUser(Page, "Vul aub een positief gewicht in.");
                    }
                    else
                    {
                        if (ProcessFirstWeighing())
                        {
                            PanelFirstWeighing.Visible = true;
                            PanelFirstWeighing.Enabled = true;
                            PanelFirstWeighingNr.Visible = true;

                            ButtonSecondWeighing.Visible = true;
                            //ButtonDestroyAndBack.Visible = true;
                            ButtonNew.Visible = true;

                            PanelInvoice.Visible = true;
                            PanelInvoice.Enabled = true;
                            FrameShowInvoiceB.Visible = false;
                            FrameShowInvoiceA.Attributes["src"] = "WebFormPopup.aspx?UC=ShowReport&d=DataSetWeighing&r=ReportWeighingA4&Id=" + CurrentWeighingId.ToString();

                            URLPopUpControlLegalDocuments.URLToPopup = "webformpopup.aspx?UC=FreightLegalDocuments&FreightId=" + CurrentWeighingId.ToString();
                            URLPopUpControlLegalDocuments.Visible = true;
                        }
                        else
                        {
                            CurrentPageNr--;
                            EnableCorrectScreenElements();
                        }
                    }
                    break;
                case 20:
                    PanelSecondWeighingOrderNumber.Visible = true;
                    PanelSecondWeighingOrderNumber.Enabled = true;
                    LabelWeighingLoaded.Text = "";

                    ButtonRevert.Visible = true;
                    ButtonContinue.Visible = true;
                    break;
                case 21:
                    // load freight
                    Freight frg = Freight.SelectFreightByFreightNr(Convert.ToInt64(TextBoxOrderNumber.Text), new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session));

                    if (frg != null) 
                    {
                        if (LabelWeighingLoaded.Text == "") { LoadFirstWeighingData(frg); LabelWeighingLoaded.Text = "x"; }

                        PanelSecondWeighingOrderNumber.Visible = true;
                        PanelFirstWeighing.Visible = true;
                        PanelFirstWeighing.Enabled = true;
                        PanelSecondWeighing.Visible = true;

                        CalendarWithTimeControl2.SelectedDateTime = Common.CurrentClientDateTime(Session);

                        ButtonRevert.Visible = true;
                        ButtonContinue.Visible = true;

                        if (frg.FreightStatus != "2nd weighing")
                        {
                            Common.InformUser(Page, "Deze weging heeft niet de juiste status voor het invoeren van een tweede weging. U kunt deze gegevens wel invoeren als u het zeker weet.");
                        }
                    }
                    else
                    {
                        CurrentPageNr--;
                        EnableCorrectScreenElements();
                        if (frg == null)
                        {
                            Common.InformUser(Page, "Geef aub een correct vrachtnummer op.");
                        }
                        else
                        {
                            Common.InformUser(Page, "Het opgegeven vrachtnummer is correct, maar deze vracht is al verder verwerkt. Een tweede weging opgeven kan maar één keer.");
                        }
                    }
                    break;
                case 22:
                    PanelSecondWeighingOrderNumber.Visible = true;
                    PanelFirstWeighing.Visible = true;
                    PanelSecondWeighing.Visible = true;
                    PanelSecondWeighing.Enabled = true;

                    SetCustomerType();

                    ButtonRevert.Visible = true;
                    ButtonPrintAndProcess.Visible = true;
                    break;
                case 23:
                    if (Convert.ToInt64("0" + TextBoxGrossWeight.Text) <= Convert.ToInt64("0" + TextBoxWeight2.Text))
                    {
                        CurrentPageNr--;
                        EnableCorrectScreenElements();
                        Common.InformUser(Page, "Vul aub een gewicht in wat kleiner is dan de eerste weging.");
                    }
                    else
                    {
                        if (ProcessSecondWeighing())
                        {
                            PanelSecondWeighingOrderNumber.Visible = true;
                            PanelFirstWeighing.Visible = true;
                            PanelSecondWeighing.Visible = true;
                            PanelInvoice.Visible = true;
                            PanelInvoice.Enabled = true;

                            ButtonNew.Visible = true;
                            //ButtonDestroyAndBack.Visible = true;

                            ButtonSortOrder.Visible = CheckBoxWeighingActionSort.Checked;
                            ButtonInvoiceOrder.Visible = CheckBoxWeighingActionInvoice.Checked;

                            FrameShowInvoiceB.Visible = CheckBoxWeighingActionPay.Checked;

                            FrameShowInvoiceA.Attributes["src"] = "WebFormPopup.aspx?UC=ShowReport&d=DataSetWeighing&r=ReportWeighingA4&Id=" + CurrentWeighingId.ToString();
                            if (FrameShowInvoiceB.Visible)
                            {
                                FrameShowInvoiceB.Attributes["src"] = "WebFormPopup.aspx?UC=ShowReport&d=DataSetInvoice&r=ReportInvoiceA4&Id=" + LabelGeneratedInvoiceId.Text.ToString();
                            }

                            URLPopUpControlLegalDocuments.URLToPopup = "webformpopup.aspx?UC=FreightLegalDocuments&FreightId=" + CurrentWeighingId.ToString();
                            URLPopUpControlLegalDocuments.Visible = true;
                        }
                        else
                        {
                            CurrentPageNr--;
                            EnableCorrectScreenElements();
                        }
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

        public Guid CurrentWeighingId
        {
            get
            {
                if (LabelCurrentOrderId.Text != "")
                {
                    return new Guid(LabelCurrentOrderId.Text);
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
            if (CurrentPageNr == 1)
            {
                if (RadioButtonListWeighingType.SelectedIndex == 0)
                {
                    CurrentPageNr = 10;
                }
                else
                {
                    CurrentPageNr = 20;
                }
            }
            else if (CurrentPageNr == 20)
            {
                CurrentPageNr++;
                EnableCorrectScreenElements();
                CurrentPageNr++;
                EnableCorrectScreenElements();
            }
            else
            {
                CurrentPageNr++;
            }
        }

        protected void ButtonPrintAndProcess_Click(object sender, EventArgs e)
        {
            if (CurrentPageNr < 20)
            {
                // for the first weighing just register this freight and the freightweighing
                CurrentPageNr++;
            }
            else
            {
                // for the second weighing register the freight and the freightweighing and optionally create the invoice for the weighing
                CurrentPageNr++;
            }
        }

        protected bool ProcessFirstWeighing()
        {
            ModelTMSContainer ControlObjectContext = new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session);
            bool Success = false;

            // start transaction
            using (TransactionScope TS = new TransactionScope())
            {
                try
                {
                    // process freight
                    // setup base objects
                    Freight frg = new Freight();
                    FreightWeighing frgw = new FreightWeighing();
                    FreightWeighingMaterial frgm = new FreightWeighingMaterial();
                    frg.FreightWeighing.Add(frgw);
                    frgw.FreightWeighingMaterial.Add(frgm);
                    
                    ControlObjectContext.AddToFreightSet(frg);

                    // save the first weighing data in the freight
                    SaveFirstWeighingData(frg, ControlObjectContext);

                    // assign the weighing number
                    frg.AssignFreightNumber(ControlObjectContext);
                    LabelFreightNr.Text = frg.OurReference.ToString();
                    CurrentWeighingId = frg.Id;
                    frg.Description = "Weegbon " + frg.OurReference + " / " + Common.CurrentClientDateTime(Session).ToString();
                    frg.FreightType = "Weighing";

                    // and save to persistent storage
                    ControlObjectContext.SaveChanges(System.Data.Objects.SaveOptions.DetectChangesBeforeSave);

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

                    // reset the weighing id when not saved
                    CurrentWeighingId = Guid.Empty;
                }
            }

            if (Success)
            {
                // when success advance panel
                CurrentPageNr++;
            }
            return Success;
        }

        protected bool ProcessSecondWeighing()
        {
            ModelTMSContainer ControlObjectContext = new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session);
            bool Success = false;

            // start transaction
            using (TransactionScope TS = new TransactionScope())
            {
                try
                {
                    // process freight
                    Freight frg = Freight.SelectFreightByFreightId(CurrentWeighingId, ControlObjectContext);

                    // add second weighing data
                    SaveFirstWeighingData(frg, ControlObjectContext);
                    SaveSecondWeighingData(frg, ControlObjectContext);

                    // generate the invoice for weighing if required
                    if (CheckBoxWeighingActionPay.Checked)
                    {
                        Invoice inv = new Invoice();
                        InvoiceLine invl = new InvoiceLine();

                        // create invoice
                        ControlObjectContext.AddToInvoiceSet(inv);
                        inv.Description = frg.Description;
                        inv.InvoiceType = "Buy";
                        inv.Relation = frg.FromRelation;
                        inv.Location = frg.SourceOrDestinationLocation;
                        inv.Ledger = ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.LedgerSet", "Id", Guid.Parse(ComboBoxLedger.SelectedValue))) as Ledger; 
                        invl.Invoice = inv;
                        invl.Description = "Wegen";
                        invl.OriginalPrice = frg.SourceOrDestinationLocation.DefaultWeighingTariff;
                        invl.VATPercentage = frg.SourceOrDestinationLocation.DefaultVATPercentage;
                        Location loc = ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.LocationSet", "Id", Guid.Parse(ComboBoxWeighingLocation.SelectedValue))) as Location;
                        invl.LedgerBookingCode = loc.DefaultWeighingTariffBookingCode;
                        inv.RecalcTotals();

                        // only process the invoice for cash ledgers
                        if (inv.Ledger.LedgerType == "Cash") { inv.ProcessInvoice(ControlObjectContext, inv.Id, true, Common.CurrentClientDateTime(Session)); }

                        LabelGeneratedInvoiceId.Text = inv.Id.ToString();
                    }

                    // and save to persistent storage
                    ControlObjectContext.SaveChanges(System.Data.Objects.SaveOptions.DetectChangesBeforeSave);

                    // commit the transaciton
                    TS.Complete();
                    Success = true;

                    CurrentWeighingId = frg.Id;
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
                CurrentPageNr++;
            }
            return Success;
        }

        protected void ButtonDestroyOrderAndBack_Click(object sender, EventArgs e)
        {
            // check whether this weighing may be deleted. If this is not the case then inform the user that this weighing is already processed.
            ModelTMSContainer ControlObjectContext = new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session);
            bool Success = false;

            // start transaction
            using (TransactionScope TS = new TransactionScope())
            {
                try
                {
                    // delete freight
                    Freight frg = Freight.SelectFreightByFreightId(CurrentWeighingId, ControlObjectContext);
                    ControlObjectContext.DeleteObject(frg);

                    // and save to persistent storage
                    ControlObjectContext.SaveChanges(System.Data.Objects.SaveOptions.DetectChangesBeforeSave);

                    // commit the transaciton
                    TS.Complete();
                    Success = true;

                    CurrentWeighingId = Guid.Empty;
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
                // when success reverse panel
                CurrentPageNr--;
            }
        }

        protected void ButtonNewOrder_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsoluteUri); 
        }

        protected void ButtonInvoiceOrder_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebFormFreightInvoice.aspx?FreightId=" + CurrentWeighingId.ToString());
        }

        protected void ButtonSortOrder_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebFormFreightNew.aspx?FreightId=" + CurrentWeighingId.ToString());
        }

        protected void ButtonSecondOrder_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebFormFreightWeigh.aspx?FreightId=" + CurrentWeighingId.ToString());
        }

        protected void RadioButtonListBuyOrSell_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetCustomerType();

            FixUpComboBoxProducts();
        }

        private void SetCustomerType()
        {
            if (RadioButtonListBuyOrSell.SelectedIndex == 0)
            {
                LabelCustomerType.Text = "Creditor";
                LabelMaterialInvoiceType.Text = "Buy";
            }
            else
            {
                LabelCustomerType.Text = "Debtor";
                LabelMaterialInvoiceType.Text = "Sell";
            }
        }

        private void FixUpComboBoxProducts()
        {
            try
            {
                ComboBoxProduct.DataBind();
            }
            catch
            {
                ComboBoxProduct.SelectedIndex = -1;
            }
        }

        public void SetCorrectLocationCashLedger()
        {
            ModelTMSContainer ControlObjectContext = new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session);
            Location loc = ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.LocationSet", "Id", Guid.Parse(ComboBoxWeighingLocation.SelectedValue))) as Location;

            ComboBoxLedger.DataBind();
            ListItem li = ComboBoxLedger.Items.FindByValue(loc.CashLedger.Id.ToString());
            if (li != null)
            {
                li.Selected = true;
            }
        }

        public void SaveFirstWeighingData(Freight frg, ModelTMSContainer ControlObjectContext)
        {
            FreightWeighing fgw = frg.FreightWeighing.First<FreightWeighing>();
            FreightWeighingMaterial fwm = fgw.FreightWeighingMaterial.First<FreightWeighingMaterial>();

            frg.FreightDirection = "To warehouse";
            if (RadioButtonListBuyOrSell.SelectedIndex == 1)
            {
                frg.FreightDirection = "To customer";
            }
            frg.SourceOrDestinationLocation = ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.LocationSet", "Id", Guid.Parse(ComboBoxWeighingLocation.SelectedValue))) as Location;
            frg.FreightDateTime = CalendarWithTimeControlDateTime1.SelectedDateTime;
            frg.FreightStatus = "2nd weighing";

            fgw.Key1 = TextBoxKey1.Text;
            try { fgw.Weight1 = Convert.ToDouble(TextBoxGrossWeight.Text); }  catch { };
            fgw.IsDriverInTruck = CheckBoxIsDriverInTruck.Checked;
            fgw.WeighingDateTime1 = frg.FreightDateTime;
            fgw.Description = TextBoxPMV.Text;

            frg.OurTruck = null;
            if (ComboBoxOurPlate.SelectedValue != "")
            {
                frg.OurTruck = ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.TruckSet", "Id", Guid.Parse(ComboBoxOurPlate.SelectedValue))) as Truck;
            }

            frg.YourTruckPlate = TextBoxCustomerPlate.Text;
            frg.YourDriverName = TextBoxCustomerID.Text;

            frg.FromRelation = ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.RelationSet", "Id", Guid.Parse(ComboBoxCustomer.SelectedValue))) as Relation;
            LabelCustomerGuid.Text = frg.FromRelation.Id.ToString();

            fwm.GrossWeight = fgw.Weight1;
            fwm.Material = ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.MaterialSet", "Id", Guid.Parse(ComboBoxProduct.SelectedValue))) as Material;
            fwm.Description = fwm.Material.Description;
        }

        public void LoadFirstWeighingData(Freight frg)
        {
            DataBind();

            FreightWeighing fgw = frg.FreightWeighing.First<FreightWeighing>();
            FreightWeighingMaterial fwm = fgw.FreightWeighingMaterial.First<FreightWeighingMaterial>();

            TextBoxOrderNumber.Text = frg.OurReference.ToString();
            TextBoxDescription.Text = frg.Description;

            if (frg.FreightDirection == "To warehouse")
            {
                RadioButtonListBuyOrSell.SelectedIndex = 0;
            }
            else
            {
                RadioButtonListBuyOrSell.SelectedIndex = 1;
            }
            SetCustomerType();

            ComboBoxWeighingLocation.DataBind();
            ListItem li = ComboBoxWeighingLocation.Items.FindByValue(frg.SourceOrDestinationLocation.Id.ToString());
            if (li != null)
            {
                li.Selected = true;
            }
            CalendarWithTimeControlDateTime1.SelectedDateTime = frg.FreightDateTime;

            TextBoxKey1.Text = fgw.Key1;
            TextBoxGrossWeight.Text = fgw.Weight1.ToString();
            CheckBoxIsDriverInTruck.Checked = fgw.IsDriverInTruck ;
            TextBoxPMV.Text = fgw.Description;

            ComboBoxOurPlate.DataBind();
            if (frg.OurTruck != null)
            {
                li = ComboBoxOurPlate.Items.FindByValue(frg.OurTruck.Id.ToString());
                if (li != null)
                {
                    li.Selected = true;
                    ComboBoxOurPlate.SelectedIndex = ComboBoxOurPlate.Items.IndexOf(li);
                }
            }
            TextBoxCustomerPlate.Text = frg.YourTruckPlate;
            TextBoxCustomerID.Text = frg.YourDriverName;

            ComboBoxCustomer.DataBind();
            li = ComboBoxCustomer.Items.FindByValue(frg.FromRelation.Id.ToString());
            if (li != null)
            {
                li.Selected = true;
            }
            LabelCustomerGuid.Text = frg.FromRelation.Id.ToString();

            ComboBoxProduct.DataBind();
            li = ComboBoxProduct.Items.FindByValue(fwm.Material.Id.ToString());
            if (li != null)
            {
                li.Selected = true;
            }

            CurrentWeighingId = frg.Id;

            // make sure the cash ledger of the current location is set
            SetCorrectLocationCashLedger();
        }

        public void SaveSecondWeighingData(Freight frg, ModelTMSContainer ControlObjectContext)
        {
            FreightWeighing fgw = frg.FreightWeighing.First<FreightWeighing>();
            FreightWeighingMaterial fwm = fgw.FreightWeighingMaterial.First<FreightWeighingMaterial>();

            fgw.WeighingDateTime2 = CalendarWithTimeControl2.SelectedDateTime;
            fgw.Key2 = TextBoxKey2.Text;
            try { fgw.Weight2 = Convert.ToDouble("0"+TextBoxWeight2.Text); }  catch { };
            fwm.TarraWeight = fgw.Weight2;
            fwm.NetWeight = fwm.GrossWeight - fwm.TarraWeight;
            frg.TotalNetWeight = fwm.NetWeight;
            frg.Description = TextBoxDescription.Text;
            frg.Comments = TextBoxComments.Text;
            frg.FreightStatus = "Done";

            frg.RecalcTotalWeightFromWeighing();

            if (CheckBoxWeighingActionSort.Checked) { frg.FreightStatus = "In sorting"; }
            if (CheckBoxWeighingActionInvoice.Checked) { frg.FreightStatus = "To be invoiced"; }
        }

        protected void ComboBoxWeighingLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetCorrectLocationCashLedger();
            FixUpComboBoxProducts();
            ShowCorrectCustomer();
        }

        protected void ShowCorrectCustomer()
        {
            string SelVal = ComboBoxWeighingLocation.SelectedValue;

            if (SelVal == "")
            {
                if (ComboBoxWeighingLocation.Items.Count > 0)
                {
                    SelVal = ComboBoxWeighingLocation.Items[0].Value;
                }
            }

            Common.SetCustomerToDefaultOfLocation(SelVal, ComboBoxCustomer, (LabelCustomerType.Text == "Creditor" ? "Buy" : "Sell"), ControlObjectContext);
        }

        public void ForceFirstWeighing()
        {
            PanelCustomerInformation.Visible = false;
            CurrentPageNr = 10;
            EnableCorrectScreenElements();
        }
    }
}