using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;

namespace TMS_Recycling
{
    public partial class WebFormRentLanding : ClassTMSWebPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //LabelDTFormat.Text = Thread.CurrentThread.CurrentUICulture.GetConsoleFallbackUICulture().DateTimeFormat.FullDateTimePattern + " " +Common.ReturnEntitySQLDateTimeString(DateTime.Now);
        }
    }
}