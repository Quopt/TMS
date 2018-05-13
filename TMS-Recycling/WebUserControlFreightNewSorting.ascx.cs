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
    public partial class WebUserControlFreightNewSorting : ClassTMSUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ComboBoxWeighingLocation.DataBind();
                ComboBoxCustomer.DataBind();

                // parse the FreightId and link this one up to that freight
                if (Request.Params["FreightId"] != null)
                {
                    //ModelTMSContainer ControlObjectContext = new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session);

                    CurrentWeighingId = new Guid(Request.Params["FreightId"].ToString());

                    Freight frg = Freight.SelectFreightByFreightId(CurrentWeighingId, ControlObjectContext);

                    if ((frg.FreightStatus == "In sorting") && (frg.FreightSortingMaterial.Count == 0))
                    {
                        // only create a new sorting form for an existing weighing if this weighing is "In sorting" and not filled in yet
                        PrepareOldWeighingForSorting();

                        // show form
                        CurrentPageNr = 2;
                    }
                    else
                    {
                        CurrentWeighingId = Guid.Empty;
                    }
                }

                Common.LimitLocationList(ComboBoxWeighingLocation.Items, Session, ControlObjectContext);
                ShowCorrectCustomer();
            }
        }

        protected bool PrepareOldWeighingForSorting()
        {
            // check whether this weighing may be deleted. If this is not the case then inform the user that this weighing is already processed.
            ModelTMSContainer ControlObjectContext = new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session);
            bool Success = false;

            // start transaction
            using (TransactionScope TS = new TransactionScope())
            {
                try
                {
                    // add weighing materual as sorting material
                    Freight frg = Freight.SelectFreightByFreightId(CurrentWeighingId, ControlObjectContext);
                    

                    frg.RecalcTotalWeightFromWeighing();

                    foreach (FreightWeighing fw in frg.FreightWeighing)
                    {
                        foreach (FreightWeighingMaterial fwm in fw.FreightWeighingMaterial)
                        {
                            FreightSortingMaterial fsm = new FreightSortingMaterial();

                            fsm.Material = fwm.Material;
                            fsm.Weight = fwm.NetWeight;
                            fsm.GrossWeight = fwm.GrossWeight;
                            fsm.TarraWeight = fwm.TarraWeight;
                            fsm.Freight = frg;

                            ControlObjectContext.AddToFreightSortingMaterialSet(fsm);
                        }
                    }

                    frg.RecalcTotalWeightFromSorting();

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

        protected void Page_PreRender(object sender, EventArgs e)
        {
            EnableCorrectScreenElements();
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

        protected void ButtonContinue_Click(object sender, EventArgs e)
        {
            CurrentPageNr++;
        }


        private void EnableCorrectScreenElements()
        {
            // disable all screen elements
            PanelInvoice.Visible = false;
            PanelFirstWeighing.Visible = true;
            PanelFirstWeighing.Enabled = false;
            PanelInvoice.Enabled = false;
            ButtonContinue.Visible = false;
            ButtonDestroy.Visible = false;
            ButtonFillIn.Visible = false;
            ButtonNew.Visible = false;

            switch (CurrentPageNr)
            {
                case 1:
                    ButtonContinue.Visible = true;
                    PanelFirstWeighing.Enabled = true;
                    break;
                case 2:
                    if (CurrentWeighingId == Guid.Empty) { ProcessNewSorting(); }

                    PanelFirstWeighing.Visible = false;
                    PanelInvoice.Enabled = true;
                    PanelInvoice.Visible = true;
                    ButtonFillIn.Visible = true;
                    ButtonNew.Visible = true;

                    string InvoiceType="";
                    if (RadioButtonListBuyOrSell.SelectedIndex == 0)
                    {
                        InvoiceType = "Buy";
                    }
                    else
                    {
                        InvoiceType = "Sell";
                    }

                    FrameShowInvoiceA.Attributes["src"] = "WebFormPopup.aspx?UC=ShowReport&d=DataSetSorting&r=ReportSortingA4&Id=" + CurrentWeighingId.ToString() + "&InvoiceType=" + InvoiceType;
                    break;
            }
        }

        protected bool ProcessNewSorting()
        {
            // create a new (empty) freight
            ModelTMSContainer ControlObjectContext = new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session);
            
            bool Success = false;

            // start transaction
            using (TransactionScope TS = new TransactionScope())
            {
                try
                {
                    Freight frg = new Freight();
                    frg.AssignFreightNumber(ControlObjectContext);
                    frg.Description = "Sorteerbon " + frg.OurReference + " / " + Common.CurrentClientDateTime(Session);
                    frg.FreightDirection = "To warehouse";
                    if (RadioButtonListBuyOrSell.SelectedIndex == 1)
                    {
                        frg.FreightDirection = "To customer";
                    }
                    frg.FreightType = "Weighing";
                    frg.SourceOrDestinationLocation = ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.LocationSet", "Id", Guid.Parse(ComboBoxWeighingLocation.SelectedValue))) as Location;
                    frg.FromRelation = ControlObjectContext.GetObjectByKey(new EntityKey("ModelTMSContainer.RelationSet", "Id", Guid.Parse(ComboBoxCustomer.SelectedValue))) as Relation;
                    frg.FreightStatus = "In sorting";

                    ControlObjectContext.AddToFreightSet(frg);

                    CurrentWeighingId = frg.Id;

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

        protected void ButtonNew_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsoluteUri); 
        }

        protected void ButtonFillIn_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebFormFreightFinish.aspx?FreightId=" + CurrentWeighingId.ToString());
        }

        protected void RadioButtonListBuyOrSell_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetCustomerType();
        }

        private void SetCustomerType()
        {
            ComboBoxCustomer.SelectedIndex = -1;

            if (RadioButtonListBuyOrSell.SelectedIndex == 0)
            {
                LabelCustomerType.Text = "Creditor";
            }
            else
            {
                LabelCustomerType.Text = "Debtor";
            }
            ShowCorrectCustomer();
        }

        protected void ButtonDestroy_Click(object sender, EventArgs e)
        {

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

        protected void DropDownListLocations_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowCorrectCustomer();
        }

    }
}