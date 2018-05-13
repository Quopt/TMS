using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;
using System.IO;
using System.Collections;
using System.Transactions;
using Microsoft.Reporting.WebForms;

namespace TMS_Recycling
{
    public partial class WebUserControlNonCashPurchase : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (_ControlObjectContext == null)
            {
                _ControlObjectContext = new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session);
            }
            
            WebUserControlEditOrderMaterials1.ControlObjectContext = _ControlObjectContext;

            if (!IsPostBack)
            {
                DropDownListLocations.DataBind();
                DropDownListCustomers.DataBind();
                Common.LimitLocationList(DropDownListLocations.Items, Session, _ControlObjectContext);
                ShowCorrectCustomer();
                ShowCorrectPanels();

                ButtonRefresh_Click(sender, e);
            }

            ShowFreightButton();
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            // reshow all edits from this activity parse
            ShowCorrectPanels();


            ShowFreightButton();
        }

        protected int CurrentPanelLevel
        {
            get
            {
                return System.Convert.ToInt32(LabelCurrentPanelLevel.Text);
            }
            set
            {
                LabelCurrentPanelLevel.Text = value.ToString();
            }
        }

        private ModelTMSContainer _ControlObjectContext = null;

        protected void HideAllPanels()
        {
            PanelCustomerDetails.Visible = false;
            PanelCustomerInformation.Visible = false;
            PanelMaterials.Visible = false;
            PanelTotals.Visible = false;
            PanelPreviewInvoice.Visible = false;
            //PanelPreviewInvoice.Style["display"] = "none";
            PanelPreviewInvoice.Enabled = false;
            PanelCustomerDetails.Enabled = false;
            PanelCustomerInformation.Enabled = false;
            PanelMaterials.Enabled = false;
            PanelTotals.Enabled = false;

        }

        protected void ShowCorrectPanels()
        {
            ButtonRevert.Visible = true;
            ButtonContinue.Visible = true;
            ButtonPrintAndProcess.Visible = false;
            ButtonNewOrder.Visible = false;
            ButtonDestroyOrderAndBack.Visible = false;

            URLPopUpControlRentOut.Visible = false;
            URLPopUpControlRentSwap.Visible = false;

            HideAllPanels();

            switch (CurrentPanelLevel)
            {
                case 1:
                    ButtonRevert.Visible = false;
                    PanelCustomerInformation.Visible = true;
                    PanelCustomerInformation.Enabled = true;
                    break;
                case 2:
                    // check if required fields are filled
                    if ((DropDownListCustomers.Text != "") && (TextBox_Description.Text != ""))
                    {
                        if (LabelCustID.Text != DropDownListCustomers.SelectedValue)
                        {
                            LabelCustID.Text = DropDownListCustomers.SelectedValue;

                            DropDownListCustomerLocations.DataBind();
                            DropDownListProjects.DataBind();
                            DropDownListCustomerFreights.DataBind();

                            URLPopUpControlNewCustomerLocation.URLToPopup = "WebFormPopUp.aspx?uc=CustomerRelationLocationOverview&Id=" + LabelCustID.Text;
                            URLPopUpControlNewCustomerProject.URLToPopup = "WebFormPopUp.aspx?uc=CustomerRelationProjectOverview&Id=" + LabelCustID.Text;
                        }
                        PanelCustomerInformation.Visible = true;
                        PanelCustomerDetails.Visible = true;
                        PanelCustomerDetails.Enabled = true;
                        EntityDataSourceMaterialsContractsAndAgreements.CommandParameters["CustID"].DefaultValue = LabelCustID.Text;
                    }
                    else
                    {
                        CurrentPanelLevel = CurrentPanelLevel - 1;
                        ShowCorrectPanels();
                    }
                    break;
                case 3:
                    WebUserControlEditOrderMaterials1.CustomerID = new System.Guid(DropDownListCustomers.SelectedValue);
                    WebUserControlEditOrderMaterials1.LocationID = new System.Guid(DropDownListLocations.SelectedValue);

                    if (!DriverAndIdCheckOK())
                    {
                        Page.RegisterClientScriptBlock("Alert", "<script>alert('Geef een chauffeur of kenteken op. Eén van deze twee velden is verplicht.');</script>");
                        LabelCurrentPanelLevel.Text = "2";
                        ShowCorrectPanels();
                        break;
                    }
                    PanelCustomerInformation.Visible = true;
                    PanelCustomerDetails.Visible = true;
                    PanelMaterials.Visible = true;
                    PanelMaterials.Enabled = true;

                    WebUserControlEditOrderMaterials1.LoadOrderLines();
                    ButtonContinue.Visible = WebUserControlEditOrderMaterials1.OrderLines.Count > 0;
                    break;
                case 4:
                    PanelCustomerInformation.Visible = true;
                    PanelCustomerDetails.Visible = true;
                    PanelMaterials.Visible = true;
                    PanelTotals.Visible = true;
                    PanelTotals.Enabled = true;

                    ButtonContinue.Visible = false;
                    ButtonPrintAndProcess.Visible = true;
                    break;
                case 5:
                    ButtonRevert.Visible = false;
                    ButtonContinue.Visible = false;
                    ButtonPrintAndProcess.Visible = false;
                    ButtonNewOrder.Visible = true;
                    ButtonDestroyOrderAndBack.Visible = true;

                    PanelCustomerInformation.Visible = true;
                    PanelCustomerDetails.Visible = true;
                    PanelMaterials.Visible = true;
                    PanelTotals.Visible = true;

                    FrameShowInvoice.Attributes["src"] = "WebFormPopup.aspx?UC=ShowReport&d=DataSetOrder&r=ReportOrderA4&Id=" + LabelGeneratedOrderId.Text.ToString();

                    //PanelPreviewInvoice.Style["display"] = "";
                    PanelPreviewInvoice.Visible = true;
                    PanelPreviewInvoice.Enabled = true;

                    URLPopUpControlRentOut.Visible = true;
                    string Identification = TextBox_YourDriverName.Text;
                    if ((TextBox_YourTruckPlate.Text != "") && (Identification != ""))
                    {
                        Identification = Identification + "/" + TextBox_YourTruckPlate.Text;
                    }
                    else
                    {
                        Identification = TextBox_YourTruckPlate.Text;
                    }
                    URLPopUpControlRentOut.URLToPopup = "WebFormPopup.aspx?UC=RentOut&LocId=" + DropDownListLocations.SelectedValue + "&CustId=" + DropDownListCustomers.SelectedValue + "&Identification=" + Identification + "&Description=" + TextBox_Description.Text;
                    URLPopUpControlRentSwap.Visible = true;
                    URLPopUpControlRentSwap.URLToPopup = "WebFormPopup.aspx?UC=RentReturn&LocId=" + DropDownListLocations.SelectedValue + "&CustId=" + DropDownListCustomers.SelectedValue;

                    break;
            }

        }

        protected void URLPopUpControlNewCustomerProject_OnPopupClosed(object sender, EventArgs e)
        {
            DropDownListProjects.DataBind();
        }

        protected void URLPopUpControlNewCustomerLocation_OnPopupClosed(object sender, EventArgs e)
        {
            DropDownListCustomerLocations.DataBind();
        }

        protected void ButtonRevert_Click(object sender, EventArgs e)
        {
            CurrentPanelLevel = CurrentPanelLevel - 1;
            ShowCorrectPanels();
        }

        protected void ButtonContinue_Click(object sender, EventArgs e)
        {
            CurrentPanelLevel = CurrentPanelLevel + 1;
            ShowCorrectPanels();

            // load the order lines if this is required
            if (CurrentPanelLevel == 3)
            {
/*
                // check if a freight is linked
                if ((DropDownListCustomerFreights.SelectedValue != "") && (DropDownListCustomerFreights.SelectedValue != Guid.Empty.ToString()))
                {
                    Guid SelectedFreight = Guid.Parse(DropDownListCustomerFreights.SelectedValue);
                    Freight TempFreight = _ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.FreightSet", "Id", SelectedFreight)) as Freight;

                    foreach (FreightSortingMaterial fsm in TempFreight.FreightSortingMaterial)
                    {
                        // add all freight lines to this order with an average price
                        OrderLine TempLine = new OrderLine();
                        TempLine.Material = fsm.Material;
                        TempLine.RelationContractMaterial = null;
                        TempLine.RelationPriceAgreement = null;
                        TempLine.Description = fsm.Material.Description;
                        TempLine.Amount = Convert.ToDouble(fsm.Weight);
                        TempLine.PricePerUnit = Convert.ToDouble(fsm.Material.PurchasePrice);
                        TempLine.PriceExVAT = TempLine.PricePerUnit * TempLine.Amount;
                        WebUserControlEditOrderMaterials1.OrderLines.Add(TempLine);
                    }

                    WebUserControlEditOrderMaterials1.SaveOrderLines();
                }
*/
            }
        }

        protected void ButtonPrintAndProcess_Click(object sender, EventArgs e)
        {
            bool Success = false;

            // start transaction
            using (TransactionScope TS = new TransactionScope())
            {
                try
                {
                    // process order 

                    // create order
                    Order TempOrder = new Order();
                    TempOrder.Relation = _ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.RelationSet", "Id", Guid.Parse(DropDownListCustomers.SelectedValue))) as Relation;
                    TempOrder.Location = _ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.LocationSet", "Id", Guid.Parse(DropDownListLocations.SelectedValue))) as Location;
                    TempOrder.Description = TextBox_Description.Text;
                    TempOrder.OrderType = LabelInvoiceType.Text;
                    TempOrder.BookingDateTime = Common.CurrentClientDateTime(Session);
                    TempOrder.YourDriverName = TextBox_YourDriverName.Text;
                    TempOrder.YourTruckPlate = TextBox_YourTruckPlate.Text;
                    TempOrder.FreightID = TextBoxFreightID.Text;
                    TempOrder.GroupCode = System.Guid.NewGuid();
                    TempOrder.OrderNote = TextBoxOrderRemark.Text;
                    TempOrder.DeterminePriceDuringInvoicing = CheckBoxDeterminePriceDuringInvoice.Checked;


                    TempOrder.LinkToSingleFreight(null);
                    if (LabelFreightGuid.Text != "")
                    {
                        Freight frg = _ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.FreightSet", "Id", Guid.Parse(LabelFreightGuid.Text))) as Freight;
                        TempOrder.LinkToSingleFreight(frg);
                    } 
                    
                    if (DropDownListProjects.SelectedValue != "")
                    {
                        TempOrder.RelationProject = _ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.RelationProjectSet", "Id", Guid.Parse(DropDownListProjects.SelectedValue))) as RelationProject;
                    }

                    if (DropDownListCustomerFreights.SelectedValue != "")
                    {
                        TempOrder.Freight.Clear();
                        TempOrder.Freight.Add(_ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.FreightSet", "Id", Guid.Parse(DropDownListCustomerFreights.SelectedValue))) as Freight);
                    }

                    if (DropDownListCustomerLocations.SelectedValue != "")
                    {
                        TempOrder.RelationLocation = _ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.RelationLocationSet", "Id", Guid.Parse(DropDownListLocations.SelectedValue))) as RelationLocation;
                    }

//                    if (Session["CurrentUserID"] != null)
//                    {
//                        TempOrder.StaffMemberPurchaser = _ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.StaffMemberSet", "Id", Guid.Parse(Session["CurrentUserID"].ToString()))) as StaffMember;
//                    }
                    TempOrder.StaffMemberPurchaser = Common.CurrentLoggedInUser(Session, _ControlObjectContext);

                    _ControlObjectContext.AddToOrderSet(TempOrder);

                    // add order lines
                    for (int i = 0; i < WebUserControlEditOrderMaterials1.OrderLines.Count; i++)
                    {
                        OrderLine TempLine = ((WebUserControlEditOrderMaterials1.OrderLines[i]) as OrderLine);
                        TempLine.Order = TempOrder;
                        TempLine.Id = Guid.NewGuid();
                        _ControlObjectContext.AddToOrderLineSet(TempLine);
                    }
                    TempOrder.RecalcTotals();

                    // add dirt if required
                    Double AddDirtAmount;
                    try { AddDirtAmount = System.Convert.ToDouble(TextBoxAddDirt.Text); }
                    catch { AddDirtAmount = 0; }
                    AddDirtAmount = (AddDirtAmount - TempOrder.TotalAmount);
                    if (AddDirtAmount > 0)
                    {
                        OrderLine TempLine = new OrderLine();
                        TempLine.Order = TempOrder;
                        TempLine.Material = TempOrder.Location.MaterialForDirt;
                        TempLine.PricePerUnit = TempLine.Material.PurchasePrice;
                        if (LabelInvoiceType.Text == "Sell")
                        {
                            TempLine.PricePerUnit = TempLine.Material.SalesPrice;
                        }
                        TempLine.Amount = AddDirtAmount * TempOrder.Location.MaterialForDirt.MaterialUnit.StockKgMultiplier;
                        TempLine.Description = "- " + TempOrder.Location.MaterialForDirt.Description;
                        TempLine.RecalcTotals(); _ControlObjectContext.AddToOrderLineSet(TempLine);
                    }

                    // save the order id
                    LabelGeneratedOrderId.Text = TempOrder.Id.ToString();

                    // assign the order number if not known
                    if (LabelOrderNr.Text == "")
                    {
                        TempOrder.AssignOrderNumber(_ControlObjectContext);
                    }
                    else
                    {
                        TempOrder.OrderNumber = Convert.ToInt64(LabelOrderNr.Text);
                    }

                    // process the order if required
                    if (CheckBoxGoodsHaveBeenDelivered.Checked)
                    {
                        TempOrder.ProcessOrder(_ControlObjectContext, TempOrder.GroupCode, Common.CurrentClientDateTime(Session));
                    }

                    // and save to persistent storage
                    _ControlObjectContext.SaveChanges(System.Data.Objects.SaveOptions.DetectChangesBeforeSave);

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
                CurrentPanelLevel = CurrentPanelLevel + 1;
                ShowCorrectPanels();
            }
        }

        protected void ButtonNewOrder_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsoluteUri);
        }

        protected void ButtonDestroyOrderAndBack_Click(object sender, EventArgs e)
        {
            bool Success = false;

            if (LabelGeneratedOrderId.Text != Guid.Empty.ToString())
            {
                _ControlObjectContext = new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session);

                // start transaction
                using (TransactionScope TS = new TransactionScope())
                {
                    try
                    {
                        // roll back order
                        Order CorrOrder = _ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.OrderSet", "Id", Guid.Parse(LabelGeneratedOrderId.Text))) as Order;

                        // unprocess
                        CorrOrder.UnprocessOrder(_ControlObjectContext, CorrOrder.GroupCode, Common.CurrentClientDateTime(Session));

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
                CurrentPanelLevel = 4;
                ShowCorrectPanels();
            }
        }

        protected void ButtonRefresh_Click(object sender, EventArgs e)
        {
            if (LabelInvoiceType.Text == "Buy")
            {
                TextBox_Description.Text = "Inkoop op krediet dd " + Common.CurrentClientDateTime(Session).ToString();
            }
            else
            {
                TextBox_Description.Text = "Verkoop op krediet dd " + Common.CurrentClientDateTime(Session).ToString();
            }
        }

        protected void CheckBoxDeterminePriceDuringInvoice_CheckedChanged(object sender, EventArgs e)
        {
            CheckBoxGoodsHaveBeenDelivered.Checked = false;
            CheckBoxGoodsHaveBeenDelivered.Enabled = !CheckBoxDeterminePriceDuringInvoice.Checked;
        }

        public void LoadFromFreight(Freight frg, bool ForceReload)
        {
            if ((LabelFreightGuid.Text != frg.Id.ToString()) || ForceReload)
            {
                ListItem li = null;
                FreightWeighing fgw = null;
                if (frg.FreightWeighing.Count > 0)
                {
                    fgw = frg.FreightWeighing.First<FreightWeighing>();
                }

                LabelOrderNr.Text = frg.OurReference.ToString();
                TextBox_Description.Text = frg.Description;

                if (frg.FreightDirection == "To warehouse")
                {
                    // switch to purchase order
                    SwitchPurchaseType(InvoiceType.Buy);
                }
                else
                {
                    // switch to sales order
                    SwitchPurchaseType(InvoiceType.Sell);
                }

                // first load the customer, this is required for Freights
                DropDownListCustomers.DataBind();
                li = DropDownListCustomers.Items.FindByValue(frg.FromRelation.Id.ToString());
                if (li != null)
                {
                    li.Selected = true;
                    LabelCustID.Text = li.Value;
                }

                DropDownListLocations.DataBind();
                li = DropDownListLocations.Items.FindByValue(frg.SourceOrDestinationLocation.Id.ToString());
                if (li != null)
                {
                    li.Selected = true;
                }

                DropDownListCustomerFreights.DataBind();
                li = DropDownListCustomerFreights.Items.FindByValue(frg.Id.ToString());
                if (li != null)
                {
                    li.Selected = true;
                    DropDownListCustomerFreights.SelectedIndex = DropDownListCustomerFreights.Items.IndexOf(li);
                }

                TextBox_YourTruckPlate.Text = frg.YourTruckPlate;
                TextBox_YourDriverName.Text = frg.YourDriverName;

                if (fgw != null) { TextBoxFreightID.Text = fgw.Description; }

                WebUserControlEditOrderMaterials1.LoadFromFreight(frg);

                LabelFreightGuid.Text = frg.Id.ToString();

                // assume that goods have been delivered already by default
                CheckBoxGoodsHaveBeenDelivered.Checked = true;
            }
        }

        public void FreightSpeedCycle()
        {
            CurrentPanelLevel = 2;
            ShowCorrectPanels();

            if (DriverAndIdCheckOK())
            {
                CurrentPanelLevel = 3;
                ShowCorrectPanels();
            }
        }

        public void UnloadFreight()
        {
            LabelOrderNr.Text = "";
            ButtonRefresh_Click(null, null);

            TextBox_YourTruckPlate.Text = "";
            TextBox_YourDriverName.Text = "";
            TextBoxFreightID.Text = "";

            WebUserControlEditOrderMaterials1.UnloadFreight();

            LabelFreightGuid.Text = "";
        }

        public void SwitchPurchaseType(InvoiceType it)
        {
            switch (it)
            {
                case InvoiceType.Buy:
                    LabelInvoiceType.Text = "Buy";
                    LabelReverseInvoiceType.Text = "Sell";
                    LabelCustomerType.Text = "Creditor";
                    LabelFreightType.Text = "To warehouse";
                    break;
                case InvoiceType.Sell:
                    LabelInvoiceType.Text = "Sell";
                    LabelReverseInvoiceType.Text = "Buy";
                    LabelCustomerType.Text = "Debtor";
                    LabelFreightType.Text = "To customer";
                    break;
            }
            WebUserControlEditOrderMaterials1.SwitchPurchaseType(it);
            ShowCorrectCustomer();
        }

        protected void DropDownListCustomerFreights_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowFreightButton();
            UnloadFreight();
            ReloadOrUnloadFreight();
        }

        private void ShowFreightButton()
        {
            URLPopUpControlShowFreightData.Visible = false;
            if (DropDownListCustomerFreights.SelectedIndex >= 1)
            {
                URLPopUpControlShowFreightData.Visible = true;
                URLPopUpControlShowFreightData.URLToPopup = "WebFormPopup.aspx?UC=FreightBase&FreightId=" + DropDownListCustomerFreights.SelectedValue.ToString();
            }
            ButtonReloadFreight.Visible = URLPopUpControlShowFreightData.Visible;
        }

        protected void ButtonReloadFreight_Click(object sender, EventArgs e)
        {
            UnloadFreight();
            ReloadOrUnloadFreight();
        }

        private void ReloadOrUnloadFreight()
        {
            if ((DropDownListCustomerFreights.SelectedIndex >= 1) && (URLPopUpControlShowFreightData.Visible))
            {
                // load the freight
                UnloadFreight();
                LoadFromFreight(_ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.FreightSet", "Id", Guid.Parse(DropDownListCustomerFreights.SelectedValue))) as Freight, true);
            }
            else
            {
                UnloadFreight();
            }
        }

        protected void ShowCorrectCustomer()
        {
            string SelVal = DropDownListLocations.SelectedValue;

            if (SelVal == "")
            {
                if (DropDownListLocations.Items.Count > 0)
                {
                    SelVal = DropDownListLocations.Items[0].Value;
                }
            }

            Common.SetCustomerToDefaultOfLocation(SelVal, DropDownListCustomers, LabelInvoiceType.Text, _ControlObjectContext);
        }

        protected void DropDownListLocations_SelectedIndexChanged1(object sender, EventArgs e)
        {
            ShowCorrectCustomer();
        }

        public bool FinalStage()
        {
            return CurrentPanelLevel == 5;
        }

        public bool DriverAndIdCheckOK()
        {
            return (!((TextBox_YourDriverName.Text == "") && (TextBox_YourTruckPlate.Text == "")));
        }

    }
}