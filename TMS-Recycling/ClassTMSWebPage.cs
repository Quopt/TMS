using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace TMS_Recycling
{
    public class ClassTMSWebPage : System.Web.UI.Page
    {
        private ModelTMSContainer _ControlObjectContextX = null;
        public ModelTMSContainer _ControlObjectContext
        {
            get
            {
                if (_ControlObjectContextX == null)
                {
                    _ControlObjectContextX = new ModelTMSContainer(this.Session["CustomerConnectString"].ToString(), this.Session);
                }
                return _ControlObjectContextX;
            }
        }

        public ClassTMSWebPage()
        {
            this.Load += new EventHandler(this.Page_Load);
            this.PreRenderComplete += new EventHandler(this.Page_PreRenderBase);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Common.DisableViewStateOnButtons(Page.Controls); //disable the view state on all buttons otherwise they may be hidden by the installation of user rights on the form

                if (Request.Params["Message"] != null)
                {
                    Common.InformUser(Page, Request.Params["Message"]);
                }
            }

            if (Session != null)
            {
                if (Application[Session["CurrentUserID"].ToString()].ToString().Trim() != Session.SessionID.Trim())
                {
                    Session["LogoutMessage"] = "U bent op een ander werkstation ingelogd. Daarom bent u hier uitgelogd.";
                    Response.Redirect("login.aspx?cust=" + Session["LoginCustParameter"].ToString().Trim(), false);
                    FormsAuthentication.SignOut();
                }
            }

        }

        private void InstallUserRights()
        {
            ClassSecurity SecCheck = new ClassSecurity();

            // check if access to this page is allowed
            if ((Request.Url.LocalPath.ToLower().IndexOf("landing.aspx") < 0) && // landing pages are NOT secured !!! 
                 (Request.Url.LocalPath.ToLower().IndexOf("default.aspx") < 0) && // nor is the default page
                 (Request.Url.LocalPath.ToLower().IndexOf("webformpopup.aspx") < 0) ) // nor is the popup
            {
                if ((!SecCheck.AccessToFormAllowed(Page, _ControlObjectContext, (Session[Page.Request.Url.LocalPath]).ToString() )) && (Request.Url.AbsolutePath.ToLower().IndexOf("default.aspx") < 0))
                {
                    Response.Redirect("Default.aspx?Message=" + "U heeft geen toegang tot het gekozen formulier omdat u geen rechten heeft om dit formulier te mogen bekijken.", true);
                }
            }

            // check if there are menus & buttons on this page to which access is allowed
            SecCheck.InstallUserRightsIntoPage(Page, _ControlObjectContext);
        }

        protected void Page_PreRenderBase(object sender, EventArgs e)
        {
            InstallUserRights();
        }

    }
}