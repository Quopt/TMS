using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;

namespace TMS_Recycling
{
    public partial class WebFormError : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string err = "Onbekende fout";
                try
                {
                    Exception objErr = Server.GetLastError().GetBaseException();
                    err = "Error Caught in TMS event\n" +
                            "Error in: " + Request.Url.ToString() +
                            "\nError Message:" + objErr.Message.ToString() +
                            "\nStack Trace:" + objErr.StackTrace.ToString() +
                            "\n Assembly : " + this.GetType().Assembly.GetName() +
                            "\n Version : " + this.GetType().Assembly.GetName().Version.ToString();
                }
                catch { }
                TextBoxError.Text = err;
                try
                {
                    TMSMail tmsm = new TMSMail();
                    tmsm.Subject = "Error in TMS system";
                    tmsm.Body = err ;
                    // add the user id
                    try
                    {
                        if (Session["CurrentUserID"] != null)
                        {
                            tmsm.Body = tmsm.Body + "\n " + Session["CurrentUserID"].ToString()
                                + "\n" + Session["CurrentUserName"].ToString();
                        }
                    }
                    catch { }
                    // add the connect string
                    try
                    {
                        tmsm.Body = tmsm.Body + "\n" +
                          Session["CustomerConnectSQLString"];
                    }
                    catch { }
                    // and e-mail
                    tmsm.To.Add( "support@quopt.com");
#if !DEBUG                                        
                    tmsm.Send();
#endif
                }
                catch
                {
                }
                EventLog.WriteEntry("TMS", err, EventLogEntryType.Error);
                Server.ClearError();
            }
        }
    }
}