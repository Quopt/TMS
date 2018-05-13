using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace TMS_Recycling
{
    public partial class CalendarWithTimeControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetDateTimePattern();
            }
        }

        private void SetDateTimePattern()
        {
            TextBoxDate_CalendarExtender.Format = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern + " " + CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern;
        }

        public DateTime SelectedDateTime
        {
            set
            {
                //TextBoxDate.Text = value.ToString("MM/dd/yyyy HH:mm:ss");
                SetDateTimePattern();
                TextBoxDate.Text = value.ToString();
            }
            get
            {
                DateTime temp;
                try
                {
                    //temp = DateTime.ParseExact(TextBoxDate.Text, "MM/dd/yyyy HH:mm:ss", CultureInfo.CurrentCulture);
                    temp = DateTime.Parse(TextBoxDate.Text);
                }
                catch (Exception)
                {
                    temp = Common.CurrentClientDateTime(Session);
                }
                TextBoxDate.Text = temp.ToString();
                return temp;
            }
        }

        public string Text
        {
            get
            {
                return SelectedDateTime.ToString();
            }
            set
            {
                SelectedDateTime = Convert.ToDateTime(value);
            }
        }

        public Boolean Enabled
        {
            set
            {
                ButtonDateSelect.Enabled = value;
                TextBoxDate.Enabled = value;
            }
            get
            {
                return ButtonDateSelect.Enabled;
            }
        }

        protected void CalendarDate_SelectionChanged(object sender, EventArgs e)
        {
            DateTime Temp ;
            Temp = new DateTime( CalendarDate.SelectedDate.Year, CalendarDate.SelectedDate.Month, CalendarDate.SelectedDate.Day,
                SelectedDateTime.Hour, SelectedDateTime.Minute, SelectedDateTime.Second) ;
            SelectedDateTime = Temp;
            divCalendar.Style.Add("display", "none");
        }

        protected void ButtonSetDate_Click(object sender, EventArgs e)
        {
            CalendarDate.SelectedDate = SelectedDateTime;
            CalendarDate.VisibleDate = SelectedDateTime;
            CalendarDate.TodaysDate = Common.CurrentClientDateTime(Session).Date;

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