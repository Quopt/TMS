using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TMS_Recycling
{
    public partial class WebUserControlRentMaterialBase : ClassTMSUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetName = "RentalItem";

            URLPopUpControlLink.URLToPopup = "WebFormPopup.aspx?UC=ShowLinks&UCE=RentLedgerBase&BO=RentalItemActivitySet as it inner join RentalItemSet as ra on it.RentalItem.Id = ra.Id&SF=it.Id,it.Description,it.RentStartDateTime,it.RentEndStartDateTime,it.TotalRentPrice&DF=x,Omschrijving,Verhuurdatum en -tijd start,Eind,Verhuurprijs&SEL=ra.Id&ORD=it.RentStartDateTime desc&LNK=Id&ID=" + KeyID.ToString();

            if (!IsPostBack)
            {
                Common.AddRentalItemStateList(ComboBox_ItemState_SelectedValue.Items, true);
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