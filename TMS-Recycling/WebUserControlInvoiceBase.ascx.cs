using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Transactions;
using System.Data.Objects;
using System.Data;
using System.Data.Common;

namespace TMS_Recycling
{
    public partial class WebUserControlInvoiceBase : ClassTMSUserControl
    {
        public InvoiceType InvoiceType
        {
            get
            {
                if (LabelInvoiceType.Text == "Buy")
                {
                    return InvoiceType.Buy;
                }
                if (LabelInvoiceType.Text == "Sell")
                {
                    return InvoiceType.Sell;
                }
                if (LabelInvoiceType.Text == "BuyLedger")
                {
                    return InvoiceType.BuyLedger;
                }
                if (LabelInvoiceType.Text == "SellLedger")
                {
                    return InvoiceType.SellLedger;
                }
                if (LabelInvoiceType.Text == "Rent")
                {
                    return InvoiceType.Rent;
                }
                return InvoiceType.Unknown;
            }
            set
            {
                if ((value == InvoiceType.Buy) || (value == InvoiceType.Unknown))
                {
                    LabelInvoiceType.Text = "Buy";
                    LabelRelationType.Text = "Creditor";
                    LabelWorkType.Text = "ByUs";
                    LabelAPType.Text = "Paid";
                    LabelReportType.Text = "ReportInvoiceA4";
                }
                if (value == InvoiceType.Sell)
                {
                    LabelInvoiceType.Text = "Sell";
                    LabelRelationType.Text = "Debtor";
                    LabelWorkType.Text = "ByCustomer";
                    LabelAPType.Text = "Received";
                    LabelReportType.Text = "ReportInvoiceA4";
                }
                if (value == InvoiceType.BuyLedger) 
                {
                    LabelInvoiceType.Text = "Buy";
                    LabelRelationType.Text = "Creditor";
                    LabelWorkType.Text = "ByUs";
                    LabelAPType.Text = "Paid";
                    LabelReportType.Text = "ReportInvoiceRentA4";
                }
                if (value == InvoiceType.SellLedger)
                {
                    LabelInvoiceType.Text = "Sell";
                    LabelRelationType.Text = "Debtor";
                    LabelWorkType.Text = "ByCustomer";
                    LabelAPType.Text = "Received";
                    LabelReportType.Text = "ReportInvoiceRentA4";
                } 
                if (value == InvoiceType.Rent)
                {
                    LabelInvoiceType.Text = "Rent";
                    LabelRelationType.Text = "Debtor";
                    LabelWorkType.Text = "ByCustomer";
                    LabelAPType.Text = "Received";
                    LabelReportType.Text = "ReportInvoiceRentA4";
                }
            }
        }
        
        public void InitUserControl()
        {
            SetName = "Invoice";

            if (!IsPostBack)
            {
                Common.AddInvoiceStatusList(DropDownList_InvoiceStatus_SelectedValue.Items, true);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // initaliaze this user control
            InitUserControl();

            // set show invoice popup button
            if (Visible)
            {
                if (!IsPostBack)
                {
                    if (Request.Params["Id"] != null)
                    {
                        KeyID = new Guid(Request.Params["Id"].ToString());
                    }
                }

                URLPopUpControlShowInvoice.Visible = (DataItem as Invoice) != null;
                if ((DataItem as Invoice) != null)
                {
                    URLPopUpControlShowInvoice.URLToPopup = "WebFormPopup.aspx?UC=ShowReport&d=DataSetInvoice&r=" + LabelReportType.Text + "&Id=" + (DataItem as Invoice).Id.ToString();
                }
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (DataItem != null)
            {
                Invoice inv = DataItem as Invoice;

                // Update the VAT price for this location if not set yet
                UpdateLocationVATPrice();


                // check if we know the invoice type
                if (LabelInvoiceType.Text == "Unknown")
                {
                    InvoiceType = Common.DetermineInvoiceType(inv.InvoiceType, inv.InvoiceSubType);
                }

                // lookup the VAT percentage
                if ( (TextBoxFreeLineVATPercentage.Text == "")  && ((DataItem as Invoice).Relation != null) )
                {
                    if ((DataItem as Invoice).Location != null)
                    {
                        TextBoxFreeLineVATPercentage.Text = (DataItem as Invoice).Relation.MustPayVat() ? (DataItem as Invoice).Location.DefaultVATPercentage.ToString() : "0";
                    }
                    else
                    {
                        TextBoxFreeLineVATPercentage.Text = (DataItem as Invoice).Relation.MustPayVat() ? "" : "0";
                    }
                }

                // set items to disabled if editing is not allowed
                Boolean ena;
                ena = ((inv.InvoiceStatus == "Open") || (inv.InvoiceStatus == "PPaid")) && (!(DataItem as Invoice).IsCorrected);
                TextBox_Description_Text.Enabled = ena;
                CheckBox_IsCorrected_Checked.Enabled = ena;
                CalendarWithTimeControl_BookingDateTime_SelectedDateTime.Enabled = ena;
                DropDownList_Relation.Enabled = ena;
                DropDownList_Location.Enabled = ena;
                GridViewOrderLines.Enabled = ena;
                //GridViewOrders.Enabled = ena;
                TextBoxFreeLinePrice.Enabled = ena;
                TextBoxFreeLineVATPercentage.Enabled = ena;
                ButtonToProcessed.Visible = ena;
                ButtonAddInvoiceLine.Visible = ena;
                ButtonDelete.Visible = ena;
                LabelProcessingInfo.Visible = ena;
                TextBoxFreeLineVATPercentage.Enabled = ena;
                DropDownListFreeLineLedgerBookingCode.Enabled = ena;
                TextBox_InvoiceNote.Enabled = ena;
                DropDownList_Ledger.Enabled = ena;
                ButtonAddAPCorrection.Visible = ena;
                ButtonAddWorkCorrection.Visible = ena;
                //TextBox_AlreadyPaid.Enabled = ena;
                TextBox_DiscountPercentage.Enabled = ena;
                URLPopUpControlPartialPayment.Visible = ena;

                PanelLinkedOrders.Visible = (InvoiceType == InvoiceType.Buy) || (InvoiceType == InvoiceType.Sell);

                ButtonCorrectInvoice.Visible = ((DataItem as Invoice).InvoiceStatus == "Paid") && (!(DataItem as Invoice).IsCorrected);
                ButtonCreateClonedInvoiceAndOrders.Visible = ((DataItem as Invoice).InvoiceStatus == "Open") && ((DataItem as Invoice).IsCorrected);

                URLPopUpControlPartialPayment.URLToPopup = "WebFormPopUp.aspx?uc=InvoicePartialPayment&Id=" + inv.Id.ToString();
            }
        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            StandardButtonCancelClickHandler(sender, e);
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            SaveDataIntoDataItemFromControls();

            (DataItem as Invoice).RecalcTotals();

            StandardButtonSaveClickHandler(sender, e);

            RebindControls();
            EntityDataSourceInvoiceLines.DataBind();
        }

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            // start transaction 
            using (TransactionScope TS = new TransactionScope())
            {
                (DataItem as Invoice).RemoveAllOrdersFromInvoice(ControlObjectContext);
                if (StandardButtonDeleteClickMethod(sender, e))
                {
                    TS.Complete();
                }
                else
                {
                    TS.Dispose();
                }
            }
        }

        protected void ButtonToProcessed_Click(object sender, EventArgs e)
        {
            // start transaction 
            using (TransactionScope TS = new TransactionScope())
            {
                try
                {
                    // first save this information
                    if (StandardSaveHandler(sender, e, false))
                    {
                        // process order 
                        (DataItem as Invoice).ProcessInvoice(ControlObjectContext, (DataItem as Invoice).GroupCode, true, Common.CurrentClientDateTime(Session));

                        ControlObjectContext.SaveChanges(SaveOptions.DetectChangesBeforeSave);

                        // commit the transaciton
                        TS.Complete();
                    }
                    else
                    {
                        TS.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    // rollback
                    TS.Dispose();

                    // inform user
                    Common.InformUserOnTransactionFail(ex, Page);
                }
            }

            RebindControls();
            DataBind();
        }

        protected void ButtonAddInvoiceLine_Click(object sender, EventArgs e)
        {
            StandardSaveHandler(sender,e,false);

            InvoiceLine il = new InvoiceLine();

            try
            {
                il.Invoice = (DataItem as Invoice);
                il.LedgerBookingCode = ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.LedgerBookingCodeSet", "Id", Guid.Parse(DropDownListFreeLineLedgerBookingCode.SelectedValue))) as LedgerBookingCode;
                il.Ledger = il.Invoice.Ledger;
                il.Description = TextBoxFreeLine.Text;
                il.OriginalPrice = Convert.ToDouble(TextBoxFreeLinePrice.Text);
                il.VATPercentage = Convert.ToDouble(TextBoxFreeLineVATPercentage.Text);

                il.Invoice.InvoiceLine.Add(il);
                il.LineNumber = il.Invoice.InvoiceLine.Count;

                il.Invoice.RecalcTotals();
            }
            catch { }

            try
            {
                ControlObjectContext.SaveChanges(SaveOptions.DetectChangesBeforeSave);
            }
            catch (Exception ex)
            {
                Common.InformUserOnTransactionFail(ex, Page);
            }

            RebindControls();
            DataBind();

            TextBoxFreeLine.Text = "";
            TextBoxFreeLinePrice.Text = "";
        }

        protected void ButtonAddAPCorrection_Click(object sender, EventArgs e)
        {
            StandardSaveHandler(sender, e, false);

            try
            {
                RelationAdvancePayment AdvPay = ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.RelationAdvancePaymentSet", "Id", Guid.Parse(DropDownListAPCorrection.SelectedValue))) as RelationAdvancePayment;
                (DataItem as Invoice).AddAdvancePaymentCorrection(ControlObjectContext,
                    AdvPay,
                    Convert.ToDouble(TextBoxCorrectionAP.Text) * -1,
                    LabelAdvancePayment.Text + " " + AdvPay.Description);

                (DataItem as Invoice).RecalcTotals();
            }
            catch { }

            try
            {
                ControlObjectContext.SaveChanges(SaveOptions.DetectChangesBeforeSave);
            }
            catch (Exception ex)
            {
                Common.InformUserOnTransactionFail(ex, Page);
            }

            RebindControls();
            DataBind();
        }

        protected void ButtonAddWorkCorrection_Click(object sender, EventArgs e)
        {
            StandardSaveHandler(sender, e, false);

            InvoiceLine il = new InvoiceLine();

            try
            {
                RelationWork RelWork = ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.RelationWorkSet", "Id", Guid.Parse(DropDownListWorkCorrection.SelectedValue))) as RelationWork;

                (DataItem as Invoice).AddWorkCorrection(ControlObjectContext,
                     RelWork,
                     Convert.ToDouble(TextBoxWorkCorrectionAmount.Text) * -1,
                     LabelWorkCorrection.Text + " " + RelWork.Description);

                (DataItem as Invoice).RecalcTotals();
            }
            catch { }

            try
            {
                ControlObjectContext.SaveChanges(SaveOptions.DetectChangesBeforeSave);
            }
            catch (Exception ex)
            {
                Common.InformUserOnTransactionFail(ex, Page);
            }

            RebindControls();
            DataBind();
        }

        protected void GridViewOrderLines_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            e.Cancel = true;

            InvoiceLine TempLine;
            TempLine = ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.InvoiceLineSet", "Id", Guid.Parse( e.Keys[0].ToString() ))) as InvoiceLine;

            if (TempLine.Material != null)
            {
                Page.RegisterClientScriptBlock("Alert", "<script>alert('U mag deze regel niet verwijderen. Deze regel bevat een materiaal uit een order.');</script>");
            }
            else
            {
                (DataItem as Invoice).InvoiceLine.Remove(TempLine);
                ControlObjectContext.DeleteObject(TempLine);
                (DataItem as Invoice).RecalcTotals();

                ControlObjectContext.SaveChanges(SaveOptions.DetectChangesBeforeSave);

                RebindControls();
                DataBind();
            }
        }

        protected void GridViewOrderLines_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            e.Cancel = true;

            InvoiceLine il;
            il = ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.InvoiceLineSet", "Id", Guid.Parse(e.Keys[0].ToString()))) as InvoiceLine;

            try
            {
                il.Description = e.NewValues[0].ToString();
                il.Amount = Convert.ToDouble(e.NewValues[1].ToString());
                il.PricePerUnit = Convert.ToDouble(e.NewValues[2].ToString());
                il.DiscountPercentage = Convert.ToDouble(e.NewValues[3].ToString());
                il.VATPercentage = Convert.ToDouble(e.NewValues[4].ToString());
                il.Invoice.RecalcTotals();
            }
            catch { }

            try
            {
                ControlObjectContext.SaveChanges(SaveOptions.DetectChangesBeforeSave);
            }
            catch (Exception ex)
            {
                Common.InformUserOnTransactionFail(ex, Page);
            }

            RebindControls();
            DataBind();

            GridViewOrderLines.EditIndex = -1;
        }

        protected void ButtonCorrectInvoice_Click(object sender, EventArgs e)
        {
            // correct invoice, the user may decide whether or not to create a new invoice with these orders
            // start transaction
            using (TransactionScope TS = new TransactionScope())
            {
                try
                {
                    // process order 
                    (DataItem as Invoice).UnprocessInvoice(ControlObjectContext, (DataItem as Invoice).GroupCode, Common.CurrentClientDateTime(Session));
                    (DataItem as Invoice).IsCorrected = true;

                    ControlObjectContext.SaveChanges(SaveOptions.DetectChangesBeforeSave);

                    // commit the transaciton
                    TS.Complete();

                    // inform user
                    Page.RegisterClientScriptBlock("Alert", "<script>alert('De factuur en eventueel bijbehordende orders zijn volledig tegengeboekt. U kunt de factuur opnieuw aanmaken.');</script>");
                }
                catch (Exception ex)
                {
                    // rollback
                    TS.Dispose();

                    // inform user
                    Common.InformUserOnTransactionFail(ex, Page);
                }
            }

            RebindControls();
            DataBind();

        }

        protected void ButtonCreateClonedInvoiceAndOrders_Click(object sender, EventArgs e)
        {
            // start transaction
            using (TransactionScope TS = new TransactionScope())
            {
                try
                {
                    // clone order 
                    Invoice NewInvoice = (DataItem as Invoice).CloneToNew(ControlObjectContext, false, null, Common.CurrentClientDateTime(Session));

                    ControlObjectContext.SaveChanges(SaveOptions.DetectChangesBeforeSave);

                    Response.Redirect( Request.AppRelativeCurrentExecutionFilePath + "?InvoiceNumber=" + NewInvoice.InvoiceNumber.ToString(), false);

                    // commit the transaciton
                    TS.Complete();

                    // inform user
                    Common.InformUser(Page, "De factuur en eventueel bijbehordende orders zijn gekloond. De factuur wordt nu geopend.");
                }
                catch (Exception ex)
                {
                    // rollback
                    TS.Dispose();

                    // inform user
                    Common.InformUserOnTransactionFail(ex, Page);
                }
            }

        }

        protected void GridViewOrders_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem != null)
            {
                TableCell tc = e.Row.Cells[e.Row.Cells.Count - 1];

                DbDataRecord ddr = e.Row.DataItem as DbDataRecord;
                URLPopUpControl upc = LoadControl("URLPopUpControl.ascx") as URLPopUpControl;

                upc.URLToPopup = "WebFormPopup.aspx?UC=OrderBase&OrderId=" + ddr.GetValue(0).ToString();
                upc.Text = "Toon order";
                tc.Controls.Add(upc);
            }
        }

        protected void DropDownList_Location_SelectedIndexChanged(object sender, EventArgs e)
        {
            EntityDataSourceLedgers.DataBind();

            // Update the VAT price for this location
            if (DropDownList_Location.SelectedValue != "")
            {
                UpdateLocationVATPriceFromDropDown();
            }
        }

        public void UpdateLocationVATPriceFromDropDown()
        {
            Location loc = ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.LocationSet", "Id", new Guid(DropDownList_Location.SelectedValue))) as Location;
            TextBoxFreeLineVATPercentage.Text = loc.DefaultVATPercentage.ToString();
        }

        public void UpdateLocationVATPrice()
        {
            if (DataItem != null)
            {
                Invoice inv = (DataItem as Invoice);
                if (inv.Location != null)
                {
                    TextBoxFreeLineVATPercentage.Text = inv.Location.DefaultVATPercentage.ToString();
                }
                else
                {
                    UpdateLocationVATPriceFromDropDown();
                }
            }
        }

        protected void GridViewOrders_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "UnlinkOrder")
            {
                if (GridViewOrderLines.Enabled)
                {
                    int Row = Convert.ToInt32(e.CommandArgument);
                    int OrderNumber = Convert.ToInt32(GridViewOrders.Rows[Row].Cells[1].Text);

                    // load order
                    string Query = "SELECT VALUE it FROM OrderSet as it WHERE it.OrderNumber = @OrderNumber";
                    ObjectQuery<Order> query = new ObjectQuery<Order>(Query, ControlObjectContext);
                    query.Parameters.Add(new ObjectParameter("OrderNumber", OrderNumber));
                    ObjectResult<Order> ilines = query.Execute(MergeOption.AppendOnly);

                    Order TempOrder = ilines.First<Order>();

                    // start transaction
                    using (TransactionScope TS = new TransactionScope())
                    {
                        try
                        {
                            TempOrder.Invoice.RemoveOrderFromInvoice(ControlObjectContext, TempOrder);

                            // commit the transaciton
                            ControlObjectContext.SaveChanges(SaveOptions.DetectChangesBeforeSave);
                            TS.Complete();

                            // inform user
                            Common.InformUser(Page, "De order is van de factuur ontkoppeld. De orderregels zijn van deze factuur verwijderd. U dient deze order nogmaals op ean factuur te plaatsen.");
                        }
                        catch (Exception ex)
                        {
                            // rollback
                            TS.Dispose();

                            // inform user
                            Common.InformUserOnTransactionFail(ex, Page);
                        }
                    }

                    // reload data in the interface
                    DataBind();
                    RebindControls();
                }
                else
                {
                    Common.InformUser(Page, "Deze order kan niet worden ontkoppeld. De factuur is reeds verwerkt.");
                }
            }
        }

        public void SwitchPurchaseType(InvoiceType it)
        {
            InvoiceType = it;
        }

        protected void URLPopUpControlPartialPayment_OnBeforePopUpOpened(object sender, EventArgs e)
        {
            ButtonSave_Click(sender, e);
        }

        protected void URLPopUpControlPartialPayment_OnPopupClosed(object sender, EventArgs e)
        {
            RebindControls();
        }

    }

}