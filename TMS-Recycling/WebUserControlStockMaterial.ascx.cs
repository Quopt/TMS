using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Objects;

namespace TMS_Recycling
{
    public partial class WebUserControlStockMaterial : ClassTMSUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetName = "Material";

            if (!IsPostBack)
            {
                Common.AddMaterialCategoryList(DropDownList_Category_SelectedValue.Items, true);
                Common.AddMaterialInvoiceTypeList(DropDownList_InvoiceType_SelectedValue.Items, true);
                Common.AddBaselCodeList(DropDownList_BaselCode_SelectedValue.Items, true);
                Common.AddHCodeList(DropDownList_HCode_SelectedValue.Items, true);
                Common.AddPhysicalShapeList(DropDownList_PhysicalShape_SelectedValue.Items, true);
            }
        }

        void Page_PreRender(object sender, EventArgs e)
        {
            DropDownListMaterialStockPosition.DataBind();
            DropDownListMaterialStockPosition.SelectedValue = (DataItem as Material).MaterialId.ToString();

            URLPopUpControlCorrectStockLevel.URLToPopup = "WebFormPopUp.aspx?uc=StockCorrectLevel&Id=" + KeyID.ToString();
        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            StandardButtonCancelClickHandler(sender, e);
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            (DataItem as Material).MaterialId = Guid.Parse(DropDownListMaterialStockPosition.SelectedValue);
            StandardButtonSaveClickHandler(sender, e);
        }

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            StandardButtonDeleteClickHandler(sender, e);
        }

        protected void URLPopUpControlCorrectStockLevel_OnBeforePopUpOpened(object sender, EventArgs e)
        {
            ButtonSave_Click(sender, e);
        }

        protected void URLPopUpControlCorrectStockLevel_OnPopupClosed(object sender, EventArgs e)
        {
            RebindControls();
        }

        protected void ButtonResetAvgPurchasePrice_Click(object sender, EventArgs e)
        {
            Material Mat = (DataItem as Material);
            Mat.AvgPurchasePriceTotalPrice = 0;
            Mat.AvgPurchasePriceTotalWeight = 0;
            Mat.AvgPurchasePrice = 0;
            ControlObjectContext.SaveChanges();
            RebindControls();
        }

        protected void ButtonResetAvgSalesPrice_Click(object sender, EventArgs e)
        {
            Material Mat = (DataItem as Material);
            Mat.AvgSalesPriceTotalPrice = 0;
            Mat.AvgSalesPriceTotalWeight = 0;
            Mat.AvgSalesPrice = 0;
            ControlObjectContext.SaveChanges();
            RebindControls();
        }

    }
}