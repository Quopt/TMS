using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TMS_Recycling
{
    public partial class WebFormSettingChangePassword : ClassTMSWebPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonChangePassword_Click(object sender, EventArgs e)
        {
            ModelTMSContainer Temp = new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session);
            StaffMember sm = Common.CurrentLoggedInUser(Page.Session, Temp);

            if (sm != null)
            {
                if (sm.Password.ToLower() == TextBoxOldPassword.Text.ToLower())
                {
                    if (TextBoxNewPassword.Text == TextBoxNewPassword2.Text)
                    {
                        sm.Password = TextBoxNewPassword.Text;
                        Temp.SaveChanges();
                        Common.InformUser(Page, "Het wachtwoord is gewijzigd.");
                    }
                    else
                    {
                        Common.InformUser(Page, "Het controle wachtwoord is niet gelijk aan het nieuwe wachtwoord. Deze moeten gelijk zijn. Voer ze opnieuw in. Het wachtwoord is NIET gewijzigd.");
                        TextBoxNewPassword2.Text = "";
                        TextBoxNewPassword.Text = "";
                    }
                }
                else
                {
                    Common.InformUser(Page, "Het oude wachtwoord is niet correct! Het wachtwoord is NIET gewijzigd.");
                }
            }
        }
    }
}