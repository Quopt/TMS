using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace TMS_Recycling
{
    public class ClassComboBox : ComboBox
    {
        // create and hook up the prerender event
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PreRender += new EventHandler(GenericPreRender);
        }

        // remove duplicates in the prerender event
        protected void GenericPreRender(object sender, EventArgs e)
        {
            if (Items != null)
            {
                if (Items.Count > 0)
                {
                    List<string> ValueList = new List<string>();
                    ListItem[] myListItemArray = new ListItem[Items.Count];
                    Items.CopyTo(myListItemArray, 0);

                    foreach (ListItem li in myListItemArray)
                    {
                        if (ValueList.IndexOf(li.Text) >= 0)
                        {
                            Items.Remove(li);
                        }
                        else
                        {
                            ValueList.Add(li.Text);
                        }
                    }
                }
            }

        }

    }
}