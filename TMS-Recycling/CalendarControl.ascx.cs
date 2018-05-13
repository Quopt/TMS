using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace TMS_Recycling
{
    public partial class CalendarControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TextBoxDate_CalendarExtender.Format = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
            }
        }

        public DateTime SelectedDate
        {
            set
            {
                TextBoxDate.Text = value.ToShortDateString();
            }
            get
            {
                DateTime temp;
                try
                {
                    temp = DateTime.Parse(TextBoxDate.Text);
                }
                catch (Exception)
                {
                    temp = Common.CurrentClientDate(Session);
                }
                TextBoxDate.Text = temp.ToShortDateString();
                return temp;
            }
        }

        public string Text
        {
            get
            {
                return SelectedDate.ToString();
            }
            set
            {
                SelectedDate = Convert.ToDateTime(value);
            }
        }

        public Boolean Enabled
        {
            set
            {
                ButtonSetDate.Enabled = value;
                TextBoxDate.Enabled = value;
            }
            get
            {
                return ButtonSetDate.Enabled;
            }
        }

        protected void CalendarDate_SelectionChanged(object sender, EventArgs e)
        {
            TextBoxDate.Text = CalendarDate.SelectedDate.ToShortDateString();
            divCalendar.Style.Add("display", "none");
        }

        protected void ButtonSetDate_Click(object sender, EventArgs e)
        {
            CalendarDate.SelectedDate = DateTime.Parse(TextBoxDate.Text);
            CalendarDate.VisibleDate = DateTime.Parse(TextBoxDate.Text);
            CalendarDate.TodaysDate = Common.CurrentClientDate(Session);

            if (divCalendar.Style["display"] == "")
            {
                divCalendar.Style.Add("display", "none");
            }
            else
            {
                divCalendar.Style.Add("display", "");
            }
        }
    }
}