using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.Data.SqlClient;

namespace TMS_Recycling
{
    public partial class WebUserControlShowReport : ClassTMSUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!IsPostBack) && (Request.Params["d"]!=null))
            {
                // set the connection to execute the dataset for the report builder
                SqlConnection connection = new SqlConnection(Session["CustomerConnectSQLString"].ToString());

                Type adapterType = Type.GetType("TMS_Recycling." + Request.Params["d"].ToString());
                DataSet newSet = Activator.CreateInstance(adapterType) as DataSet;

                // set the reportviewers report path. This report will be shown
                ReportViewerShow.LocalReport.ReportPath = "Reports\\" + Request.Params["r"].ToString() + ".rdlc";

                // link up the reports dataset and datasources and fill them with data
                ReportViewerShow.LocalReport.DataSources.Clear();
                ReportDataSourceCollection RepDSColl = ReportViewerShow.LocalReport.DataSources;
                ClassDataSetHelper.Load(newSet, RepDSColl, connection, Request.Params);

                // get the URL parameters & set them in the report
                List<ReportParameter> paramList = new List<ReportParameter>();
                ReportParameterInfoCollection rpic = ReportViewerShow.LocalReport.GetParameters();
                for (int i=0; i < Request.Params.Count; i++)
                {
                    if (rpic[Request.Params.GetKey(i)] != null)
                    {
                        if ((Request.Params.GetKey(i).IndexOf("Id") == Request.Params.GetKey(i).Length - 2) && (Request.Params.GetValues(i)[0] == "")) // if this parametername ends with Id and has an empty value then make it an empty guid
                        {
                            paramList.Add(new ReportParameter(Request.Params.GetKey(i), Guid.Empty.ToString() ));
                        }
                        else
                        {
                            paramList.Add(new ReportParameter(Request.Params.GetKey(i), Request.Params.GetValues(i)));
                        }
                    }
                }
                ReportViewerShow.LocalReport.SetParameters(paramList);

                // refresh the report
                ReportViewerShow.LocalReport.Refresh();
            }
        }

        private string _DataSetName = "";
        public string DataSetName
        {
            get
            {
                return _DataSetName;
            }
            set
            {
                _DataSetName = value;
            }
        }

        private string _Report = "";
        public string Report
        {
            get
            {
                return _Report;
            }
            set
            {
                _Report = value;
            }
        }

        private NameValueCollection _Params = new NameValueCollection();
        public NameValueCollection Params
        {
            get
            {
                return _Params;
            }
            set
            {
                _Params = value;
            }
        }

        void GenerateReport()
        {
            GenerateReport(DataSetName, Report, Params);
        }

        void GenerateReport(string ReportDataSet, string ReportName, NameValueCollection ReportParameters)
        {
            SqlConnection connection = new SqlConnection(Session["CustomerConnectSQLString"].ToString());

            Type adapterType = Type.GetType("TMS_Recycling." + ReportDataSet);
            DataSet newSet = Activator.CreateInstance(adapterType) as DataSet;


            ReportViewerShow.LocalReport.ReportPath = "Reports\\" + ReportName + ".rdlc";

            ReportViewerShow.LocalReport.DataSources.Clear();
            ReportDataSourceCollection RepDSColl = ReportViewerShow.LocalReport.DataSources;
            ClassDataSetHelper.Load(newSet, RepDSColl, connection, ReportParameters);
            ReportViewerShow.LocalReport.Refresh();
        }

    }
}