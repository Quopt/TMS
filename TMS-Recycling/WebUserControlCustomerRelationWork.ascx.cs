using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TMS_Recycling
{
    public partial class WebUserControlCustomerRelationWork : ClassTMSUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetName = "RelationWork";

            if (!IsPostBack)
            {
                //Common.AddRelactionContactFollowUpStateList(DropDownList_FollowUpState_SelectedValue.Items, true);
                //Common.AddRelactionContactTypeList(DropDownList_FollowUpState_SelectedValue.Items, true);
                Common.AddWorkTypeList(DropDownList_WorkType_SelectedValue.Items, true);
            }

            URLPopUpControlLink.URLToPopup = "WebFormPopup.aspx?UC=ShowLinks&UCE=InvoiceBase&BO=InvoiceSet as it inner join InvoiceLineSet as il on il.Invoice.Id = it.Id inner join RelationWorkSet as ra on il.RelationWork.Id = ra.Id&SF=it.Id,it.Description,it.InvoiceNumber, it.BookingDateTime&DF=x,Omschrijving,Factuurnummer,Boekdatum en -tijd&SEL=ra.Id&ORD=it.BookingDateTime desc&LNK=Id&ID=" + KeyID.ToString();
            URLPopUpControlShowContract.URLToPopup = "WebFormPopup.aspx?UC=ShowReport&d=DataSetRelationWork&r=ReportRelationWork&Id=" + KeyID.ToString();

        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            StandardButtonCancelClickHandler(sender, e);
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            StandardButtonSaveClickHandler(sender, e);

            RebindControls();
        }

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            StandardButtonDeleteClickHandler(sender, e);
        }
    }
}