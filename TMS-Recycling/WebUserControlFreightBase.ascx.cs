using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TMS_Recycling
{
    public partial class WebUserControlFreightBase : ClassTMSUserControl
    {

        public void InitUserControl()
        {
            SetName = "Freight";

            if (!IsPostBack)
            {
                //Common.AddInvoiceStatusList(DropDownList_InvoiceStatus_SelectedValue.Items, true);
                Common.AddFreightTypeList(DropDownList_FreightType_SelectedValue.Items, true);
                Common.AddFreightStatusList(DropDownList_FreightStatus_SelectedValue.Items, true);
                Common.AddFreightDirectionList(DropDownList_FreightDirection_SelectedValue.Items, true);

                // check for a freightid
                if (Request.Params["FreightId"] != null)
                {
                    KeyID = new Guid(Request.Params["FreightId"].ToString());
                }
            }

            URLPopUpControlWeighing.Visible = false;
            URLPopUpControlSorting.Visible = false;
            URLPopUpControlInvoice.Visible = false;
            URLPopUpControlSort.Visible = false;
            if (DataItem != null)
            {
                Freight frg = DataItem as Freight;
                if (frg != null)
                {
                    string InvoiceType = "";
                    if (frg.FreightDirection == "To warehouse")
                    {
                        InvoiceType = "Buy";
                    }
                    else
                    {
                        InvoiceType = "Sell";
                    }

                    URLPopUpControlSorting.URLToPopup = "WebFormPopup.aspx?UC=ShowReport&d=DataSetSorting&r=ReportSortingA4&Id=" + frg.Id.ToString() + "&InvoiceType=" + InvoiceType;
                    URLPopUpControlWeighing.URLToPopup = "WebFormPopup.aspx?UC=ShowReport&d=DataSetWeighing&r=ReportWeighingA4&Id=" + frg.Id.ToString() ;
                    URLPopUpControlInvoice.URLToPopup = "WebFormPopup.aspx?UC=FreightInvoice&FreightId=" + frg.Id.ToString();
                    URLPopUpControlSort.URLToPopup = "WebFormPopup.aspx?UC=FreightFinish&FreightId=" + frg.Id.ToString();
                    URLPopUpControlLegalDocuments.URLToPopup = "WebFormPopup.aspx?UC=FreightLegalDocuments&FreightId=" + frg.Id.ToString();

                    URLPopUpControlSorting.Visible = (frg.FreightSortingMaterial.Count > 0);
                    URLPopUpControlSorting.Visible = true;
                    URLPopUpControlWeighing.Visible = (frg.FreightWeighing.Count > 0);
                    URLPopUpControlInvoice.Visible = true;
                    URLPopUpControlSort.Visible = true;
                }
            }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            // initaliaze this user control
            InitUserControl();
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
            StandardButtonDeleteClickHandler(sender, e);
        }

        protected void GeneralOnPopupClosed(object sender, EventArgs e)
        {
            RebindControls();
        }

    }
}