using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TMS_Recycling
{
    public partial class WebUserControlRentMaterialTypeBase : ClassTMSUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetName = "RentalType";

            if (!IsPostBack)
            {
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            SwitchOnCheckBoxesInAlternativeMaterialTypes();
        }

        public void LoadRentalTypesCheckBoxes()
        {
            RentalType rt = (DataItem as RentalType);
            CheckBoxListAlternativeMaterialTypes.Items.Clear();
            IEnumerable<RentalType> query = ControlObjectContext.RentalTypeSet.Where<RentalType>(m => m.IsActive).OrderBy(m => m.Description);

            IEnumerator<RentalType> rtEnum = query.GetEnumerator();
            bool Show = true;
            while (rtEnum.MoveNext())
            {
                if (rt != null)
                {
                    Show = rt.Id != rtEnum.Current.Id;
                }
                if (Show)
                {
                    CheckBoxListAlternativeMaterialTypes.Items.Add(new ListItem(rtEnum.Current.Description, rtEnum.Current.Id.ToString()));
                }
            }
        }

        protected void SwitchOnCheckBoxesInAlternativeMaterialTypes()
        {
            if (DataItem != null)
            {
                RentalType rt = (DataItem as RentalType);
                foreach (RentalType CheckRt in rt.AlternativeRentalTypes)
                {
                    ListItem li = CheckBoxListAlternativeMaterialTypes.Items.FindByValue(CheckRt.Id.ToString());
                    if (li != null) { li.Selected = true; }
                }
            }
        }

        protected void SaveCheckBoxesInAlternativeMaterialTypes()
        {
            RentalType rt = (DataItem as RentalType);

            // delete all elements
            foreach (RentalType CheckRt in rt.AlternativeRentalTypes.ToArray<RentalType>())
            {
                rt.AlternativeRentalTypes.Remove(CheckRt);
            }

            // add new elements
            foreach (ListItem li in CheckBoxListAlternativeMaterialTypes.Items)
            {
                if (li.Selected)
                {
                    RentalType rtNew = ControlObjectContext.GetObjectByKey(new System.Data.EntityKey("ModelTMSContainer.RentalTypeSet","Id",new Guid(li.Value) )) as RentalType;
                    rt.AlternativeRentalTypes.Add(rtNew);
                }
            }
        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            StandardButtonCancelClickHandler(sender, e);
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            SaveCheckBoxesInAlternativeMaterialTypes();
            StandardButtonSaveClickHandler(sender, e);
        }

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            StandardButtonDeleteClickHandler(sender, e);
        }
    }
}