using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Common;

namespace TMS_Recycling
{
    public partial class WebUserControlShowLinks : ClassTMSUserControl
    {
        public string UserControlForEditing = "",
                      BaseObject = "",
                      ShowFields = "",
                      SelectionField = "",
                      SelectionID="",
                      OrderField="",
                      LinkField="",
                      DisplayFields="";

        protected void Page_Load(object sender, EventArgs e)
        {
//            if (!IsPostBack)
            {
                // extract URL parameters
                if (Request.Params["UCE"] != null)
                {
                    UserControlForEditing = Request.Params["UCE"];
                }
                if (Request.Params["BO"] != null)
                {
                    BaseObject = Request.Params["BO"];
                }
                if (Request.Params["SF"] != null)
                {
                    ShowFields = Request.Params["SF"];
                }
                if (Request.Params["DF"] != null)
                {
                    DisplayFields = Request.Params["DF"];
                }
                if (Request.Params["SEL"] != null)
                {
                    SelectionField = Request.Params["SEL"];
                }
                if (Request.Params["ID"] != null)
                {
                    SelectionID = Request.Params["ID"];
                }
                if (Request.Params["ORD"] != null)
                {
                    OrderField = Request.Params["ORD"];
                }
                if (Request.Params["LNK"] != null)
                {
                    LinkField = Request.Params["LNK"];
                }

                // configure the entity data source
                // use the commandtext to specify the query
                string Query = "select " + ShowFields + " from " + BaseObject + " where " + SelectionField + "=@Par order by " + OrderField;
                EntityDataSourceLinks.CommandText = Query;
                EntityDataSourceLinks.CommandParameters.Clear();
                EntityDataSourceLinks.CommandParameters.Add("Par", System.Data.DbType.Guid, SelectionID);

                // now generate the columns
                GridView1.AutoGenerateColumns = false;
                string[] FieldList = ShowFields.Split(',');
                string[] FieldNames = DisplayFields.Split(',');
                for (int i = 0; i < FieldList.Count(); i++)
                {
                    BoundField bc = new BoundField();

                    bc.DataField = FieldList[i];
                    bc.HeaderText = FieldList[i];
                    if (FieldNames.Count() > i)
                    {
                        bc.HeaderText = FieldNames[i];
                    }
                    bc.Visible = bc.HeaderText.ToUpper() != "X";

                    // strip of leading this like it.
                    if (bc.DataField.IndexOf('.') > 0) { bc.DataField = bc.DataField.Split('.')[1]; }
                    if (bc.HeaderText.IndexOf('.') > 0) { bc.HeaderText = bc.HeaderText.Split('.')[1]; }

                    GridView1.Columns.Add(bc);
                }
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((LinkField != "") && (UserControlForEditing != "") && (e.Row.DataItem != null))
            {
                TableCell tc = e.Row.Cells[0];

                DbDataRecord ddr = e.Row.DataItem as DbDataRecord;

                URLPopUpControl upc = LoadControl("URLPopUpControl.ascx") as URLPopUpControl;

                upc.URLToPopup = "WebFormPopup.aspx?UC="+UserControlForEditing+"&Id=" + ddr[LinkField].ToString();
                upc.Text = "Open";
                tc.Controls.Add(upc);
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            EntityDataSourceLinks.DataBind();
        }

        protected void EntityDataSourceLinks_ContextCreated(object sender, EntityDataSourceContextCreatedEventArgs e)
        {
            // force loading of all metadata
            //e.Context.MetadataWorkspace.LoadFromAssembly(System.Reflection.Assembly.GetAssembly(e.Context.GetType()));
        }

    }
}