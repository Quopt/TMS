using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace TMS_Recycling
{
    public partial class WebUserControlBookKeepingMutations : ClassTMSUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // get the ledger description
                if (Request.Params["Id"] != null)
                {
                    EntityKey TempKey = new EntityKey("ModelTMSContainer.LedgerSet", "Id", Guid.Parse(Request.Params["Id"]));
                    Ledger TempLedger = ControlObjectContext.GetObjectByKey(TempKey) as Ledger;
                    LabelLedgerName.Text = TempLedger.Description;
                }
                else
                { // this must be linked to the ledgerbookingcodes ...
                    EntityKey TempKey = new EntityKey("ModelTMSContainer.LedgerBookingCodeSet", "Id", Guid.Parse(Request.Params["LedgerBookingCodeId"]));
                    LedgerBookingCode TempLedger = ControlObjectContext.GetObjectByKey(TempKey) as LedgerBookingCode;
                    LabelLedgerName.Text = TempLedger.Description;
                }

                // set the correct filters
                CalendarControlStartDate.SelectedDate = new DateTime(Common.CurrentClientDate(Session).Year, 1, 1);
                CalendarControlEndDate.SelectedDate = Common.CurrentClientDate(Session);

                DropDownListBookingType.Items.Clear();
                DropDownListBookingType.Items.Add(new ListItem("Alle", ""));
                Common.AddLedgerBookingTypeList(DropDownListBookingType.Items, false);
                DropDownListBookingType.SelectedValue = "";

                ButtonSearch_Click(sender, e);
            }
        }

        void Page_PreRender(object sender, EventArgs e)
        {
            if (WebUserControlLedgerMutation1.RefreshRequired)
            {
                WebUserControlLedgerMutation1.RefreshRequired = false;
                WebUserControlLedgerMutation1.Visible = false;
                ButtonSearch_Click(sender, e);
            }
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            EntityDataSourceGridBase.DefaultContainerName = EntityDataSourceGridBase.DefaultContainerName;

            if (Request.Params["Id"] != null)
            {
                EntityDataSourceGridBase.CommandParameters["Id"].DefaultValue = Request.Params["Id"];
            }
            else
            {
                EntityDataSourceGridBase.CommandParameters["Id"].DefaultValue = Request.Params["LedgerBookingCodeId"];
            }
            EntityDataSourceGridBase.CommandParameters["EmptyGUID"].DefaultValue = Guid.Empty.ToString();
            EntityDataSourceGridBase.CommandParameters["Description"].DefaultValue = TextBoxFilterName.Text == "" ? "%" : "%" + TextBoxFilterName.Text + "%";
            EntityDataSourceGridBase.CommandParameters["LocationDescription"].DefaultValue = ComboBoxLocationDescription.Text == "" ? "%" : "%" + ComboBoxLocationDescription.Text + "%";
            EntityDataSourceGridBase.CommandParameters["UserName"].DefaultValue = TextBoxUserName.Text == "" ? "%" : "%" + TextBoxUserName.Text + "%";
            EntityDataSourceGridBase.CommandParameters["BookingType"].DefaultValue = DropDownListBookingType.SelectedValue == "" ? "%" : "%" + DropDownListBookingType.SelectedValue + "%";
            EntityDataSourceGridBase.CommandParameters["StartDate"].DefaultValue = CalendarControlStartDate.SelectedDate.ToString();
            EntityDataSourceGridBase.CommandParameters["EndDate"].DefaultValue = CalendarControlEndDate.SelectedDate.AddDays(1).ToString();
            EntityDataSourceGridBase.CommandParameters["CorrectedStatus"].DefaultValue = "True";
            EntityDataSourceGridBase.CommandParameters["CorrectedStatus2"].DefaultValue = "False";
            if (DropDownListCorrected.SelectedValue == "1")
            {
                EntityDataSourceGridBase.CommandParameters["CorrectedStatus2"].DefaultValue = "True";
            }
            if (DropDownListCorrected.SelectedValue == "2")
            {
                EntityDataSourceGridBase.CommandParameters["CorrectedStatus"].DefaultValue = "False";
            }

            EntityDataSourceGridBase.DataBind();
        }

        protected void GridViewResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            WebUserControlLedgerMutation1.Visible = true;
            WebUserControlLedgerMutation1.DataBind();
            WebUserControlLedgerMutation1.KeyID = new System.Guid(GridViewResults.SelectedDataKey.Value.ToString());
        }

        public bool DetailVisible()
        {
            return WebUserControlLedgerMutation1.Visible;
        }

        public Guid DetailKeyId()
        {
            return WebUserControlLedgerMutation1.KeyID;
        }

    }
}