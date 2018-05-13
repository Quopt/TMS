using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Xml;
using System.Data.Objects;
using System.Data;

namespace TMS_Recycling
{
    public class RentalItemActivityListItem 
    {
        public double DiscountPercentage=0, RentPrice=0, TotalRentPrice=0, Vat=0, BailPrice=0;
        public int RentalItemAmount=1;
        public Guid Id =  System.Guid.NewGuid(), RentalItemId = Guid.Empty, RentalTypeId = Guid.Empty, CustomerLocationId = Guid.Empty;
        public string RentalItem="", RentalType="", CustomerLocation="";
        public bool TreatAsAdvancePayment = false;

        public RentalItemActivityListItem Clone()
        {
            RentalItemActivityListItem ria = new RentalItemActivityListItem();
            Common.CloneProperties(this, ria);
            return ria;
        }
    }

    public partial class WebUserControlRentMaterials : ClassTMSUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
                EntityDataSourceMaterials.CommandParameters["StartDate"].DefaultValue = Common.ReturnEntitySQLDateTimeString(Common.CurrentClientDateTime(Session));
                EntityDataSourceMaterials.CommandParameters["EndDate"].DefaultValue = Common.ReturnEntitySQLDateTimeString(Common.CurrentClientDateTime(Session));
                EntityDataSourceMaterials.CommandParameters["BorderEndDate"].DefaultValue = Common.ReturnEntitySQLDateTimeString(new DateTime(2099, 12, 31));
                EntityDataSourceMaterials.CommandParameters["LocationId"].DefaultValue = LocationID.ToString();
                
                SaveOrderLines();

                ComboBoxMaterialType.DataBind();
                DropDownListMaterials_SelectedIndexChanged(null, null);

            }

            XmlDataSourceOrderLines.Data = LabelOrderLines.Text;
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            // force reload of gridview
            GridViewOrderMaterials.DataSourceID = "";
            GridViewOrderMaterials.DataSourceID = "XmlDataSourceOrderLines";
            XmlDataSourceOrderLines.Data = LabelOrderLines.Text;
            GridViewOrderMaterials.DataBind();

            // check if we need to try to bind the materials 
//            ComboBoxMaterialType.DataBind();
//            DropDownListMaterials_SelectedIndexChanged(null, null);

            // recalc the totals
            RecalcTotals();
        }

        protected void ButtonAddMaterial_Click(object sender, EventArgs e)
        {
            bool NotAddedYet = true;

            LoadOrderLines();

            // create the new Ria rental type
            ModelTMSContainer ControlObjectContext = new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session);
            RentalItemActivityListItem ria = new RentalItemActivityListItem();

            ria.RentalTypeId = new Guid(ComboBoxMaterialType.SelectedValue);
            ria.RentalType = ComboBoxMaterialType.SelectedItem.Text;
            ria.TreatAsAdvancePayment = CheckBoxTreatAsAdvancePayment.Checked;
 
            if (RadioButtonListSpecificOrAmount.SelectedValue == "Specific")
            {
                ria.RentalItemId = new Guid(ComboBoxMaterials.SelectedValue);
                ria.RentalItem = ComboBoxMaterials.SelectedItem.Text;

                RentalItem ri = ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.RentalItemSet", "Id", ria.RentalItemId)) as RentalItem;
                ria.BailPrice = ri.BailPrice;
                ri.CalculateRentForPeriod(StartRentDate, EndRentDate, out ria.RentPrice, out ria.Vat, out ria.TotalRentPrice);
            }
            else
            {
                try { ria.RentalItemAmount = Convert.ToInt32(TextBoxAmount.Text); }
                catch { }
            }

            if (ComboBoxCustomerLocation.SelectedValue != "")
            {
                ria.CustomerLocation = ComboBoxCustomerLocation.SelectedItem.Text;
                ria.CustomerLocationId = new Guid(ComboBoxCustomerLocation.SelectedValue);
            }

            try { ria.DiscountPercentage = Convert.ToDouble(TextBoxDiscountPercentage.Text); }
            catch { };

            // check if this material has not been added yet
            foreach (RentalItemActivityListItem riali in OrderLines)
            {
                if (ria.RentalItemId != Guid.Empty)
                {
                    // checking for specific material
                    if ((ria.RentalItemId == riali.RentalItemId) && (ria.RentalTypeId == riali.RentalTypeId))
                    {
                        NotAddedYet = false;
                        break;
                    }
                    // checking for specific material group
                    if ((ria.RentalTypeId == riali.RentalTypeId) && (riali.RentalItemId == Guid.Empty))
                    {
                        NotAddedYet = false;
                        break;
                    }
                }
                else
                {
                    // checking for specific material group
                    if (ria.RentalTypeId == riali.RentalTypeId)
                    {
                        NotAddedYet = false;
                        break;
                    }
                }
            }

            // add the material if not added yet, otherwise inform the user
            if (NotAddedYet)
            {
                OrderLines.Add(ria);

                SaveOrderLines();
            }
            else
            {
                Common.InformUser(Page, "U heeft dit materiaal al aan deze verhuring toegevoegd.");
            }
        }

        public void ExpandOrderLines()
        {
            // if there are orderlines with multiple amounts of the same general material type then expand those lines into specific materials
            // raise an exception if there are insufficient materials available
            RentalItemActivityListItem[] OldOrderList = OrderLines.ToArray<RentalItemActivityListItem>();
            bool Success = true;

            LoadOrderLines();

            try
            {
                foreach (RentalItemActivityListItem ria in OrderLines.ToArray<RentalItemActivityListItem>())
                {
                    if (ria.RentalItemId == Guid.Empty)
                    {
                        string Query = EntityDataSourceMaterials.CommandText;
                        ModelTMSContainer ControlObjectContext = new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session);

                        Query = Query.Substring(0, Query.ToLower().IndexOf("order by") );
                        Query = Query.Substring(Query.ToLower().IndexOf("from"));
                        Query = "select value it " + Query + " order by it.BaseRentalPrice, it.Description";

                        ObjectQuery<RentalItem> oq = new ObjectQuery<RentalItem>(Query, ControlObjectContext);

                        oq.Parameters.Add( new ObjectParameter( "StartDate", StartRentDate));
                        oq.Parameters.Add( new ObjectParameter( "EndDate", EndRentDate));
                        oq.Parameters.Add( new ObjectParameter( "BorderEndDate", Common.ReturnEntitySQLDateTimeString(new DateTime(2099, 12, 31)) ));
                        oq.Parameters.Add( new ObjectParameter( "LocationId", LocationID));
                        oq.Parameters.Add( new ObjectParameter( "RentalType", ria.RentalTypeId));

                        RentalItem[] RIs = oq.ToArray<RentalItem>();

                        int AmountToAdd = ria.RentalItemAmount;
                        if (RIs.Count() >= ria.RentalItemAmount)
                        {
                            // remove the orderline and expand with the specific material
                            OrderLines.Remove(ria);

                            foreach(RentalItem ri in RIs)
                            {
                                // and add the line for this item
                                RentalItemActivityListItem NewRia = new RentalItemActivityListItem();
                                NewRia.RentalItemId = ri.Id;
                                NewRia.RentalItem = ri.Description;
                                NewRia.RentalItemAmount = 1;
                                NewRia.RentalType = ri.RentalType.Description;
                                NewRia.RentalTypeId = ri.RentalType.Id;
                                NewRia.TreatAsAdvancePayment = ria.TreatAsAdvancePayment;

                                ri.CalculateRentForPeriod(StartRentDate, EndRentDate, out NewRia.RentPrice, out NewRia.Vat, out NewRia.TotalRentPrice);
                                NewRia.BailPrice = ri.BailPrice;

                                OrderLines.Add(NewRia);

                                // need to add more ?
                                AmountToAdd--;
                                if (AmountToAdd <= 0) { break; }
                            }

                            Success = true;
                        }
                        else
                        {
                            Success = false;
                            throw new Exception( string.Format("Het aantal beschikbare materialen van {0} is onvoldoende om deze verhuring te kunnen uitleveren.", ria.RentalType));
                        }
                    }
                }
            }
            finally
            {
//                if (!Success)
//                {
//                    OrderLines.Clear();
//                    foreach(RentalItemActivityListItem OldRia in OldOrderList)
//                    {
//                        OrderLines.Add(OldRia);
//                    }
//                }
            }

            if (Success)
            {
                SaveOrderLines();
            }
        }

        public Guid CustomerID
        {
            set
            {
                LabelCustomerID.Text = value.ToString();
                URLPopUpControlCustomerRelationLocation.URLToPopup = "webformpopup.aspx?uc=CustomerRelationLocationOverview&Id=" + LabelCustomerID.Text;
                ComboBoxCustomerLocation.DataBind();
            }
            get
            {
                return new Guid(LabelCustomerID.Text);
            }
        }


        public Guid LocationID
        {
            set
            {
                LabelLocationID.Text = value.ToString();
                EntityDataSourceMaterials.CommandParameters["LocationId"].DefaultValue = LocationID.ToString();
                CheckAlternativeMaterialsSettings();
            }
            get
            {
                return new Guid(LabelLocationID.Text);
            }
        }
        
        public DateTime StartRentDate
        {
            set
            {
                LabelStartRentDate.Text = Common.DateTimeToString(value);
                EntityDataSourceMaterials.CommandParameters["StartDate"].DefaultValue = Common.ReturnEntitySQLDateTimeString(  value);
                CheckAlternativeMaterialsSettings();
            }
            get
            {
                return Common.StringToDateTime(LabelStartRentDate.Text);
            }
        }

        public DateTime EndRentDate
        {
            set
            {
                LabelEndRentDate.Text = Common.DateTimeToString(value);
                EntityDataSourceMaterials.CommandParameters["EndDate"].DefaultValue = Common.ReturnEntitySQLDateTimeString(value);
                CheckAlternativeMaterialsSettings();
            }
            get
            {
                return Common.StringToDateTime(LabelEndRentDate.Text);
            }
        }

        public List<RentalItemActivityListItem> OrderLines = new List<RentalItemActivityListItem>();
        public void LoadOrderLines()
        {
            // load the order lines from the label into the OrderLines
            if (LabelOrderLines.Text != "")
            {
                StringReader TempData = new StringReader(LabelOrderLines.Text);
                XmlTextReader CurrentLines = new XmlTextReader(TempData);
                OrderLines = new List<RentalItemActivityListItem>();

                while (CurrentLines.Read())
                {
                    if (CurrentLines.Name == "OrderLine")
                    {
                        RentalItemActivityListItem RIA = new RentalItemActivityListItem();

                        RIA.DiscountPercentage = Convert.ToDouble(CurrentLines.GetAttribute("discountpercentage"));
                        RIA.RentPrice = Convert.ToDouble(CurrentLines.GetAttribute("rentprice"));
                        RIA.TotalRentPrice = Convert.ToDouble(CurrentLines.GetAttribute("totalrentprice"));
                        RIA.Vat = Convert.ToDouble(CurrentLines.GetAttribute("vat"));
                        RIA.RentalItemAmount = Convert.ToInt32(CurrentLines.GetAttribute("rentalitemamount"));

                        RIA.RentalItem = CurrentLines.GetAttribute("rentalitem");
                        RIA.RentalType = CurrentLines.GetAttribute("rentaltype");
                        RIA.CustomerLocation= CurrentLines.GetAttribute("customerlocation");

                        RIA.Id = new Guid(CurrentLines.GetAttribute("id"));
                        RIA.RentalItemId = new Guid( CurrentLines.GetAttribute("rentalitemid"));
                        RIA.RentalTypeId = new Guid( CurrentLines.GetAttribute("rentaltypeid"));
                        RIA.CustomerLocationId = new Guid( CurrentLines.GetAttribute("customerlocationid"));

                        RIA.BailPrice = Convert.ToDouble(CurrentLines.GetAttribute("bailprice"));
                        RIA.TreatAsAdvancePayment = Convert.ToBoolean(CurrentLines.GetAttribute("treatasadvancepayment"));

                        OrderLines.Add(RIA);
                    }
                }
            }
        }

        public bool HasOrderLines()
        {
            LoadOrderLines();
            return OrderLines.Count > 0;
        }

        public void SaveOrderLines()
        {
            StringWriter TempData = new StringWriter();
            XmlTextWriter CurrentLines = new XmlTextWriter(TempData);

            CurrentLines.WriteStartElement("OrderLines");
            for (int i = 0; i < OrderLines.Count; i++)
            {
                RentalItemActivityListItem RIA = OrderLines[i];

                CurrentLines.WriteStartElement("OrderLine");

                CurrentLines.WriteAttributeString("discountpercentage", RIA.DiscountPercentage.ToString());
                CurrentLines.WriteAttributeString("rentprice", RIA.RentPrice.ToString());
                CurrentLines.WriteAttributeString("totalrentprice", RIA.TotalRentPrice.ToString());
                CurrentLines.WriteAttributeString("vat", RIA.Vat.ToString());
                CurrentLines.WriteAttributeString("rentalitemamount", RIA.RentalItemAmount.ToString());
                CurrentLines.WriteAttributeString("rentalitem", RIA.RentalItem);
                CurrentLines.WriteAttributeString("rentaltype", RIA.RentalType);
                CurrentLines.WriteAttributeString("id", RIA.Id.ToString());
                CurrentLines.WriteAttributeString("rentalitemid", RIA.RentalItemId.ToString());
                CurrentLines.WriteAttributeString("rentaltypeid", RIA.RentalTypeId.ToString());
                CurrentLines.WriteAttributeString("customerlocationid", RIA.CustomerLocationId.ToString());
                CurrentLines.WriteAttributeString("customerlocation", RIA.CustomerLocation);
                CurrentLines.WriteAttributeString("bailprice", RIA.BailPrice.ToString());
                CurrentLines.WriteAttributeString("treatasadvancepayment", RIA.TreatAsAdvancePayment.ToString());

                CurrentLines.WriteEndElement();
            }
            CurrentLines.WriteEndElement();

            TempData.Close();
            LabelOrderLines.Text = TempData.ToString();
        }

        protected void DropDownListMaterials_SelectedIndexChanged(object sender, EventArgs e)
        {
            EntityDataSourceMaterials.CommandParameters["RentalType"].DefaultValue = ComboBoxMaterialType.SelectedValue;
            ComboBoxMaterialType.DataBind();
            ComboBoxMaterials.DataBind();

            // Check if we can enable the URLPopUpControlAlternativeMaterials
            CheckAlternativeMaterialsSettings();
        }

        private void CheckAlternativeMaterialsSettings()
        {
            RentalType rt = ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.RentalTypeSet", "Id", new Guid(ComboBoxMaterialType.SelectedValue))) as RentalType;
            URLPopUpControlAlternativeMaterials.Visible = rt.AlternativeRentalTypes.Count > 0;
            URLPopUpControlAlternativeMaterials.URLToPopup = "WebFormPopUp.aspx?uc=RentAlternativeMaterials&RentalType=" + ComboBoxMaterialType.SelectedValue +
                "&LocationId=" + EntityDataSourceMaterials.CommandParameters["LocationId"].DefaultValue +
                "&StartDate=" + EntityDataSourceMaterials.CommandParameters["StartDate"].DefaultValue +
                "&EndDate=" + EntityDataSourceMaterials.CommandParameters["EndDate"].DefaultValue;
        }

        protected void URLPopUpControlCustomerRelationLocation_PopupClosed(object sender, EventArgs e)
        {
            ComboBoxCustomerLocation.DataBind();
        }

        protected void URLPopUpControlRentMaterialTypes_PopupClosed(object sender, EventArgs e)
        {
            ComboBoxMaterialType.DataBind();
        }

        public int TotalRentalItemAmount = 0;
        public double TotalRentPrice = 0, TotalBailPrice =0, TotalRentPriceExVAT=0;
        public void RecalcTotals()
        {
            TotalRentPrice = 0; 
            TotalBailPrice = 0;
            TotalRentalItemAmount = 0;
            TotalRentPriceExVAT =0;

            foreach (RentalItemActivityListItem ria in OrderLines)
            {
                TotalRentalItemAmount = ria.RentalItemAmount + TotalRentalItemAmount;
                TotalRentPrice = ria.TotalRentPrice + TotalRentPrice;
                TotalBailPrice = ria.BailPrice + TotalBailPrice;
                TotalRentPriceExVAT = ria.RentPrice + TotalRentPriceExVAT;
            }
            LabelTotalPriceVAT.Text = TotalRentPrice.ToString();
            LabelTotalPrice.Text = TotalRentPriceExVAT.ToString();
        }

        public void AddRentMaterialsToRentLedgerAndRentalItemActivitySet(ModelTMSContainer ControlObjectContext, RentLedger rl)
        {
            LoadOrderLines();

            // add each rental item line
            foreach (RentalItemActivityListItem ria in OrderLines)
            {
                RentalItemActivity riaactivity = new RentalItemActivity();

                // hookup items
                riaactivity.RentalItem = ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.RentalItemSet", "Id", ria.RentalItemId)) as RentalItem;

                // create rentalitemactivity line
                riaactivity.RentStartDateTime = StartRentDate;
                riaactivity.RentEndStartDateTime = EndRentDate;
                riaactivity.Description = riaactivity.RentalItem.Description;
                riaactivity.InvoiceStatus = "Open";
                riaactivity.CalculatedRentPrice = ria.RentPrice;
                riaactivity.BaseRentPrice = ria.RentPrice;
                riaactivity.DiscountPercentage = ria.DiscountPercentage;
                riaactivity.VATRentPrice = ria.Vat;
                riaactivity.TotalRentPrice = ria.TotalRentPrice;
                riaactivity.IsTreatedAsAdvancePayment = ria.TreatAsAdvancePayment;
                riaactivity.GenerateDescription();

                riaactivity.UpdateAdvancePaymentStatus(ControlObjectContext, true, ria.TreatAsAdvancePayment);

                rl.RentalItemActivity.Add(riaactivity);
            }
        }

        protected void GridViewOrderMaterials_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            e.Cancel = true;

            LoadOrderLines();

            Guid SearchGuidID = Guid.Parse(e.Keys[0].ToString());

            for (int i = OrderLines.Count - 1; i >= 0; i--)
            {
                RentalItemActivityListItem TempLine = OrderLines[i];
                if (TempLine.Id == SearchGuidID)
                {
                    OrderLines.RemoveAt(i);
                    break;
                }
            }

            SaveOrderLines();
        }


    }
}