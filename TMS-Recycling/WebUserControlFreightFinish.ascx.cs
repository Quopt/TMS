using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Xml;
using System.Data.Objects;
using System.Transactions;

namespace TMS_Recycling
{
    public partial class WebUserControlFreightFinish : ClassTMSUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LabelMaterialData.Text = "";

                // if a freight id is given then immediately cycle to the weight panel
                if (Request.Params["FreightId"] != null)
                {
                    Freight frg = Freight.SelectFreightByFreightId(new Guid(Request.Params["FreightId"]), new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session));
                    if (frg != null)
                    {
                        TextBoxOrderNumber.Text = frg.OurReference.ToString();
                        CurrentPageNr = 2;
                        EnableCorrectScreenElements();
                    }
                }
            }
            XmlDataSourceMaterials.Data = LabelMaterialData.Text;
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            EnableCorrectScreenElements();

            if (LabelMaterialData.Text != "") 
            {
                // unfortunately the gridview does not bind when told, so use this trick to force a bind to the correct data
                XmlDataSourceMaterials.Data = "<MaterialLines> <MaterialLine id=\"\" GrossAmount=\"0\" NetAmount=\"0\" description=\"testmaterial\"  materialid=\"\" TotalAmount=\"0\" /></MaterialLines>";
                GridViewMaterials.DataBind();
                XmlDataSourceMaterials.Data = LabelMaterialData.Text;
                GridViewMaterials.DataBind();
            }
            LabelPreviousPageNr.Text = LabelCurrentPageNr.Text;
        }

        private void EnableCorrectScreenElements()
        {
            string InvoiceType = "";

            PanelSecondWeighingOrderNumber.Visible = true;
            PanelSecondWeighingOrderNumber.Enabled = false;
            PanelBasicData.Visible = false;
            PanelBasicData.Enabled = false;
            PanelMaterials.Visible = false;
            PanelMaterials.Enabled = false;
            PanelTotals.Visible = false;
            PanelTotals.Enabled = false;
            PanelInvoice.Visible = false;
            PanelInvoice.Enabled = false;

            ButtonContinue.Visible = false;
            ButtonDestroyAndBack.Visible = false;
            ButtonInvoiceOrder.Visible = false;
            ButtonNew.Visible = false;
            ButtonPrintAndProcess.Visible = false;
            ButtonProcessAndCashInvoice.Visible = false;
            ButtonRevert.Visible = false;

            URLPopUpControlShowSorting.Visible = false;
            LabelWeightWarning.Visible = false;

            switch (CurrentPageNr)
            {
                case 1 :
                    PanelSecondWeighingOrderNumber.Enabled = true;
                    ButtonContinue.Visible = true;
                    break;
                case 2:
                    ButtonContinue.Visible = true;
                    ButtonRevert.Visible = true;

                    PanelBasicData.Visible = true;
                    PanelBasicData.Enabled = true;

                    // get the data from the already registered weighing
                    if (Convert.ToInt32(LabelPreviousPageNr.Text) < CurrentPageNr)
                    {
                        if ((!CheckBoxNewSorting.Checked))
                        {
                            bool Success = false;
                            try
                            {
                                long FreightNr = Convert.ToInt64(TextBoxOrderNumber.Text);
                                Freight frg = Freight.SelectFreightByFreightNr(FreightNr, new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session));
                                if (frg != null)
                                {
                                    LoadFreightData(frg);
                                    CurrentWeighingId = frg.Id;
                                    Success = true;

                                    if (RadioButtonListBuyOrSell.SelectedIndex == 0)
                                    {
                                        InvoiceType = "Buy";
                                    }
                                    else
                                    {
                                        InvoiceType = "Sell";
                                    }

                                    URLPopUpControlShowSorting.URLToPopup = "WebFormPopup.aspx?UC=ShowReport&d=DataSetSorting&r=ReportSortingA4&Id=" + CurrentWeighingId.ToString() + "&InvoiceType=" + InvoiceType;
                                }
                                else
                                {
                                    Common.InformUser(Page, "Geef aub een geldig nummer op voor de sorteerbon die nog niet verder is verwerkt.");
                                }
                            }
                            catch { }

                            if (!Success)
                            {
                                CurrentPageNr--;
                                EnableCorrectScreenElements();
                            }
                        }
                        else
                        {
                            ComboBoxWeighingLocation.DataBind();
                            ComboBoxOurPlate.DataBind();
                            ComboBoxCustomer.DataBind();
                            CalendarWithTimeControlDateTime1.SelectedDateTime = Common.CurrentClientDateTime(Session);
                            TextBoxTotalNetWeight.Text = "0";
                            ShowCorrectCustomer();
                        }
                    }
                    break;
                case 3:
                    ButtonContinue.Visible = true;
                    ButtonRevert.Visible = true;

                    PanelBasicData.Visible = true;
                    PanelMaterials.Enabled = true;
                    PanelMaterials.Visible = true;

                    // load materials from the sorting into the xml data source
                    InitSortingMaterials();

                    break;
                case 4:
                    PanelBasicData.Visible = true;
                    PanelMaterials.Visible = true;
                    PanelTotals.Visible = true;
                    PanelTotals.Enabled = true;

                    // save in XML
                    LoadSortingMaterials();// load them in the freightsortingmaterial lines
                    SaveSortingMaterials();// save them in the screen & freightsortingmaterial lines

                    // calc total weights
                    double TotalWeightInLines = Convert.ToDouble(LabelTotalWeightInOrder.Text);
                    double TotalWeightInOrder = 0;
                    try { TotalWeightInOrder = Convert.ToDouble(TextBoxTotalNetWeight.Text); } catch { TotalWeightInOrder = 0.1; };
                    LabelWeightWarning.Visible = TotalWeightInOrder < TotalWeightInLines;

                    if (LabelWeightWarning.Visible)
                    {
                        Common.InformUser(Page, LabelWeightWarning.Text);
                    }

                    ButtonContinue.Visible = true;
                    ButtonRevert.Visible = true;
                    ButtonProcessAndCashInvoice.Visible = true;
                    break;
                case 5:
                    PanelBasicData.Visible = true;
                    PanelMaterials.Visible = true;
                    PanelTotals.Visible = true;
                    PanelInvoice.Visible = true;
                    PanelInvoice.Enabled = true;

                    // store the sorted materials which are relevant with the weighing
                    LoadSortingMaterials();
                    if (StoreSorting())
                    {
                        // if storing was successfull then show the sorting slip
                        if (RadioButtonListBuyOrSell.SelectedIndex == 0)
                        {
                            InvoiceType = "Buy";
                        }
                        else
                        {
                            InvoiceType = "Sell";
                        }
                        FrameShowInvoice.Attributes["src"] = "WebFormPopup.aspx?UC=ShowReport&d=DataSetSorting&r=ReportSortingA4&Id=" + CurrentWeighingId.ToString() + "&InvoiceType=" + InvoiceType;
                    
                        ButtonDestroyAndBack.Visible = true;
                        ButtonInvoiceOrder.Visible = true;
                        ButtonNew.Visible = true;
                    } 
                    else 
                    {
                        CurrentPageNr-- ;
                        EnableCorrectScreenElements();
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
            CurrentPageNr++;
        }

        protected void ButtonPrintAndProcess_Click(object sender, EventArgs e)
        {
            CurrentPageNr++;
        }

        protected void ButtonDestroyOrderAndBack_Click(object sender, EventArgs e)
        {
            if (UnstoreSorting()) { CurrentPageNr--; }
        }

        protected void ButtonNewOrder_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsoluteUri); 
        }

        protected void ButtonInvoiceOrder_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebFormFreightInvoice.aspx?FreightId=" + CurrentWeighingId.ToString());
        }

        protected void LoadFreightData(Freight frg)
        {
            RadioButtonListBuyOrSell.SelectedIndex = frg.FreightDirection == "To customer" ? 1 : 0;
            RadioButtonListBuyOrSell_SelectedIndexChanged(null, null);

            CalendarWithTimeControlDateTime1.SelectedDateTime = frg.FreightDateTime;
            TextBoxTotalNetWeight.Text = frg.TotalNetWeight.ToString();
            TextBoxPMV.Text = frg.FirstFreightWeighingDescription();
            TextBoxCustomerPlate.Text = frg.YourTruckPlate;
            TextBoxCustomerID.Text = frg.YourDriverName;

            ComboBoxWeighingLocation.DataBind();
            ListItem li = ComboBoxWeighingLocation.Items.FindByValue(frg.SourceOrDestinationLocation.Id.ToString());
            if (li != null)
            {
                li.Selected = true;
            }

            ComboBoxCustomer.DataBind();
            li = ComboBoxCustomer.Items.FindByValue(frg.FromRelation.Id.ToString());
            if (li != null)
            {
                li.Selected = true;
            }

            ComboBoxOurPlate.DataBind();
            if (frg.OurTruck != null)
            {
                li = ComboBoxOurPlate.Items.FindByValue(frg.OurTruck.Id.ToString());
                if (li != null)
                {
                    li.Selected = true;
                }
            }
            //ComboBoxWeighingLocation = ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.LocationSet", "Id", frg.SourceOrDestinationLocation )) as Location;
            //ComboBoxCustomer =  ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.RelationSet", "Id", frg.FromRelation.Id)) as Relation;
            //ComboBoxOurPlate = ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.TruckSet", "Id", frg.OurTruck.Id)) as Truck;
        }

        private List<FreightSortingMaterial> MaterialLines = new List<FreightSortingMaterial>();
        protected void InitSortingMaterials()
        {
            if (LabelMaterialData.Text == "")
            {
                // get the current freight
                Freight frg=Freight.SelectFreightByFreightId( CurrentWeighingId, ControlObjectContext );

                // enumerate all materials for this collection
                string Query = "select value it from MaterialSet as it where it.IsActive and (not it.IsWorkInsteadOfMaterial) and it.Location.Id = @LocationId and ((it.InvoiceType = @InvoiceType) or (it.InvoiceType = \"Both\")) order by it.Description";
                ObjectQuery<Material> query = new ObjectQuery<Material>(Query, ControlObjectContext).Include("Location");
                query.Parameters.Add(new ObjectParameter("LocationId", new Guid(ComboBoxWeighingLocation.SelectedValue)));
                query.Parameters.Add(new ObjectParameter("InvoiceType", LabelMaterialInvoiceType.Text ));
                ObjectResult<Material> MaterialList = query.Execute(MergeOption.AppendOnly);

                foreach(Material Mat in MaterialList)
                {
                    FreightSortingMaterial fsm = new FreightSortingMaterial();

                    fsm.Material = Mat;

                    if (frg != null)
                    {
                        FreightSortingMaterial existing_fsm = frg.CheckForSortingMaterial(Mat, ControlObjectContext);
                        if (existing_fsm != null) { fsm = existing_fsm; }
                    }

                    fsm.Description = Mat.Description;

                    MaterialLines.Add(fsm);
                }

                SaveSortingMaterials();
            }
        }

        protected void LoadSortingMaterials()
        {
            if (LabelMaterialData.Text != "")
            {
                StringReader TempData = new StringReader(LabelMaterialData.Text);
                XmlTextReader CurrentMaterial = new XmlTextReader(TempData);
                MaterialLines = new List<FreightSortingMaterial>();
                Double TotalWeight = 0;

                while (CurrentMaterial.Read())
                {
                    if (CurrentMaterial.Name == "MaterialLine")
                    {
                        // process this orderline
                        FreightSortingMaterial TempFSMLine = new FreightSortingMaterial();

                        TempFSMLine.Id = Guid.Parse(CurrentMaterial.GetAttribute("id"));
                        TempFSMLine.Description = CurrentMaterial.GetAttribute("description");
                        TempFSMLine.GrossWeight = Convert.ToDouble(CurrentMaterial.GetAttribute("GrossAmount"));
                        TempFSMLine.TarraWeight = Convert.ToDouble(CurrentMaterial.GetAttribute("NetAmount"));
                        TempFSMLine.Material = ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.MaterialSet", "Id", Guid.Parse(CurrentMaterial.GetAttribute("materialid")))) as Material;
                        TempFSMLine.RecalcTotals();
                        TotalWeight = TotalWeight + TempFSMLine.Weight;
                        MaterialLines.Add(TempFSMLine);
                    }
                }

                LabelTotalWeightInOrder.Text = TotalWeight.ToString();
                CurrentMaterial.Close();
            }
        }

        protected void SaveSortingMaterials()
        {
            StringWriter TempData = new StringWriter();
            XmlTextWriter CurrentMaterial = new XmlTextWriter(TempData);
            Double TotalWeight = 0;

            CurrentMaterial.WriteStartElement("MaterialLines");
            for (int i = 0; i < MaterialLines.Count; i++)
            {
                FreightSortingMaterial CurrLine = MaterialLines[i] as FreightSortingMaterial;

                GridViewRow gvr = null;
                try
                {
                    if (GridViewMaterials.Rows.Count > i) { gvr = GridViewMaterials.Rows[i]; }
                }
                catch { };

                CurrentMaterial.WriteStartElement("MaterialLine");

                if (gvr != null)
                {
                    try { CurrLine.GrossWeight = Convert.ToDouble((gvr.Cells[2].Controls[1] as TextBox).Text.ToString()); }
                    catch { };
                    try { CurrLine.TarraWeight = Convert.ToDouble((gvr.Cells[3].Controls[1] as TextBox).Text.ToString()); }
                    catch { };
                }

                CurrentMaterial.WriteAttributeString("id", CurrLine.Id.ToString());
                CurrentMaterial.WriteAttributeString("GrossAmount", CurrLine.GrossWeight.ToString());
                CurrentMaterial.WriteAttributeString("NetAmount", CurrLine.TarraWeight.ToString());
                CurrLine.RecalcTotals();
                TotalWeight = TotalWeight + CurrLine.Weight;
                CurrentMaterial.WriteAttributeString("TotalAmount", CurrLine.Weight.ToString());
                CurrentMaterial.WriteAttributeString("description", CurrLine.Description);
                CurrentMaterial.WriteAttributeString("materialid", CurrLine.Material.Id.ToString());

                CurrentMaterial.WriteEndElement();
            }
            CurrentMaterial.WriteEndElement();

            LabelTotalWeightInOrder.Text = TotalWeight.ToString();
            LabelTotalUnassignedWeightInOrder.Text = "0";
            CheckBoxSubtractOverweight.Checked = false;
            CheckBoxSubtractOverweight.Visible = false;
            try
            {
                double TotalNetWeight = Convert.ToDouble(TextBoxTotalNetWeight.Text);
                LabelTotalUnassignedWeightInOrder.Text = (TotalNetWeight - TotalWeight).ToString();
                double UnassignedWeight = Convert.ToDouble(LabelTotalUnassignedWeightInOrder.Text);
                long FreightNr = Convert.ToInt64(TextBoxOrderNumber.Text);
                Freight frg = Freight.SelectFreightByFreightNr(FreightNr, ControlObjectContext);
                if ((UnassignedWeight < 0) && (frg.FreightWeighing.Count > 0) && (TotalNetWeight != 0))
                {
                    CheckBoxSubtractOverweight.Visible = true;
                    CheckBoxSubtractOverweight.Checked = true;
                    // use the first material of the first freightweighing as a default. This method is not suitable for multiple weighings
                    Material OrgMat = frg.FreightWeighing.First<FreightWeighing>().FreightWeighingMaterial.First<FreightWeighingMaterial>().Material;
                    CheckBoxSubtractOverweight.Text= string.Format("Te veel gesorteerd gewicht aftrekken van {0}.", OrgMat.Description);
                }
            }
            catch { }
            TempData.Close();
            LabelMaterialData.Text = TempData.ToString();
        }

        protected bool UnstoreSorting()
        {
            Freight frg = null;

            bool Success = false;

            // start transaction
            using (TransactionScope TS = new TransactionScope())
            {
                try
                {
                    frg = Freight.SelectFreightByFreightId(CurrentWeighingId, ControlObjectContext);
                    frg.FreightStatus = "In sorting";

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
            return Success;
        }

        protected bool StoreSorting()
        {
            Freight frg = null;

            bool Success = false;
            ControlObjectContext = new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session);

            // start transaction
            using (TransactionScope TS = new TransactionScope())
            {
                try
                {
                    frg = Freight.SelectFreightByFreightId(CurrentWeighingId, ControlObjectContext);

                    if (frg == null)
                    {
                        // create new Freight
                        frg = new Freight();
                        frg.AssignFreightNumber(ControlObjectContext);
                        frg.Description = "Sorteerbon " + frg.OurReference.ToString() + " / " + frg.FreightDateTime.ToString();
                        CurrentWeighingId = frg.Id;
                    }

                    frg.SourceOrDestinationLocation = ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.LocationSet", "Id", new Guid(ComboBoxWeighingLocation.SelectedValue))) as Location;
                    frg.FromRelation = ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.RelationSet", "Id", new Guid(ComboBoxCustomer.SelectedValue))) as Relation;
                    if (ComboBoxOurPlate.SelectedValue != "")
                    {
                        frg.OurTruck = ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.TruckSet", "Id", new Guid(ComboBoxOurPlate.SelectedValue))) as Truck;
                    }
                    else
                    {
                        frg.OurTruck = null;
                    }

                    frg.FreightDirection = RadioButtonListBuyOrSell.SelectedIndex == 1 ? "To customer" : "To warehouse";
                    frg.FreightDateTime = CalendarWithTimeControlDateTime1.SelectedDateTime;
                    frg.FreightType = "Weighing";
                    try { frg.TotalNetWeight = Convert.ToDouble(TextBoxTotalNetWeight.Text); }
                    catch { }
                    //TextBoxPMV.Text = frg.FirstFreightWeighingDescription();
                    frg.YourTruckPlate = TextBoxCustomerPlate.Text;
                    frg.YourDriverName = TextBoxCustomerID.Text;

                    frg.FreightStatus = "To be invoiced";
                    foreach (FreightSortingMaterial fsm in frg.FreightSortingMaterial.ToArray<FreightSortingMaterial>())
                    {
                        ControlObjectContext.DeleteObject(fsm);
                    }
                    foreach(FreightSortingMaterial fsm in MaterialLines)
                    {
                        fsm.RecalcTotals();
                        if (fsm.RelevantLine())
                        {
                            FreightSortingMaterial fsmNew = new FreightSortingMaterial();
                            fsmNew.Material = ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.MaterialSet", "Id", fsm.Material.Id )) as Material;
                            fsmNew.Description = fsmNew.Material.Description;
                            fsmNew.GrossWeight = fsm.GrossWeight;
                            fsmNew.TarraWeight = fsm.TarraWeight;
                            fsmNew.Freight = frg;
                            fsmNew.RecalcTotals();
                        }
                    }

                    // add dirt if required
                    Double RemainingDirt = Convert.ToDouble(LabelTotalUnassignedWeightInOrder.Text );
                    if ((RemainingDirt > 0) && ( CheckBoxRestIsDirt.Checked) )
                    {
                        FreightSortingMaterial Dirt = new FreightSortingMaterial();
                        Dirt.Material = frg.SourceOrDestinationLocation.MaterialForDirt;
                        Dirt.Description = Dirt.Material.Description;
                        Dirt.GrossWeight = RemainingDirt;
                        Dirt.TarraWeight = 0;
                        Dirt.Freight = frg;
                        Dirt.RecalcTotals();
                    }

                    // subtract overweight materials if required
                    if ((CheckBoxSubtractOverweight.Checked) && (RemainingDirt < 0))
                    {
                        // use the first material of the first freightweighing as a default. This method is not suitable for multiple weighings
                        Material OrgMat = frg.FreightWeighing.First<FreightWeighing>().FreightWeighingMaterial.First<FreightWeighingMaterial>().Material;

                        foreach (FreightSortingMaterial fsm in frg.FreightSortingMaterial)
                        {
                            if (fsm.Material == OrgMat)
                            {
                                fsm.GrossWeight = fsm.GrossWeight + RemainingDirt;
                                fsm.RecalcTotals();
                                break;
                            }
                        }
                    }
                    frg.RecalcTotalWeightFromSorting();

                    // and save to persistent storage
                    ControlObjectContext.SaveChanges(System.Data.Objects.SaveOptions.DetectChangesBeforeSave);

                    // commit the transaction
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
            return Success;

        }

        protected void RadioButtonListBuyOrSell_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetCustomerType();
            LabelMaterialData.Text = "";
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

            EntityDataSourceCustomers.DataBind();
        }

        protected void ComboBoxWeighingLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            LabelMaterialData.Text = "";
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

            Common.SetCustomerToDefaultOfLocation(SelVal, ComboBoxCustomer, LabelMaterialInvoiceType.Text, ControlObjectContext);
        }

        protected void ButtonProcessAndCashInvoice_Click(object sender, EventArgs e)
        {
            CurrentPageNr++;
            EnableCorrectScreenElements();
            Response.Redirect("WebFormFreightInvoice.aspx?Speed=0&FreightId=" + CurrentWeighingId.ToString());
        }


    }
}