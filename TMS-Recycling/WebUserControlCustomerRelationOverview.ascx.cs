using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Objects;

namespace TMS_Recycling
{
    public partial class WebUserControlCustomerRelationOverview : ClassTMSUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        void Page_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Params["Id"] != null)
                {
                    WebUserControlCustomerRelation1.DataBind();
                    WebUserControlCustomerRelation1.KeyID = new System.Guid(Request.Params["Id"]);
                    WebUserControlCustomerRelation1.Visible = true;
                }
            } 
            
            if (WebUserControlCustomerRelation1.RefreshRequired)
            {
                WebUserControlCustomerRelation1.RefreshRequired = false;
                WebUserControlCustomerRelation1.Visible = false;
                ButtonSearch_Click(sender, e);
            }
        }

        protected void GridViewResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            WebUserControlCustomerRelation1.DataBind();
            WebUserControlCustomerRelation1.KeyID = new System.Guid(GridViewResults.SelectedDataKey.Value.ToString());
            WebUserControlCustomerRelation1.Visible = true;
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            EntityDataSourceRelation.DefaultContainerName = EntityDataSourceRelation.DefaultContainerName;
            EntityDataSourceRelation.DataBind();
        }

        protected void ButtonNew_Click(object sender, EventArgs e)
        {
            Relation NewObj = new Relation();
            ModelTMSContainer Temp = new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session);

            Temp.AddToRelationSet(NewObj);
            NewObj.CustomerType = "Both";
            NewObj.PreferredCurrency = "Eur";
            Temp.SaveChanges(SaveOptions.DetectChangesBeforeSave);

            WebUserControlCustomerRelation1.KeyID = NewObj.Id;
            WebUserControlCustomerRelation1.Visible = true;

        }

        public bool DetailVisible()
        {
            return WebUserControlCustomerRelation1.Visible;
        }

        public Guid DetailKeyId()
        {
            return WebUserControlCustomerRelation1.KeyID;
        }
    }
}