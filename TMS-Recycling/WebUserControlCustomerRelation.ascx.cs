using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TMS_Recycling
{
    public partial class WebUserControlCustomerRelation : ClassTMSUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetName = "Relation";

            if (!IsPostBack)
            {
                Common.AddCurrencyList(DropDownList_PreferredCurrency_SelectedValue.Items, true);
                Common.AddRelationTypeList(DropDownList_CustomerType_SelectedValue.Items, true);
                Common.AddCountryList(DropDownList_Country_SelectedValue.Items, true);
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