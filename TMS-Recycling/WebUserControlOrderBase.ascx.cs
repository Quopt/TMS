using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Objects;
using System.Transactions;

namespace TMS_Recycling
{

    public partial class WebUserControlOrderBase : ClassTMSUserControl
    {
        public InvoiceType OrderType
        {
            get
            {
                if (LabelRelationType.Text == "Creditor")
                {
                    return InvoiceType.Buy;
                }
                else
                {
                    return InvoiceType.Sell;
                }
            }
            set
            {
                if (value == InvoiceType.Buy)
                {
                    LabelRelationType.Text = "Creditor";
                    LabelOrderType.Text = "Buy";
                    LabelMaterialType.Text = "Buy";
                }
                else
                {
                    LabelRelationType.Text = "Debtor";
                    LabelOrderType.Text = "Sale";
                    LabelMaterialType.Text = "Sell";
                }
            }
        }

        public void InitUserControl()
        {
            SetName = "Order";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            InitUserControl();
            

            if (!IsPostBack)
            {
                Common.AddOrderStatusList(DropDownList_OrderStatus_SelectedValue.Items, true);

                if (Request.Params["OrderId"] != null)
                {
                    KeyID = new System.Guid(Request.Params["OrderId"].ToString());
                }
                if (Request.Params["Id"] != null)
                {
                    KeyID = new System.Guid(Request.Params["Id"].ToString());
                }
            }

            // set show order popup button
            if (Visible)
            {
                URLPopUpControlShowOrder.Visible = DataItem  != null;
                if ((DataItem as Order) != null)
                {
                    URLPopUpControlShowOrder.URLToPopup = "WebFormPopup.aspx?UC=ShowReport&d=DataSetOrder&r=ReportOrderA4&Id=" + (DataItem as Order).Id.ToString();
                }
            }

            ShowInvoiceButton();
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if ((DataItem as Order).Relation == null)
            {
                LabelCustID.Text = (DataItem as Order).Relation.Id.ToString();
            }
            else
            {
                LabelCustID.Text = "{00000000-0000-0000-0000-000000000000}";
            }

            // set items to disabled if editing is not allowed
            Boolean ena;
            ena = ((DataItem as Order).OrderStatus == "Open") && (!(DataItem as Order).IsCorrected);
            TextBox_Description_Text.Enabled = ena;
            CheckBox_IsCorrected_Checked.Enabled = ena;
            CheckBox_DeterminePriceDuringInvoicing_Checked.Enabled = ena;
            //DropDownList_OrderStatus_SelectedValue.Enabled = ena;
            CalendarWithTimeControl_BookingDateTime_SelectedDateTime.Enabled = ena;
            DropDownList_Relation.Enabled = ena;
            DropDownList_Location.Enabled = ena;
            DropDownList_StaffMemberPurchaser.Enabled = ena;
            DropDownList_RelationLocation.Enabled = ena;
            DropDownList_RelationProject.Enabled = ena;
            DropDownList_RelationContact.Enabled = ena;
            TextBox_YourTruckPlate.Enabled = ena;
            TextBox_YourDriverName.Enabled = ena;
            DropDownList_Freight.Enabled = ena;
            TextBox_FreightID.Enabled = ena;
            GridViewOrderLines.Enabled = ena;
            DropDownListMaterials.Enabled = ena;
            TextBoxAmount.Enabled = ena;
            TextBoxPrice.Enabled = ena;
            ButtonAddMaterial.Visible = ena;
            ButtonToProcessed.Visible = ena;
            ButtonDelete.Visible = ((DataItem as Order).OrderStatus == "Processed") && (!(DataItem as Order).IsCorrected) && ((DataItem as Order).Invoice == null);
            ButtonDelete.Visible = ButtonDelete.Visible || ((DataItem as Order).OrderStatus == "Open"); // open orders may be corrected as well since they have not been processed yet
            ButtonDelete.Visible = ButtonDelete.Visible && (!(DataItem as Order).IsCorrected); //corrected orders cannot be corrected again ...
            ButtonCloneOrder.Visible = (DataItem as Order).IsCorrected;
            LabelDeleteOrderWarning.Visible = (DataItem as Order).Invoice != null;
            ButtonCancel.Visible = ena;
            LabelProcessingInfo.Visible = ena;

            ShowInvoiceButton();
        }

        public void ShowInvoiceButton()
        {
            // set show invoice button
            if (!DataItemPresent) { KeyID = KeyID; }
            URLPopUpControlInvoice.Visible = (DataItemPresent) && ((DataItem as Order).Invoice != null);
            if (URLPopUpControlInvoice.Visible)
            {
                URLPopUpControlInvoice.URLToPopup = "WebFormPopup.aspx?UC=InvoiceBase&Id=" + (DataItem as Order).Invoice.Id.ToString();
                URLPopUpControlInvoice.Text = "Open factuur " + (DataItem as Order).Invoice.InvoiceNumber.ToString();
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

            ModelTMSContainer TempContext = new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session);

            // start transaction
            using (TransactionScope TS = new TransactionScope())
            {
                try
                {
                    // roll back order
                    Order CorrOrder = TempContext.GetObjectByKey(new EntityKey("ModelTMSContainer.OrderSet", "Id", (DataItem as Order).Id)) as Order;

                    // unprocess
                    CorrOrder.UnprocessOrder(TempContext, CorrOrder.GroupCode, Common.CurrentClientDateTime(Session));

                    // and save to persistent storage
                    TempContext.SaveChanges(System.Data.Objects.SaveOptions.DetectChangesBeforeSave);

                    // commit
                    TS.Complete();
                }
                catch (Exception ex)
                {
                    // rollback transaction 
                    TS.Dispose();

                    // inform user
                    Common.InformUserOnTransactionFail(ex, Page);
                }
            }

            // reload data into the form
            ControlObjectContext.Refresh(RefreshMode.StoreWins, DataItem);
            RebindControls();
            DataBind();
        }

        protected void ButtonAddMaterial_Click(object sender, EventArgs e)
        {
            OrderLine NewLine = new OrderLine();
            Material MatItem;
            RelationPriceAgreement PAItem;
            RelationContractMaterial ContractItem;

            DetermineCurrentSelectedMaterial(out MatItem, out PAItem, out ContractItem);

            NewLine.Order = (DataItem as Order);
            try
            {
                NewLine.PricePerUnit = Convert.ToDouble(TextBoxPrice.Text);
                NewLine.Amount = Convert.ToDouble(TextBoxAmount.Text);
                NewLine.Material = MatItem;
                NewLine.Description = MatItem.Description;
                NewLine.RelationPriceAgreement = PAItem;
                NewLine.RelationContractMaterial = ContractItem;
                ControlObjectContext.AddToOrderLineSet(NewLine);

                (DataItem as Order).RecalcTotals();
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

            DataBind();
        }

        protected void DropDownListMaterials_SelectedIndexChanged(object sender, EventArgs e)
        {
            Material MatItem;
            RelationPriceAgreement PAItem;
            RelationContractMaterial ContractItem;

            DetermineCurrentSelectedMaterial(out MatItem, out PAItem, out ContractItem);

            // set description and price
            string Description = "";
            if (MatItem != null)
            {
                Description = LabelInStock.Text + MatItem.CurrentStockLevel.ToString();
                TextBoxPrice.Text = MatItem.PurchasePrice.ToString();
            }
            if (PAItem != null)
            {
                Description = LabelDeliveryPeriode.Text + PAItem.StartDateTime.ToShortDateString() + " - " + PAItem.EndDateTime.ToShortDateString();
                TextBoxPrice.Text = PAItem.PricePerUnit.ToString();
            }
            if (ContractItem != null)
            {
                Description = /*ContractItem.RelationContract.Description + "<BR>" + */
                    LabelDeliveryPeriode.Text + ContractItem.RelationContract.ContractStartDate.ToShortDateString() + " - " +
                    ContractItem.RelationContract.ContractEndDate.ToShortDateString() + "<BR>" + LabelContractAmount.Text + ContractItem.MinAmount.ToString() + "-" + ContractItem.MaxAmount.ToString() + "<BR>" +
                    LabelDeliveredAmount.Text + ContractItem.DeliveredAmount.ToString() + "<BR>" +
                    LabelMaxContractAmount.Text + Convert.ToString(ContractItem.MaxAmount - ContractItem.DeliveredAmount);
                TextBoxPrice.Text = ContractItem.PricePerUnit.ToString();
            }

            // for a price agreement or contract add a description
            LabelMaterialDescription.Text = Description;
        }

        private void DetermineCurrentSelectedMaterial(out Material MatItem, out RelationPriceAgreement PAItem, out RelationContractMaterial ContractItem)
        {
            // locate the material, price agreement or contract in the entitydatasource
            Guid SelID = Guid.Parse(DropDownListMaterials.SelectedValue);

            // try to load the data from material, relationcontract or relationpriceagreement
            MatItem = null;
            PAItem = null;
            ContractItem = null;
            EntityKey TempKey = new EntityKey("ModelTMSContainer.MaterialSet", "Id", SelID);
            try { MatItem = ControlObjectContext.GetObjectByKey(TempKey) as Material; }
            catch { };
            if (MatItem == null)
            {
                TempKey = new EntityKey("ModelTMSContainer.RelationPriceAgreementSet", "Id", SelID);
                try
                {
                    PAItem = ControlObjectContext.GetObjectByKey(TempKey) as RelationPriceAgreement;
                    MatItem = PAItem.Material;
                }
                catch { };
            }
            if ((MatItem == null) && (PAItem == null))
            {
                TempKey = new EntityKey("ModelTMSContainer.RelationContractMaterialSet", "Id", SelID);
                try
                {
                    ContractItem = ControlObjectContext.GetObjectByKey(TempKey) as RelationContractMaterial;
                    MatItem = ContractItem.Material;
                }
                catch { };
            }
        }

        protected void GridViewOrderLines_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            e.Cancel = true;

            OrderLine TempLine;
            TempLine = ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.OrderLineSet", "Id", Guid.Parse( e.Keys[0].ToString() ) )) as OrderLine;

            (DataItem as Order).OrderLine.Remove(TempLine);
            ControlObjectContext.DeleteObject(TempLine);
            ControlObjectContext.SaveChanges(SaveOptions.DetectChangesBeforeSave);

            RebindControls();
            DataBind();
        }

        protected void GridViewOrderLines_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            e.Cancel = true;

            OrderLine TempLine;
            try
            {
                TempLine = ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.OrderLineSet", "Id", Guid.Parse(e.Keys[0].ToString()))) as OrderLine;
                TempLine.Description = e.NewValues[0].ToString();
                TempLine.Amount = Math.Round(Convert.ToDouble(e.NewValues[1].ToString()), 4);
                TempLine.PricePerUnit = Math.Round(Convert.ToDouble(e.NewValues[2].ToString()), 4);
                TempLine.AlreadyDeliveredAmount = Math.Round(Convert.ToDouble(e.NewValues[3].ToString()), 4);

                TempLine.Order.RecalcTotals();
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

            GridViewOrderLines.EditIndex = -1;

            RebindControls();
            DataBind();
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
                        (DataItem as Order).ProcessOrder(ControlObjectContext, (DataItem as Order).GroupCode, Common.CurrentClientDateTime(Session));

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

        protected void ButtonCloneOrder_Click(object sender, EventArgs e)
        {
            // start transaction
            using (TransactionScope TS = new TransactionScope())
            {
                try
                {
                    // clone order 
                    Order NewOrder = (DataItem as Order).CloneToNew(ControlObjectContext, false, null, Common.CurrentClientDateTime(Session));

                    ControlObjectContext.SaveChanges(SaveOptions.DetectChangesBeforeSave);

                    // commit the transaciton
                    TS.Complete();

                    // inform user
                    Page.RegisterClientScriptBlock("Alert", "<script>alert('De order is gekloond. De order wordt nu geopend.');</script>");

                    Response.Redirect("~\\WebFormPurchaseLedger.aspx?OrderNumber=" + NewOrder.OrderNumber.ToString(), false);
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


        public void SwitchPurchaseType(InvoiceType it)
        {
            OrderType = it;
        }

    }
}