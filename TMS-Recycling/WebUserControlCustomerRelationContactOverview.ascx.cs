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
    public partial class WebUserControlCustomerRelationContactOverview : ClassTMSUserControl
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

                DropDownListRelationType.Items.Clear();
                DropDownListRelationType.Items.Add(new ListItem("Alle types", ""));
                Common.AddCustomerRelationTypeList(DropDownListRelationType.Items, false);
                DropDownListRelationType.SelectedIndex = 0;

                DetailTable.Visible = false;
                WebUserControlCustomerRelationContactLogOverview1.Visible = false;

                // if the relation contact id is set then load this up immediately
                if (Request.Params["RelationContactId"] != null)
                {
                    // show the tables
                    WebUserControlCustomerRelationContact1.Visible = true;
                    WebUserControlCustomerRelationContactLogOverview1.Visible = true;
                    DetailTable.Visible = true;
                    
                    // load up details
                    WebUserControlCustomerRelationContact1.KeyID = new System.Guid(Request.Params["RelationContactId"].ToString());
                    EntityDataSourceRelation.DefaultContainerName = EntityDataSourceRelation.DefaultContainerName;
                    EntityDataSourceRelation.DataBind();

                    // load up contact log overviw
                    WebUserControlCustomerRelationContactLogOverview1.RelationContactId = WebUserControlCustomerRelationContact1.KeyID;
                    LabelContractID.Text = WebUserControlCustomerRelationContact1.KeyID.ToString();
                    LabelDataBindDetails.Text = "1";

                    // if the relatation contact log id is set as well then load this up immediately
                    if (Request.Params["RelationContactLogId"] != null)
                    {
                        WebUserControlCustomerRelationContactLogOverview1.RelationContactLogId = new System.Guid(Request.Params["RelationContactLogId"].ToString());
                        LabelDataBindDetails.Text = "2";
                    }
                }
            }
        }

        void Page_PreRender(object sender, EventArgs e)
        {
            if (WebUserControlCustomerRelationContact1.RefreshRequired)
            {
                WebUserControlCustomerRelationContact1.RefreshRequired = false;
                WebUserControlCustomerRelationContact1.Visible = false;
                WebUserControlCustomerRelationContactLogOverview1.Visible = false;
                DetailTable.Visible = false;
                ButtonSearch_Click(sender, e);
            }

            if (LabelDataBindDetails.Text != "")
            {
                if (LabelDataBindDetails.Text == "2")
                {
                    WebUserControlCustomerRelationContactLogOverview1.DataBind();
                }
                WebUserControlCustomerRelationContact1.DataBind();

                LabelDataBindDetails.Text = "";
            }

        }

        protected void GridViewResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            WebUserControlCustomerRelationContact1.DataBind();
            WebUserControlCustomerRelationContact1.KeyID = new System.Guid(GridViewResults.SelectedDataKey.Value.ToString());
            WebUserControlCustomerRelationContactLogOverview1.RelationContactId = WebUserControlCustomerRelationContact1.KeyID;
            LabelContractID.Text = WebUserControlCustomerRelationContact1.KeyID.ToString();
            WebUserControlCustomerRelationContact1.Visible = true;
            WebUserControlCustomerRelationContactLogOverview1.Visible = true;
            DetailTable.Visible = true;
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            EntityDataSourceRelation.DefaultContainerName = EntityDataSourceRelation.DefaultContainerName;
            EntityDataSourceRelation.DataBind();
        }

        protected void ButtonNew_Click(object sender, EventArgs e)
        {
            RelationContact NewObj = new RelationContact();
            ModelTMSContainer Temp = new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session);
            EntityKey TempKey = new EntityKey("ModelTMSContainer.RelationSet", "Id", Guid.Parse(Request.Params["Id"]));
            Relation TempObj = Temp.GetObjectByKey(TempKey) as Relation;

            NewObj.Relation = TempObj;

            NewObj.RelationType = "Other";
            NewObj.PhoneNumber = "";
            NewObj.PrivateEMail = "";
            NewObj.PrivateMobilePhone = "";
            NewObj.MobilePhone = "";
            NewObj.HomePhone = "";
            NewObj.EMail = "";
            NewObj.Description = "Nieuwe relatie dd " + Common.CurrentClientDateTime(Session).ToString();

            Temp.AddToRelationContactSet(NewObj);
            Temp.SaveChanges(SaveOptions.DetectChangesBeforeSave);

            WebUserControlCustomerRelationContact1.KeyID = NewObj.Id;
            WebUserControlCustomerRelationContact1.Visible = true;
            WebUserControlCustomerRelationContactLogOverview1.Visible = true;
            WebUserControlCustomerRelationContactLogOverview1.RelationContactId = NewObj.Id;
            DetailTable.Visible = true;
            //WebUserControlCustomerRelationContactLogOverview1.Visible = false;

        }

    }

}