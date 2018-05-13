using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Objects;

namespace TMS_Recycling
{
    public partial class WebUserControlRentMaterialTypeOverview : ClassTMSUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ButtonSearch_Click(sender, e);
        }

        void Page_PreRender(object sender, EventArgs e)
        {
            if (WebUserControlRentMaterialTypeBase1.RefreshRequired)
            {
                WebUserControlRentMaterialTypeBase1.RefreshRequired = false;
                WebUserControlRentMaterialTypeBase1.Visible = false;
                ButtonSearch_Click(sender, e);
            }
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            EntityDataSourceGridBase.DefaultContainerName = EntityDataSourceGridBase.DefaultContainerName;
            EntityDataSourceGridBase.WhereParameters["Description"].DefaultValue = "%" + TextBoxFilterName.Text + "%";
            if (CheckBoxFilterIsActive.Checked)
            {
                EntityDataSourceGridBase.WhereParameters["IsActive"].DefaultValue = "true";
            }
            else
            {
                EntityDataSourceGridBase.WhereParameters["IsActive"].DefaultValue = "false";
            }
            EntityDataSourceGridBase.DataBind();
        }

        protected void GridViewResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            WebUserControlRentMaterialTypeBase1.KeyID = new System.Guid(GridViewResults.SelectedDataKey.Value.ToString());
            WebUserControlRentMaterialTypeBase1.Visible = true;
            WebUserControlRentMaterialTypeBase1.LoadRentalTypesCheckBoxes();
            WebUserControlRentMaterialTypeBase1.DataBind();
        }

        protected void ButtonNew_Click(object sender, EventArgs e)
        {
            RentalType NewMat = new RentalType();
            ModelTMSContainer Temp = new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session);

            NewMat.Description = "Materiaalsoort " + Common.CurrentClientDateTime(Session).ToString();
            NewMat.LedgerBookingCode = Temp.LedgerBookingCodeSet.First<LedgerBookingCode>();
                
            Temp.AddToRentalTypeSet(NewMat);
            Temp.SaveChanges(SaveOptions.DetectChangesBeforeSave);

            WebUserControlRentMaterialTypeBase1.KeyID = NewMat.Id;
            WebUserControlRentMaterialTypeBase1.Visible = true;
        }

        public bool DetailVisible()
        {
            return WebUserControlRentMaterialTypeBase1.Visible;
        }

        public Guid DetailKeyId()
        {
            return WebUserControlRentMaterialTypeBase1.KeyID;
        }
    }
}