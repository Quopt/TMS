using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Objects;
using System.Data;

namespace TMS_Recycling
{
    public partial class WebFormCustomerRelationAdvancePayment : ClassTMSWebPage
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
            if (WebUserControlCustomerRelationAdvancePayment1.RefreshRequired)
            {
                WebUserControlCustomerRelationAdvancePayment1.RefreshRequired = false;
                WebUserControlCustomerRelationAdvancePayment1.Visible = false;
                ButtonSearch_Click(sender, e);
            }


        }

        protected void GridViewResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            WebUserControlCustomerRelationAdvancePayment1.DataBind();
            WebUserControlCustomerRelationAdvancePayment1.KeyID = new System.Guid(GridViewResults.SelectedDataKey.Value.ToString());
            WebUserControlCustomerRelationAdvancePayment1.Visible = true;
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            EntityDataSourceRelation.DefaultContainerName = EntityDataSourceRelation.DefaultContainerName;
            EntityDataSourceRelation.DataBind();
        }

        protected void ButtonNew_Click(object sender, EventArgs e)
        {
            RelationAdvancePayment NewObj = new RelationAdvancePayment();
            ModelTMSContainer Temp = new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session);
            EntityKey TempKey = new EntityKey("ModelTMSContainer.RelationSet", "Id", Guid.Parse(Request.Params["Id"]));
            Relation TempObj = Temp.GetObjectByKey(TempKey) as Relation;

            NewObj.Relation = TempObj;
            NewObj.PaymentDateTime = Common.CurrentClientDate(Session); 
            NewObj.Ledger = Temp.LedgerSet.First();
            NewObj.LedgerBookingCode = Temp.LedgerBookingCodeSet.First();

            Temp.AddToRelationAdvancePaymentSet(NewObj);
            Temp.SaveChanges(SaveOptions.DetectChangesBeforeSave);

            WebUserControlCustomerRelationAdvancePayment1.KeyID = NewObj.Id;
            WebUserControlCustomerRelationAdvancePayment1.Visible = true;

        }
    }
}