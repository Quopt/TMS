using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Common;
using AjaxControlToolkit;
using System.Data.Objects;
using System.Transactions;
using System.Data;

namespace TMS_Recycling
{
    public partial class WebUserControlRentReturn : ClassTMSUserControl
    {
        public int PageNr
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
                DropDownListLocations.DataBind();
                DropDownListCustomers.DataBind();

                bool CustIdSet = false;
                bool LocIdSet = false;

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

                if (LocIdSet && CustIdSet)
                {
                    PageNr = 2;
                }
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            ShowCorrectElements();
        }

        public void ShowCorrectElements()
        {
            PanelCustomerInformation.Enabled = false;
            PanelRentOverview.Enabled = false;
            PanelInvoice.Enabled = false;
            PanelCreatedInvoice.Enabled = false;
            PanelCustomerInformation.Visible = true;
            PanelRentOverview.Visible = false;
            PanelInvoice.Visible = false;
            PanelCreatedInvoice.Visible = false;

            ButtonPrevious.Visible = false;
            ButtonNext.Visible = false;
            ButtonProcess.Visible = false;
            ButtonUnprocess.Visible = false;
            ButtonNewRentReturn.Visible = false;

            FrameShowInvoiceA.Visible = false;

            URLPopUpControlOpenInvoice.Visible = false;

            try
            {
                switch (PageNr)
                {
                    case 1:
                        PanelCustomerInformation.Enabled = true;
                        ButtonNext.Visible = true;
                        break;
                    case 2:
                        PanelRentOverview.Enabled = true;
                        PanelRentOverview.Visible = true;
                        ButtonPrevious.Visible = true;
                        ButtonNext.Visible = true;
                        break;
                    case 3:
                        PanelRentOverview.Visible = true;
                        PanelInvoice.Visible = true;
                        PanelInvoice.Enabled = true;

                        ButtonPrevious.Visible = true;

                        // check if there is at least one rental item activity selected, otherwise inform user & cycle back
                        if (AmountOfRentalItemsSelected() <= 0)
                        {
                            Common.InformUser(Page, "U dient minimaal één verhuring te selecteren.");
                            PageNr--;
                            ShowCorrectElements();
                        }
                        else
                        {
                            // otherwise enable correct checboxes for invoices
                            CheckBoxDisableDamagedAndLost.Visible = NrOfDamagedOrLostItems() > 0;

                            CheckBoxInvoice.Visible = NrOfUnInvoicedItems() > 0;

                            CheckBoxBailReturn.Visible = NrOfInvoicedItems() > 0;
                            LabelBailAmount.Visible = CheckBoxBailReturn.Visible;
                            TextBoxBail.Visible = CheckBoxBailReturn.Visible;

                            ButtonProcess.Visible = true;

                            ComboBoxLedger.DataBind();
                        }

                        break;
                    case 4:
                        PanelRentOverview.Visible = true;
                        PanelInvoice.Visible = true;
                        PanelCreatedInvoice.Visible = LabelInvoiceId.Text != "";
                        ButtonUnprocess.Visible = true;
                        ButtonNewRentReturn.Visible = true;

                        if (LabelInvoiceId.Text != "")
                        {
                            URLPopUpControlOpenInvoice.Visible = true;
                            URLPopUpControlOpenInvoice.URLToPopup = "WebFormPopup.aspx?UC=InvoiceBase&Id=" + LabelInvoiceId.Text;
                            FrameShowInvoiceA.Attributes["src"] = "WebFormPopup.aspx?UC=ShowReport&d=DataSetInvoice&r=ReportInvoiceRentA4&Id=" + LabelInvoiceId.Text;
                            FrameShowInvoiceA.Visible = true;
                        }

                        break;
                }
            }
            catch (Exception ex)
            {
                // inform user
                Common.InformUserOnGeneralFail(ex, Page, "Het scherm kan niet goed worden getoond. Mogelijk heeft iemand anders de onderliggende gegevens gewijzigd. Ga niet door maar begin opnieuw met deze actie door deze uit het rechtermenu te kiezen.");
            }
        }

        public Int64 RentalNr
        {
            get
            {
                try
                {
                    return Convert.ToInt64(TextBoxRentNumber.Text);
                }
                catch
                {
                }
                return -1;
            }
        }

        private string MaterialAvailableQuery = "select value it from RentalItemSet as it " +
            "where it.IsActive and (it.RentalType.Id = @RentalType) and (it.Location.Id = @LocationID) and " +
            "( (it.ItemState = \"Available\") or (it.ItemState = \"Rented\" ) ) and " +
            "( it not in ( " +
            "	select value itx2.RentalItem " +
            "	from RentalItemActivitySet as itx2 " +
            "	where itx2.RentalItem.Id = it.Id and " +
            " (  ((@StartDate <= itx2.RentStartDateTime) and (@EndDate >= itx2.RentStartDateTime)) ) or " +
            " (  ((@StartDate >= itx2.RentStartDateTime) and (@StartDate <= itx2.RentEndStartDateTime)) " +
            "	 )  ) ) order by it.Description";

        protected void GridViewRentedOutMaterials_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DateTime EndDateRent;

            if (e.Row.DataItem != null)
            { // register the row id in the hint 
                (e.Row.Cells[0].Controls[1] as CheckBox).ToolTip = (e.Row.DataItem as DbDataRecord).GetValue(0).ToString();
                (e.Row.Cells[0].Controls[1] as CheckBox).Checked = RentalNr > 0;

                // get the combobox
                ComboBox cbx = (e.Row.Cells[8].Controls[3] as ComboBox);

                // grab the possible new enddate from the grid
                EndDateRent = new DateTime(2100, 1, 1);
                try
                {
                    EndDateRent = Convert.ToDateTime((e.Row.DataItem as DbDataRecord).GetValue(4));
                    EndDateRent = Convert.ToDateTime((e.Row.Cells[4].Controls[0]).ToString()); // original value
                }
                catch { }

                ObjectQuery Results = ControlObjectContext.CreateQuery<RentalItem>(MaterialAvailableQuery,
                    new ObjectParameter("RentalType", (e.Row.DataItem as DbDataRecord).GetValue(2)),
                    new ObjectParameter("LocationId", (e.Row.DataItem as DbDataRecord).GetValue(1)),
                    new ObjectParameter("StartDate", (e.Row.DataItem as DbDataRecord).GetValue(3)),
                    new ObjectParameter("EndDate", EndDateRent),
                    new ObjectParameter("BorderEndDate", new DateTime(2100, 1, 1)));

                LoadComboBox(cbx, Results);

                URLPopUpControl upc = (e.Row.Cells[8].Controls[5] as URLPopUpControl);
                // get the corresponding RentalItemActivitySet
                Guid Temp = new Guid((e.Row.DataItem as DbDataRecord).GetValue(0).ToString());
//                RentalItemActivity ria = ControlObjectContext.RentalItemActivitySet.Where(m => m.Id == Temp).First<RentalItemActivity>();
                RentalItemActivity ria = ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.RentalItemActivitySet", "Id", Temp )) as RentalItemActivity;
                upc.Visible = false;
                if ((ria != null) && (ria.InvoiceLine != null))
                {
                    upc.Visible = true;
                    upc.URLToPopup = "WebFormPopup.aspx?UC=InvoiceBase&Id=" + ria.InvoiceLine.Invoice.Id.ToString();
                }
            }
        }

        private static void LoadComboBox(ComboBox cbx, ObjectQuery Results)
        {
            // load the combo
            ListItem li = cbx.Items[0];
            cbx.Items.Clear();
            cbx.Items.Add(li);
            foreach (RentalItem ri in Results)
            {
                cbx.Items.Add(new ListItem(ri.Description, ri.Id.ToString()));
            }
        }

        protected void ButtonNewRentReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsoluteUri);
        }

        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            // update the availability in the combo box of this line
            Button btn = (sender as Button);
            GridViewRow gvr = btn.Parent.Parent as GridViewRow;
            Guid Temp = new Guid((gvr.Cells[0].Controls[1] as CheckBox).ToolTip);

            // get the corresponding RentalItemActivitySet
//            RentalItemActivity ria = ControlObjectContext.RentalItemActivitySet.Where(m => m.Id == Temp).First<RentalItemActivity>();
            RentalItemActivity ria = ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.RentalItemActivitySet", "Id", Temp)) as RentalItemActivity;


            // grab the possible new enddate from the grid
            DateTime EndDateRent = ria.RentEndStartDateTime;
            try
            {
                EndDateRent = Convert.ToDateTime((gvr.Cells[4].Controls[1] as CalendarWithTimeControl).Text);
            }
            catch { }

            (gvr.Cells[4].Controls[1] as CalendarWithTimeControl).Text = EndDateRent.ToString();


            ObjectQuery Results = ControlObjectContext.CreateQuery<RentalItem>(MaterialAvailableQuery,
                new ObjectParameter("RentalType", ria.RentalItem.RentalType.Id),
                new ObjectParameter("LocationId", ria.RentLedger.Location.Id),
                new ObjectParameter("StartDate", ria.RentStartDateTime),
                new ObjectParameter("EndDate", EndDateRent),
                new ObjectParameter("BorderEndDate", new DateTime(2100, 1, 1)));

            // get the combobox to reload
            ComboBox cbx = (gvr.Cells[8].Controls[3] as ComboBox);
            LoadComboBox(cbx, Results);
        }

        public int AmountOfRentalItemsSelected()
        {
            int Result = 0;

            foreach (GridViewRow gvr in GridViewRentedOutMaterials.Rows)
            {
                if ((gvr.Cells[0].Controls[1] as CheckBox).Checked)
                {
                    Result++;
                }
            }

            return Result;
        }

        public int NrOfDamagedOrLostItems()
        {
            int Result = 0;

            foreach (GridViewRow gvr in GridViewRentedOutMaterials.Rows)
            {
                if (((gvr.Cells[0].Controls[3] as RadioButtonList).SelectedIndex >= 1) && ((gvr.Cells[0].Controls[1] as CheckBox).Checked))
                {
                    Result++;
                }
            }

            return Result;
        }

        public int NrOfUnInvoicedItems()
        {
            int Result = 0;

            foreach (GridViewRow gvr in GridViewRentedOutMaterials.Rows)
            {
                Guid Temp = new Guid((gvr.Cells[0].Controls[1] as CheckBox).ToolTip);

                // get the corresponding RentalItemActivitySet
//                RentalItemActivity ria = ControlObjectContext.RentalItemActivitySet.Where(m => m.Id == Temp).First<RentalItemActivity>();
                RentalItemActivity ria = ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.RentalItemActivitySet", "Id", Temp)) as RentalItemActivity;

                if ((ria.InvoiceLine == null) && ((gvr.Cells[0].Controls[1] as CheckBox).Checked))
                {
                    Result++;
                }
            }

            return Result;
        }

        public int NrOfInvoicedItems()
        {
            int Result = 0;

            foreach (GridViewRow gvr in GridViewRentedOutMaterials.Rows)
            {
                Guid Temp = new Guid((gvr.Cells[0].Controls[1] as CheckBox).ToolTip);

                // get the corresponding RentalItemActivitySet
//                RentalItemActivity ria = ControlObjectContext.RentalItemActivitySet.Where(m => m.Id == Temp).First<RentalItemActivity>();
                RentalItemActivity ria = ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.RentalItemActivitySet", "Id", Temp)) as RentalItemActivity;

                if ((ria.InvoiceLine != null) && ((gvr.Cells[0].Controls[1] as CheckBox).Checked))
                {
                    Result++;
                }
            }

            return Result;
        }

        public void UpdateRentalItemStatus(ModelTMSContainer ControlObjectContext)
        {
            foreach (GridViewRow gvr in GridViewRentedOutMaterials.Rows)
            {
                Guid Temp = new Guid((gvr.Cells[0].Controls[1] as CheckBox).ToolTip);

                // get the corresponding RentalItemActivitySet
//                RentalItemActivity ria = ControlObjectContext.RentalItemActivitySet.Where(m => m.Id == Temp).First<RentalItemActivity>();
                RentalItemActivity ria = ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.RentalItemActivitySet", "Id", Temp)) as RentalItemActivity;

                if ((ria.InvoiceLine != null) && ((gvr.Cells[0].Controls[1] as CheckBox).Checked))
                {
                    ria.InvoiceStatus = (gvr.Cells[0].Controls[3] as RadioButtonList).SelectedValue;
                }
            }
        }

        public void ResetRentalItemStatus(ModelTMSContainer ControlObjectContext)
        {
            foreach (GridViewRow gvr in GridViewRentedOutMaterials.Rows)
            {
                Guid Temp = new Guid((gvr.Cells[0].Controls[1] as CheckBox).ToolTip);

                // get the corresponding RentalItemActivitySet
//                RentalItemActivity ria = ControlObjectContext.RentalItemActivitySet.Where(m => m.Id == Temp).First<RentalItemActivity>();
                RentalItemActivity ria = ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.RentalItemActivitySet", "Id", Temp)) as RentalItemActivity;

                ria.CheckInvoiceStatus();
            }
        }

        public void CreateNewInvoice(ModelTMSContainer ControlObjectContext)
        {
            LabelInvoiceId.Text = "";

            if ((CheckBoxInvoice.Visible && CheckBoxInvoice.Checked) ||
                 (CheckBoxBailReturn.Visible && CheckBoxBailReturn.Checked))
            {
                Invoice inv = new Invoice();
                ControlObjectContext.InvoiceSet.AddObject(inv);
                Guid Temp;
                inv.Relation = ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.RelationSet", "Id", Guid.Parse( DropDownListCustomers.SelectedValue ) ) ) as Relation;
                inv.Ledger = ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.LedgerSet", "Id", Guid.Parse( ComboBoxLedger.SelectedValue ) ) ) as Ledger;

                // determine location 
                inv.Location = GetFirstLocation(ControlObjectContext);

                inv.Description = "Verhuurfactuur dd " + Common.CurrentClientDateTime(Session).ToString();
                inv.InvoiceType = "Sell";
                inv.InvoiceSubType = "Rent";
                inv.BookingDateTime = Common.CurrentClientDateTime(Session);
                if (LabelGroupId.Text == "") { LabelGroupId.Text = Guid.NewGuid().ToString(); }
                inv.GroupCode = new Guid(LabelGroupId.Text);
                inv.DiscountPercentage = Convert.ToDouble("0" + TextBoxDiscount.Text);

                if ((CheckBoxInvoice.Visible && CheckBoxInvoice.Checked))
                {
                    // invoice rental item activities
                    foreach (GridViewRow gvr in GridViewRentedOutMaterials.Rows)
                    {
                        Temp = new Guid((gvr.Cells[0].Controls[1] as CheckBox).ToolTip);

                        // get the corresponding RentalItemActivitySet
//                        RentalItemActivity ria = ControlObjectContext.RentalItemActivitySet.Where(m => m.Id == Temp).First<RentalItemActivity>();
                        RentalItemActivity ria = ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.RentalItemActivitySet", "Id", Temp)) as RentalItemActivity;

                        if ((CheckBoxInvoice.Visible && CheckBoxInvoice.Checked) && (ria.InvoiceLine == null))
                        {
                            ria.AddRentToInvoice(ControlObjectContext, inv);
                        }

                        if (!ria.RentLedger.Invoice.Contains(inv)) { ria.RentLedger.Invoice.Add(inv); }
                    }
                }

                // add bail to invoice if required
                if (CheckBoxBailReturn.Visible && CheckBoxBailReturn.Checked && (TextBoxBail.Text.Trim() != "0"))
                {
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
                }

                if (inv.InvoiceLine.Count > 0)
                {
                    // recalc & process invoice
                    inv.GenerateInvoiceNumber(ControlObjectContext);
                    inv.AllInvoiceLinesToSameLedger(inv.Ledger);
                    inv.RecalcTotals();

                    // save invoice id
                    LabelInvoiceId.Text = inv.Id.ToString();
                }
                else
                {
                    ControlObjectContext.InvoiceSet.DeleteObject(inv);
                }
            }
        }

        private Location GetFirstLocation(ModelTMSContainer ControlObjectContext)
        {
            Guid Temp;
            foreach (GridViewRow gvr in GridViewRentedOutMaterials.Rows)
            {
                if ((gvr.Cells[0].Controls[1] as CheckBox).Checked)
                {
                    Temp = new Guid((gvr.Cells[0].Controls[1] as CheckBox).ToolTip);
                    RentalItemActivity ria = ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.RentalItemActivitySet", "Id", Temp)) as RentalItemActivity;
                    return ria.RentLedger.Location;
                }
            }
            return null;
        }


        public void UnprocessInvoice()
        {
            bool Success = false;

            ModelTMSContainer _ControlObjectContext = new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session);

            // start transaction
            using (TransactionScope TS = new TransactionScope())
            {
                try
                {
                    // roll back order

                    // correct invoice if there
                    if (LabelInvoiceId.Text != "")
                    {
                        Invoice CorrInvoice = _ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.InvoiceSet", "Id", Guid.Parse(LabelInvoiceId.Text))) as Invoice;

                        // unprocess
                        CorrInvoice.UnprocessInvoice(_ControlObjectContext, CorrInvoice.GroupCode, Common.CurrentClientDateTime(Session));
                    }

                    // correct rentalitemactivity status
                    ResetRentalItemStatus(_ControlObjectContext);

                    // remove any generated swap
                    if (LabelNewRIAId.Text != "")
                    {
                        string[] IDs = LabelNewRIAId.Text.Split(';');
                        foreach (string IDstring in IDs)
                        {
                            if (IDstring.Trim() != "")
                            {
                                RentalItemActivity NewRia = _ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.RentalItemActivitySet", "Id", Guid.Parse(IDstring))) as RentalItemActivity;
                                if (NewRia != null)
                                {
                                    _ControlObjectContext.RentalItemActivitySet.DeleteObject(NewRia);
                                }
                            }
                        }
                    }

                    // enable any disabled item
                    if (LabelDisabledItems.Text != "")
                    {
                        string[] IDs = LabelDisabledItems.Text.Split(';');
                        foreach (string IDstring in IDs)
                        {
                            if (IDstring.Trim() != "")
                            {
                                RentalItem NewRI = _ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.RentalItemSet", "Id", Guid.Parse(IDstring))) as RentalItem;

                                if (NewRI != null)
                                {
                                    NewRI.ItemState = "Available";
                                }
                            }
                        }
                    }

                    // update the datetime from the original values in the grid. 
                    GrabUpdatedEndDateTimesForRIAs(_ControlObjectContext, true, true);

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

            if (Success)
            {
                // when success revert 
                PageNr--;
                ShowCorrectElements();
            }

        }

        public void SwapItems(ModelTMSContainer _ControlObjectContext)
        {

            // disable item
            LabelNewRIAId.Text = "";
            Guid Temp;
            Guid RentalItemGuid;
            foreach (GridViewRow gvr in GridViewRentedOutMaterials.Rows)
            {
                // need this item be swapped?
                // get the combobox
                ComboBox cbx = (gvr.Cells[8].Controls[3] as ComboBox);
                if (cbx.SelectedIndex >= 1)
                {
                    // get the corresponding RentalItemActivitySet
                    Temp = new Guid((gvr.Cells[0].Controls[1] as CheckBox).ToolTip);
//                    RentalItemActivity ria = _ControlObjectContext.RentalItemActivitySet.Where(m => m.Id == Temp).First<RentalItemActivity>();
                    RentalItemActivity ria = _ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.RentalItemActivitySet", "Id", Temp)) as RentalItemActivity;

                    // and swap
                    RentalItemActivity NewRia = ria.CloneToNew(_ControlObjectContext);
                    RentalItemGuid = new Guid(cbx.SelectedValue);
//                    NewRia.RentalItem = _ControlObjectContext.RentalItemSet.Where(m => m.Id == RentalItemGuid).First<RentalItem>();
                    NewRia.RentalItem = _ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.RentalItemSet", "Id", RentalItemGuid)) as RentalItem;
                    // make sure that the new item's rent starts when the old item''s period ends
                    NewRia.RentEndStartDateTime = ria.RentEndStartDateTime;
                    ria.RentEndStartDateTime = Common.CurrentClientDateTime(Session);
                    NewRia.RentStartDateTime = ria.RentEndStartDateTime;
                    _ControlObjectContext.RentalItemActivitySet.AddObject(NewRia);

                    // recalc rent price & update the invoice line if there is an invoiceline and the invoicestatus is still open or partially paid
                    ria.RecalcRentPrice(true);
                    ria.CheckInvoiceStatus();
                    ria.UpdateLinkedInvoiceLine();
                    NewRia.RecalcRentPrice(true);

                    LabelNewRIAId.Text = NewRia.Id.ToString() + ";";
                }
            }
        }

        public void DisableItemsAndSendMail(ModelTMSContainer _ControlObjectContext)
        {
            // disable item
            Guid Temp;
            LabelDisabledItems.Text = "";
            foreach (GridViewRow gvr in GridViewRentedOutMaterials.Rows)
            {
                // need this item be disabled?
                if ((gvr.Cells[0].Controls[3] as RadioButtonList).SelectedIndex >= 1)
                {
                    // disable item
                    Temp = new Guid((gvr.Cells[0].Controls[1] as CheckBox).ToolTip);

                    // get the corresponding RentalItemActivitySet
//                    RentalItemActivity ria = _ControlObjectContext.RentalItemActivitySet.Where(m => m.Id == Temp).First<RentalItemActivity>();
                    RentalItemActivity ria = _ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.RentalItemActivitySet", "Id", Temp)) as RentalItemActivity;

                    // and disable
                    ria.RentalItem.SendDisabledEMail(true);

                    LabelDisabledItems.Text = ria.RentalItem.Id.ToString() + ";" + LabelDisabledItems.Text;
                }
            }


        }

        protected void ButtonPrevious_Click(object sender, EventArgs e)
        {
            PageNr--;
        }

        protected void ButtonNext_Click(object sender, EventArgs e)
        {
            if (PageNr == 1) { GridViewRentedOutMaterials.DataBind(); };
            PageNr++;
        }

        protected void ButtonUnprocess_Click(object sender, EventArgs e)
        {
            UnprocessInvoice();
            PageNr--;
        }

        public void GrabUpdatedEndDateTimesForRIAs(ModelTMSContainer ControlObjectContext, bool RecalcIfChanged, bool RestoreWithOriginal)
        {

            Guid Temp;
            foreach (GridViewRow gvr in GridViewRentedOutMaterials.Rows)
            {
                Temp = new Guid((gvr.Cells[0].Controls[1] as CheckBox).ToolTip);

                // get the corresponding RentalItemActivitySet
//                RentalItemActivity ria = ControlObjectContext.RentalItemActivitySet.Where(m => m.Id == Temp).First<RentalItemActivity>();
                RentalItemActivity ria = ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.RentalItemActivitySet", "Id", Temp)) as RentalItemActivity;

                // grab the possible new enddate from the grid
                DateTime OldDateTime = ria.RentEndStartDateTime;
                try
                {
                    if (RestoreWithOriginal)
                    {
                        ria.RentEndStartDateTime = Convert.ToDateTime((gvr.Cells[3].Controls[0] as TextBox).ToolTip);
                    }
                    else
                    {
                        ria.RentEndStartDateTime = Convert.ToDateTime((gvr.Cells[4].Controls[1] as CalendarWithTimeControl).Text);
                    }
                }
                catch { }

                if (OldDateTime != ria.RentEndStartDateTime)
                {
                    ria.RecalcRentPrice(true);

                    // update the invoice line if there is an invoiceline and the invoicestatus is still open or partially paid
                    ria.CheckInvoiceStatus();
                    ria.UpdateLinkedInvoiceLine();
                }
            }
        }

        protected void ButtonProcess_Click(object sender, EventArgs e)
        {
            ModelTMSContainer _ControlObjectContext = new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session);

            // start transaction
            using (TransactionScope TS = new TransactionScope())
            {
                try
                {
                    // grab updated end date tiems for these materials
                    GrabUpdatedEndDateTimesForRIAs(_ControlObjectContext, true, false);

                    // create invoice if required
                    CreateNewInvoice(_ControlObjectContext);

                    // replace items with indicated items as required
                    SwapItems(_ControlObjectContext);

                    // send mails if required
                    if (CheckBoxDisableDamagedAndLost.Visible && CheckBoxDisableDamagedAndLost.Checked)
                    {
                        DisableItemsAndSendMail(_ControlObjectContext); // set to status 'Not available'
                    }

                    // update the rental item status
                    UpdateRentalItemStatus(_ControlObjectContext);

                    // and save to persistent storage
                    _ControlObjectContext.SaveChanges(System.Data.Objects.SaveOptions.DetectChangesBeforeSave);

                    // commit
                    TS.Complete();

                    PageNr++;
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