using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace TMS_Recycling
{
    public partial class WebUserControlBookKeepingClosuresOverview :  ClassTMSUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CalendarControlStartDate.SelectedDate = Common.CurrentClientDate(Session).AddDays(-31);
                CalendarControlEndDate.SelectedDate = Common.CurrentClientDate(Session);
                ButtonSearch_Click(null, null);

                // get the ledger description
                if (Request.Params["Id"] != null)
                {
                    EntityKey TempKey = new EntityKey("ModelTMSContainer.LedgerSet", "Id", Guid.Parse(Request.Params["Id"]));
                    Ledger TempLedger = ControlObjectContext.GetObjectByKey(TempKey) as Ledger;
                    LabelObjectName.Text = TempLedger.Description;

                    // check if we are actual with material closures
                    LedgerSet.CheckLedgerClosures(ControlObjectContext, Page);

                }
                else
                { // this must be linked to the ledgerbookingcodes ...
                    EntityKey TempKey = new EntityKey("ModelTMSContainer.LedgerBookingCodeSet", "Id", Guid.Parse(Request.Params["LedgerBookingCodeId"]));
                    LedgerBookingCode TempLedger = ControlObjectContext.GetObjectByKey(TempKey) as LedgerBookingCode;
                    LabelObjectName.Text = TempLedger.Description;

                    // check if we are actual with material closures
                    LedgerBookingCodeSet.CheckLedgerBookingCodeClosures(ControlObjectContext, Page);
                }
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (WebUserControlBookKeepingClosureBase1.RefreshRequired)
            {
                WebUserControlBookKeepingClosureBase1.RefreshRequired = false;
                WebUserControlBookKeepingClosureBase1.Visible = false;
                ButtonSearch_Click(sender, e);
            }
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            EntityDataSourceClosures.DefaultContainerName = EntityDataSourceClosures.DefaultContainerName;

            EntityDataSourceClosures.WhereParameters["StartDate"].DefaultValue = CalendarControlStartDate.SelectedDate.ToString();
            EntityDataSourceClosures.WhereParameters["EndDate"].DefaultValue = CalendarControlEndDate.SelectedDate.AddDays(1).ToString();

            if (Request.Params["Id"] != null)
            {
                EntityDataSourceClosures.WhereParameters["ID"].DefaultValue = Request.Params["Id"];
            }
            else
            {
                EntityDataSourceClosures.WhereParameters["ID"].DefaultValue = Request.Params["LedgerBookingCodeId"];
            }

            EntityDataSourceClosures.DataBind();

        }

        protected void GridViewResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            WebUserControlBookKeepingClosureBase1.KeyID = new System.Guid(GridViewResults.SelectedDataKey.Value.ToString());
            WebUserControlBookKeepingClosureBase1.Visible = true;
        }

    }
}