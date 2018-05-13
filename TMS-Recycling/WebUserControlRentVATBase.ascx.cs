using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Objects;

namespace TMS_Recycling
{
    public partial class WebUserControlRentVATBase : ClassTMSUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetName = "RentalTypeVAT";
        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            StandardButtonCancelClickHandler(sender, e);
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            // grab data from the screen into the object
            SaveDataIntoDataItemFromControls();

            // check if there already is a VAT rate for this location
            RentalTypeVAT rtv = DataItem as RentalTypeVAT;
            ObjectQuery<RentalTypeVAT> oqsm = ControlObjectContext.RentalTypeVATSet.Where("(it.RentalType.Id=@id) and (it.IsActive) and (it.Location.Id = @LocationId)",  new ObjectParameter("id", rtv.RentalType.Id), new ObjectParameter("LocationId", rtv.Location.Id) );

            // check if this is a new save. then the count border is lower.
            int CountBorder = 0;
            ObjectQuery<RentalTypeVAT> ExistenceCheck = ControlObjectContext.RentalTypeVATSet.Where("(it.Id=@id) and (it.IsActive)", new ObjectParameter("id", rtv.Id ));
            if (ExistenceCheck.Count() >= 1) { CountBorder = 1; }

            // now check if we may add or just shed a warning to the user
            if ((rtv.IsActive) && (oqsm.Count() > CountBorder))
            {
                Common.InformUser(Page, "U heeft al een BTW tarief voor deze lokatie toegevoegd. U kunt maar één actief BTW niveau voor een materiaaltype per lokatie opgeven.");
            }
            else
            {
                StandardButtonSaveClickHandler(sender, e);
            }
        }

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            StandardButtonDeleteClickHandler(sender, e);
        }
    }
}