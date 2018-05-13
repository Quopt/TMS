using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Objects;

namespace TMS_Recycling
{
    public partial class WebFormCustomerRelationWork : ClassTMSWebPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // get the relation description
                ModelTMSContainer _ControlObjectContext = new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session);
                EntityKey TempKey = new EntityKey("ModelTMSContainer.RelationSet", "Id", Guid.Parse(Request.Params["Id"]));
                Relation TempObj = _ControlObjectContext.GetObjectByKey(TempKey) as Relation;
                LabelObjectName.Text = TempObj.Description;
            }
        }

        void Page_PreRender(object sender, EventArgs e)
        {
            if (WebUserControlCustomerRelationWork1.RefreshRequired)
            {
                WebUserControlCustomerRelationWork1.RefreshRequired = false;
                WebUserControlCustomerRelationWork1.Visible = false;
                ButtonSearch_Click(sender, e);
            }


        }

        protected void GridViewResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            WebUserControlCustomerRelationWork1.DataBind();
            WebUserControlCustomerRelationWork1.KeyID = new System.Guid(GridViewResults.SelectedDataKey.Value.ToString());
            WebUserControlCustomerRelationWork1.Visible = true;
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            EntityDataSourceRelation.DefaultContainerName = EntityDataSourceRelation.DefaultContainerName;
            EntityDataSourceRelation.DataBind();
        }

        protected void ButtonNew_Click(object sender, EventArgs e)
        {
            RelationWork NewObj = new RelationWork();
            ModelTMSContainer Temp = new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session);
            EntityKey TempKey = new EntityKey("ModelTMSContainer.RelationSet", "Id", Guid.Parse(Request.Params["Id"]));
            Relation TempObj = Temp.GetObjectByKey(TempKey) as Relation;

            NewObj.Relation = TempObj;
            NewObj.LedgerBookingCode = Temp.LedgerBookingCodeSet.First();
            NewObj.AgreementDateTime = Common.CurrentClientDate(Session); 

            Temp.AddToRelationWorkSet(NewObj);
            Temp.SaveChanges(SaveOptions.DetectChangesBeforeSave);

            WebUserControlCustomerRelationWork1.KeyID = NewObj.Id;
            WebUserControlCustomerRelationWork1.Visible = true;

        }
    }
}