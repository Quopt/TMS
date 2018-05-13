using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace TMS_Recycling
{
    public partial class WebUserControlCustomerRelationContract : ClassTMSUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetName = "RelationContract";

            if (!IsPostBack)
            {
                Common.AddRelationContractStatusList(DropDownList_ContractStatus_SelectedValue.Items, true);
                Common.AddContractTypeList(DropDownList_ContractType_SelectedValue.Items, true);
            }

            URLPopUpControlLink.URLToPopup = "WebFormPopup.aspx?UC=ShowLinks&UCE=OrderBase&BO=OrderSet as it inner join OrderLineSet as ol on it.id=ol.Order.Id inner join RelationContractMaterialSet as ra on ol.RelationContractMaterial.Id = ra.Id inner join RelationContractSet as rcs on ra.RelationContract.Id = rcs.Id&SF=it.Id,it.Description,it.OrderNumber,it.BookingDateTime&DF=x,Omschrijving,Ordernummer,Boekdatum en -tijd&SEL=rcs.Id&ORD=it.BookingDateTime desc&LNK=Id&ID=" + KeyID.ToString();
            URLPopUpControlShowContract.URLToPopup = "WebFormPopup.aspx?UC=ShowReport&d=DataSetRelationContract&r=ReportRelationContract&Id=" + KeyID.ToString();

            URLPopUpControlContractGuidance.URLToPopup = "WebFormPopup.aspx?UC=ShowLinks&UCE=OrderBase&BO=OrderSet as it inner join MaterialMutationSet as mms on it.id=mms.Order.Id inner join ContractGuidanceMaterialMutationSet as cmms on cmms.Id=mms.ContractGuidanceMaterialMutation.Id inner join RelationContractMaterialSet as rcms on rcms.Id=cmms.RelationContractMaterial.Id inner join RelationContractSet as rcs on rcs.Id=rcms.RelationContract.Id&SF=it.Id,it.Description,it.OrderNumber,it.BookingDateTime&DF=x,Omschrijving,Ordernummer,Boekdatum en -tijd&SEL=rcs.Id&ORD=it.BookingDateTime desc&LNK=Id&ID=" + KeyID.ToString();
        }

        protected void Page_PreRender(object Sender, EventArgs e)
        {
            // if contractguidance is active then set the right warning color
            LabelContractGuidanceFeedback.Visible = false;
            CheckBox_HasContractGuidance_Checked.BackColor = LabelHasContractGuidance.BackColor;
            if ( (DataItem != null) && ((DataItem as RelationContract).HasContractGuidance) )
            {
                RelationContract rcm = DataItem as RelationContract;
                ContractGuidanceFillType cgft = rcm.ContractGuidanceFilled();
                LabelContractGuidanceFeedback.Visible = true;

                switch (cgft)
                {
                    case ContractGuidanceFillType.NotEnoughToReachMin :
                        CheckBox_HasContractGuidance_Checked.BackColor = Color.Red;
                        if (rcm.ContractType == "Buy")
                        {
                            LabelContractGuidanceFeedback.Text = "Nog onvoldoende materiaal aanwezig voor aankoop";
                        }
                        else
                        {
                            LabelContractGuidanceFeedback.Text = "Nog onvoldoende materiaal aanwezig voor uitlevering";
                        }
                        break;
                    case ContractGuidanceFillType.BetweenMinAndMax :
                        CheckBox_HasContractGuidance_Checked.BackColor = Color.Orange;
                        LabelContractGuidanceFeedback.Text = "Tussen min en max hoeveelheid aanwezig";
                        break;
                    case ContractGuidanceFillType.Filled :
                        CheckBox_HasContractGuidance_Checked.BackColor = Color.Green;
                        if (rcm.ContractType == "Buy")
                        {
                            LabelContractGuidanceFeedback.Text = "Voldoende materiaal aanwezig voor aankoop";
                        }
                        else
                        {
                            LabelContractGuidanceFeedback.Text = "Voldoende materiaal aanwezig voor uitlevering";
                        }
                        break;
                }

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
            StandardButtonDeleteClickHandler(sender, e);
        }
    }
}