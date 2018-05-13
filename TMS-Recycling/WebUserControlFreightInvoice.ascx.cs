using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TMS_Recycling
{
    public partial class WebUserControlFreightInvoice : ClassTMSUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // check if a freight is given on the Url
                if (Request.Params["FreightId"] != null)
                {
                    Freight frg = Freight.SelectFreightByFreightId(new Guid(Request.Params["FreightId"]), new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session));
                    if (frg != null)
                    {
                        TextBoxOrderNumber.Text = frg.OurReference.ToString();
                        CurrentPageNr = 2;
                        EnableCorrectScreenElements();

                        if (Request.Params["Speed"] != null)
                        {
                            CurrentPageNr = 3;
                            EnableCorrectScreenElements();
                            if (WebUserControlCashPurchase1.Visible)
                            {
                                WebUserControlCashPurchase1.FreightSpeedCycle();
                            }
                            else
                            {
                                WebUserControlNonCashPurchase1.FreightSpeedCycle();
                            }
                        }
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
            WebUserControlCashPurchase1.Visible = false;
            WebUserControlNonCashPurchase1.Visible = false;

            PanelSecondWeighingOrderNumber.Visible = true;
            PanelSecondWeighingOrderNumber.Enabled = false;
            PanelInvoiceInformation.Visible = false;
            PanelInvoiceInformation.Enabled = false;

            ButtonContinue.Visible = false;
            ButtonRevert.Visible = false;

            switch (CurrentPageNr)
            {
                case 1 :
                    ButtonContinue.Visible = true;
                    PanelSecondWeighingOrderNumber.Enabled = true;
                    break;
                case 2:
                    ButtonContinue.Visible = true;
                    ButtonRevert.Visible = true;
                    PanelInvoiceInformation.Visible = true;
                    PanelInvoiceInformation.Enabled = true;

                    // check if this is a valid freight number
                    bool Success = false;
                    try
                    {
                        long FreightNr = Convert.ToInt64(TextBoxOrderNumber.Text);
                        Freight frg = Freight.SelectFreightByFreightNr(FreightNr, new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session));
                        if (frg != null)
                        {
                            CurrentWeighingId = frg.Id;
                            Success = true;

                            string InvoiceType = "";
                            if (frg.FreightDirection == "To warehouse")
                            {
                                InvoiceType = "Buy";
                            }
                            else
                            {
                                InvoiceType = "Sell";
                            }

                            URLPopUpControlShowSorting.URLToPopup = "WebFormPopup.aspx?UC=ShowReport&d=DataSetSorting&r=ReportSortingA4&Id=" + CurrentWeighingId.ToString() + "&InvoiceType=" + InvoiceType;

                            if (frg.FreightStatus != "To be invoiced")
                            {
                                Common.InformUser(Page,"Deze vracht is nog niet uitgezocht of al gefactureerd. U kunt deze vracht nogmaals factureren. Deze wordt dan nogmaals op een factuur geplaatst.");
                            }
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

                    break;
                case 3:
                    PanelInvoiceInformation.Visible = true;

                    switch (RadioButtonListInvoiceType.SelectedIndex)
                    {
                        case 0:
                            WebUserControlCashPurchase1.Visible = true;
                            WebUserControlCashPurchase1.LoadFromFreight( Freight.SelectFreightByFreightId(CurrentWeighingId, new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session) ), false);
                            break;
                        case 1:
                            WebUserControlNonCashPurchase1.Visible = true;
                            WebUserControlNonCashPurchase1.LoadFromFreight( Freight.SelectFreightByFreightId(CurrentWeighingId, new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session) ), false);
                            break;
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

        protected void ButtonNewOrder_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsoluteUri); 
        }
    }
}