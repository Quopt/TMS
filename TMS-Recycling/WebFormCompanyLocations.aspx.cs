using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Objects;

namespace TMS_Recycling
{
    public partial class WebFormCompanyLocations : ClassTMSWebPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        void Page_PreRender(object sender, EventArgs e)
        {
            if (WebUserControlCompanyLocation1.RefreshRequired)
            {
                WebUserControlCompanyLocation1.RefreshRequired = false;
                WebUserControlCompanyLocation1.Visible = false;
                ButtonSearch_Click(sender, e);
            }
        }

        protected void GridViewResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            WebUserControlCompanyLocation1.DataBind();
            WebUserControlCompanyLocation1.KeyID = new System.Guid(GridViewResults.SelectedDataKey.Value.ToString());
            WebUserControlCompanyLocation1.Visible = true;
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            EntityDataSourceCompanyLocation.DefaultContainerName = EntityDataSourceCompanyLocation.DefaultContainerName;
            EntityDataSourceCompanyLocation.DataBind();
        }

        protected void ButtonNew_Click(object sender, EventArgs e)
        {
            Location NewObj = new Location();
            ModelTMSContainer Temp = new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session);

            NewObj.DefaultWeighingTariffBookingCode = Temp.LedgerBookingCodeSet.First<LedgerBookingCode>();
            NewObj.DefaultBailPriceBookingCode = Temp.LedgerBookingCodeSet.First<LedgerBookingCode>();
            Temp.AddToLocationSet(NewObj);
            
            Temp.SaveChanges(SaveOptions.DetectChangesBeforeSave);

            WebUserControlCompanyLocation1.KeyID = NewObj.Id;
            WebUserControlCompanyLocation1.Visible = true;

        }
    }
}