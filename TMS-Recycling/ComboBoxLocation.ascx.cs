using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TMS_Recycling
{
    public partial class ComboBoxLocation : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public bool AutoPostback
        {
            get
            {
                return ClassComboBoxLocation1.AutoPostBack;
            }
            set
            {
                ClassComboBoxLocation1.AutoPostBack = value;
            }
        }

        public string SelectedValue
        {
            get
            {
                if (LabelDataBound.Text == "")
                {
                    ClassComboBoxLocation1.DataBind();
                    Common.LimitLocationList(ClassComboBoxLocation1.Items, Page.Session, new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session));
                    LabelDataBound.Text = ".";
                }

                if (ClassComboBoxLocation1.SelectedIndex < 0)
                {
                    if (ClassComboBoxLocation1.Items.Count > 0)
                    {
                        ClassComboBoxLocation1.SelectedIndex = 0;
                    }
                }

                if (ClassComboBoxLocation1.SelectedIndex < 0)
                {
                    return "";
                }
                else
                {
                    return ClassComboBoxLocation1.SelectedValue;
                }
            }
            set
            {
                if (ClassComboBoxLocation1.Items.FindByValue(value) != null)
                {
                    ClassComboBoxLocation1.SelectedValue = value;
                }
            }
        }

        public string Text
        {
            get
            {
                String s = SelectedValue; // make sure an item is selected by default
                if (ClassComboBoxLocation1.Items.Count == 0)
                {
                    return "";
                }
                else
                {
                    if (ClassComboBoxLocation1.SelectedIndex < 0)
                    {
                        ClassComboBoxLocation1.SelectedIndex = 0;
                    }

                    if (ClassComboBoxLocation1.SelectedValue == "00000000-0000-0000-0000-000000000000")
                    {
                        return "";
                    }
                    else
                    {
                        return ClassComboBoxLocation1.SelectedItem.Text;
                    }
                }
            }
            set
            {
                ListItem li = ClassComboBoxLocation1.Items.FindByText(value);
                if (li != null)
                {
                    ClassComboBoxLocation1.SelectedValue = li.Value;
                }
            }
        }

    }
}