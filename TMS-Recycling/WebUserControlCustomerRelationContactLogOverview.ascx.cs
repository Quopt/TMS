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
    public partial class WebUserControlCustomerRelationContactLogOverview : ClassTMSUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // get the relation description
                ModelTMSContainer _ControlObjectContext = new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session);

                if (Request.Params["CustomerRelationId"] != null)
                {
                    EntityKey TempKey = new EntityKey("ModelTMSContainer.RelationContactSet", "Id", Guid.Parse(Request.Params["CustomerRelationId"]));
                    RelationContact TempObj = _ControlObjectContext.GetObjectByKey(TempKey) as RelationContact;
                    RelationContactId = TempObj.Id;
                }
            }
        }

        void Page_PreRender(object sender, EventArgs e)
        {
            if (WebUserControlCustomerRelationContactLog1.RefreshRequired)
            {
                WebUserControlCustomerRelationContactLog1.RefreshRequired = false;
                WebUserControlCustomerRelationContactLog1.Visible = false;
                ButtonRefresh_Click(sender, e);
            }
        }

        protected void GridViewContractMaterials_SelectedIndexChanged(object sender, EventArgs e)
        {
            WebUserControlCustomerRelationContactLog1.DataBind();
            WebUserControlCustomerRelationContactLog1.KeyID = new System.Guid(GridViewContractMaterials.SelectedDataKey.Value.ToString());
            WebUserControlCustomerRelationContactLog1.Visible = true;
        }

        protected void ButtonNewContractMaterial_Click(object sender, EventArgs e)
        {
            RelationContactLog NewObj = new RelationContactLog();
            ModelTMSContainer Temp = new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session);
            EntityKey TempKey = new EntityKey("ModelTMSContainer.RelationContactSet", "Id", RelationContactId);
            RelationContact TempObj = Temp.GetObjectByKey(TempKey) as RelationContact;

            NewObj.RelationContact = TempObj;
            NewObj.ContactType = "Other";
            NewObj.FollowUpDateTime = Common.CurrentClientDateTime(Session);
            NewObj.PausedUntilDateTime = Common.CurrentClientDateTime(Session);
            NewObj.ContactDateTime = Common.CurrentClientDateTime(Session);
            NewObj.FollowUpState = "Handled";
            NewObj.Description = "Nieuw contactmoment dd " + Common.CurrentClientDateTime(Session).ToString();

            Temp.AddToRelationContactLogSet(NewObj);
            Temp.SaveChanges(SaveOptions.DetectChangesBeforeSave);

            WebUserControlCustomerRelationContactLog1.KeyID = NewObj.Id;
            WebUserControlCustomerRelationContactLog1.Visible = true;
        }

        protected void ButtonRefresh_Click(object sender, EventArgs e)
        {
            GridViewContractMaterials.DataBind();
        }

        public Guid RelationContactId
        {
            get
            {
                return new Guid(LabelContractID.Text);
            }
            set
            {
                LabelContractID.Text = value.ToString();
            }
        }

        public Guid RelationContactLogId
        {
            get
            {
                return WebUserControlCustomerRelationContactLog1.KeyID;
            }
            set
            {
                WebUserControlCustomerRelationContactLog1.DataBind();
                WebUserControlCustomerRelationContactLog1.Visible = true;
                WebUserControlCustomerRelationContactLog1.KeyID = value;
            }
        }
    }
}