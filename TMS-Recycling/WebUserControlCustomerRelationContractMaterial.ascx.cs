using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TMS_Recycling
{
    public partial class WebUserControlCustomerRelationContractMaterial : ClassTMSUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetName = "RelationContractMaterial";

            if (Request.Params["Id"] != null)
            {
                KeyID = new Guid(Request.Params["Id"].ToString());
            }

            if (!IsPostBack)
            {

            }
        }

        protected void Page_PreRender(object Sender, EventArgs e)
        {
            if (DataItem != null)
            {
                RelationContractMaterial rcm = DataItem as RelationContractMaterial;
                RelationContract rc = rcm.RelationContract;

                if (rc != null)
                {
                    LabelOnContract.Visible = rc.HasContractGuidance;
                    LabelOnContract_AvgStockUnits.Visible = rc.HasContractGuidance;
                    LabelOnContractPrice.Visible = rc.HasContractGuidance;
                    LabelOnContractAvgPricePerUnit.Visible = rc.HasContractGuidance;
                    LabelRequiredProfitContractGuidance.Visible = rc.HasContractGuidance;
                    TextBox_AvgRequiredProfitPerUnit.Visible = rc.HasContractGuidance;

                    if (rc.HasContractGuidance)
                    {
                        LabelOnContractAvgPricePerUnit.Text = rcm.AvgStockUnitPrice().ToString();
                    }
                }
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
            Visible = false;
        }
    }
}