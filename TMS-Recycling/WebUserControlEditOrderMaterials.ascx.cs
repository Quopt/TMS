using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Xml;
using System.Collections;
using System.Data;
using System.Data.Objects;
using System.Transactions;

namespace TMS_Recycling
{
    public partial class WebUserControlEditOrderMaterials : ClassTMSUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Params["CustId"] != null)
                {
                    try
                    {
                        CustomerID = new System.Guid(Request.Params["CustId"].ToString());
                    }
                    catch (Exception)
                    {
                        /* suffocate */
                    }
                }
                if (Request.Params["OrderId"] != null)
                {
                    try
                    {
                        OrderID = new System.Guid(Request.Params["OrderId"].ToString());
                    }
                    catch (Exception)
                    {
                        /* suffocate */
                    }
                }
                if (Request.Params["ShowAlreadyDeliveredAmount"] != null)
                {
                    ShowAlreadyDeliveredAmount = true;
                }
                if (Request.Params["ShowSaveButton"] != null)
                {
                    ShowSaveButton = true;
                }
                if (Request.Params["OrderNr"] != null)
                {
                    OrderNr = Convert.ToInt64(Request.Params["OrderNr"].ToString());
                }
            }

            if (LabelOrderData.Text != "") { LoadOrderLines(); }
            XmlDataSourceOrderLines.Data = LabelOrderData.Text;
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            LoadOrderLines(false);
            if (LabelMaterialDescription.Text == "") { DropDownListMaterials.DataBind(); UpdateMaterialDescription(); }

            // force reload of gridview
            GridViewOrderMaterials.DataSourceID = "";
            GridViewOrderMaterials.DataSourceID = "XmlDataSourceOrderLines";
            XmlDataSourceOrderLines.Data = LabelOrderData.Text;
        }


        // customer id for this order
        private System.Guid _CustomerID;
        public System.Guid CustomerID
        {
            set
            {
                _CustomerID = value;
                LabelCustID.Text = _CustomerID.ToString();
            }
            get
            {
                return new System.Guid(LabelCustID.Text);
            }
        }

        // location for this order
        private System.Guid _LocationID;
        public System.Guid LocationID
        {
            set
            {
                _LocationID = value;
                LabelLocationID.Text = _LocationID.ToString();
            }
            get
            {
                return new System.Guid(LabelLocationID.Text);
            }
        }

        public System.Int64 OrderNr
        {
            set
            {
                // load order
                string Query = "SELECT VALUE it FROM OrderSet as it WHERE it.OrderNumber = @OrderNumber";
                ObjectQuery<Order> query = new ObjectQuery<Order>(Query, ControlObjectContext);
                query.Parameters.Add(new ObjectParameter("OrderNumber", value ));
                ObjectResult<Order> ilines = query.Execute(MergeOption.AppendOnly);
                Order TempOrder = ilines.First<Order>();

                OrderID = TempOrder.Id;

                LabelOrderNr.Text = "(" + value.ToString() + ")";
                LabelOrderNr.Visible = true;
            }
        }

        public Boolean ShowAlreadyDeliveredAmount
        {
            set
            {
                GridViewOrderMaterials.Columns[8].Visible = value;
            }
            get
            {
                return GridViewOrderMaterials.Columns[8].Visible;
            }
        }

        // order id for this order
        private System.Guid _OrderID;
        public System.Guid OrderID
        {
            set
            {
                _OrderID = value;
                LabelOrderID.Text = _OrderID.ToString();

                // try to load the orderlines from this order
                string Query = "SELECT VALUE it " +
                    "FROM OrderLineSet as it WHERE it.Order.id = @Order_Id";

                ObjectQuery<OrderLine> query = new ObjectQuery<OrderLine>(Query, ControlObjectContext);
                query.Parameters.Add(new ObjectParameter("Order_ID", _OrderID));
                ObjectResult<OrderLine> olines = query.Execute(MergeOption.AppendOnly);

                OrderLines.Clear();
                foreach (OrderLine ol in olines)
                {
                    OrderLines.Add(ol);
                }

                SaveOrderLines();
            }
            get
            {
                return new System.Guid(LabelOrderID.Text);
            }
        }

        public Boolean ShowSaveButton
        {
            set
            {
                ButtonSave.Visible = value;
            }
            get
            {
                return ButtonSave.Visible;
            }
        }

        // this control may have its own control object context
/*
        private ModelTMSContainer _ControlObjectContext = new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session);
        public ModelTMSContainer ControlObjectContext
        {
            set
            {
                _ControlObjectContext = value;
            }
            get
            {
                return _ControlObjectContext;
            }
        }
*/
        // current order line data 
        public ArrayList OrderLines = new ArrayList();
        public void LoadOrderLines(bool SaveOrderLine = true)
        {
            LabelTotalWeight.Text = "...";
            LabelTotalPrice.Text = "...";
            LabelTotalPriceVAT.Text = "...";

            OrderLines.Clear();
            if (LabelOrderData.Text != "")
            {
                StringReader TempData = new StringReader(LabelOrderData.Text);
                XmlTextReader CurrentOrder = new XmlTextReader(TempData);
                OrderLines = new ArrayList();
                Double TotalWeight = 0;
                Double TotalPrice = 0;
                Double TotalPriceVAT = 0;

                while (CurrentOrder.Read())
                {
                    if (CurrentOrder.Name == "OrderLine")
                    {
                        // process this orderline
                        OrderLine TempOrderLine = new OrderLine();
                        TempOrderLine.Id = Guid.Parse(CurrentOrder.GetAttribute("id"));
                        TempOrderLine.Description = CurrentOrder.GetAttribute("description");
                        TempOrderLine.Amount = Convert.ToDouble(CurrentOrder.GetAttribute("amount"));
                        TempOrderLine.AlreadyDeliveredAmount = Convert.ToDouble(CurrentOrder.GetAttribute("alreadydeliveredamount"));
                        TempOrderLine.PricePerUnit = Convert.ToDouble(CurrentOrder.GetAttribute("priceperunit"));
                        TempOrderLine.PriceExVAT = TempOrderLine.Amount * TempOrderLine.PricePerUnit;
                        TempOrderLine.Material = ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.MaterialSet", "Id", Guid.Parse(CurrentOrder.GetAttribute("materialid")))) as Material;
                        TotalWeight = TotalWeight + TempOrderLine.AmountInKgs;
                        TotalPrice = TotalPrice + TempOrderLine.PriceExVAT;
                        TotalPriceVAT = TotalPriceVAT + TempOrderLine.PriceWithVAT;
                        if (CurrentOrder.GetAttribute("priceagreementid") != "")
                        {
                            TempOrderLine.RelationPriceAgreement = ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.RelationPriceAgreementSet", "Id", Guid.Parse(CurrentOrder.GetAttribute("priceagreementid")))) as RelationPriceAgreement;
                        }
                        if (CurrentOrder.GetAttribute("contractmaterialid") != "")
                        {
                            TempOrderLine.RelationContractMaterial = ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.RelationContractMaterialSet", "Id", Guid.Parse(CurrentOrder.GetAttribute("contractmaterialid")))) as RelationContractMaterial;
                        }

                        OrderLines.Add(TempOrderLine);
                    }
                }
                LabelTotalWeight.Text = TotalWeight.ToString();
                LabelTotalPrice.Text = Math.Round(TotalPrice,2).ToString();
                LabelTotalPriceVAT.Text = Math.Round(TotalPriceVAT, 2).ToString();
            }
            if (SaveOrderLine) { SaveOrderLines(); }
        }

        public void SaveOrderLines()
        {
            StringWriter TempData = new StringWriter();
            XmlTextWriter CurrentOrder = new XmlTextWriter(TempData);
            Double TotalWeight = 0;

            CurrentOrder.WriteStartElement("OrderLines");
            for (int i = 0; i < OrderLines.Count; i++)
            {
                OrderLine CurrLine = OrderLines[i] as OrderLine;

                CurrentOrder.WriteStartElement("OrderLine");

                CurrentOrder.WriteAttributeString("id", CurrLine.Id.ToString());
                CurrentOrder.WriteAttributeString("amount", CurrLine.Amount.ToString());
                TotalWeight = TotalWeight + CurrLine.Amount;
                CurrentOrder.WriteAttributeString("totalamount", CurrLine.PriceExVAT.ToString());
                CurrentOrder.WriteAttributeString("alreadydeliveredamount", CurrLine.AlreadyDeliveredAmount.ToString());
                CurrentOrder.WriteAttributeString("priceperunit", CurrLine.PricePerUnit.ToString());
                CurrentOrder.WriteAttributeString("description", CurrLine.Description);
                if (CurrLine.RelationContractMaterial != null)
                {
                    CurrentOrder.WriteAttributeString("contractmaterialid", CurrLine.RelationContractMaterial.Id.ToString());
                }
                else
                {
                    CurrentOrder.WriteAttributeString("contractmaterialid", "");
                }
                if (CurrLine.RelationPriceAgreement != null)
                {
                    CurrentOrder.WriteAttributeString("priceagreementid", CurrLine.RelationPriceAgreement.Id.ToString());
                }
                else
                {
                    CurrentOrder.WriteAttributeString("priceagreementid", "");
                }
                CurrentOrder.WriteAttributeString("materialid", CurrLine.Material.Id.ToString());

                CurrentOrder.WriteEndElement();
            }
            CurrentOrder.WriteEndElement();

            LabelTotalWeight.Text = TotalWeight.ToString();
            TempData.Close();
            LabelOrderData.Text = TempData.ToString();
            PanelPrices.Visible = OrderLines.Count > 0;
        }

        public void ResetOrderLines()
        {
            OrderLines = new ArrayList();
        }


        protected void ButtonAddMaterial_Click(object sender, EventArgs e)
        {
            Boolean Continue = false;
            if ((TextBoxAmount.Text != "") && (TextBoxPrice.Text != ""))
            {
                // values filled, check for numbers
                try
                {
                    Double Temp = Convert.ToDouble(TextBoxPrice.Text);
                    Temp = Convert.ToDouble(TextBoxAmount.Text);
                    Continue = (Temp != 0);
                }
                catch (Exception)
                { /*just suffocate */ }
            }

            if (Continue)
            {
                // add the material, type and ID to the grid

                LoadOrderLines();

                OrderLine TempLine = new OrderLine();

                Material MatItem;
                RelationPriceAgreement PAItem;
                RelationContractMaterial ContractItem;

                DetermineCurrentSelectedMaterial(out MatItem, out PAItem, out ContractItem);

                TempLine.Material = MatItem;
                TempLine.RelationContractMaterial = ContractItem;
                TempLine.RelationPriceAgreement = PAItem;
                TempLine.Description = DropDownListMaterials.SelectedItem.Text;
                TempLine.Amount = Convert.ToDouble(TextBoxAmount.Text);
                TempLine.PricePerUnit = Convert.ToDouble(TextBoxPrice.Text);
                TempLine.PriceExVAT = TempLine.PricePerUnit * TempLine.Amount;
                OrderLines.Add(TempLine);

                SaveOrderLines();
            }
        }

        protected void DropDownListMaterials_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMaterialDescription();
        }

        private void UpdateMaterialDescription()
        {
            Material MatItem;
            RelationPriceAgreement PAItem;
            RelationContractMaterial ContractItem;

            DetermineCurrentSelectedMaterial(out MatItem, out PAItem, out ContractItem);

            // set description and price
            string Description = "";
            if (MatItem != null)
            {
                Description = /*MatItem.Description + "<BR>" + */
                     LabelInStock.Text + MatItem.CurrentStockLevel.ToString();

                if (LabelInvoiceType.Text == "Buy")
                {
                    TextBoxPrice.Text = MatItem.PurchasePrice.ToString();
                }
                else
                {
                    TextBoxPrice.Text = MatItem.SalesPrice.ToString();
                }

                if (MatItem.MaterialUnit != null) 
                {
                    LabelPrice.Text = LabelPriceBase.Text + " (" + MatItem.MaterialUnit.StockUnit + ")";
                }
            }
            if (PAItem != null)
            {
                Description = /*PAItem.Description + "<BR>" + */
                      LabelDeliveryPeriode.Text + PAItem.StartDateTime.ToShortDateString() + " - " + PAItem.EndDateTime.ToShortDateString();
                TextBoxPrice.Text = PAItem.PricePerUnit.ToString();
            }
            if (ContractItem != null)
            {
                Description = /*ContractItem.RelationContract.Description + "<BR>" + */
                    LabelDeliveryPeriode.Text + ContractItem.RelationContract.ContractStartDate.ToShortDateString() + " - " +
                    ContractItem.RelationContract.ContractEndDate.ToShortDateString() + "<BR>" + LabelContractAmount.Text + ContractItem.MinAmount.ToString() + "-" + ContractItem.MaxAmount.ToString() + "<BR>" +
                    LabelDeliveredAmount.Text + ContractItem.DeliveredAmount.ToString() + "<BR>" +
                    LabelMaxContractAmount.Text + Convert.ToString(ContractItem.MaxAmount - ContractItem.DeliveredAmount);

                if (ContractItem.RelationContract.HasContractGuidance)
                {
                    Description = Description + "<BR>" + LabelContractGuidanceAmount.Text + ContractItem.AvgStockUnits.ToString();
                }
                TextBoxPrice.Text = ContractItem.PricePerUnit.ToString();
            }

            // for a price agreement or contract add a description
            LabelMaterialDescription.Text = Description;
        }

        private void DetermineCurrentSelectedMaterial(out Material MatItem, out RelationPriceAgreement PAItem, out RelationContractMaterial ContractItem)
        {
            MatItem = null;
            PAItem = null;
            ContractItem = null;

            if (DropDownListMaterials.SelectedValue != "")
            {
                // locate the material, price agreement or contract in the entitydatasource
                Guid SelID = Guid.Parse(DropDownListMaterials.SelectedValue);

                // try to load the data from material, relationcontract or relationpriceagreement

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
        }

        protected void GridViewOrderMaterials_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            e.Cancel = true;

            LoadOrderLines();

            Guid SearchGuidID = Guid.Parse(e.Keys[0].ToString());

            for (int i = OrderLines.Count - 1; i >= 0; i--)
            {
                OrderLine TempLine = OrderLines[i] as OrderLine;
                if (TempLine.Id == SearchGuidID)
                {
                    OrderLines.RemoveAt(i);
                    break;
                }
            }

            SaveOrderLines();
        }

        protected void GridViewOrderMaterials_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            e.Cancel = true;
            LoadOrderLines();

            Guid SearchGuidID = Guid.Parse(e.Keys[0].ToString());

            for (int i = OrderLines.Count - 1; i >= 0; i--)
            {
                OrderLine TempLine = OrderLines[i] as OrderLine;
                if (TempLine.Id == SearchGuidID)
                {
                    string TempStr;
                    try
                    {
                        TempStr = e.NewValues[0].ToString();
                        TempLine.Amount = Convert.ToDouble(TempStr);
                        TempStr = e.NewValues[1].ToString();
                        TempLine.PricePerUnit = Math.Round(Convert.ToDouble(TempStr), 4);
                        TempLine.PriceExVAT = Math.Round(TempLine.Amount * TempLine.PricePerUnit, 2) ;

                        if (ShowAlreadyDeliveredAmount)
                        {
                            TempStr = e.NewValues[2].ToString();
                            TempLine.AlreadyDeliveredAmount = Math.Round(Convert.ToDouble(TempStr), 4);
                        }
                    }
                    catch { };
                    break;
                }
            }

            GridViewOrderMaterials.EditIndex = -1;
            SaveOrderLines();
        }

        public void DataBind()
        {
            DropDownListMaterials.DataBind();
            DropDownListMaterials_SelectedIndexChanged(null, null);
        }

        // event to which you can subscribe. fires when the save button is clicked
        public event EventHandler OnSaveButtonClicked;

        protected void FireOnSaveButtonClicked(EventArgs e)
        {
            if (OnSaveButtonClicked != null)
            {
                OnSaveButtonClicked(this, e);
            }
        }


        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            bool Success = false;
            string Query = "SELECT VALUE it FROM OrderSet as it WHERE it.id = @Order_Id";

            // save here
            using (TransactionScope TS = new TransactionScope())
            {

                try
                {
                    ControlObjectContext = new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session);

                    ObjectQuery<Order> query = new ObjectQuery<Order>(Query, ControlObjectContext).Include("OrderLine");
                    query.Parameters.Add(new ObjectParameter("Order_ID", OrderID));
                    ObjectResult<Order> OrderList = query.Execute(MergeOption.AppendOnly);
                    Order ThisOrder = OrderList.First<Order>();

                    LoadOrderLines();

                    // delete all order lines
                    List<OrderLine> oll = ThisOrder.OrderLine.ToList();
                    for(int i=oll.Count-1; i>=0; i--) 
                    {
                        ControlObjectContext.DeleteObject(oll[i]);
                    }
                    ThisOrder.OrderLine.Clear();

                    // add new order lines
                    foreach (OrderLine ol in OrderLines)
                    {
                        ThisOrder.OrderLine.Add(ol);
                    }
                    ThisOrder.RecalcTotals();
                    
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
                }
            }
            if (Success)
            {
                // fire the event for the save button
                FireOnSaveButtonClicked(e);
            }
        }

        public void LoadFromFreight(Freight frg)
        {
            UnloadFreight();
            LoadOrderLines(false);

            if (frg.FreightSortingMaterial.Count > 0)
            {
                foreach (FreightSortingMaterial fsm in frg.FreightSortingMaterial)
                {
                    OrderLine ol = new OrderLine();

                    ol.Amount = fsm.Weight;
                    ol.Material = fsm.Material;
                    ol.Description = fsm.Description;
                    if (frg.FreightDirection == "To warehouse")
                    {
                        ol.PricePerUnit = fsm.Material.PurchasePrice;
                    }
                    else
                    {
                        ol.PricePerUnit = fsm.Material.SalesPrice;
                    }
                    ol.RecalcTotals();

                    // check for pmv
                    if (frg.FreightWeighing.Count > 0)
                    {
                        foreach (FreightWeighing fw in frg.FreightWeighing)
                        {
                            foreach (FreightWeighingMaterial fwm in fw.FreightWeighingMaterial)
                            {
                                ol.Comments = fwm.Description; //pmv, only from the first line
                                break;
                            }
                            break;
                        }
                    }

                    OrderLines.Add(ol);
                }
            }
            else if (frg.FreightWeighing.Count > 0)
            {
                foreach (FreightWeighing fw in frg.FreightWeighing)
                {
                    foreach (FreightWeighingMaterial fwm in fw.FreightWeighingMaterial)
                    {
                        OrderLine ol = new OrderLine();

                        ol.Amount = fwm.NetWeight;
                        ol.Material = fwm.Material;
                        ol.Description = fwm.Material.Description;
                        ol.Comments = fwm.Description; //pmv
                        if (frg.FreightDirection == "To warehouse")
                        {
                            ol.PricePerUnit = fwm.Material.PurchasePrice;
                        }
                        else
                        {
                            ol.PricePerUnit = fwm.Material.SalesPrice;
                        }
                        ol.RecalcTotals();

                        OrderLines.Add(ol);
                    }
                }
            }

            SaveOrderLines();
        }

        public void UnloadFreight()
        {
            LabelOrderData.Text = "";
        }

        public void SwitchPurchaseType(InvoiceType it)
        {
            switch (it)
            {
                case InvoiceType.Buy:
                    LabelInvoiceType.Text = "Buy";
                    LabelReverseInvoiceType.Text = "Sell";
                    break;
                case InvoiceType.Sell:
                    LabelInvoiceType.Text = "Sell";
                    LabelReverseInvoiceType.Text = "Buy";
                    break;
            }
        }
    }
}