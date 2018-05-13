using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Text;
using System.Configuration;
using System.Net;

namespace TMS_Recycling
{
    public class TMSMail : MailMessage
    {
        public TMSMail()
        {
            this.BodyEncoding = Encoding.UTF8;
            this.IsBodyHtml = false;
        }

        public string SMTPServer
        {
            get
            {
                string Server = ConfigurationManager.AppSettings.Get("SMTPServer");
                if (Server == "") { return "smtp.google.com"; }
                return Server;
            }
        }

        public int SMTPPort
        {
            get
            {
                string Setting = ConfigurationManager.AppSettings.Get("SMTPPort");
                if (Setting == "") { return 0; }
                return Convert.ToInt32(Setting);
            }
        }

        public bool SMTPUseSSL
        {
            get
            {
                string Setting = ConfigurationManager.AppSettings.Get("SMTPUseSSL");
                if (Setting == "") { return false; }
                Setting = Setting.Substring(0, 1).ToUpper() ;
                return ((Setting == "T") || (Setting == "Y"));
            }
        }

        public string SMTPUserName
        {
            get
            {
                string Setting = ConfigurationManager.AppSettings.Get("SMTPUserName");
                return Setting;
            }
        }

        public string SMTPPassword
        {
            get
            {
                string Setting = ConfigurationManager.AppSettings.Get("SMTPPassword");
                return Setting;
            }
        }

        public string SMTPFrom
        {
            get
            {
                string Setting = ConfigurationManager.AppSettings.Get("SMTPFrom");
                return Setting;
            }
        }

        public string SMTPFromName
        {
            get
            {
                string Setting = ConfigurationManager.AppSettings.Get("SMTPFromName");
                return Setting;
            }
        }

        public void Send()
        {
            SmtpClient smtp;

            if (From == null) { From = new MailAddress(SMTPFrom, SMTPFromName); }

            if (SMTPPort != 0)
            {
                smtp = new SmtpClient(SMTPServer, SMTPPort);
            }
            else
            {
                smtp = new SmtpClient(SMTPServer);
            }
            
            smtp.EnableSsl = SMTPUseSSL;
            if (SMTPUserName != "")
            {
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(SMTPUserName, SMTPPassword);
            }

            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Timeout = 5;

            smtp.Send(this);
        }

    }
}