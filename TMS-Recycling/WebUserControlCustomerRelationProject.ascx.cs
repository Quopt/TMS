using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TMS_Recycling
{
    public partial class WebUserControlCustomerRelationProject : ClassTMSUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetName = "RelationProject";

            URLPopUpControlLink.URLToPopup = "WebFormPopup.aspx?UC=ShowLinks&UCE=OrderBase&BO=OrderSet as it inner join RelationProjectSet as ra on it.RelationProject.Id = ra.Id&SF=it.Id,it.Description,it.OrderNumber,it.BookingDateTime&DF=x,Omschrijving,Ordernummer,Boekdatum en -tijd&SEL=ra.Id&ORD=it.BookingDateTime desc&LNK=Id&ID=" + KeyID.ToString();
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