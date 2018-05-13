using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace TMS_Recycling
{
    public class ClassEntityDataSource : EntityDataSource
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (!this.DesignMode)
            {
                ConnectionString = Page.Session["CustomerConnectString"].ToString();
            }

            Load += new EventHandler(GenericOnLoad);
        }

        protected void GenericOnLoad(object sender, EventArgs e)
        {
            if ((!this.DesignMode) && (ConnectionString != Page.Session["CustomerConnectString"].ToString()))
            {
                ConnectionString = Page.Session["CustomerConnectString"].ToString();
            }
        }
    }
}