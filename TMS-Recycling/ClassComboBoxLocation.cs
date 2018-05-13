using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMS_Recycling
{
    public class ClassComboBoxLocation : ClassComboBox
    {
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            Common.LimitLocationList(Items, Page.Session, new ModelTMSContainer(Page.Session["CustomerConnectString"].ToString(), Page.Session));
        }
    }
}