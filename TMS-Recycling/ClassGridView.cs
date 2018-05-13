using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Text;

namespace TMS_Recycling
{
    public class ClassGridView : GridView
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.ShowHeaderWhenEmpty = true;
            this.EmptyDataText = "Er zijn geen gegevens gevonden die voldoen aan uw zoekcriteria.";

            Load += new EventHandler(GenericOnLoad);
            PreRender += new EventHandler(GenericPreRender);
        }

        protected void GenericOnLoad(object sender, EventArgs e)
        {
            try
            {
                if ((this != null) && (this.Rows != null))
                {
                    if (Rows.Count > 0)
                    {
                        ImageButton ib = new ImageButton();
                        ib.ImageUrl = "Images/Paperclip.png";
                        ib.Click += new ImageClickEventHandler(DownloadClick);
                        ib.ToolTip = "Opslaan huidige selectie";
                        HeaderRow.Cells[0].Controls.Add(ib);
                    }
                }
            }
            catch
            {
            }
        }

        protected void GenericPreRender(object sender, EventArgs e)
        {
            try
            {
                if (HeaderRow.Cells[0].Controls.Count == 0)
                {
                    GenericOnLoad(null, null);
                }
                DataBind();
            }
            catch
            {
            }
        }

        private void WriteToOutputStream(string s)
        {
            UTF8Encoding x = new UTF8Encoding();
            s=s.Replace("€", "&euro;");
            s=s.Replace("√", "&radic;");
            Page.Response.BinaryWrite(x.GetBytes(s));
        }

        protected void DownloadClick(object sender, ImageClickEventArgs e)
        {
            GridView TempGv = new GridView();

            foreach (DataControlField dcf in Columns)
            {
                TempGv.Columns.Add(dcf);
            }
            TempGv.DataSourceID = DataSourceID;

            TempGv.AutoGenerateColumns = false;
            TempGv.ID = "temp" + Guid.NewGuid().ToString();
            Controls.Add(TempGv);
            TempGv.DataBind();

            // open a data channel to the client
            HttpResponse Res = Page.Response;
            Res.ContentType = "application/octet-stream";
            Res.AddHeader("Content-Disposition", "attachment; filename=\"" + ID + " "+ Common.CurrentClientDateTime(Page.Session).ToString() + ".htm\"");
            Res.HeaderEncoding = Encoding.UTF8;

            WriteToOutputStream("<TABLE><TR>");
            foreach (TableCell tc in TempGv.HeaderRow.Cells)
            {
                WriteToOutputStream("<TD>");
                WriteToOutputStream(tc.Text.Trim());
                WriteToOutputStream("</TD>");
            }
            WriteToOutputStream("</TR>");


            foreach (GridViewRow gvr in TempGv.Rows)
            {
                WriteToOutputStream("<TR>");

                foreach (TableCell tc in gvr.Cells)
                {
                    string ToWrite = tc.Text.Trim();

                    if (ToWrite == "")
                    {
                        foreach (Control c in tc.Controls)
                        {
                            if (c.GetType().ToString().IndexOf("CheckBox")>=0)
                            {
                                ToWrite = (c as CheckBox).Checked ? "√" : "X";
                            }
                        }
                    }

                    WriteToOutputStream("<TD>" + ToWrite + "</TD>");
                }
                WriteToOutputStream("</TR>");
            }

            Res.Flush();
            Res.End();
        }
        
    }
}