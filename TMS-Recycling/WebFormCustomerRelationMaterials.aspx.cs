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
    public partial class WebFormCustomerRelationMaterials : ClassTMSWebPage
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
            if (WebUserControlCustomerRelationMaterial1.RefreshRequired)
            {
                WebUserControlCustomerRelationMaterial1.RefreshRequired = false;
                WebUserControlCustomerRelationMaterial1.Visible = false;
                ButtonSearch_Click(sender, e);
            }


        }

        protected void GridViewResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            WebUserControlCustomerRelationMaterial1.DataBind();
            WebUserControlCustomerRelationMaterial1.KeyID = new System.Guid(GridViewResults.SelectedDataKey.Value.ToString());
            WebUserControlCustomerRelationMaterial1.Visible = true;
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            EntityDataSourceRelation.DefaultContainerName = EntityDataSourceRelation.DefaultContainerName;
            EntityDataSourceRelation.DataBind();
        }

        protected void ButtonNew_Click(object sender, EventArgs e)
        {
            RelationMaterial NewObj = new RelationMaterial();
            ModelTMSContainer Temp = new ModelTMSContainer(Session["CustomerConnectString"].ToString(), Session);
            EntityKey TempKey = new EntityKey("ModelTMSContainer.RelationSet", "Id", Guid.Parse(Request.Params["Id"]));
            Relation TempObj = Temp.GetObjectByKey(TempKey) as Relation;

            NewObj.Relation = TempObj;
            NewObj.Material = Temp.MaterialSet.First();

            Temp.AddToRelationMaterialSet(NewObj);
            Temp.SaveChanges(SaveOptions.DetectChangesBeforeSave);

            WebUserControlCustomerRelationMaterial1.KeyID = NewObj.Id;
            WebUserControlCustomerRelationMaterial1.Visible = true;

        }
    }
}