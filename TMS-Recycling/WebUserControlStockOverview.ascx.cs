using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Objects;

namespace TMS_Recycling
{
    public partial class WebUserControlStockOverview : ClassTMSUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ButtonSearch_Click(sender, e);
        }

        void Page_PreRender(object sender, EventArgs e)
        {
            if (WebUserControlStockMaterialDetail.RefreshRequired)
            {
                WebUserControlStockMaterialDetail.RefreshRequired = false;
                WebUserControlStockMaterialDetail.Visible = false;
                ButtonSearch_Click(sender, e);
            }
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            EntityDataSourceGridBase.DefaultContainerName = EntityDataSourceGridBase.DefaultContainerName;
            EntityDataSourceGridBase.CommandParameters["Description"].DefaultValue = "%" + TextBoxFilterName.Text + "%";
            EntityDataSourceGridBase.CommandParameters["LocationDescription"].DefaultValue = ComboBoxLocation1.Text == "" ? "%" : "%" + ComboBoxLocation1.Text + "%";
            if (CheckBoxFilterIsActive.Checked)
            {
                EntityDataSourceGridBase.CommandParameters["IsActive"].DefaultValue = "true";
            }
            else
            {
                EntityDataSourceGridBase.CommandParameters["IsActive"].DefaultValue = "false";
            }
            EntityDataSourceGridBase.DataBind();
        }

        protected void GridViewResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            WebUserControlStockMaterialDetail.Visible = true;
            WebUserControlStockMaterialDetail.DataBind();
            WebUserControlStockMaterialDetail.KeyID = new System.Guid(GridViewResults.SelectedDataKey.Value.ToString());
        }

        protected void ButtonNew_Click(object sender, EventArgs e)
        {
            Material NewMat = new Material();
            ModelTMSContainer Temp = new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session);

            NewMat.PurchaseLedgerBookingCode = Temp.LedgerBookingCodeSet.First();
            NewMat.SalesLedgerBookingCode = Temp.LedgerBookingCodeSet.First();
            NewMat.MaterialUnit = Temp.MaterialUnitSet.First();
            NewMat.Location = Temp.LocationSet.First();
            NewMat.Category = "Other";

            Temp.AddToMaterialSet(NewMat);
            Temp.SaveChanges( SaveOptions.DetectChangesBeforeSave );

            WebUserControlStockMaterialDetail.KeyID = NewMat.Id;
            WebUserControlStockMaterialDetail.Visible = true;
        }

        public bool DetailVisible()
        {
            return WebUserControlStockMaterialDetail.Visible;
        }

        public Guid DetailKeyId()
        {
            return WebUserControlStockMaterialDetail.KeyID;
        }

    }

    
}