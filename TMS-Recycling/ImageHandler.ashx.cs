using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace TMS_Recycling
{
    /// <summary>
    /// Summary description for ImageHandler
    /// </summary>
    public class ImageHandler : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            System.Data.SqlClient.SqlDataReader rdr = null;
            System.Data.SqlClient.SqlConnection conn = null;
            System.Data.SqlClient.SqlCommand selcmd = null;
            try
            {
                String Field, Table, Id;
                bool Continue = true;

                Field =  context.Request.QueryString["Field"].ToUpper();
                Table =  context.Request.QueryString["Table"].ToUpper();
                Id =  context.Request.QueryString["Id"];

                // prevent SQL injection attacks
                if (Table != "LOCATIONSET") { Continue = false; }
                if ( (Field != "COMPANYLOGOIMAGE") && (Field != "COMPANYMEMBERSHIPSLOGO") ) { Continue = false; }

                if (Continue )
                {
                    conn = new System.Data.SqlClient.SqlConnection(context.Session["CustomerConnectSQLString"].ToString());
                    selcmd = new System.Data.SqlClient.SqlCommand
                        ("select "+ Field + " from "+Table+" where Id='" + Id + "' ", conn);
                    conn.Open();
                    rdr = selcmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        if (rdr[Field] != DBNull.Value)
                        {
                            context.Response.ContentType = "image/png";
                            context.Response.BinaryWrite((byte[])rdr[Field]);
                        }
                    }
                    if (rdr != null)
                        rdr.Close();
                }
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}