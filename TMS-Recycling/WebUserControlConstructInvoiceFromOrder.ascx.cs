using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Common;
using System.Transactions;
using System.Data;
using System.Data.Objects;

namespace TMS_Recycling
{
    public partial class WebUserControlConstructInvoiceFromOrder : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            URLPopUpControlInvoice.URLToPopup = "WebFormPopup.aspx?UC=InvoiceBase&Id=" + LabelGeneratedInvoiceId.Text;
            WebUserControlEditOrderMaterials1.OnSaveButtonClicked += WebUserControlEditMaterials_OnSave;

            if (!IsPostBack)
            {
                EnableCurrentPageElements();
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
        }

        private void GenerateOpenOrderButtons()
        {
            // add order lines
            for (int i = 0; i < GridViewOpenOrders.Rows.Count; i++)
            {
                URLPopUpControl upc = LoadControl("URLPopUpControl.ascx") as URLPopUpControl;

                upc.URLToPopup = "WebFormPopup.aspx?UC=EditOrderMaterials&OrderNr=" + GridViewOpenOrders.Rows[i].Cells[2].Text + "&ShowAlreadyDeliveredAmount=0&ShowSaveButton=0";
                upc.Text = "Open ordermaterialen";
                
                int CellNr = GridViewOpenOrders.Rows[i].Cells.Count - 1;
                GridViewOpenOrders.Rows[i].Cells[CellNr].Controls.Clear();
                GridViewOpenOrders.Rows[i].Cells[CellNr].Controls.Add(upc);
            }
        }

        int CurrentPage
        {
            get
            {
                return Convert.ToInt32(TextBoxCurrentPage.Text);
            }
            set
            {
                TextBoxCurrentPage.Text = value.ToString();
            }
        }

        protected void EnableCurrentPageElements()
        {
            PanelCustomerInformation.Visible = true;
            PanelOpenOrders.Visible = false;
            PanelCustomerInformation.Enabled = false;
            PanelOpenOrders.Enabled = false;
            PanelTotals.Visible = false;
            PanelTotals.Enabled = false;
            PanelPreviewInvoice.Visible = false;
            PanelPreviewInvoice.Enabled = false;
            PanelAdvancePayments.Visible = false;
            PanelAdvancePayments.Enabled = false;
            ButtonContinue.Visible = false;
            ButtonDestroyOrderAndBack.Visible = false;
            ButtonNewOrder.Visible = false;
            ButtonPrintAndProcess.Visible = false;
            ButtonRevert.Visible = false;
            URLPopUpControlInvoice.Visible = false;
            WebUserControlEditOrderMaterials1.Visible = false;

            switch (CurrentPage)
            {
                case 1 :
                    PanelCustomerInformation.Enabled = true;
                    ButtonContinue.Visible = true;
                    TextBoxInvoiceDescription.Text = "Verzamelfactuur dd " + Common.CurrentClientDateTime(Session).ToString();

                    GridViewOpenOrders.DataBind();
                    break;
                case 2 :
                    if (DropDownListCustomers.SelectedValue == "") 
                    {
                        CurrentPage--;
                        EnableCurrentPageElements();
                        Common.InformUser(Page, "Selecteer aub een klant op deze lokatie. Als er geen klant is aangegeven zijn er geen open inkooporders die op factuur kunnen worden geplaatst.");
                    }
                    else
                    {
                        PanelOpenOrders.Visible = true;
                        PanelOpenOrders.Enabled = true;
                        ButtonContinue.Visible = true;
                        ButtonRevert.Visible = true;

                        WebUserControlEditAdvancePayments1.CustomerID = new System.Guid(DropDownListCustomers.SelectedValue);
                    }
                    break;
                case 3 :
                    PanelTotals.Visible = true;
                    PanelTotals.Enabled = true;
                    PanelOpenOrders.Visible = true;
                    WebUserControlEditAdvancePayments1.LoadAPLines();
                    ButtonContinue.Visible = WebUserControlEditAdvancePayments1.CustomerHasOpenAdvancePayments();
                    ButtonPrintAndProcess.Visible = !WebUserControlEditAdvancePayments1.CustomerHasOpenAdvancePayments();
                    ButtonRevert.Visible = true;

                    CountOrders();

                    if (NrOfOrders > 0)
                    {
                        LabelNrOfPurchasesNr.Text = NrOfOrders.ToString();
                        LabelTotalPriceExVatNr.Text = OrderPriceTotals.ToString();
                        LabelTotalWeightNr.Text = OrderWeightTotals.ToString();
                    }
                    else
                    {   // no orders are selected, revert to order selection
                        CurrentPage--;
                        EnableCurrentPageElements();
                        // show message to user to ask for an order selection
                        Common.InformUser(Page, "Selecteer aub minimaal één order.");
                    }

                    break;
                case 4 :
                    PanelTotals.Visible = true;
                    PanelOpenOrders.Visible = true;
                    PanelAdvancePayments.Visible = true;
                    PanelAdvancePayments.Enabled = true;

                    ButtonPrintAndProcess.Visible = true; 
                    ButtonRevert.Visible = true;

                    break;
                case 5 :
                    PanelTotals.Visible = true;
                    PanelOpenOrders.Visible = true;
                    PanelAdvancePayments.Visible = true;
                    PanelPreviewInvoice.Visible = true;
                    PanelPreviewInvoice.Enabled = true;

                    ButtonDestroyOrderAndBack.Visible = true;
                    ButtonNewOrder.Visible = true;

                    URLPopUpControlInvoice.Visible = true;

                    FrameShowInvoice.Attributes["src"] = "WebFormPopup.aspx?UC=ShowReport&d=DataSetInvoice&r=ReportInvoiceA4&Id=" + LabelGeneratedInvoiceId.Text;

                    break;
            }
        }

        private int NrOfOrders = 0;
        private double OrderWeightTotals, OrderPriceTotals;
        private void CountOrders()
        {
            NrOfOrders = 0;
            OrderWeightTotals=0;
            OrderPriceTotals = 0;

            for (int i = 0; i < GridViewOpenOrders.Rows.Count; i++)
            {
                if ((GridViewOpenOrders.Rows[i].Cells[0].Controls[1] as CheckBox).Checked)
                {
                    NrOfOrders++;

                    OrderWeightTotals = OrderWeightTotals + Convert.ToDouble(GridViewOpenOrders.Rows[i].Cells[7].Text); 
                    OrderPriceTotals = OrderPriceTotals + Convert.ToDouble(GridViewOpenOrders.Rows[i].Cells[6].Text); 
                }

            }
        }

        protected void ButtonRevert_Click(object sender, EventArgs e)
        {
            CurrentPage--;

            EnableCurrentPageElements();
        }

        protected void ButtonContinue_Click(object sender, EventArgs e)
        {
            CurrentPage++;

            EnableCurrentPageElements();
        }

        protected void ButtonPrintAndProcess_Click(object sender, EventArgs e)
        {
            ModelTMSContainer _ControlObjectContext = new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session);

            // start transaction
            using (TransactionScope TS = new TransactionScope())
            {
                try
                {
                    // create new invoice
                    Invoice NewInvoice = new Invoice();
                    NewInvoice.Relation = _ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.RelationSet", "Id", Guid.Parse(DropDownListCustomers.SelectedValue))) as Relation;
                    if (DropDownListLocations.SelectedValue != "")
                    {
                        NewInvoice.Location = _ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.LocationSet", "Id", Guid.Parse(DropDownListLocations.SelectedValue))) as Location;
                    }
                    NewInvoice.Description = TextBoxInvoiceDescription.Text;
                    NewInvoice.InvoiceNote = TextBoxInvoiceNote.Text;
                    NewInvoice.InvoiceType = LabelInvoiceType.Text ;
                    NewInvoice.BookingDateTime = Common.CurrentClientDateTime(Session);
                    NewInvoice.InvoiceNote = TextBoxInvoiceNote.Text;
                    _ControlObjectContext.AddToInvoiceSet(NewInvoice);

                    // add order lines
                    for (int i = 0; i < GridViewOpenOrders.Rows.Count; i++)
                    {
                        if ((GridViewOpenOrders.Rows[i].Cells[0].Controls[1] as CheckBox).Checked)
                        {
                            // load order
                            string Query = "SELECT VALUE it FROM OrderSet as it WHERE it.OrderNumber = @OrderNumber";
                            ObjectQuery<Order> query = new ObjectQuery<Order>(Query, _ControlObjectContext);
                            query.Parameters.Add( new ObjectParameter("OrderNumber", Convert.ToInt64(GridViewOpenOrders.Rows[i].Cells[2].Text) ) );
                            ObjectResult<Order> ilines = query.Execute(MergeOption.AppendOnly);
                            Order TempOrder = ilines.First<Order>();

                            // save location in the invoice if this was unknown yet
                            if ((TempOrder.Location != null) && (NewInvoice.Location == null))
                            {
                                NewInvoice.Location = TempOrder.Location;

                                if (NewInvoice.Ledger == null)
                                // set ledger for this invoice (default bank)
                                {
                                    NewInvoice.Ledger = NewInvoice.Location.BankLedger;
                                }
                            }
                            
                            // add to invoice
                            NewInvoice.AddOrderToInvoice(_ControlObjectContext, TempOrder);
                        }
                    }
                    
                    // subtract advance payments
                    WebUserControlEditAdvancePayments1.LoadAPLines();
                    for (int i = 0; i < WebUserControlEditAdvancePayments1.AdvancePaymentLines.Count; i++)
                    {
                        Guid TempGuid;
                        TempGuid = (WebUserControlEditAdvancePayments1.AdvancePaymentLines[i] as RelationAdvancePayment).Id;

                        // load the work or advance payment object
                        RelationAdvancePayment CurrAP = null;
                        RelationWork CurrWork = null;
                        try
                        {
                            CurrAP = _ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.RelationAdvancePaymentSet", "Id", TempGuid)) as RelationAdvancePayment;
                            NewInvoice.AddAdvancePaymentCorrection(_ControlObjectContext, CurrAP, (WebUserControlEditAdvancePayments1.AdvancePaymentLines[i] as RelationAdvancePayment).Amount, (WebUserControlEditAdvancePayments1.AdvancePaymentLines[i] as RelationAdvancePayment).Description);
                        }
                        catch (Exception) { };
                        try
                        {
                            CurrWork = _ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.RelationWorkSet", "Id", TempGuid)) as RelationWork;
                            NewInvoice.AddWorkCorrection(_ControlObjectContext, CurrWork, (WebUserControlEditAdvancePayments1.AdvancePaymentLines[i] as RelationAdvancePayment).Amount, (WebUserControlEditAdvancePayments1.AdvancePaymentLines[i] as RelationAdvancePayment).Description);
                        }
                        catch (Exception) { };
                    }

                    // process the invoice
                    if (LabelGeneratedInvoiceId.Text != "")
                    {
                        // use old invoice id as group code
                        NewInvoice.GroupCode = new Guid(LabelGeneratedInvoiceId.Text);
                    }
                    NewInvoice.GenerateInvoiceNumber(_ControlObjectContext);
                    //NewInvoice.ProcessInvoice(_ControlObjectContext, NewInvoice.GroupCode); DO NOT PROCESS INVOICE !!! CUSTOMER HAS TO DO THAT SEPERATELY !!!
                    LabelGeneratedInvoiceId.Text = NewInvoice.Id.ToString();

                    // and save to persistent storage
                    _ControlObjectContext.SaveChanges(System.Data.Objects.SaveOptions.DetectChangesBeforeSave);

                    // commit the transaciton
                    TS.Complete();

                    // when success advance panel
                    CurrentPage=5;
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

        protected void ButtonDestroyOrderAndBack_Click(object sender, EventArgs e)
        {
            ModelTMSContainer _ControlObjectContext = new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session);
            bool Success = false;

            if (LabelGeneratedInvoiceId.Text != Guid.Empty.ToString())
            {
                // start transaction
                using (TransactionScope TS = new TransactionScope())
                {
                    try
                    {

                        // roll back invoice & order
                        Invoice CorrInvoice = _ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.InvoiceSet", "Id", Guid.Parse(LabelGeneratedInvoiceId.Text))) as Invoice;

                        // unprocess
                        CorrInvoice.IsCorrected = true; // make sure user cannot edit this invoice any more
                        // if this invoice is processed then unprocess it
                        CorrInvoice.UnprocessInvoiceWithOrdersAsOptional(_ControlObjectContext, CorrInvoice.GroupCode, Common.CurrentClientDateTime(Session) );

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
                CurrentPage--;
                EnableCurrentPageElements();
            }
        }

        protected void ButtonNewOrder_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsoluteUri); 
        }

        protected void DropDownListLocations_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewOpenOrders.DataBind();
            EntityDataSourceCustomers.DataBind();
        }

        protected void GridViewOpenOrders_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                WebUserControlEditOrderMaterials1.Visible = true;
                int RowNr = Convert.ToInt32(e.CommandArgument);
                WebUserControlEditOrderMaterials1.OrderNr = Convert.ToInt64(GridViewOpenOrders.Rows[RowNr].Cells[2].Text);
                PanelOpenOrdersDetails.Enabled = (GridViewOpenOrders.Rows[RowNr].Cells[5].Text == "Open");
            }
        }

        public void SwitchPurchaseType(InvoiceType it)
        {
            switch (it)
            {
                case InvoiceType.Buy:
                    LabelInvoiceType.Text = "Buy";
                    LabelCustomerType.Text = "Creditor";
                    break;
                case InvoiceType.Sell:
                    LabelInvoiceType.Text = "Sell";
                    LabelCustomerType.Text = "Debtor";
                    break;
            }
            WebUserControlEditAdvancePayments1.SwitchPurchaseType(it);
            WebUserControlEditOrderMaterials1.SwitchPurchaseType(it);
        }

        protected void WebUserControlEditMaterials_OnSave(object sender, EventArgs e)
        {
            // remember selected orders
            List<string> Selection = new List<string>();
            for (int i = 0; i < GridViewOpenOrders.Rows.Count; i++)
            {
                if ((GridViewOpenOrders.Rows[i].Cells[0].Controls[1] as CheckBox).Checked)
                {
                    Selection.Add(GridViewOpenOrders.Rows[i].Cells[2].Text);
                }
            }

            GridViewOpenOrders.DataBind();

            // reset the selected orders
            for (int i = 0; i < GridViewOpenOrders.Rows.Count; i++)
            {
                (GridViewOpenOrders.Rows[i].Cells[0].Controls[1] as CheckBox).Checked = false;
                if ( Selection.IndexOf(GridViewOpenOrders.Rows[i].Cells[2].Text) >= 0)
                {
                    (GridViewOpenOrders.Rows[i].Cells[0].Controls[1] as CheckBox).Checked = true;
                }
            }

        }
    }
}