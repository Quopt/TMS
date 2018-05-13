using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TMS_Recycling
{

    public partial class URLPopUpControl : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (Visible) { Visible = MaxPopupDepth >= CurrentPopupDepth; }
        }

        public int MaxPopupDepth
        {
            get
            {
                return Convert.ToInt32(LabelMaxPopupDepth.Text);
            }
            set
            {
                LabelMaxPopupDepth.Text = value.ToString();
            }
        }

        public int CurrentPopupDepth
        {
            get
            {
                if (Request.Params["PopupDepth"] != null)
                {
                    return Convert.ToInt32(Request.Params["PopupDepth"]) + 1;
                }
                else
                {
                    return 1;
                }
            }
        }

        public string URLToPopup
        {
            get
            {
                return LabelURLToPopUp.Text;
            }
            set
            {
                LabelURLToPopUp.Text = value;
            }
        }

        public string Text
        {
            get
            {
                return ButtonOpenPopUp.Text;
            }
            set
            {
                ButtonOpenPopUp.Text = value;
                TextBoxPopUpName.Text = value;
            }
        }

        public bool Visible
        {
            get
            {
                return ButtonOpenPopUp.Visible;
            }
            set
            {
                ButtonOpenPopUp.Visible = value;
            }
        }

        // this event is called before the popup is opened. This gives the chance to the programmer to set the URL to open. 
        public event EventHandler BeforePopUpOpened;
        public event EventHandler PopUpClosed;

        protected void OnClicked(EventArgs e)
        {
            if (BeforePopUpOpened != null)
            {
                BeforePopUpOpened(this, e);
            }
        }

        protected void OnClosed(EventArgs e)
        {
            if (PopUpClosed != null)
            {
                PopUpClosed(this, e);
            }
        }

        protected void ButtonOpenPopUp_Click(object sender, EventArgs e)
        {
            OnClicked(e);

            if (URLToPopup.IndexOf("?") > 0)
            {
                IFramePopUp.Attributes["src"] = URLToPopup + "&PopupDepth=" + CurrentPopupDepth.ToString();
            }
            else
            {
                IFramePopUp.Attributes["src"] = URLToPopup + "?PopupDepth=" + CurrentPopupDepth.ToString(); 
            }
            divPopup.Style.Add("display", "");
            divPopupContent.Style.Add("display", "");
        }

        protected void ButtonClosePopUp_Click(object sender, EventArgs e)
        {
            divPopup.Style.Add("display", "none");
            divPopupContent.Style.Add("display", "none");

            OnClosed(e);
        }


    }
}